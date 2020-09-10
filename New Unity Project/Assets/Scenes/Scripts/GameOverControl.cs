using System.Collections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
public class GameOverControl : MonoBehaviour
{

    public Text GameOverBar;
    public static bool gameover;
    private AudioSource fail;
    public void Start()
    {
        fail = GameObject.Find("gameover").GetComponent<AudioSource>();
        gameover = false ;
    }
    public void Update()
    {
        GameObject over = GameObject.Find("gameover");
        if (gameover == true)
        {     
            over.transform.position = new Vector2(37.4f, 7.7f);
            fail.Play();
            AudioSource background = GameObject.Find("Main Camera").GetComponent<AudioSource>();
            background.Stop();
            gameover = false;
        }

    }
}
