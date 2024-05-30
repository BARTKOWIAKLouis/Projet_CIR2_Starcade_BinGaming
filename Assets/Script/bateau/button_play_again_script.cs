using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class button_play_again_script : MonoBehaviour
{
    public GameObject logic_manager;

    public bool play_again;



    void Start()
    {
        logic_manager = GameObject.FindGameObjectWithTag("logic_manager_tag");
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        gameObject.GetComponent<SpriteRenderer>().color = Color.red;
        play_again = true;

        logic_manager.GetComponent<logic_script>().kill_winner();
        logic_manager.GetComponent<logic_script>().start_CB();
    }
    public void OnTriggerExit2D(Collider2D other)
    {
        gameObject.GetComponent<SpriteRenderer>().color = Color.white;
        play_again = false;
    }

}
