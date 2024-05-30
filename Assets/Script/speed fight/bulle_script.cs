using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.InputSystem;
using static Unity.Burst.Intrinsics.X86.Avx;


public class bulle_script : MonoBehaviour

{
    
    public int new_tape_i;
    public string tape;
    public SpawnBulles spawn;
    public progress_script progress;
    public int joueur;

    public Sprite[] sprite_tab_1;
    public Sprite[] sprite_tab_2;
    public Sprite[] sprite_tab_3;
    public Sprite[] sprite_tab_4;


    public bool active;
    public bool fin;

    public bool do_1;
    public bool do_2;
    public bool fail_1;
    public bool fail_2;
    


    // Start is called before the first frame update
    void Start()
    {
        active = false;
        fin = false;
        spawn = GameObject.FindGameObjectWithTag("Spawn").GetComponent<SpawnBulles>();
        progress = GameObject.FindGameObjectWithTag("Progress").GetComponent<progress_script>();

        joueur = spawn.donne_id(gameObject);

        spawn.supp_tab_proch(spawn.prochaine_bulle_tab[joueur]);

        tape = spawn.proch_int[joueur];

        new_tape_i = tape[0] - '0';

        switch (joueur)
        {
            case 0:
                gameObject.GetComponent<SpriteRenderer>().sprite = sprite_tab_1[new_tape_i];
                break;

            case 1:
                gameObject.GetComponent<SpriteRenderer>().sprite = sprite_tab_2[new_tape_i];
                break;

            case 2:
                gameObject.GetComponent<SpriteRenderer>().sprite = sprite_tab_3[new_tape_i];
                break;

            case 3:
                gameObject.GetComponent<SpriteRenderer>().sprite = sprite_tab_4[new_tape_i];
                break;
        }

        

    }

    // Update is called once per) frame
    void Update()
    {
        if (do_1)
        {
            do_1 = false;
            bool det1 = spawn.supp_tab(gameObject);
            bool det2 = progress.av_j1();
            if (det1 && det2)
            {

                Destroy(gameObject);
            }
        }
        if (do_2)
        {

            do_2 = false;
            bool det1 = spawn.supp_tab(gameObject);
            bool det2 = progress.av_j2();
            if (det1 && det2)
            {

                Destroy(gameObject);
            }
        }
        if (fail_1)
        {
            fail_1 = false;
            progress.trompe_j1();
        }
        if (fail_2)
        {
            fail_2 = false;
            progress.trompe_j2();
        }
    }

    //public void haut(InputAction.CallbackContext context)
    public void up(string context)
    {
        fin = progress.fin;
        if (!active && !fin)
        {
            active = true;
            if (tape == "0 haut" && context=="on")
            {

                if (joueur == 0)
                {/*
                    bool det1 = spawn.supp_tab(gameObject);
                    bool det2 = progress.av_j1();
                    if (det1 && det2)
                    {

                        Destroy(gameObject);
                    }*/
                    do_1 = true;
                }
                else
                {
                    /*bool det1 = spawn.supp_tab(gameObject);
                    bool det2 = progress.av_j2();
                    if (det1 && det2)
                    {
                        
                        Destroy(gameObject);
                    }*/

                    do_2 = true;
                }
            }
            else if (context=="on")
            {

                if (joueur == 0)
                {
                    //progress.trompe_j1();
                    fail_1 = true;
                }
                else
                {
                    //progress.trompe_j2();
                    fail_2 = true;
                }
            }

            

        }
        active = false;

    }

    //public void droite(InputAction.CallbackContext context)
    public void right(string context)
    {
        fin = progress.fin;
        if (!active && !fin)
        {
            active = true;
            if (tape == "1 droite" && context=="on")
            {

                if (joueur == 0)
                {/*
                    bool det1 = spawn.supp_tab(gameObject);
                    bool det2 = progress.av_j1();
                    if (det1 && det2)
                    {

                        Destroy(gameObject);
                    }*/
                    do_1 = true;
                }
                else
                {
                    /*bool det1 = spawn.supp_tab(gameObject);
                    bool det2 = progress.av_j2();
                    if (det1 && det2)
                    {
                        
                        Destroy(gameObject);
                    }*/

                    do_2 = true;
                }
            }
            else if (context=="on")
            {

                if (joueur == 0)
                {
                    //progress.trompe_j1();
                    fail_1 = true;
                }
                else
                {
                    //progress.trompe_j2();
                    fail_2 = true;
                }
            }

        }

        active = false;
    }

    //public void bas(InputAction.CallbackContext context)
    public void down(string context)
    {
        fin = progress.fin;
        if (!active && !fin)
        {
            active= true;
            if (tape == "2 bas" && context=="on")
            {

                if (joueur == 0)
                {/*
                    bool det1 = spawn.supp_tab(gameObject);
                    bool det2 = progress.av_j1();
                    if (det1 && det2)
                    {

                        Destroy(gameObject);
                    }*/
                    do_1 = true;
                }
                else
                {
                    /*bool det1 = spawn.supp_tab(gameObject);
                    bool det2 = progress.av_j2();
                    if (det1 && det2)
                    {
                        
                        Destroy(gameObject);
                    }*/

                    do_2 = true;
                }
            }
            else if (context=="on")
            {

                if (joueur == 0)
                {
                    //progress.trompe_j1();
                    fail_1= true;
                }
                else
                {
                    //progress.trompe_j2();
                    fail_2= true;
                }
            }

        }
        active = false;
        
    }

    //public void gauche(InputAction.CallbackContext context)
    public void left(string context)
    {
        fin = progress.fin;
        if (!active && !fin)
        {
            active = true;
            if (tape == "3 gauche" && context=="on")
            {


                if (joueur == 0)
                {/*
                    bool det1 = spawn.supp_tab(gameObject);
                    bool det2 = progress.av_j1();
                    if (det1 && det2)
                    {

                        Destroy(gameObject);
                    }*/
                    do_1 = true;
                }
                else
                {
                    /*bool det1 = spawn.supp_tab(gameObject);
                    bool det2 = progress.av_j2();
                    if (det1 && det2)
                    {
                        
                        Destroy(gameObject);
                    }*/

                    do_2 = true;
                }
            }
            else if (context=="off")
            {

                if (joueur == 0)
                {
                    //progress.trompe_j1();
                    fail_1  = true;
                }
                else
                {
                    //progress.trompe_j2();
                    fail_2 = true;
                }
            }

        }

        active = false;
        
    }
}

