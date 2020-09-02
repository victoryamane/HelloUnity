using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour
{

    private Transform player;

    private void Start() {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void LateUpdate() {
        Vector3 playerpos = player.position;
        playerpos.z = transform.position.z;
        transform.position = playerpos;
    }
}