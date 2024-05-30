using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Windows;
using static UnityEngine.GraphicsBuffer;
using UnityEngine.InputSystem;

public class logic_script : MonoBehaviour
{
    public GameObject Script_Client;


    public GameObject CB_menu_screen;
    public GameObject CB_Victory_Screen;
    public GameObject vaisseau;
    private GameObject map_instance;
    public GameObject state_of_player;
    private GameObject[] tab_state_of_player;
    public UnityEngine.Vector3[] pos_of_state_of_player;

    public int Nb_player;
    private int[] start_position_taken;
    public GameObject player;
    public GameObject[] player_Tab;
    public string[] action_maps;

    public UnityEngine.Vector3 spawn_position1;
    public UnityEngine.Vector3 spawn_position2;
    public UnityEngine.Vector3 spawn_position3;
    public UnityEngine.Vector3 spawn_position4;

    public GameObject wining_player;
    private GameObject winner_gameobject;
    public int winner_id;
    public bool CB_started;

    public Sprite[][] sprite_mvt;
    public Sprite[] sprites;
    
    public Sprite[] sprite_mvt_P1;
    public Sprite[] sprite_mvt_P2;
    public Sprite[] sprite_mvt_P3;
    public Sprite[] sprite_mvt_P4;


    private float timer_avant_start;
    private bool first_start;

    void Start()
    {
        
        sprite_mvt = new Sprite[4][];
        sprite_mvt[0] = sprite_mvt_P1;
        sprite_mvt[1] = sprite_mvt_P2;
        sprite_mvt[2] = sprite_mvt_P3;
        sprite_mvt[3] = sprite_mvt_P4;


        //start_CB();
    }


    private void Update()
    {
        int test = 0;



        timer_avant_start += Time.deltaTime;
        if (!first_start && timer_avant_start > 0.5)
        {
            Nb_player = Script_Client.GetComponent<WebSocketClient_war>().Nombre_player;
            Debug.Log(Nb_player);
            start_CB();
            first_start = true;
        }



        if (CB_started)
        {
            for (int i = 0; i < Nb_player; i++)
            {
                if (player_Tab[i] == null)
                {
                    tab_state_of_player[i].GetComponent<SpriteRenderer>().color = new Color(0, 0, 0, 1);
                    test++;
                }
                else
                {

                    tab_state_of_player[i].transform.GetChild(0).GetComponent<SpriteRenderer>().color = new Color(0, 0, 0, 1);
                    tab_state_of_player[i].transform.GetChild(1).GetComponent<SpriteRenderer>().color = new Color(0, 0, 0, 1);

                    if (player_Tab[i].GetComponent<mvt_player>().actionA)
                    {
                        tab_state_of_player[i].transform.GetChild(0).GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
                    }

                    if (player_Tab[i].GetComponent<mvt_player>().actionB)
                    {
                        tab_state_of_player[i].transform.GetChild(1).GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
                    }

                    winner_id = i;
                }
            }

            if (test >= Nb_player - 1)
            {
                GameObject[] lazers = GameObject.FindGameObjectsWithTag("Lazer");
                foreach (GameObject lazer in lazers)
                {
                    Destroy(lazer);
                }

                CB_Victory_Screen.SetActive(true);
                for (int i = 0; i < Nb_player; i++)
                {
                    Destroy(tab_state_of_player[i]);
                }
                Destroy(player_Tab[winner_id]);
                Destroy(map_instance);
                winner();
                CB_started = false;

            }





        }
    }





    public void start_CB()
    {   
        CB_menu_screen.SetActive(false);
        CB_Victory_Screen.SetActive(false);
        generate_map();
        spawn_player_CB();
        spwan_state_of_player();
        CB_started = true;
    }


    public void generate_map()
    {
       map_instance = Instantiate(vaisseau);
    }

    public void spawn_player_CB()
    {
        start_position_taken = new int[4];
        start_position_taken[0] = -1;
        start_position_taken[1] = -1;
        start_position_taken[2] = -1;
        start_position_taken[3] = -1;


        player_Tab = new GameObject[4];
        action_maps = new string[4];


        action_maps[0] = "Player1";
        action_maps[1] = "Player2";
        action_maps[2] = "Player3";
        action_maps[3] = "Player4";


        for (int i = 0; i < Nb_player; i++)
        {

            random_start_position(i);
        }

    }


    public void random_start_position(int i)
    {

        int random_start_position = UnityEngine.Random.Range(1, 5);
        while (random_start_position == start_position_taken[0] || random_start_position == start_position_taken[1] || random_start_position == start_position_taken[2])
        {
            random_start_position = UnityEngine.Random.Range(1, 5);
        }
        start_position_taken[i] = random_start_position;




        if (random_start_position == 1)
        {
            GameObject clone = Instantiate(player, spawn_position1, UnityEngine.Quaternion.Euler(0, 0, 0));
            clone.GetComponent<PlayerInput>().SwitchCurrentActionMap(action_maps[i]);
            clone.GetComponent<SpriteRenderer>().sprite = sprites[i];
            clone.GetComponent<mvt_player>().set_sprite_deplacement(sprite_mvt[i]);
            Script_Client.GetComponent<WebSocketClient_war>().setScript(clone.GetComponent<mvt_player>(), i);

            player_Tab[i] = clone;
        }
        else if (random_start_position == 2)
        {
            GameObject clone = Instantiate(player, spawn_position2, UnityEngine.Quaternion.Euler(0, 0, 0));
            clone.GetComponent<PlayerInput>().SwitchCurrentActionMap(action_maps[i]);
            clone.GetComponent<SpriteRenderer>().sprite = sprites[i];
            clone.GetComponent<mvt_player>().set_sprite_deplacement(sprite_mvt[i]);
            Script_Client.GetComponent<WebSocketClient_war>().setScript(clone.GetComponent<mvt_player>(), i);

            player_Tab[i] = clone;
        }
        else if (random_start_position == 3)
        {
            GameObject clone = Instantiate(player, spawn_position3, UnityEngine.Quaternion.Euler(0, 0, 0));
            clone.GetComponent<PlayerInput>().SwitchCurrentActionMap(action_maps[i]);
            clone.GetComponent<SpriteRenderer>().sprite = sprites[i];
            clone.GetComponent<mvt_player>().set_sprite_deplacement(sprite_mvt[i]);
            Script_Client.GetComponent<WebSocketClient_war>().setScript(clone.GetComponent<mvt_player>(), i);

            player_Tab[i] = clone;
        }
        else
        {
            GameObject clone = Instantiate(player, spawn_position4, UnityEngine.Quaternion.Euler(0, 0, 0));
            clone.GetComponent<PlayerInput>().SwitchCurrentActionMap(action_maps[i]);
            clone.GetComponent<SpriteRenderer>().sprite = sprites[i];
            clone.GetComponent<mvt_player>().set_sprite_deplacement(sprite_mvt[i]);
            Script_Client.GetComponent<WebSocketClient_war>().setScript(clone.GetComponent<mvt_player>(), i);

            player_Tab[i] = clone;
        }

    }




    public void winner()
    {
        winner_gameobject = Instantiate(wining_player, UnityEngine.Vector3.zero, UnityEngine.Quaternion.identity);
        winner_gameobject.GetComponent<PlayerInput>().SwitchCurrentActionMap(action_maps[winner_id]);
        winner_gameobject.GetComponent<SpriteRenderer>().sprite = sprites[winner_id];
        winner_gameobject.GetComponent<winner_script>().set_sprite_deplacement(sprite_mvt[winner_id]);

        Script_Client.GetComponent<WebSocketClient_war>().setScript_winner(winner_gameobject.GetComponent<winner_script>(), winner_id);

    }


    public void spwan_state_of_player()
    {
        tab_state_of_player = new GameObject[Nb_player];
        for (int i = 0; i < Nb_player; i++)
        {
            tab_state_of_player[i] = Instantiate(state_of_player, pos_of_state_of_player[i], UnityEngine.Quaternion.Euler(0, 0, 0));
            tab_state_of_player[i].GetComponent<SpriteRenderer>().sprite = sprites[i];
        }
    }



    public void kill_winner()
    {
        Destroy(winner_gameobject);
    }


}
