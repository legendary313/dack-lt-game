﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingFlat : MonoBehaviour
{
    [SerializeField]
    private float speed;
    Vector3 PosA, PosB, nextPos;
    [SerializeField]
    private Transform childTransform;
 
    [SerializeField]
    private Transform transformB;
    private bool isWaiting = false;
    // Use this for initialization
    void Start () {
        PosA = childTransform.localPosition;
        PosB = transformB.localPosition;
        nextPos = PosB;
    }
    
    // Update is called once per frame
    void Update () {
        if(isWaiting == false){
            Move();
        }
    }
    IEnumerator MoveCoroutine()
    {
        isWaiting = true;
        yield return new WaitForSeconds(5);
        isWaiting = false;
    }

    private void Move()
    {
        childTransform.localPosition = Vector3.MoveTowards(childTransform.localPosition,nextPos, speed*Time.deltaTime);
        if(Vector3.Distance(childTransform.localPosition,nextPos) <= 0.1)
        {
            ChangeDestination();
            StartCoroutine(MoveCoroutine());
        }
    }

    private void ChangeDestination()
    {
        nextPos = nextPos != PosA ? PosA : PosB;
    }

    // private void OnCollisionEnter2D(Collision2D collision)
    // { 

    //     if(collision.collider.transform.CompareTag("Player"))
    //     {
    //         Debug.Log("in");
    //         collision.collider.gameObject.GetComponentInParent<Transform>().parent = gameObject.transform;
    //     }
    // }

    // private void OnCollisionExit2D(Collision2D collision)
    // {
    //     if(collision.collider.transform.CompareTag("Player"))
    //     {
    //         Debug.Log("out");
    //         collision.collider.transform.SetParent(null);
    //     }
    // }
}
