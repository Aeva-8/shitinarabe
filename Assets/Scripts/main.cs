using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;

[System.Serializable]
public class RoomInfo
{
    int roomId;
    int gameStatus;
    int partStatus;
    int turnStatus;
    int playerLimit;
    List<playerData> parts = new List<playerData>();
    List<leads> ls = new List<leads>();

}
[System.Serializable]
public class playerData
{
    string name;
    bool madness;
}
[System.Serializable]
public class leads
{
    int attribute;
    int villagerId;
    int cardStatus;
    bool isFront;
}

public class main : MonoBehaviour

    
{
    protected virtual void CheckTouch()
    {
        PointerEventData pointer = new PointerEventData(EventSystem.current);

        if (Input.touchCount <= 0)
        {
            return;
        }
        Touch touch = Input.GetTouch(0);
        if (touch.phase == TouchPhase.Began)
        {
            pointer.position = touch.position;
            List<RaycastResult> result = new List<RaycastResult>();
            EventSystem.current.RaycastAll(pointer, result);
            foreach (RaycastResult raycastResult in result)
            {
                Debug.Log(raycastResult.gameObject.name);
            }
        }
    }

        void Start()
    {
        
    }


    void Update()
    {
        CheckTouch();
    }
}
