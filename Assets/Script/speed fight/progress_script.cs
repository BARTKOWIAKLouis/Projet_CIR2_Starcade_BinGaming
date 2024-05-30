using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.InputSystem;
using static Unity.Burst.Intrinsics.X86.Avx;
using System;


public class progress_script : MonoBehaviour

{

    public Vector3 gauche = new Vector3(-0.2005f, 0, 0);
    public Vector3 droite = new Vector3(0.2005f, 0, 0);

    public Vector3 gauche_error = new Vector3(-0.1005f, 0, 0);
    public Vector3 droite_error = new Vector3(0.1005f, 0, 0);

    public GameObject atteinte1;
    public GameObject atteinte2;
    public GameObject progression;
    public GameObject barre;
    public GameObject fond_fin;
    public GameObject sortie;
    public GameObject entree;
    public GameObject bouton_sortie;
    public GameObject bouton_entree;
    public GameObject rond1;
    public GameObject rond2;

    public int color;

    public bool active;
    public bool fin;

    public GameObject win_screen;
    public GameObject fond;
    public TMP_Text win_text;

    public SpawnBulles spawn;

    public int nb_joueurs;

    public Sprite[] sprite_tab_1;
    public Sprite[] sprite_tab_2;
    public Sprite[] sprite_tab_3;
    public Sprite[] sprite_tab_4;

    // Start is called before the first frame update
    void Start()
    {
        spawn = GameObject.FindGameObjectWithTag("Spawn").GetComponent<SpawnBulles>();
        active = false;
        fin = false;
        nb_joueurs = spawn.nombre_joueurs;
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x <= atteinte1.transform.position.x)
        {
            color = 1;
            finish(1);


        }

        if (transform.position.x >= atteinte2.transform.position.x)
        {
            if (nb_joueurs == 2)
            {
                color = 2;
            }
            else if (nb_joueurs == 4)
            {
                string m = spawn.equipe2[0];
                color = (m[7] - '0');
            }
            finish(2);
        }
    }

    public void trompe_j1()
    {
        transform.position += new Vector3(0.1005f, 0, 0);
    }

    public void trompe_j2()
    {
        transform.position += new Vector3(-0.1005f, 0, 0);
    }

    public bool av_j1()
    {
        transform.position += new Vector3(-0.2005f, 0, 0);
        return true;
    }

    public bool av_j2()
    {

        transform.position += new Vector3(0.2005f, 0, 0);
        return true;

    }

    public void finish(int nb){

        if (nb == 1)
        {
            if (nb_joueurs == 2)
            {
                fin = true;
                //win_text.text = "Joueur 1 win !";
                //win_screen.SetActive(true);
                fond.SetActive(false);
                atteinte1.SetActive(false);
                atteinte2.SetActive(false);
                barre.SetActive(false);
                progression.SetActive(false);
                fond_fin.SetActive(true);
                sortie.SetActive(true);
                entree.SetActive(true);
                for (int i = 0; i < 2; i++)
                {
                    Destroy(spawn.bulle_tab[i]);
                }
                for (int i = 0; i < 2; i++)
                {
                    Destroy(spawn.prochaine_bulle_tab[i]);
                }

                active_bouton();
            }

            else if (nb_joueurs == 4)
            {
                fin = true;
                //win_text.text = string.Concat(spawn.equipe1[0], " and ", spawn.equipe1[1], " win !");
                //win_screen.SetActive(true);
                fond.SetActive(false);
                atteinte1.SetActive(false);
                atteinte2.SetActive(false);
                barre.SetActive(false);
                progression.SetActive(false);
                fond_fin.SetActive(true);
                sortie.SetActive(true);
                entree.SetActive(true);
                for (int i = 0; i < 4; i++)
                {
                    Destroy(spawn.bulle_tab[i]);
                }
                for (int i = 0; i < 4; i++)
                {
                    Destroy(spawn.prochaine_bulle_tab[i]);
                }

                active_bouton();
            }

        }

        else
        {
            if (nb_joueurs == 2)
            {
                fin = true;
                //win_text.text = "Joueur 2 win !";
                //win_screen.SetActive(true);
                fond.SetActive(false);
                atteinte1.SetActive(false);
                atteinte2.SetActive(false);
                barre.SetActive(false);
                progression.SetActive(false);
                fond_fin.SetActive(true);
                sortie.SetActive(true);
                entree.SetActive(true);
                for (int i = 0; i < 2; i++)
                {
                    Destroy(spawn.bulle_tab[i]);
                }
                for (int i = 0; i < 2; i++)
                {
                    Destroy(spawn.prochaine_bulle_tab[i]);
                }

                active_bouton();
            }

            else if (nb_joueurs == 4)
            {
                fin = true;
                //win_text.text = string.Concat(spawn.equipe2[0], " and ", spawn.equipe2[1], " win !");
                //win_screen.SetActive(true);
                fond.SetActive(false);
                atteinte1.SetActive(false);
                atteinte2.SetActive(false);
                barre.SetActive(false);
                progression.SetActive(false);
                fond_fin.SetActive(true);
                sortie.SetActive(true);
                entree.SetActive(true);
                for (int i = 0; i < 4; i++)
                {
                    Destroy(spawn.bulle_tab[i]);
                }
                for (int i = 0; i < 4; i++)
                {
                    Destroy(spawn.prochaine_bulle_tab[i]);
                }
            }

            active_bouton();
        }


    }

    public void active_bouton()
    {
        bouton_entree.SetActive(true);
        bouton_sortie.SetActive(true);
        rond1.SetActive(true);
        rond2.SetActive(true);
        if (color == 1) 
        {
            bouton_sortie.GetComponent<SpriteRenderer>().sprite = sprite_tab_1[3];
            bouton_entree.GetComponent<SpriteRenderer>().sprite = sprite_tab_1[1];
        }

        else if (color == 2)
        {
            bouton_sortie.GetComponent<SpriteRenderer>().sprite = sprite_tab_2[3];
            bouton_entree.GetComponent<SpriteRenderer>().sprite = sprite_tab_2[1];
        }

        else if (color == 3)
        {
            bouton_sortie.GetComponent<SpriteRenderer>().sprite = sprite_tab_3[3];
            bouton_entree.GetComponent<SpriteRenderer>().sprite = sprite_tab_3[1];
        }
        else if (color == 4)
        {
            bouton_sortie.GetComponent<SpriteRenderer>().sprite = sprite_tab_4[3];
            bouton_entree.GetComponent<SpriteRenderer>().sprite = sprite_tab_4[1];
        }
    }
   

}
