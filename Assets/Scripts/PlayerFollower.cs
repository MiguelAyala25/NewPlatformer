using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFollower : MonoBehaviour
{
    public Transform object1;
    public Transform object2;

    void Update()
    {
        float midpointY = ((object1.position.y + object2.position.y) / 2)-20;

        transform.position = new Vector3(transform.position.x, midpointY, transform.position.z);
    }
}
