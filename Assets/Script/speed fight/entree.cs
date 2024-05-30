using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class entree : MonoBehaviour
{
    public GameObject Script_Client;
    public GameObject spawner;


    public Sprite sprite_entree;

    public SpawnBulles spawn;
    public progress_script progress;
    public GameObject progress_object;
    public bool do_it;

    // Start is called before the first frame update
    void Start()
    {
        Script_Client = spawner.GetComponent<SpawnBulles>().Script_Client;

        spawn = GameObject.FindGameObjectWithTag("Spawn").GetComponent<SpawnBulles>();
        //progress = GameObject.FindGameObjectWithTag("Progress").GetComponent<progress_script>();
        progress = progress_object.GetComponent<progress_script>();

        int color = progress.color;

        int emp = color - 1;

        //gameObject.GetComponent<PlayerInput>().SwitchCurrentActionMap(spawn.joueurs[emp]);

        if (Script_Client.TryGetComponent<WebSocketClient_speed_fight>(out WebSocketClient_speed_fight script1))
        {
            script1.setScript_winner_1(gameObject.GetComponent<entree>(), emp);
        }
        if (Script_Client.TryGetComponent<WebSocketClient_speed_fight_4p>(out WebSocketClient_speed_fight_4p script2))
        {
            script2.setScript_winner_1(gameObject.GetComponent<entree>(), emp);
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        if(do_it)
        {
            Debug.Log("right mon ga");
            spawn.restartGame();
        }
    }

    public void right(string context)
    //public void droite(InputAction.CallbackContext context)
    {
        if (context== "on")
        {
            //spawn.restartGame();
            do_it = true;
        }
    }
}
