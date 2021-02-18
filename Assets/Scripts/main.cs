using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class GameData
{

    public static int RoomId;
    public static int GameStatus=1;
    public static int PlayerLimit=4;
    public static int Winner = -1;
    public static int Turn = 0;
    //Turnはどのプレイヤーのターンかの情報を持つ
    public static List<int> Player_state = new List<int>();
    public static List<List<int>> PlayerHand =new List<List<int>>();
    public static List<List<int>> Field = new List<List<int>>();
    public static List<int> Not_Adjacent = new List<int>();

    public static int GetPlayerLimit()
    {
        return PlayerLimit;
    }
    public static List<List<int>> Field_value
    {
        get
        {
            return Field;
        }
        set
        {
            Field = value;
        }

    }
    
}

public class main : MonoBehaviour
{
    public static int Turn_State = -1;
    //Turn_State 0は待機,1はプレイできる状態,2でターンエンド状態-1でほかの人のターンまたその他のターン
    [SerializeField] GameObject cardPrefab;
    [SerializeField] Transform playerField;
    [SerializeField] DropPlace drop_place;
    [SerializeField] GameObject Text_UI;
    [SerializeField] Text State_text;
    List<int> Shuffle(List<int> list)
    {
        return list = list.OrderBy(i => Guid.NewGuid()).ToList();
    }
    void Set_Playerstate()
    {
        for (int i = 0; i < GameData.PlayerLimit; i++)
        {
            GameData.Player_state.Add(0);
        }
    }
    void Generate_Field()
    {
        for (int i = 0; i < 4; i++)
        {
            List<int> add_tmp = new List<int>();
            GameData.Field.Add(add_tmp);
        }

        
    }
    void Generate_Card()
    {
        List<int> yama = new List<int>();
        List<int> tmp_yama= new List<int>();
        for (int i = 0; i < 52; i++)
        {
            yama.Add(i);
            tmp_yama.Add(i);
        }

        Set_Field(tmp_yama);
        yama = Shuffle(yama);
        for (int i = 0; i < GameData.GetPlayerLimit(); i++)
        {
            List<int> player = new List<int>();
            GameData.PlayerHand.Add(player);
        }
        int player_tmp = 0;
        foreach (int i in yama)
        {
            GameData.PlayerHand[player_tmp].Add(i);
            player_tmp++;
            if (player_tmp == GameData.GetPlayerLimit())
            {
                player_tmp = 0;
            }
            
        }
        
    }
    void Check7()
    {
        int tmp;
        for (int i = 0; i < 4; i++)
        {
            tmp = 6 + 13 * i;
            //ここからすべてのソートの7を抜く
            for (int j = 0; j < GameData.PlayerLimit; j++)
            {
                if (GameData.PlayerHand[j].Contains(tmp))
                {
                    GameData.PlayerHand[j].Remove(tmp);
                    GameData.Field[tmp / 13].Add(tmp);
                    Debug.Log("七抜いた回数" + i);
                }
            }
        }
       
    }
    void Generate_Hand(int player)
    {
        foreach (int i in GameData.PlayerHand[player])
        {
            GameObject obj= Instantiate(cardPrefab, playerField);
            obj.name = i.ToString();
            TextMeshProUGUI tm = obj.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>();
            string tmp="";
            switch (i / 13)
            {
                case 0:
                    tmp = "♠";
                    break;
                case 1:
                    tmp = "♥";
                    break;

                case 2:
                    tmp = "♣";
                    break;

                case 3:
                    tmp = "♦";
                    break;
            }
            //Debug.Log(tm.text);
            tm.text = tm.text.Replace("x", ((i%13)+1).ToString());
            tm.text = tm.text.Replace("d", tmp);
        }

    }
    void Set_Field(List<int> tmp_list)
    {
        int count_tmp;
        for (int i = 0; i < 4; i++)
        {
            count_tmp = 6 + 13 * i;
            tmp_list.Remove(count_tmp);
        }
            
        foreach (int i in tmp_list)
        {
            GameObject obj_tmp;
            if (i % 13 < 6)
            {
                obj_tmp = GameObject.Find("Field").transform.GetChild(i / 13).gameObject.transform.GetChild(1).gameObject;
            }
            else
            {
                obj_tmp = GameObject.Find("Field").transform.GetChild(i / 13).gameObject.transform.GetChild(2).gameObject;
            }

            GameObject obj = Instantiate(cardPrefab, obj_tmp.transform);
            obj.name = i.ToString();
            TextMeshProUGUI tm = obj.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>();
            string tmp = "";
            switch (i / 13)
            {
                case 0:
                    tmp = "♠";
                    break;
                case 1:
                    tmp = "♥";
                    break;

                case 2:
                    tmp = "♣";
                    break;

                case 3:
                    tmp = "♦";
                    break;
            }
            //Debug.Log(tm.text);
            tm.text = tm.text.Replace("x", ((i % 13) + 1).ToString());
            tm.text = tm.text.Replace("d", tmp);
            Image image = obj.GetComponent<Image>();
            image.color = new Color(image.color.r, image.color.g, image.color.b, 0);
            TextMeshProUGUI card_txt = obj.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
            card_txt.color = new Color(image.color.r, image.color.g, image.color.b, 0);
        }
    }


    void Stand_by()
    {
        Text_UI.SetActive(true);
        int player_tmp = (GameData.Turn + 1);
        State_text.text = "Player "+player_tmp.ToString() + "のターン";
    }
    void Game_End()
    {
        Text_UI.SetActive(true);
        if (GameData.Winner == 0)
        {
            State_text.text = "全員失格";
        }
        else
        {
            State_text.text = GameData.Turn.ToString() + "の勝利";
        }
        
    }
    // Start is called before the first frame update
    void Start()
    {

        Set_Playerstate();
        Generate_Field();
        Generate_Card();
        Check7();
        Generate_Hand(0);
        Turn_State = 0;
        Stand_by();
      
        
    }

    // Update is called once per frame
    void Update()
    {
        if (GameData.GameStatus == 1)
        {
            if (Turn_State == 2)
            {
                //ターンエンド後nameUI切り替え
                TextMeshProUGUI tm = GameObject.Find("Player_Name").transform.GetChild(GameData.Turn).gameObject.GetComponent<TextMeshProUGUI>();
                tm.color = Color.gray;
                //プレイヤーが4回パスしていたら
                //自分のカードをNot_Adjacent(隣接していないカード群)に入れる。
                //カードのアルファ値を255に
                if (GameData.Player_state[GameData.Turn] == 4)
                {
                    foreach (int i in GameData.PlayerHand[GameData.Turn])
                    {
                        GameObject obj_tmp = GameObject.Find("Field").transform.GetChild(i / 13).gameObject;
                        //Debug.Log("i=" + i + "obj_tmp=" + obj_tmp.name);
                        drop_place.DropField(i, obj_tmp);
                        GameData.Not_Adjacent.Add(i);
                        //GameData.PlayerHand[GameData.Turn].Remove(i);
                    }
                }
                //Not_Adjacentが隣接していたらFieldに入れる
                for (int i=0;i< GameData.Not_Adjacent.Count();i++) 
                {
                    for (int j = 0; j < 4; j++)
                    {

                        if (drop_place.Check(i, j))
                        {
                            GameData.Field[j].Add(i);
                            GameData.Not_Adjacent.Remove(i);
                        }
                    }

                }

                //ゲーム終了判定をここに入れる

                if (GameData.PlayerHand[GameData.Turn].Count() == 0)
                {
                    //プレイヤーの手札が0枚になったので勝利してゲーム終了
                    GameData.Winner = GameData.Turn;
                    GameData.GameStatus = 0;
                }
                else
                {
                    int tmp = 0;
                    foreach (int i in GameData.Player_state)
                    {
                        tmp = tmp + i;
                    }
                    if (tmp == 4 * GameData.PlayerLimit)
                    {
                        //player全員が脱落してゲーム終了
                        GameData.Winner = 0;
                        GameData.GameStatus = 0;
                    }
                    else
                    {

                        //次の脱落していないプレイヤーのターンに切り替える
                        do
                        {
                            GameData.Turn++;
                            if (GameData.Turn == GameData.PlayerLimit)
                            {
                                GameData.Turn = 0;
                            }
                        } while (GameData.Player_state[GameData.Turn] == 4);
                        TextMeshProUGUI tm_next = GameObject.Find("Player_Name").transform.GetChild(GameData.Turn).gameObject.GetComponent<TextMeshProUGUI>();
                        tm_next.color = Color.black;
                        //今のプレイヤーの手札オブジェクトをすべて消し、次のプレイヤーの手札を生成
                        foreach (Transform child in GameObject.Find("Footer").transform)
                        {
                            GameObject.Destroy(child.gameObject);
                        }
                        Generate_Hand(GameData.Turn);
                        //準備状態にする
                        Turn_State = 0;
                        Stand_by();
                    }
                }


            }
        }
        else if (GameData.GameStatus == 0)
        {
            //ゲーム終了
            Game_End();
        }
        
    }
}
