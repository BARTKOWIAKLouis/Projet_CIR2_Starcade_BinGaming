using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Experimental.Rendering;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.VFX;
//using static UnityEditor.PlayerSettings;


public class Logic_script_ice : MonoBehaviour
{

    public GameObject Script_Client;
   
    //public GameObject SHO_Menu_Screen;
    public GameObject SHO_Victory_Screen;
    public GameObject map;
    private GameObject map_instance;
    public GameObject state_of_player;
    private GameObject[] tab_state_of_player;
    public UnityEngine.Vector3[] pos_of_state_of_player;

    public GameObject wining_player;
    private GameObject winner_gameobject;
    public GameObject player;
    public GameObject[] player_Tab;
    public int Nb_Player=4;
    private int[] start_position_taken;
    public string[] action_maps;
    public Sprite[] sprites;
    public GameObject[] lazer_color;

    public Vector3 spawn_position1;
    public Vector3 spawn_position2;
    public Vector3 spawn_position3;
    public Vector3 spawn_position4;
    public int winner_id = 0;
    private bool sho_started = false;


    private float timer_avant_start;
    private bool first_start;

    private void Start()
    {
        //Start_Game_SHO();
    }
    private void Update()
    {

        timer_avant_start += Time.deltaTime;
        if (!first_start && timer_avant_start > 0.1)
        {
            Nb_Player = Script_Client.GetComponent<WebSocketClient_ice>().Nombre_player;
            Debug.Log(Nb_Player);
            Start_Game_SHO();
            first_start = true;
        }




        int test = 0;

        if (sho_started)
        {
            for (int i = 0; i < Nb_Player; i++)
            {
                if (player_Tab[i] == null)
                {
                    tab_state_of_player[i].GetComponent<SpriteRenderer>().color = new Color(0,0,0,1);
                    test++;
                }
                else
                {

                    tab_state_of_player[i].transform.GetChild(0).GetComponent<SpriteRenderer>().color = new Color(0, 0, 0, 1);
                    tab_state_of_player[i].transform.GetChild(1).GetComponent<SpriteRenderer>().color = new Color(0, 0, 0, 1);

                    if (player_Tab[i].GetComponent<mvt_player_ice>().actionA)
                    {
                        tab_state_of_player[i].transform.GetChild(0).GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
                    }

                    if (player_Tab[i].GetComponent<mvt_player_ice>().actionB)
                    {
                        tab_state_of_player[i].transform.GetChild(1).GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
                    }

                    winner_id = i;
                }
            }

            if (test == Nb_Player - 1)
            {
                SHO_Victory_Screen.SetActive(true);
                for(int i = 0; i < Nb_Player; i++)
                {
                    Destroy(tab_state_of_player[i]);
                }
                Destroy(map_instance);
                winner();
                sho_started = false;

            }





        }
    }





    public void Start_Game_SHO()
    {
        Debug.Log("in start ?");

        

        SHO_Victory_Screen.SetActive(false);
        //SHO_Menu_Screen.SetActive(false);
        map_instance = Instantiate(map);
        SHO_Spawn_Player();
        spwan_state_of_player();

        sho_started = true;
    }

    public void SHO_Spawn_Player()
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
            GameObject clone = Instantiate(player, spawn_position1, Quaternion.Euler(0, 0, 315));
            clone.GetComponent<PlayerInput>().SwitchCurrentActionMap(action_maps[i]);
            clone.GetComponent<SpriteRenderer>().sprite = sprites[i];
            clone.GetComponent<mvt_player_ice>().color_lazer(lazer_color[i]);
            Script_Client.GetComponent<WebSocketClient_ice>().setScript(clone.GetComponent<mvt_player_ice>(), i);

            player_Tab[i] = clone;
        }
        else if (random_start_position == 2)
        {
            GameObject clone = Instantiate(player, spawn_position2, Quaternion.Euler(0, 0, 225));
            clone.GetComponent<PlayerInput>().SwitchCurrentActionMap(action_maps[i]);
            clone.GetComponent<SpriteRenderer>().sprite = sprites[i];
            clone.GetComponent<mvt_player_ice>().color_lazer( lazer_color[i]);
            Script_Client.GetComponent<WebSocketClient_ice>().setScript(clone.GetComponent<mvt_player_ice>(), i);

            player_Tab[i] = clone;
        }
        else if (random_start_position == 3)
        {
            GameObject clone = Instantiate(player, spawn_position3, Quaternion.Euler(0, 0, 135));
            clone.GetComponent<PlayerInput>().SwitchCurrentActionMap(action_maps[i]);
            clone.GetComponent<SpriteRenderer>().sprite = sprites[i];
            clone.GetComponent<mvt_player_ice>().color_lazer( lazer_color[i]);
            Script_Client.GetComponent<WebSocketClient_ice>().setScript(clone.GetComponent<mvt_player_ice>(), i);

            player_Tab[i] = clone;
        }
        else
        {
            GameObject clone = Instantiate(player, spawn_position4, Quaternion.Euler(0, 0, 45));
            clone.GetComponent<PlayerInput>().SwitchCurrentActionMap(action_maps[i]);
            clone.GetComponent<SpriteRenderer>().sprite = sprites[i];
            clone.GetComponent<mvt_player_ice>().color_lazer(lazer_color[i]);
            Script_Client.GetComponent<WebSocketClient_ice>().setScript(clone.GetComponent<mvt_player_ice>(), i);

            player_Tab[i] = clone;
        }

    }


    public void winner()
    {
        winner_gameobject = Instantiate(wining_player, Vector3.zero, Quaternion.identity);
        winner_gameobject.GetComponent<PlayerInput>().SwitchCurrentActionMap(action_maps[winner_id]);
        winner_gameobject.GetComponent<SpriteRenderer>().sprite = sprites[winner_id];
        Script_Client.GetComponent<WebSocketClient_ice>().setScript_winner(winner_gameobject.GetComponent<Winner_script_ice>(), winner_id);
    }


    public void spwan_state_of_player()
    {
        tab_state_of_player = new GameObject[Nb_Player];
        for (int i = 0; i < Nb_Player; i++)
        {
            tab_state_of_player[i] = Instantiate(state_of_player, pos_of_state_of_player[i], Quaternion.Euler(0, 0, 270));
            tab_state_of_player[i].GetComponent<SpriteRenderer>().sprite = sprites[i];
        }
    }



    public void kill_winner()
    {
        Destroy(winner_gameobject);
    }
}
