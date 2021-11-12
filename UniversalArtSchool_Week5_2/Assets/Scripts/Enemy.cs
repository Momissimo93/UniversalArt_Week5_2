using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Character
{

    private void Awake()
    {
        GetAnimator();
        GetBoxCollider();
    }

    private void Start()
    {
        facingForward = true;
    }
    // Update is called once per frame
    void Update()
    {
        EmittingRaysFromFeet();
        Move();
        EmittingEyeSight();
    }

    private void FixedUpdate()
    {
    }
}
