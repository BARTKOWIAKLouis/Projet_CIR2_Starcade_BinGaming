using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class planet0Script : MonoBehaviour
{
    public ZoomCam zoo;
    private Collider2D collider2D;
    public string nouvellescene;
    public bool planete= false;
    [SerializeField] Animator transition_fondu;
    public float transitionTime = 1f;


    private bool enter;
    

   

  

    // Start is called before the first frame update
    void Start()
    {

        // Get the Collider2D component attached to this GameObject
        collider2D = GetComponent<Collider2D>();

        // Calculate the bounds of the collider
        Bounds bounds = collider2D.bounds;

        // Print the bounds
        Debug.Log("Collider2D Limits:");
        Debug.Log("xmin: " + bounds.min.x);
        Debug.Log("xmax: " + bounds.max.x);
        Debug.Log("ymin: " + bounds.min.y);
        Debug.Log("ymax: " + bounds.max.y);
        zoo = GameObject.FindGameObjectWithTag("zoom").GetComponent<ZoomCam>();
    }


    // Update is called once per frame
    private void Update()
    {
        if (enter)
        {
            Debug.Log("lance planète");
            StartCoroutine(LoadLevel(nouvellescene));
        }

    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("enter");
        planete = true;
        
        zoo.ZoomIn();
    }


    public void fonction_Act1(string context)
    //public void essaye_entrer_planete(InputAction.CallbackContext context)
    {
        Debug.Log("try_enter");
        if (context == "on" && planete == true)
        {

            enter = true;
            
            
        }
    }

    IEnumerator LoadLevel(string levelnext)
    {
        transition_fondu.SetTrigger("start");
        yield return new WaitForSeconds(transitionTime);
        SceneManager.LoadScene(levelnext);
        transition_fondu.SetTrigger("end");

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Debug.Log("exit");
        planete = false;
        
        zoo.ZoomOut();
    }
}

