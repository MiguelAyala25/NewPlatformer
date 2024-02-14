using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public List<GameObject> players = new List<GameObject>();
    public GameObject playerWithShield; // ref to player
    public float shieldTransferCooldown = 2.0f;
    private float shieldTransferTimer = 0;

    private Color OriginalColor;
 

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // clean player list
        players.Clear();
        playerWithShield = null;
    }
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    //Add a player to listt
    public void RegisterPlayer(GameObject player, Color originalColor)
    {
        //save original colors
        OriginalColor = originalColor;

        if (!players.Contains(player))
        {
            players.Add(player);
            // give shield to first player saved
            if (players.Count == 1)
            {
                playerWithShield = player;
            }
        }
    }

    public void TransferShield(GameObject requestingPlayer)
    {
        if (CanTransferShield() && requestingPlayer == playerWithShield)
        {
            GameObject oldPlayerWithShield = playerWithShield; // save player with shield

            // search for the other player
            foreach (var player in players)
            {
                if (player != requestingPlayer)
                {
                    playerWithShield = player; // give the other player the shield
                    player.GetComponent<SpriteRenderer>().color = Color.blue;
                    break;
                }
            }
            oldPlayerWithShield.GetComponent<SpriteRenderer>().color = OriginalColor;

            shieldTransferTimer = shieldTransferCooldown;
        }
    }


    public bool CanTransferShield()
    {
        return shieldTransferTimer <= 0;
    }

    private void Update()
    {
        if (shieldTransferTimer > 0)
        {
            shieldTransferTimer -= Time.deltaTime;
        }
    }
}
