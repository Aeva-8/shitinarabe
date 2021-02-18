using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OK : MonoBehaviour
{
    [SerializeField] GameObject Text_UI;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public  void OnClick()
    {
        if (GameData.GameStatus == 0)
        {
            //ゲーム終了時
            UnityEditor.EditorApplication.isPlaying = false;
        }
        else {
            //待機
            main.Turn_State = 1;
            Text_UI.SetActive(false);
        }
        
    }

}
