using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdBehavior : MonoBehaviour
{
    [SerializeField]
    private float speed;
    Vector3 PosA, PosB,PosC, PosD, nextPos;
    [SerializeField]
    private Transform childTransform;
 
    [SerializeField] private Transform transformB;
    [SerializeField] private Transform transformC;
    [SerializeField] private Transform transformD;

    [SerializeField] private Animator birdAnim;
    private bool isWaiting = false;
    // Use this for initialization
    void Start () {
        PosA = childTransform.localPosition;
        PosB = transformB.localPosition;
        PosC = transformC.localPosition;
        PosD = transformD.localPosition;
        nextPos = PosA;
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
        birdAnim.SetBool("isWaiting",isWaiting);
        yield return new WaitForSeconds(10);
        isWaiting = false;
        birdAnim.SetBool("isWaiting",isWaiting);
    }

    private void Move()
    {
        LookForward();        
        childTransform.localPosition = Vector3.MoveTowards(childTransform.localPosition,nextPos, speed*Time.deltaTime);
        if(Vector3.Distance(childTransform.localPosition,nextPos) <= 0.1)
        {
            ChangeDestination();
            StartCoroutine(MoveCoroutine());
        }
    }
    private void LookForward()
    {
        if(nextPos.x < childTransform.localPosition.x)
        {
            childTransform.rotation = Quaternion.Euler(0f, 180f, 0f);
        }
        else if(nextPos.x > childTransform.localPosition.x)
        {
            childTransform.rotation = Quaternion.Euler(0f, 0, 0f);
        }
    }
    private void ChangeDestination()
    {
        if(nextPos == PosA)
        {
            nextPos = PosB;
        }
        else if(nextPos == PosB)
        {
            nextPos = PosC;
        }
        else if(nextPos == PosC)
        {
            nextPos = PosD;
        }
        else if(nextPos == PosD)
        {
            nextPos = PosA;
        }
    }

}