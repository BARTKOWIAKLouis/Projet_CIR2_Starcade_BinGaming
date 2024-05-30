using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.VFX;
//using static UnityEditor.PlayerSettings;



public class Logicscript_tron: MonoBehaviour
{
    public GameObject Script_Client; //camera


    public GameObject Tron_Menu_Screen;
    public GameObject Tron_Victory_Screen;
    public GameObject winner_gameobject;
    private GameObject winningplayer;
    public GameObject player;
    public GameObject[] player_Tab;
    public GameObject state_of_player;
    public GameObject[] tab_state_of_player;
    public Vector3[] pos_of_state_of_player;
    public int Nb_Player;
    private int[] start_position_taken;
    public bool tron_started;

    public Vector3 spawn_position1;
    public Vector3 spawn_position2;
    public Vector3 spawn_position3;
    public Vector3 spawn_position4;

    public string[] action_maps;

    public int Idmap = 0;
    public int nb_tron_map = 6;
    public GameObject tron_map1;
    public GameObject tron_map2;
    public GameObject tron_map3;
    public GameObject tron_map4;
    public GameObject tron_map5;
    public GameObject tron_map6;

    public GameObject Object_btnR;
    public GameObject Object_btnL;

    private GameObject tron_map_actuel;
    public int winner_id;

    public Sprite[] sprites;

    private float timer_avant_start;
    private bool first_start;

    // Start is called before the first frame update
    void Start()
    {
        //Nb_Player = Script_Client.GetComponent<WebSocketClient_tron>().Nombre_player;
        
    }

    // Update is called once per frame
    private void Update()
    {
        timer_avant_start += Time.deltaTime;
        if(!first_start && timer_avant_start > 0.1)
        {
            Nb_Player = Script_Client.GetComponent<WebSocketClient_tron>().Nombre_player;
            Debug.Log(Nb_Player);
            Start_Game_Tron();
            first_start = true;
        }


        int test = 0;

        if (tron_started)
        {
            for (int i = 0; i < Nb_Player; i++)
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

                    if (player_Tab[i].GetComponent<Movement_player_tron>().actionA)
                    {
                        tab_state_of_player[i].transform.GetChild(0).GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
                    }

                    if (player_Tab[i].GetComponent<Movement_player_tron>().actionB)
                    {
                        tab_state_of_player[i].transform.GetChild(1).GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
                    }
                
                    winner_id = i;
                }
                
            }
            Debug.Log(test);
            if (test >= Nb_Player - 1)
            {
                GameObject[] cubes = GameObject.FindGameObjectsWithTag("wall");
                foreach (GameObject cube in cubes)
                {
                    Destroy(cube);
                }

                Tron_Victory_Screen.SetActive(true);
                
                for (int i = 0; i < Nb_Player; i++)
                {
                    Destroy(tab_state_of_player[i]);
                }
                
                Destroy(player_Tab[winner_id]);
                Destroy(tron_map_actuel);
                winner();
                tron_started = false;
                
            }





        }
    }


    public void Start_Game_Tron()
    {
        Tron_Menu_Screen.SetActive(false);
        Tron_Victory_Screen.SetActive(false);
        tron_started = true;
        Tron_Spawn_Player();
        spwan_state_of_player();
    }

    public void Tron_Spawn_Player()
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

        
        for (int i = 0; i < Nb_Player; i++)
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
            GameObject clone = Instantiate(player, spawn_position1, Quaternion.Euler(0, 0, 270));
            clone.GetComponent<PlayerInput>().SwitchCurrentActionMap(action_maps[i]);
            clone.GetComponent<SpriteRenderer>().sprite = sprites[i];
            clone.GetComponent<Movement_player_tron>().tron_color_cube(i);
            Script_Client.GetComponent<WebSocketClient_tron>().setScript(clone.GetComponent<Movement_player_tron>(), i);

            player_Tab[i] = clone;
        }
        else if (random_start_position == 2)
        {
            GameObject clone = Instantiate(player, spawn_position2, Quaternion.Euler(0, 0, 270));
            clone.GetComponent<PlayerInput>().SwitchCurrentActionMap(action_maps[i]);
            clone.GetComponent<SpriteRenderer>().sprite = sprites[i];
            clone.GetComponent<Movement_player_tron>().tron_color_cube(i);
            Script_Client.GetComponent<WebSocketClient_tron>().setScript(clone.GetComponent<Movement_player_tron>(), i);

            player_Tab[i] = clone;
        }
        else if (random_start_position == 3)
        {
            GameObject clone = Instantiate(player, spawn_position3, Quaternion.Euler(0, 0, 90));
            clone.GetComponent<PlayerInput>().SwitchCurrentActionMap(action_maps[i]);
            clone.GetComponent<SpriteRenderer>().sprite = sprites[i];
            clone.GetComponent<Movement_player_tron>().tron_color_cube(i);
            Script_Client.GetComponent<WebSocketClient_tron>().setScript(clone.GetComponent<Movement_player_tron>(), i);

            player_Tab[i] = clone;
        }
        else
        {
            GameObject clone = Instantiate(player, spawn_position4, Quaternion.Euler(0, 0, 90));
            clone.GetComponent<PlayerInput>().SwitchCurrentActionMap(action_maps[i]);
            clone.GetComponent<SpriteRenderer>().sprite = sprites[i];
            clone.GetComponent<Movement_player_tron>().tron_color_cube(i);
            Script_Client.GetComponent<WebSocketClient_tron>().setScript(clone.GetComponent<Movement_player_tron>(), i);

            player_Tab[i] = clone;
        }
    }





    public void Tron_next_map()
    {
        Button btnR = Object_btnR.GetComponent<Button>();
        Button btnL = Object_btnL.GetComponent<Button>();
        if ( Idmap != nb_tron_map)
        {
            Destroy(tron_map_actuel);

            Idmap++;

            Tron_setup_map(Idmap);
            if (Idmap == nb_tron_map)
            {
                btnR.interactable = false;
            }
            btnL.interactable = true;
        }
    }

    public void Tron_previous_map()
    {
        Button btnR = Object_btnR.GetComponent<Button>();
        Button btnL = Object_btnL.GetComponent<Button>();
        if (Idmap != 0)
        {
            Destroy(tron_map_actuel);

            Idmap--;

            Tron_setup_map(Idmap);
            if (Idmap == 0)
            {
                btnL.interactable = false;
            }
            btnR.interactable = true;
        }
    }

    public void Tron_setup_map(int idmap)
    {
        switch (idmap)
        {
            case 1:
                {
                    tron_map_actuel= Instantiate(tron_map1);
                    break;
                }
            case 2:
                {
                    tron_map_actuel = Instantiate(tron_map2);
                    break;
                }
            case 3:
                {
                    tron_map_actuel = Instantiate(tron_map3);
                    break;
                }
            case 4:
                {
                    tron_map_actuel = Instantiate(tron_map4);
                    break;
                }
            case 5:
                {
                    tron_map_actuel = Instantiate(tron_map5);
                    break;
                }
            case 6:
                {
                    tron_map_actuel = Instantiate(tron_map6);
                    break;
                }
        }

    }



    public void winner()
    {
        winningplayer = Instantiate(winner_gameobject, UnityEngine.Vector3.zero, UnityEngine.Quaternion.identity);
        winningplayer.GetComponent<PlayerInput>().SwitchCurrentActionMap(action_maps[winner_id]);
        winningplayer.GetComponent<SpriteRenderer>().sprite = sprites[winner_id];
        Script_Client.GetComponent<WebSocketClient_tron>().setScript_winner(winningplayer.GetComponent<winner_tron_script>(), winner_id);
    }


    public void kill_winner()
    {
        Destroy(winningplayer);
    }


    public void spwan_state_of_player()
    {
        tab_state_of_player = new GameObject[Nb_Player];
        for (int i = 0; i < Nb_Player; i++)
        {
            tab_state_of_player[i] = Instantiate(state_of_player, pos_of_state_of_player[i], UnityEngine.Quaternion.Euler(0, 0, 270));
            tab_state_of_player[i].GetComponent<SpriteRenderer>().sprite = sprites[i];
        }
    }


    public void get_nb_of_player(){
        Nb_Player = Script_Client.GetComponent<WebSocketClient_tron>().Nombre_player;
    }


}



