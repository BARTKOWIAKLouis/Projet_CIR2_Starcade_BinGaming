using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using static System.TimeZoneInfo;

public class button_menu_script_tron: MonoBehaviour
{
    public string nouvellescene;
    [SerializeField] Animator transition_fondu;
    public float transitionTime = 1f;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame

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
        StartCoroutine(LoadLevel(nouvellescene));

    }


    private void OnTriggerExit2D(Collider2D collision)
    {
        Debug.Log("exit");
        


    }
}
