using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    [SerializeField]
    private string _nextScene; 

    public void LoadNextScene()
    {
        SceneManager.LoadScene(_nextScene);
    }
}
