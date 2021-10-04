using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BGM : MonoBehaviour
{
    public AudioClip BGM1;
    public AudioClip BGM2;
    public AudioClip BGM3;
    AudioSource audioSource;
    public playermove Player;
    public GameObject player;

    Rigidbody2D rigid;
    
    void Awake() {
        player = GameObject.Find("player");
        this.audioSource=GetComponent<AudioSource>();
        rigid = GetComponent<Rigidbody2D>();
    }
    void Start(){
        Ms1();
    }

    void PlaySound(string action) {
        switch(action){
            case "0":
                audioSource.clip = BGM1;
                break;
            
            case "1":
                audioSource.clip = BGM2;
                break;

            case "2":
                audioSource.clip = BGM3;
                break;
        }
        audioSource.Play();
        
    }

    public void Ms1(){
        PlaySound("0");
    }

    public void Ms2(){
        PlaySound("1");
    }

    public void Ms3(){
        PlaySound("2");
    }

}

