using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.InputSystem;
using System.Linq;
using System.ComponentModel;
using JetBrains.Annotations;


public class SpawnBulles : MonoBehaviour
{
    public GameObject Script_Client;
    public GameObject Script_Client_2p;
    public GameObject Script_Client_4p;

    public int nombre_joueurs;

    public GameObject bulle;
    public GameObject proch_bulle;
    public GameObject bulle_4;
    public GameObject proch_bulle_4;
    public GameObject[] bulle_tab;
    public GameObject[] prochaine_bulle_tab;

    public GameObject atteinte1;
    public GameObject atteinte2;
    public GameObject point_prog;
    public GameObject barre;

    public GameObject button_start;

    public Vector3[] pos;

    public Vector3[] proch_pos;

    public Vector3[] pos_4;

    public Vector3[] proch_pos_4;

    public bool lancement;

    public string[] joueurs = { "Joueur 1", "Joueur 2", "Joueur 3" , "Joueur 4"};


    public int joueur;

    public string tape;

    public SpawnBulles spawn;


    public string[] proch_int;

    public string[] equipe1;
    public string[] equipe2;



    private float timer_avant_start;
    private bool first_start;

    public string nouvellescene;
    public bool retour = false;
    [SerializeField] Animator transition_fondu;
    public float transitionTime = 1f;


    // Start is called before the first frame update
    void Start()
    {
        Script_Client = Instantiate(Script_Client_2p);
    }

    // Update is called once per frame
    void Update()
    {
        timer_avant_start += Time.deltaTime;
        if (!first_start && timer_avant_start > 0.1)
        {
            nombre_joueurs = Script_Client.GetComponent<WebSocketClient_speed_fight>().Nombre_player;
            Debug.Log(nombre_joueurs);
            if(nombre_joueurs == 3)
            {
                StartCoroutine(LoadLevel(nouvellescene));
            }
            if(nombre_joueurs == 4)
            {
                Destroy(Script_Client);
                Script_Client = Instantiate(Script_Client_4p);
                //Script_Client.GetComponent<WebSocketClient_speed_fight>().enabled = false;
                //Script_Client.GetComponent<WebSocketClient_speed_fight_4p>().enabled = true;
            }
            Invoke("lancer_game", 2.0f);
            first_start = true;
        }

        

    }

    IEnumerator LoadLevel(string levelnext)
    {
        transition_fondu.SetTrigger("start");
        yield return new WaitForSeconds(transitionTime);
        SceneManager.LoadScene(levelnext);
        transition_fondu.SetTrigger("end");

    }


    public void lancer_game()
    {
        
        barre.SetActive(true);
        point_prog.SetActive(true); 
        atteinte1.SetActive(true);
        atteinte2.SetActive(true);

        if (nombre_joueurs == 2)
        {
            bulle_tab = new GameObject[2];
            prochaine_bulle_tab = new GameObject[2];
            proch_int = new string[2];

            for (int i = 0; i < 2; i++)
            {
                GameObject clone = Instantiate(proch_bulle, proch_pos[i], Quaternion.identity);
                Script_Client.GetComponent<WebSocketClient_speed_fight>().setScript(clone.GetComponent<bulle_script>(), i);
                prochaine_bulle_tab[i] = clone;


            }

            for (int i = 0; i < 2; i++)
            {
                GameObject clone = Instantiate(bulle, pos[i], Quaternion.identity);
                //clone.GetComponent<PlayerInput>().SwitchCurrentActionMap(joueurs[i]);
                Script_Client.GetComponent<WebSocketClient_speed_fight>().setScript(clone.GetComponent<bulle_script>(), i);

                bulle_tab[i] = clone;


            }
        }
        else if (nombre_joueurs == 4) 
        {

            bulle_tab = new GameObject[4];
            prochaine_bulle_tab = new GameObject[4];
            proch_int = new string[4];
            equipe1 = new string[2];
            equipe2 = new string[2];

            int equipe = Random.Range(0, 3);

            switch (equipe)
            {
                case 0:
                    equipe1[0] = "Joueur 1";
                    equipe1[1] = "Joueur 2";
                    equipe2[0] = "Joueur 3";
                    equipe2[1] = "Joueur 4";
                    break;
                case 1:
                    equipe1[0] = "Joueur 1";
                    equipe1[1] = "Joueur 3";
                    equipe2[0] = "Joueur 2";
                    equipe2[1] = "Joueur 4";
                    break;
                case 2:
                    equipe1[0] = "Joueur 1";
                    equipe1[1] = "Joueur 4";
                    equipe2[0] = "Joueur 2";
                    equipe2[1] = "Joueur 3";
                    break;
            }

            for (int i = 0; i < 4; i++)
            {
                int new_equipe = equipage(i);
                GameObject clone = Instantiate(proch_bulle_4, proch_pos_4[new_equipe], Quaternion.identity);
                
                prochaine_bulle_tab[i] = clone;

            }

            for (int i = 0; i < 4; i++)
            {
                int new_equipe = equipage(i);
                GameObject clone = Instantiate(bulle_4, pos_4[new_equipe], Quaternion.identity);
                //clone.GetComponent<PlayerInput>().SwitchCurrentActionMap(joueurs[i]);
                Script_Client.GetComponent<WebSocketClient_speed_fight_4p>().setScript(clone.GetComponent<bulle_script_4>(), i);
                bulle_tab[i] = clone;


            }


        }

        lancement = true;
    }

    public int trouver_id(GameObject go, GameObject[] tab)
    {

        if (go != null)
        {
            int taille = tab.Length;

            for (int i = 0; i < taille; i++)
            {
                if (go == tab[i])
                {
                    return i;
                }
            }
        }
        else
        {
            int taille = tab.Length;

            for (int i = 0; i < taille; i++)
            {
                if ( tab[i] == null)
                {
                    return i;
                }
            }
        }

        return 0;

        

    }

    public int donne_id(GameObject go)
    {
        return trouver_id(go, bulle_tab);
    }

    public int donne_id_proch(GameObject go)
    {
        return trouver_id(go, prochaine_bulle_tab);
    }

    public bool supp_tab(GameObject bulle_)
    {
        if (nombre_joueurs == 2)
        {
            int emp = trouver_id(bulle_, bulle_tab);
            GameObject clone = Instantiate(bulle, pos[emp], Quaternion.identity);
            //clone.GetComponent<PlayerInput>().SwitchCurrentActionMap(joueurs[emp]);
            Script_Client.GetComponent<WebSocketClient_speed_fight>().setScript(clone.GetComponent<bulle_script>(), emp);
            bulle_tab[emp] = clone;
        }
        else if (nombre_joueurs == 4)
        {
            int emp = trouver_id(bulle_, bulle_tab);
            int new_equipe = equipage(emp);
            GameObject clone = Instantiate(bulle_4, pos_4[new_equipe], Quaternion.identity);
            //clone.GetComponent<PlayerInput>().SwitchCurrentActionMap(joueurs[emp]);
            Script_Client.GetComponent<WebSocketClient_speed_fight_4p>().setScript(clone.GetComponent<bulle_script_4>(), emp);
            bulle_tab[emp] = clone;
        }
        return true;
    }

    public bool supp_tab_proch(GameObject proch_bulle_)
    {
        if (nombre_joueurs == 2)
        {
            int emp = trouver_id(proch_bulle_, prochaine_bulle_tab);
            Destroy(proch_bulle_);

            GameObject clone = Instantiate(proch_bulle, proch_pos[emp], Quaternion.identity);

            prochaine_bulle_tab[emp] = clone;
        }
        else if (nombre_joueurs == 4)
        {
            int emp = trouver_id(proch_bulle_, prochaine_bulle_tab);
            Destroy(proch_bulle_);

            int new_equipe = equipage(emp);

            GameObject clone = Instantiate(proch_bulle_4, proch_pos_4[new_equipe], Quaternion.identity);

            prochaine_bulle_tab[emp] = clone;
        }
        return true;
    }



    public void restartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public string choix_bulle(int j)
    {
        int new_tape = Random.Range(0, 4);
        
        switch (new_tape)
        {
            case 0:

                proch_int[j] = "0 haut";
                return "0 haut";


            case 1:

                proch_int[j] = "1 droite";
                return "1 droite";


            case 2:

                proch_int[j] = "2 bas";
                return "2 bas";


            case 3:

                proch_int[j] = "3 gauche";
                return "3 gauche";


        }

        

        return "error";
    }

    public int equipage(int joueur)
    {
        
        for(int i = 0; i < 2; i++)
        {
            string nom = equipe1[i];
            
            if ((nom[7] - '0')-1 == joueur)
            {
                if (i == 0)
                {
                    return 0;
                }
                else
                {
                    return 1;
                }
                
            }
            
        }
        for (int i = 0; i < 2; i++)
        {
            string nom = equipe2[i];
            
            if ((nom[7] - '0') - 1 == joueur)
            {
                if (i == 0)
                {
                    return 2;
                }
                else
                {
                    return 3;
                }

            }
        }
        return 0;
    }
}

