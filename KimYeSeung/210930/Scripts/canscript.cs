using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class canscript : MonoBehaviour

{
    Animator anim;
    Vector3 movement;
    Rigidbody2D rigid;
    SpriteRenderer spriteRenderer;
    BoxCollider2D boxcollider;
    public GameObject player;

    public AudioClip DioItem;
    AudioSource audioSource;

    float canMoveX = -150;
    float canMoveY = 84;
    


    void EatCan(){
            canMoveX = -90;
            canMoveY = 260;
    }

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        boxcollider = GetComponent<BoxCollider2D>();
        player = GameObject.Find("player");
        this.audioSource=GetComponent<AudioSource>();
    }

    void PlaySound(string action) {
        switch(action){
            case "ITEM":
                audioSource.clip = DioItem;
                break;
        }
        audioSource.Play();
        
    }

    public void gone(){
        EatCan();
        PlaySound("ITEM");
    }

    void OnTriggerEnter2D(Collider2D collision) {
        //플레이어 떨어짐
        transform.position = new Vector3(canMoveX,canMoveY,-1); 
        
    }
}

    