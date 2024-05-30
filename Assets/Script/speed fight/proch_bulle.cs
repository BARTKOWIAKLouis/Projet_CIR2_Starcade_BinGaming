using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.InputSystem;
using static Unity.Burst.Intrinsics.X86.Avx;

public class proch_bulle : MonoBehaviour
{
    // Start is called before the first frame update


    public int new_tape_i;
    public string tape;
    public SpawnBulles spawn;
    public progress_script progress;
    public int joueur;

    public Sprite[] sprite_tab;

    public bool active;
    public bool fin;

    public Sprite[] sprite_tab_1;
    public Sprite[] sprite_tab_2;
    public Sprite[] sprite_tab_3;
    public Sprite[] sprite_tab_4;


    void Start()
    {


        spawn = GameObject.FindGameObjectWithTag("Spawn").GetComponent<SpawnBulles>();
        progress = GameObject.FindGameObjectWithTag("Progress").GetComponent<progress_script>();

        joueur = spawn.donne_id_proch(gameObject);

        tape = spawn.choix_bulle(joueur);

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

    // Update is called once per frame
    void Update()
    {
        
    }
}
