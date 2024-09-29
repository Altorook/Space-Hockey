using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    public string singleplayerSceneName;
    public string multiplayerSceneName;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Start1P()
    {
        SceneManager.LoadScene(singleplayerSceneName);
    } 
    public void Start2P() 
    {
        SceneManager.LoadScene(multiplayerSceneName);
    }
}

