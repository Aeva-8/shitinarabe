using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameData
{
    public static int RoomId;
    public static int GameStatus;
    public static int PlayerLimit=4;
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
    List<int> Shuffle(List<int> list)
    {
        return list = list.OrderBy(i => Guid.NewGuid()).ToList();
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

    void Generate_Hand()
    {

    }
    // Start is called before the first frame update
    void Start()
    {
        Generate_Card();
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
