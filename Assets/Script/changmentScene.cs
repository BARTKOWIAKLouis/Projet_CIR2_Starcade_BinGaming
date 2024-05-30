using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class changmentScene : MonoBehaviour
{

    public string nouvellescene;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    public void changeScene()
    {
        SceneManager.LoadScene(nouvellescene);
    }
}
