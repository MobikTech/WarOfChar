using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraCotroller : MonoBehaviour
{
    public float damping = 2.2f;
    private Transform player;
    private Vector3 target;
    private Vector3 spawn;


    void Start()
    {
        spawn = GameObject.FindGameObjectWithTag("Respawn").transform.position;
        player = GameObject.FindGameObjectWithTag("Player").transform;
        transform.position = new Vector3(spawn.x, spawn.y, transform.position.z);
    }
    void Update()
    {
        target = new Vector3(player.position.x, player.position.y, transform.position.z);
        transform.position = Vector3.Lerp(transform.position, target, damping * Time.deltaTime);
    }
}
