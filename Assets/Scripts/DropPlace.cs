using System.Collections;
using System.Collections.Generic;
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
            card.Field = this.transform; // カードの親要素を自分（アタッチされてるオブジェクト）にする
        }
    }
}
