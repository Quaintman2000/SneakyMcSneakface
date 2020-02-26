using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Windows.Speech;

public class Enemy : MonoBehaviour
{
    //keep track of our transform
    private Transform tf;

    //keep track of of our target location
    public Transform target;

    //track what state the AI is in
    public string AIState;

    //track enemy's health
    public float health;

    //track enemy's heal rate per second
    public float restingHealRate = 1.0f;

    //if player is in range or not
    public float attackRange;

    //track health cutoff
    public int healthCutoff;

    //keep track of the movement speed
    private float speed = 5.0f;

    //track enemy's max health
    public float maxHealth;

    // Start is called before the first frame update
    void Start()
    {
        tf = gameObject.GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        if (AIState == "Idle")
        {
            //do the state behavior
            Idle();

            //check for transitions
            if (isInRange())
            {
                ChangeState("Seek");
            }
        }
        else if (AIState == "Rest")
        {
            //do state behavior
            Rest();
            //check for transitions
            if (health >= healthCutoff)
            {
                ChangeState("Idle");
            }
        }
        else if (AIState == "Seek")
        {
            //do state behavior
            Seek();
            //check for transitions
            if (health < healthCutoff)
            {
                ChangeState(("Rest"));
            }

            if (!isInRange())
            {
                ChangeState("Idle");
            }
        }
        else
        {
            Debug.LogError("State does not exist:" + AIState);
        }
    }

    public void Idle()
    {
        //do nothing
    }

    public void Rest()
    {
        //stand still
        //heal
        health += restingHealRate * Time.deltaTime;
        health = Mathf.Min(health, maxHealth);
    }

    public void Seek()
    {
        //move toward player
        Vector3 vectorToTarget = target.position - tf.position;
        tf.position += vectorToTarget.normalized * speed * Time.deltaTime;
    }

    public void ChangeState(string newState)
    {
        AIState = newState;
    }

    public bool isInRange()
    {
        return (Vector3.Distance(tf.position, target.position) <= attackRange);
    }
}
