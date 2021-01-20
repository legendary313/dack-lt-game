using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatBehavior : MonoBehaviour
{
    [SerializeField] private float speedRunToHit;
    [SerializeField] private float speedRunToSitting;

    [SerializeField]
    private Transform catObject;
    private bool hasTarget = false;
    private bool isSitting = true;

    private Transform target , beginTransform;
    private Vector3 beginPosition;
    [SerializeField] private Animator catAnim;

    // Start is called before the first frame update
    void Awake()
    {
        catObject = this.gameObject.transform;
        beginPosition= catObject.position;

    }

    // Update is called once per frame
    void Update()
    {
        if(hasTarget == true)
        {
            catObject.position = Vector3.MoveTowards(catObject.localPosition,new Vector3(target.transform.localPosition.x,catObject.localPosition.y,0f), speedRunToHit*Time.deltaTime);
        }
        if(hasTarget == false && isSitting == false)
        {
            catObject.transform.localPosition = Vector3.MoveTowards(catObject.localPosition,new Vector3(beginPosition.x,catObject.localPosition.y,0f), speedRunToSitting*Time.deltaTime);
            if(beginPosition.x < catObject.localPosition.x)
            {
                catObject.rotation = Quaternion.Euler(0f, 0, 0f);
            }
            else if(beginPosition.x > catObject.localPosition.x) 
            {
                catObject.rotation = Quaternion.Euler(0f, 180f, 0f);
            }
            if(catObject.localPosition.x == beginPosition.x) 
            {
                isSitting = true;
                catAnim.SetBool("isSitting",true);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.transform.CompareTag("Player"))
        {
            catAnim.SetBool("hasTarget",true);
            target = collision.transform;
            if(collision.transform.position.x < catObject.localPosition.x)
            {
                catObject.rotation = Quaternion.Euler(0f, 0f, 0f);
            }
            else
            {
                catObject.rotation = Quaternion.Euler(0f, 180f, 0f);
            }
            hasTarget = true;
            isSitting = false;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Debug.Log("out");
        if(collision.transform.CompareTag("Player"))
        {
            hasTarget = false;
            catAnim.SetBool("hasTarget",false);
        }
    }
}
