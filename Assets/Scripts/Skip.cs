using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skip : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnClick()
    {
        if (main.Turn_State == 1)
        {
            GameData.Player_state[GameData.Turn]++;
            main.Turn_State = 2;
        }
        
    }
}
