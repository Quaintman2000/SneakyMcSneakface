﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Pawn: MonoBehaviour
{
    public float moveSpeed = 5.0f;
    private Transform tf;
    public float health = 100.0f;

    void Start()
    {
        tf = gameObject.GetComponent<Transform>();
    }
    public virtual void Attack()
    {
        Debug.Log("Pawn Attack");
    }

    public virtual void Move()
    {
        Debug.Log("moved with pawn movement behavior");
    }
}
