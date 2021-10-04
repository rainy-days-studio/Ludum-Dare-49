using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMusic : MonoBehaviour
{
    // Keep this object persisting between scenes
    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }
}
