using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Healthbar : MonoBehaviour
{
    public static Healthbar Instance { get; private set; }

    [SerializeField] private Image healthbar;
    [SerializeField] private int healthAmount=50;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    public void TakeDamage(int damage)
    {
        healthAmount -= damage;
        healthbar.fillAmount = healthAmount / 100.0f;
        if(healthAmount<=0)
        {
            RestartScene();
        }
    }
    public void RestartScene()
    {
        string sceneName = SceneManager.GetActiveScene().name;

        SceneManager.LoadScene(sceneName);
    }
}
