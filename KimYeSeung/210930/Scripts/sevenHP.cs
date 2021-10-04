using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class sevenHP : MonoBehaviour
{

    public float turnSpeed;
    public playermove Player;
    public GameObject player;
    public AudioClip Dioitem;
    AudioSource audioSource;

    void Awake()
    {
        player = GameObject.Find("player");
        this.audioSource=GetComponent<AudioSource>();

    }

    void PlaySound() {
        audioSource.clip = Dioitem;
        audioSource.Play();
    }
    
    void Update()
    {
        transform.Rotate(Vector3.forward, turnSpeed * Time.deltaTime);
    }

    public void gone(){
        PlaySound();
        transform.position = new Vector3(400,-400,-1); 
        
    }
}
