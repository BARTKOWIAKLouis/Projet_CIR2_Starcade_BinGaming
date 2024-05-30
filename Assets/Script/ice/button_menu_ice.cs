using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;


public class button_menu : MonoBehaviour

{
    public string nouvellescene;
    [SerializeField] Animator transition_fondu;
    public bool menu = false;
    public float transitionTime = 1f;
    public GameObject logic_manager;

   
    void Start()
    {
        logic_manager = GameObject.FindGameObjectWithTag("logic_manager_tag");
    }

    public void OnTriggerEnter2D(Collider2D other)
    {

        StartCoroutine(LoadLevel(nouvellescene));
    }
    public void OnTriggerExit2D(Collider2D other)
    {
        

        menu = false;
    }

      
   
    IEnumerator LoadLevel(string levelnext)
    {
        transition_fondu.SetTrigger("start");
        yield return new WaitForSeconds(transitionTime);
        SceneManager.LoadScene(levelnext);
        transition_fondu.SetTrigger("end");

    }


}

