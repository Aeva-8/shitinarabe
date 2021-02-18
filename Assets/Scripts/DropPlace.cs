using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

// フィールドにアタッチするクラス
public class DropPlace : MonoBehaviour, IDropHandler
{
    public void OnDrop(PointerEventData eventData) // ドロップされた時に行う処理
    {
        Debug.Log("drop");
        Card_movement card = eventData.pointerDrag.GetComponent<Card_movement>(); // ドラッグしてきた情報からCardMovementを取得
        if (card != null) // もしカードがあれば、
        {
            int card_num = int.Parse(card.name);
            if (Check(card_num))
            {
                if (card_num % 13 > 6)
                {
                    //7以上
                    GameObject chil_tmp = this.transform.Find("after_7").gameObject;
                    card.Field = chil_tmp.transform;
                }
                else
                {
                    GameObject chil_tmp = this.transform.Find("before_7").gameObject;
                    card.Field = chil_tmp.transform;
                }
                //Playerhandからカードデータを消去し、Fieldにカードデータを追加する
                //Turn_stateを0にする
                GameData.PlayerHand[GameData.Turn].Remove(card_num);
                GameData.Field[int.Parse(this.name)].Add(card_num);
                main.Turn_State = 0;

            }


        }
    }
    bool Check(int card_num)
    {
        int sort = int.Parse(this.name);
        if ( sort== card_num / 13)
        {
            Debug.Log("Field : "+GameData.Field.Count());
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
