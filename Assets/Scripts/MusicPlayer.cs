using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    void Awake()
    {
        // singleton pattern says that we will just have one instance of MusicPlayer class 
        int numMusicPlayers = FindObjectsOfType<MusicPlayer>().Length;
        // if new one is 
        if (numMusicPlayers > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            // we dont destroy first game object so this class and another ones created is destroyed
            DontDestroyOnLoad(gameObject);
        }
    }
}
