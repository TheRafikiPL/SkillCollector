using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CameraController : MonoBehaviour
{
    Vector3 distFromPlayer;
    public GameObject player;
    void Start()
    {
        distFromPlayer = player.transform.position - transform.position;
    }
    
    void Update()
    {
        transform.position = player.transform.position - distFromPlayer;
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene(0);
            AudioController.instance.PlayMenuBackMusic();
        }
    }
}
