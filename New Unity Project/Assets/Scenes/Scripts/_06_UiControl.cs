using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;


public class _06_UiControl : MonoBehaviour
{
    public static int Score;
    public static int preScore;
    public Text ScoreBar;
    public void Start()
    {
        Score = 0;
        preScore = 0;
        GameObject set = GameObject.Find("0");
        Instantiate(set, new Vector3(65, 14, 0), set.transform.rotation);
    }

    public void Update()
    {
        if (preScore != Score)
        {
            Debug.Log(Score);
            for(int i = 0; i < 10; i++)
            {
                GameObject trash = GameObject.Find("" + i + "(Clone)");
                Destroy(trash);
            }
            int singledigit = Score % 10;
            GameObject single = GameObject.Find("" + singledigit);
            Instantiate(single, new Vector3(67, 14, 0), single.transform.rotation);
            if (Score >= 10)
            {
                int tendigit = ((Score % 100) - singledigit) / 10;
                Debug.Log(tendigit);
                GameObject ten = GameObject.Find("" + tendigit);
                Instantiate(ten, new Vector3(63, 14, 0), ten.transform.rotation);
            }
            preScore = Score;
        }
        
    }
}