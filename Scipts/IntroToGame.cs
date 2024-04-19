using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class IntroToGame : MonoBehaviour
{   
    private VideoPlayer player;
    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<VideoPlayer>();
        player.Play();
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyUp(KeyCode.Escape)) {
            changeScreen();
        }
        else if(player.isPaused)
        {
            changeScreen();
        }
    }
   void changeScreen()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
    }
}
