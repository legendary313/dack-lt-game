using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private GameObject followObject;
    [SerializeField] private Vector2 followOffset;
    private Vector2 threshold;  
    // Start is called before the first frame update
    void Start()
    {
        threshold = calculateThreshold();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        Vector2 follow = followObject.transform.position;
        float xDifference = Vector2.Distance(Vector2.right * transform.position.x, Vector2.right * follow.x);
        float yDifference = Vector2.Distance(Vector2.up * transform.position.y, Vector2.up * follow.y);

        Vector3 newPosition = transform.position;
        if(Mathf.Abs(xDifference) >= threshold.x){
            //0f 0f is limit camera position x
            newPosition.x = Mathf.Clamp(follow.x,0f,0f);
        }
        if(Mathf.Abs(yDifference) >= threshold.y){
            //0.7f 100f is limit camera position y
            newPosition.y = Mathf.Clamp(follow.y,0f,transform.position.y+100f);
        }
        transform.position = newPosition;
    }

    private Vector3 calculateThreshold(){
        Rect aspect = Camera.main.pixelRect;
        Vector2 t = new Vector2(Camera.main.orthographicSize * aspect.width / aspect.height, Camera.main.orthographicSize);
        t.x -= followOffset.x;
        t.y -= followOffset.y;
        return t;
    }
    private void OnDrawGizmos() {
        Gizmos.color = Color.blue;
        Vector2 border = calculateThreshold();
        Gizmos.DrawWireCube(transform.position, new Vector3(border.x * 2, border.y * 2, 1));
    }
}
