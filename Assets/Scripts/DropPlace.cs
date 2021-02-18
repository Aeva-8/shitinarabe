using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

// フィールドにアタッチするクラス
public class DropPlace : MonoBehaviour, IDropHandler
{
    public void OnDrop(PointerEventData eventData) // ドロップされた時に行う処理
    {
        Card_movement card = eventData.pointerDrag.GetComponent<Card_movement>(); // ドラッグしてきた情報からCardMovementを取得
        if (card != null) // もしカードがあれば、
        {
            int card_num = int.Parse(card.name);
            if (Check(card_num, int.Parse(this.name)))
            {
                GameObject chil_tmp;
                if (card_num % 13 > 6)
                {
                    //7以上
                    chil_tmp = this.transform.Find("after_7").gameObject.transform.FindChild(card_num.ToString()).gameObject;
                    //card.Field = chil_tmp.transform;
                }
                else
                {
                    chil_tmp = this.transform.Find("before_7").gameObject.transform.FindChild(card_num.ToString()).gameObject;
                    //card.Field = chil_tmp.transform;
                }
                Image image = chil_tmp.GetComponent<Image>();
                image.color = new Color(image.color.r, image.color.g, image.color.b, 255);
                TextMeshProUGUI card_txt = chil_tmp.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
                card_txt.color = new Color(image.color.r, image.color.g, image.color.b, 255);
                //Playerhandからカードデータを消去し、Fieldにカードデータを追加する
                //Turn_stateを0にする
                GameData.PlayerHand[GameData.Turn].Remove(card_num);
                GameData.Field[int.Parse(this.name)].Add(card_num);
                main.Turn_State = 0;

            }


        }
    }
    bool Check(int card_num ,int sort)
    {
        
        if ( sort== card_num / 13)
        {
            //Debug.Log("GameData.Field[sort].Max() : " + GameData.Field[sort].Max());
            //ソートが正しいか評価
            if (card_num == GameData.Field[sort].Min() - 1 || card_num == GameData.Field[sort].Max() + 1)
            {
                //置ける数字か評価
                return true;
            }
        }
        return false;
    }
}
