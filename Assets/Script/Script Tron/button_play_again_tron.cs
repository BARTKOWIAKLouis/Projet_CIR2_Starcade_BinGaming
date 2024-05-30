using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class button_play_again_tron : MonoBehaviour
{
    public GameObject logic_manager;


    void Start()
    {
        logic_manager = GameObject.FindGameObjectWithTag("logic_manager_tag");
    } 

    public void OnTriggerEnter2D(Collider2D other)
    {
        logic_manager.GetComponent<Logicscript_tron>().kill_winner();
        logic_manager.GetComponent<Logicscript_tron>().Start_Game_Tron();
    }
}
