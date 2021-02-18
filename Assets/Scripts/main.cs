using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class GameData
{

    public static int RoomId;
    public static int GameStatus;
    public static int PlayerLimit=4;
    public static int Turn = 0;
    //Turnはどのプレイヤーのターンかの情報を持つ
    public static List<List<int>> PlayerHand =new List<List<int>>();
    public static List<List<int>> Field = new List<List<int>>();

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
    //Turn_State 0はターンエンド状態,1はプレイできる状態-1でほかの人のターンまたその他のターン
    [SerializeField] GameObject cardPrefab;
    [SerializeField] Transform playerField;
    List<int> Shuffle(List<int> list)
    {
        return list = list.OrderBy(i => Guid.NewGuid()).ToList();
    }
    void Generate_Field()
    {
        for (int i = 0; i < 4; i++)
        {
            List<int> add_tmp = new List<int>();
        }
        
    }
    void Generate_Card()
    {
        List<int> yama = new List<int>();
        for (int i = 0; i < 52; i++)
        {
            yama.Add(i);
        }
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
            //ここから7を抜く
            for (int j = 0; j < GameData.PlayerLimit; j++)
            {
                tmp = 6 + 13 * i;
                if (GameData.PlayerHand[j].Contains(tmp))
                {
                    GameData.PlayerHand[j].Remove(tmp);
                    GameData.Field[tmp / 13].Add(tmp);
                }
            }
        }
       
    }
    void Generate_Hand()
    {
        foreach (int i in GameData.PlayerHand[0])
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
            Debug.Log(tm.text);
            tm.text = tm.text.Replace("x", ((i%13)+1).ToString());
            tm.text = tm.text.Replace("d", tmp);
        }

    }
    // Start is called before the first frame update
    void Start()
    {
        Generate_Field();
        Generate_Card();
        Generate_Hand();
        Debug.Log(GameData.GetPlayerLimit());
        for (int i = 0; i < GameData.GetPlayerLimit(); i++)
        {
            Debug.Log("player" + i);
            foreach (int j in GameData.PlayerHand[i])
            {
                Debug.Log(j);
            }
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
