using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Card_movement : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public Transform Field;
    public void OnBeginDrag(PointerEventData eventData)
    {
        if (main.Turn_State == 1)
        {
            Field = transform.parent;
            if (Field.name != "Footer")
            {
                return;
            }
            transform.SetParent(Field.parent, false);
            GetComponent<CanvasGroup>().blocksRaycasts = false;
            //Canvas canvas = this.GetComponent<Canvas>();
            //canvas.overrideSorting = true;
        }

    }

    public void OnDrag(PointerEventData eventData)
    {
        if (Field.name != "Footer")
        {
            return;
        }
        transform.position = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        transform.SetParent(Field, false);
        GetComponent<CanvasGroup>().blocksRaycasts = true;
        //Canvas canvas = this.GetComponent<Canvas>();
        //canvas.overrideSorting = true;
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
