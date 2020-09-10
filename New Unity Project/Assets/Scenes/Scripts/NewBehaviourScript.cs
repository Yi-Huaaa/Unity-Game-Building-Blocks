using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;

public class NewBehaviourScript : MonoBehaviour
{
    public static int cand;
    public int[,] copy = new int[9, 9];
    public static GameObject Hexagon;
    public GameObject scoreboard;
    public GameObject Block;
    public static bool trigger = false;
    private static bool playsound=true;
    public _06_UiControl scoreobj;
    public Color32[] Piece_color = new Color32[8];
    public int[,] brd2blk = new int[9, 9];


    public int[,,] bb = new int[25, 3, 2]
            {{ { 0, 0}, { 0, 0}, { 0, 0} },//no.0 block
             { {-1, 0}, {-2, 0}, {-3, 0} },//no.1 block
             { {-1,-1}, {-2,-2}, {-3,-3} },
             { { 0, 1}, { 0, 2}, { 0, 3} },
             { {-1, 0}, {-1,-1}, {-2,-2} },
             { { 0, 1}, { 1, 2}, { 0, 2} },
             { { 0, 2}, { 0, 1}, {-1, 0} },//6
             { { 1, 2}, { 0, 1}, {-1, 0} },
             { { 0, 1}, {-1, 0}, {-1,-1} },
             { { 0, 1}, {-1, 0}, {-1, 1} },
             { {-2,-1}, {-1, 0}, {-1,-1} },
             { { 0, 1}, {-1, 0}, {-2, 0} },//11
             { {-1, 0}, {-1, 1}, {-2, 0} },
             { {-1, 0}, {-2,-1}, {-2, 0} },//13
             { {-1,-1}, {-1, 0}, {-2, 0} },
             { { 0, 1}, {-1, 0}, {-2,-1} },
             { { 0, 1}, { 0, 2}, {-1, 1} },//16
             { {-1,-1}, {-1, 0}, {-1, 1} },
             { {-1,-1}, {-2,-2}, {-2,-1} },//18
             { { 0, 1}, {-1, 1}, {-2, 0} },//19
             { {-1,-1}, {-2,-1}, {-2, 0} },//20
             { {-1,-1}, { 0, 1}, {-1, 1} },//21
             { {-1, 0}, {-2,-1}, {-2,-2} },//22
             { { 0, 1}, {-1,-1}, {-2,-1} },//23
             { {-1, 0}, {-1, 1}, { 0, 2} },//24
            };

    public static int[,] board = new int[9, 9];
    public static int which = -1;
    public static int[] type = new int[3];
    public static bool put = false;
    public static bool gameover = false;
    //
    private bool set = true;
    private int i = 0;

    // Use this for initialization
    int Set_color(int t)
    {
        if (t == 0 )
            return 0;
        if (t <= 3 )
            return 1;
        if (t <= 7 )
            return 2;
        if (t <= 10 )
            return 3;
        if (t <= 14 )
            return 4;
        if (t <= 18 )
            return 5;
        return 6;
    }
    bool ValidPut(int x, int y, int w)
    {
        for (int i = 0; i < 3; i++)
        {           
            if (w > 2 || !Check(x + bb[type[w], i, 0], y + bb[type[w], i, 1]) || board[x + bb[type[w], i, 0], y + bb[type[w], i, 1]] == 1)
                return false;
        }
        return (Check(x, y) && board[x, y] == 0);
    }
    public void ItCanPut()
    {
        Debug.Log("(" + Point.input.x + ", " + Point.input.y + ")" + " which = " + which);
        Debug.Log(" type = " + type[which]);
        int x = Point.input.x;
        int y = Point.input.y;
        bool legal = false;
        //get color on  the board 
        /*
        for (int i = 0; i < 4; i++)
        {
            //second part , run z marked block
            // z last oe to check, if not true legal still == false
            //then i == 4, break z  loop
            if (i == 3)
            {
                if (Check(x, y) && board[x, y] == 0)
                    legal = true;
            }
            // first part, run z unmarked blocks if legal or not 
            // as i = 0,1,2 check whether legal
            // if  legal continue, if not break
            else if (which > 2 || !Check(x + bb[type[which], i, 0], y + bb[type[which], i, 1]) || board[x + bb[type[which], i, 0], y + bb[type[which], i, 1]] == 1)
                break;
        }
        */
        if(ValidPut(x, y, which) == true)
        {
            Debug.Log("truetrue");
            Color32 t = Piece_color[Set_color(type[which])];
            board[x, y] = 1;
            
            Hexagon = GameObject.Find("Hexagon (" + brd2blk[x, y] + ")");
            Hexagon.GetComponent<SpriteRenderer>().color = t;

            int tmpx, tmpy;
            for (int i = 0; i < 3; i++)
            {
                tmpx = x + bb[type[which], i, 0];
                tmpy = y + bb[type[which], i, 1];
                board[tmpx, tmpy] = 1;
                Debug.Log(t);

                Hexagon = GameObject.Find("Hexagon (" + brd2blk[tmpx, tmpy] + ")");
                Hexagon.GetComponent<SpriteRenderer>().color = t;
                /////////t no read count 
            }

            Block = GameObject.Find("block" + type[which]);
            Block.GetComponent<Transform>().position = new Vector3(-150, 150, 0);
            for (int j = 0; j < 4; j++)
            {
                Block.transform.GetChild(j).GetComponent<SpriteRenderer>().color = Piece_color[7];
                if (type[which] == 0)
                    //if (i == 0 || i == 25 || i == 50)
                    break;
            }
            //update score
            _06_UiControl.Score += (type[which] == 0) ? 1 : 4;
            //傳入新積木
            do
            {
                //need to change, if change into 0~74 blocks 
                type[which] = Random.Range(0, 24);//include 24 

                //test for the worldpoint, using the block0
                //type[which] = Random.Range(0, 2) + cand;
            }
            //check whether exist same blocks
            while (type[1] == type[2] || type[1] == type[0] || type[0] == type[2]);

            /////////////change blocks color
            Block = GameObject.Find("block" + type[which]);
            for (int j = 0; j < 4; j++)
            {
                Block.transform.GetChild(j).GetComponent<SpriteRenderer>().color = Piece_color[Set_color(type[which])];
                if (type[which] == 0)
                    //if (i == 0 || i == 25 || i == 50)
                    break;
            }
            //try
            //Hexagon.GetComponent<Renderer>().material.color = Piece_color[Set_color(type[which])]
            //Hexagon.GetComponent<Renderer>().material.color = new Color32(255, 255, 255, 125);
        }
        
        which = -1;
        put = false;
    }
    IEnumerator misblk(int i, int j, float time)
    {
        yield return new WaitForSeconds(time);
        Hexagon = GameObject.Find("Hexagon (" + brd2blk[i, j] + ")");
        Hexagon.GetComponent<SpriteRenderer>().color = new Color32(255, 255, 255, 200);
        trigger = true;
    }
    void LineClear()
    {
        for (int i = 0; i < 9; i++)
            for (int j = 0; j < 9; j++)
                copy[i, j] = board[i, j];
        for (int k = 0; k < 2; k++)
        {
            for (int j = 0; j <= 8; j++)
            {
                int i;
                if (j <= 4)
                    i = 0;
                else
                    i = j - 4;
                bool ok = true;
                while (k == 0 && Check(i, j) || k == 1 && Check(j, i))
                {
                    if (k == 0 && board[i, j] == 0 || k == 1 && board[j, i] == 0)
                    {
                        ok = false;
                        break;
                    }
                    i++;
                }
                if (ok)
                {
                    if (j <= 4)
                        i = 0;
                    else
                        i = j - 4;
                    while (k == 0 && Check(i, j) || k == 1 && Check(j, i))
                    {
                        if (k == 0)
                            copy[i, j] = 0;
                        else
                            copy[j, i] = 0;
                        i++;
                    }
                }
                if (j <= 4)
                {
                    int a = 0, b = j;
                    ok = true;
                    while (k == 0 && Check(a, b) || k == 1 && Check(b, a))
                    {
                        if (k == 0 && board[a, b] == 0 || k == 1 && board[b, a] == 0)
                        {
                            ok = false;
                            break;
                        }
                        a++;
                        b++;
                    }
                    if (ok)
                    {
                        a = 0;
                        b = j;
                        while (k == 0 && Check(a, b) || k == 1 && Check(b, a))
                        {
                            if (k == 0)
                                copy[a, b] = 0;
                            else
                                copy[b, a] = 0;
                            a++;
                            b++;
                        }
                    }
                }
            }
        }
        float time = 0;
        for (int i = 0; i < 9; i++)
            for (int j = 0; j < 9; j++)
            {
                if(board[i, j] != copy[i, j])
                {
                    if (playsound == true)
                    {
                        AudioSource sound = GameObject.Find("eliminate sound").GetComponent<AudioSource>();
                        sound.Play();
                        playsound = false;
                    }
                    //trigger = true;
                    time += 0.1f;
                    StartCoroutine(misblk(i, j, time));
                    //effect.transform.position = unit.transform.position;
                    board[i, j] = copy[i, j];

                    /*
                    Hexagon = GameObject.Find("Hexagon (" + brd2blk[i, j] + ")");
                    Hexagon.GetComponent<SpriteRenderer>().color = new Color32(255, 255, 255, 125);
                    */
                    _06_UiControl.Score++;
                }       
            }
        playsound = true;
    }
    bool GameQQ()
    {
        for(int t = 0; t < 3; t++)
            for(int i = 0; i < 9; i++)
                for(int j = 0; j < 9; j++)
                    if (ValidPut(i, j, t) == true)
                        return false;
        return true;
    }
    void Start()
    {
        Piece_color[0] = new Color32(184, 24, 245, 255);
        Piece_color[1] = new Color32(255, 251, 4, 255);
        Piece_color[2] = new Color32(0, 35, 255, 255);
        Piece_color[3] = new Color32(233, 115, 0, 255);
        Piece_color[4] = new Color32(253, 0, 2, 255);
        Piece_color[5] = new Color32(0, 214, 48, 255);
        Piece_color[6] = new Color32(156, 57, 0, 255);
        Piece_color[7] = new Color32(255, 255, 255, 0);
        //Hexagon(x) 對照 board (i,j) 的轉換 
        int a = 1;
        for (int j = 0; j < 5; j++)
            for (int i = 0; i < 5 + j; i++)
                    brd2blk[i, j] = a++;
        for (int j = 5; j < 9; j++)
            for (int i = j - 4; i < 9; i++)
                brd2blk[i, j] = a++;
        //將board[i,j] == 0, initialize, nothing put on, the board == 0
        for (int i = 0; i < 9; i++)
            for (int j = 0; j < 9; j++)
                board[i, j] = 0;
        //隨機選三塊積木
        //give out 3 blocks

           do
           {
                for (int j = 0; j < 3; j++)
                {
                //need to change, if change into 0~74 blocks 
                    type[j] = Random.Range(0, 24);//include 24 
                //test for the worldpoint, using the block0
                    //type[j] = cand + j;
                }
            }
            //check whether exist same blocks
            while (type[1] == type[2] || type[1] == type[0] || type[0] == type[2]);

        
        //then can use the color32
        GetComponent<Renderer>().material.color = new Color32(255, 255, 255, 255);
        //for (int i = 0; i < 50; i++)
        for (int i = 0; i < 25; i++)
        {
            //一開始先讓block都在旁邊
            Block = GameObject.Find("block" + i);
            Block.GetComponent<Transform>().position = new Vector3(-150, 150, 0);
            //12345
            for (int j = 0; j < 4; j++)
            {
                Block.transform.GetChild(j).GetComponent<SpriteRenderer>().color = Piece_color[7];
                if (i == 0)
                //if (i == 0 || i == 25 || i == 50)
                        break;
            }

        }
        for (int i = 0; i < 3; i++)                              // 放上可以用的三塊積木
        {
            Block = GameObject.Find("block" + type[i]);
            Block.GetComponent<Transform>().position = new Vector3(-16 + 20 * i, -12, 0);
            for (int j = 0; j < 4; j++)
            {
                Block.transform.GetChild(j).GetComponent<SpriteRenderer>().color = Piece_color[Set_color(type[i])];
                //if (type[i] == 0 || type[i] == 25 || type[i] == 50)
                if (type[i] == 0)
                    break;
            }
        }

        //scoreobj.Start();
    }

    // Update is called once per frame
    void Update()
    {


        // for testing the score displayed
        //_06_UiControl.Score++;
        //scoreobj.Update();
        

        //if the block doesn't drag tp the board, it's on the other places
        //then which == -1, hand u then go back to the previous place
        if(which == -1)
        {
            for(int i = 0; i < 3; i++)
            {
                Block = GameObject.Find("block" + type[i]);
                Block.GetComponent<Transform>().position = new Vector3(-16 + 20 * i, -12, 0);
            }
            //which = -1;
        }
        if(put == true)
        {
            ItCanPut();
            LineClear();
            if(GameQQ() == true)
            {
                GameOverControl.gameover = true;
                Debug.Log("You lose.");
            }
        }
        /*
        for(int i = 0; i < 9; i++)
            for(int j = 0; j < 9; j++)
                if(Check(i, j) && board[i, j] == 0)
                {
                    Hexagon = GameObject.Find("Hexagon (" + brd2blk[i, j] + ")");
                    Hexagon.GetComponent<SpriteRenderer>().color = new Color32(255, 255, 255, 125);
                }
         */

    }
    public static bool Check(int x, int y)
    {
        //if 0 run again
        if (y - x > 4 || x - y > 4 || x < 0 || x > 8 || y < 0 || y > 8)
            return false;
        return true;
    }


    
}

