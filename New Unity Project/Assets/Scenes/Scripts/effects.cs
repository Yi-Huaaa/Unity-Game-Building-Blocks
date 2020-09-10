using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class effects : MonoBehaviour {

    public static GameObject eliminate;
	// Use this for initialization
	void Start () {

    }
	
	// Update is called once per frame
	void Update () {
        if (NewBehaviourScript.trigger == true)
        {
            eliminate = Instantiate(gameObject);
            eliminate.transform.position = NewBehaviourScript.Hexagon.transform.position;
            Debug.Log("trigger");
            NewBehaviourScript.trigger = false;
        }
    }
}
