using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;
using TMPro;
using System;
using System.Linq;

[System.Serializable]
public class RoomInfo
{
    int roomId;
    int gameStatus;
    int partStatus;
    int turnStatus;
    int playerLimit;
    List<playerData> parts = new List<playerData>();
    List<Leads> leads = new List<Leads>();

}
[System.Serializable]
public class playerData
{
    string name;
    bool madness;
}
[System.Serializable]
public class Leads
{
    public int CardDataIndex;
    public  bool isFront;
}
[System.Serializable]
public class CardData
    {
    public int attribute;
    public int villagerId;
    }
public class main : MonoBehaviour   
{
    public TextMeshProUGUI Timer_Text;
    public GameObject Field;
    RoomInfo roominfo;
    List<Leads> leads = new List<Leads>();
    List<CardData> carddata = new List<CardData>();
    List<int> Selected_Card = new List<int>();
    //Selected_Cardはターンごとに-1で初期化
    int select_tmp=-1;
    float time_count = 10;
    GameObject CheckTouch()
    {
        PointerEventData pointer = new PointerEventData(EventSystem.current);
        if (Input.GetMouseButtonDown(0))
        {
            //mouseクリック
            pointer.position = Input.mousePosition;
            List<RaycastResult> result = new List<RaycastResult>();
            EventSystem.current.RaycastAll(pointer, result);
            foreach (RaycastResult raycastResult in result)
            {
                //Debug.Log(raycastResult.gameObject.name);
                return raycastResult.gameObject;
            }
        }
        else if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
            {
                pointer.position = touch.position;
                List<RaycastResult> result = new List<RaycastResult>();
                EventSystem.current.RaycastAll(pointer, result);
                foreach (RaycastResult raycastResult in result)
                {
                    //Debug.Log(raycastResult.gameObject.name);
                    return raycastResult.gameObject;
                }
            }
        }
        
        return null;
    }
    void villager_turn()
    {
        GameObject obj = CheckTouch();
        if (obj != null)
        {
            //オブジェクトをタッチしていた時
            if (obj.transform.parent.gameObject == Field)
            {
                Debug.Log("Field");
                //取得したオブジェクトがフィールド上にある時
                select_tmp = int.Parse(obj.name);
                Debug.Log(select_tmp + "仮選択中");
            }
            else if (obj.name == "Enter")
            {
                Debug.Log("Enter");
                //めくるボタン選択時
                if (Selected_Card.Count < 1)
                {
                    Selected_Card.Add(select_tmp);
                    select_tmp = -1;
                    int j = 1;
                    foreach (int i in Selected_Card)
                    {
                        Debug.Log(j + "枚目　：　" + i);
                        j++;
                    }
                }
            }
        }
        if (Timer_Text.text == "0")
        {
            int add_count = 0;
            //制限時間経過
            if (Selected_Card.Count == 0)
            {
                //0枚選択
                add_count = 2;
            }
            else if (Selected_Card.Count == 1)
            {
                if (select_tmp == -1)
                {
                    //1枚のみ選択
                    add_count = 1;
                }
                else
                {
                    Selected_Card.Add(select_tmp);
                }
            }
            
            //選択していない回数ランダムに選ぶ
            for (int i = 0; i < add_count; i++)
            {
                Selected_Card.Add(Convert.ToInt32((UnityEngine.Random.value * 1000) % 16));
            }
            //Debug.Log("Count" + Selected_Card.Count);
            Debug.Log("１枚目は" + Selected_Card[0] + "二枚目は" + Selected_Card[1]);
            
        }
    }
    void generate_Leads()
    {
        List<int> cardnum_tmp = new List<int>();
        CardData carddata_tmp = new CardData();
        for (int i = 0; i < 8; i++)
        {
            Leads leads_tmp = new Leads();
            //CardDataListのインスタンス生成
            if (i < 5)
            {
                carddata_tmp.attribute = 0;
                carddata_tmp.villagerId = i;
                leads_tmp.CardDataIndex = i;
                leads_tmp.isFront = false;
                leads.Add(leads_tmp);
                leads.Add(leads_tmp);

            }
            else if (i < 8)
            {
                //この辺は村人の人数によって決まる。
                carddata_tmp.attribute = i-4+1;
                leads_tmp.CardDataIndex = i;
                leads_tmp.isFront = false;
                leads.Add(leads_tmp);
                leads.Add(leads_tmp);
                carddata_tmp.villagerId = -1;
            }
            carddata.Add(carddata_tmp);
            //Debug.Log(i);
        }
        leads = leads.OrderBy(i => Guid.NewGuid()).ToList();
        for (int i = 0; i < leads.Count; i++)
        {
            Debug.Log("num i :" + i + "  leads" + leads[i].CardDataIndex);
        }






    }
        void Start()
    {
        generate_Leads();
        
        Debug.Log("leads : " + leads.Count + "carddata : " + carddata.Count);
    }

    void Timer()
    {
        time_count -= Time.deltaTime;
        if (time_count < 0)
        {
            time_count = 0;
        }
        Timer_Text.text = Convert.ToInt32(time_count).ToString();
    }
    void Update()
    {
        Timer();
        villager_turn();
        
    }
}
