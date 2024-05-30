using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Unity.Burst.Intrinsics.X86.Avx;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using System;

public class sortie : MonoBehaviour
{
    public GameObject Script_Client;
    public GameObject spawner;

    public Sprite sprite_sortie;

    public SpawnBulles spawn;
    public progress_script progress;
    public GameObject progress_object;

    public string nouvellescene;
    public bool retour = false;
    [SerializeField] Animator transition_fondu;
    public float transitionTime = 1f;

    private bool do_it;

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
            script1.setScript_winner_2(gameObject.GetComponent<sortie>(), emp);
        }
        if (Script_Client.TryGetComponent<WebSocketClient_speed_fight_4p>(out WebSocketClient_speed_fight_4p script2))
        {
            script2.setScript_winner_2(gameObject.GetComponent<sortie>(), emp);
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (do_it)
        {
            Debug.Log("left mon ga");
            StartCoroutine(LoadLevel(nouvellescene));

        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("enter");
        retour = true;
    }

    IEnumerator LoadLevel(string levelnext)
    {
        transition_fondu.SetTrigger("start");
        yield return new WaitForSeconds(transitionTime);
        SceneManager.LoadScene(levelnext);
        transition_fondu.SetTrigger("end");

    }

    public void left(string context)
    //public void gauche(InputAction.CallbackContext context)
    {
        if (context == "on")
        {
            do_it = true;
            //StartCoroutine(LoadLevel(nouvellescene));
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Debug.Log("exit");
        retour = false;

    }
}

