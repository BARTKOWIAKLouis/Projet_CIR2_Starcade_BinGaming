using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class transition : MonoBehaviour
{

    [SerializeField] Animator transition_fondu;
    public float transitionTime = 1f;
    public string nouvellescene;



    // Update is called once per frame
   

    public void belle_transition(InputAction.CallbackContext context)
    {
        Debug.Log("try_enter");
        if (context.started)
        {
            LoadNextLevel();

        }
    }

    public void LoadNextLevel()
    {
        StartCoroutine(LoadLevel(nouvellescene));

    }

    IEnumerator LoadLevel(string levelnext)
    {
        transition_fondu.SetTrigger("start");
        yield return new WaitForSeconds(transitionTime);
        SceneManager.LoadScene(levelnext);
        transition_fondu.SetTrigger("end");

    }
}
