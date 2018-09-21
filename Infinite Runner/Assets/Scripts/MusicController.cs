using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicController : MonoBehaviour {

    // Use this for initialization
    private void Awake()
    {
        GameObject[] obj = GameObject.FindGameObjectsWithTag("Music");
        if (obj.Length > 1) {
            Destroy(this.gameObject);
        }
        DontDestroyOnLoad(this.gameObject);
    }

}
