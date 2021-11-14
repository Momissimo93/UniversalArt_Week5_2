using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingEyeBullet : MonoBehaviour
{
    public float speed;
    private Transform player;
    private Vector2 target;

    private void Start()
    {
        player  = GameObject.FindGameObjectWithTag("Hero").transform;
        target = new Vector2(player.position.x, player.position.y);
    }

    private void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position,target,speed * Time.deltaTime);
    }

}
