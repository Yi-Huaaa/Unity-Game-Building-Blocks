using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseDrag : MonoBehaviour
{
    Vector2[] offset = new Vector2[26];
    // Use this for initialization
    void Start()
    {
        NewBehaviourScript.cand = 22;
        offset[0] = new Vector2(0,0);
        offset[1] = new Vector2(20, 0);
        offset[2] = new Vector2(16, 25);
        offset[3] = new Vector2(9, -21);
        offset[4] = new Vector2(8, 18);
        offset[5] = new Vector2(8, -12);
        offset[6] = new Vector2(8, -12);
        offset[7] = new Vector2(8, -12);
        offset[8] = new Vector2(8, 4);
        offset[9] = new Vector2(15, -6);
        offset[10] = new Vector2(16, 11);
        offset[11] = new Vector2(16, -5);
        offset[12] = new Vector2(16, -5);
        offset[13] = new Vector2(16, 10);
        offset[14] = new Vector2(16, 10);
        offset[15] = new Vector2(6, 3);
        offset[16] = new Vector2(15, -13);
        offset[17] = new Vector2(7, 3);
        offset[18] = new Vector2(15, 18);
        offset[19] = new Vector2(16, -4);
        offset[20] = new Vector2(16, 10);
        offset[21] = new Vector2(8, 3);
        offset[22] = new Vector2(16, 19);
        offset[23] = new Vector2(8, 3);
        offset[24] = new Vector2(16, -13);
        offset[25] = new Vector2(-5, -5);// try

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnMouseDown()
    {
        Vector2 mouse = Input.mousePosition;
        Debug.Log(mouse.x + "," + mouse.y + " click ooo ");
    }
    Vector2 posCorrect(Vector2 t)
    {
        return new Vector2((t.x - 270) / 8, (t.y - 150) / 8);
    }
    /////////////
    Point pos2brd(int x, int y)
    {
        y = y / 2;
        x = (x + y) / 2;
        return new Point(x, y);
    }
    void OnMouseDrag()
    {
        Vector2 mouse = Input.mousePosition;
        if (NewBehaviourScript.which == -1)
        {
            if (mouse.x < 190)
                NewBehaviourScript.which = 0;
            else if (mouse.x < 350)
                NewBehaviourScript.which = 1;
            else
                NewBehaviourScript.which = 2;
        }
        //(-369,-140) world point 
        // (270, 150) our world point
        transform.position = posCorrect(mouse - offset[NewBehaviourScript.type[NewBehaviourScript.which]]) ;
        //Debug.Log(mouse.x + "," + mouse.y);
    }

    void OnMouseUp()
    {
        Vector2 mouse = Input.mousePosition;
        //Debug.Log(mouse.x + "," + mouse.y + " detect mouse up OKOK");
        mouse = posCorrect(mouse - offset[25]);
        Point.input = pos2brd((int)mouse.x, (int)mouse.y);
        if(NewBehaviourScript.which != -1)
            NewBehaviourScript.put = true;
    }
}