using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Preload : MonoBehaviour
{
    // Load main scene
    void Start()
    {
        SceneManager.LoadScene("Main", LoadSceneMode.Single);
    }

}
