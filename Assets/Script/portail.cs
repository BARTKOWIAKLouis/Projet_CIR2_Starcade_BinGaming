using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using static System.TimeZoneInfo;

public class portail : MonoBehaviour
{
    public string nouvellescene;
    public bool accept=false;
    [SerializeField] Animator transition_fondu;
    public float transitionTime = 1f;

    private bool enter;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    private void Update()
    {
        if (enter)
        {
            StartCoroutine(LoadLevel(nouvellescene));
        }
    }


    IEnumerator LoadLevel(string levelnext)
    {
        transition_fondu.SetTrigger("start");
        yield return new WaitForSeconds(transitionTime);
        SceneManager.LoadScene(levelnext);
        transition_fondu.SetTrigger("end");

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("enter");
        accept = true;
    }

    

    public void fonction_Act1(string context)
    // public void entrer_planete(InputAction.CallbackContext context)
    {
        if (context == "on" && accept == true)
        {
            enter = true;
            //StartCoroutine(LoadLevel(nouvellescene));
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Debug.Log("exit");
        accept = false;

        
    }
}
