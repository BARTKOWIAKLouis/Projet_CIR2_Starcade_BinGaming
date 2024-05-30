using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static System.TimeZoneInfo;
using UnityEngine.SceneManagement;

public class admin_script : MonoBehaviour
{
    public GameObject retour_menu;
    public GameObject play_again;

    public string menu;
    public string play;
    [SerializeField] Animator transition_fondu;
    public float transitionTime = .5f;



    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(";"))
        {
            Debug.Log("m");
            if(retour_menu.TryGetComponent<retour_menu>(out retour_menu menu1))
            {
                
            }
            
        }

        if (Input.GetKeyDown("p"))
        {
            Debug.Log("p");
            LoadLevel(play);
        }

    }


    IEnumerator LoadLevel(string levelnext)
    {
        Debug.Log("pov bug ?");
        transition_fondu.SetTrigger("start");
        yield return new WaitForSeconds(transitionTime);
        SceneManager.LoadScene(levelnext);
        transition_fondu.SetTrigger("end");
        Debug.Log("yes");
    }


}
