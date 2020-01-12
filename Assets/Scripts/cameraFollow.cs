using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraFollow : MonoBehaviour
{
    public Transform player;
    public Vector3 offset;
    public float z = -10;
    void Update()
    {
        transform.position = new Vector3(player.position.x + offset.x, player.position.y + offset.y, z); // Camera follows the player with specified offset position
    }
}
