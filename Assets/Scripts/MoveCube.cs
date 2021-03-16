using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCube : MonoBehaviour
{
    private Transform tr;
    private Vector3 distance;

    // Start is called before the first frame update
    void Start()
    {
        tr = GetComponent<Transform>();
    }

    void OnMouseDown()
    {
        Vector3 mousePoint = Input.mousePosition;


        mousePoint = Camera.main.ScreenToWorldPoint(mousePoint);

        mousePoint = new Vector3(mousePoint.x, mousePoint.y, 0);

        Vector3 trmz = new Vector3(tr.position.x, tr.position.y, 0f);

        distance = trmz - mousePoint;
        //Debug.Log(distance);
    }

    // Update is called once per frame
    void OnMouseDrag()
    {
        Vector3 mousePoint = Input.mousePosition;
        

        mousePoint = Camera.main.ScreenToWorldPoint(mousePoint);

        mousePoint = new Vector3(mousePoint.x, mousePoint.y, 0);
        
        tr.position = mousePoint + distance;

    }
}
