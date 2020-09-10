using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drag : MonoBehaviour {
    public GameObject camera;
    public Vector3 item;
    // Use this for initialization
    void Start ()
    {
        camera = GameObject.Find("Main Camera");
    }

    // Update is called once per frame
    void Update()
    {
        item = this.transform.position;
        Vector3 mouse = Input.mousePosition;
        mouse.z = -camera.transform.position.z;
        Vector3 worldPoint = Camera.main.ScreenToWorldPoint(mouse);
        if (Input.GetMouseButton(0))
        {
            if (item.x == worldPoint.x && item.y == worldPoint.y)
            {
                this.transform.position = new Vector3(0, 0, 1);
            }
            this.transform.position = new Vector3(0, 0, 0);
        }
        else
        {
            this.transform.position = new Vector3(1, 0, 1);
        }
        //this.transform.position = worldPoint;

    }
}
