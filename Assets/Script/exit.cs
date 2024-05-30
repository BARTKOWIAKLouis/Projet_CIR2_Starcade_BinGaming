using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class exit : MonoBehaviour
{
    public GameObject exitGameObject;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            exitGameObject.SetActive(true);
            //Application.Quit();
        }
    }
    public void OUII()
    {
        Application.Quit();
    }
    public void NONN()
    {
        exitGameObject.SetActive(false);

    }

}
