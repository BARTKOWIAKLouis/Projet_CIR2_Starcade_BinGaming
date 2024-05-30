using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class retour_menu : MonoBehaviour
{
    // Start is called before the first frame update
    public string nouvellescene;
    public bool retour = false; 
    [SerializeField] Animator transition_fondu;
    public float transitionTime = 1f;

    private bool enter;

    // Start is called before the first frame update
    void Start()
    {

    }

    private void Update()
    {
        if (enter)
        {
            StartCoroutine(LoadLevel(nouvellescene));
        }
    }


    // Update is called once per frame

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

    public void fonction_Act1(string context)
    //public void entrer_planete(InputAction.CallbackContext context)
    {
        if (context == "on" && retour == true)
        {
            enter = true;
            //StartCoroutine(LoadLevel(nouvellescene));
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Debug.Log("exit");
        retour = false;

    }
}
