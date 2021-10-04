using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class mousemove : MonoBehaviour
{
    public float movePower = 1f;
    public int nextMove;
    public float StandBy;
    Animator anim;
    Vector3 movement;
    Rigidbody2D rigid;
    SpriteRenderer spriteRenderer;
    BoxCollider2D boxcollider;


    void Awake()
    {
        gameObject.SetActive(false);
        rigid = GetComponent<Rigidbody2D>();
        Invoke("Think", 5);
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        boxcollider = GetComponent<BoxCollider2D>();
        Invoke("start",StandBy);
    }

    void Update() 
    {
        float h = nextMove;
        
        //움직임 애니메이션
        if(h>0||h<0){
            anim.SetBool("moving", true);
        } 
        else{
            anim.SetBool("moving", false);
        } 

        
        
    }


    void FixedUpdate()
    {
        rigid.velocity = new Vector2(nextMove*movePower, rigid.velocity.y);
        
        //낭떠러지 감지
        Vector2 frontVec = new Vector2(rigid.position.x + nextMove*0.5f , rigid.position.y - 0.5f);
        Debug.DrawRay(frontVec, Vector3.down, new Color(0, 1, 0));
        RaycastHit2D rayHit = Physics2D.Raycast(frontVec, Vector3.down, 1, LayerMask.GetMask("Platform"));
        if(rayHit.collider == null){
            Turn();
        }
    }

    void OnCollisionEnter2D(Collision2D collision){
        if(collision.gameObject.tag == "Enemy"){
            Turn();
        }
    }


    //재귀 함수
    void Think(){
        nextMove = Random.Range(-1, 2);
        
        float nextThinkTime = Random.Range(1f,3f);

        Invoke("Think", nextThinkTime);

        //방향
        if(nextMove != 0){
            spriteRenderer.flipX = nextMove == 1;
        }
        
    }

    void Turn(){
        nextMove = nextMove * -1;
        spriteRenderer.flipX = nextMove == 1;

        if(spriteRenderer.flipY != true){
            CancelInvoke();
        }
        
        Invoke("Think", 2);
    }

    public void OnDamaged(){
        //죽음
        Debug.Log("전달");

        spriteRenderer.color = new Color(1,1,1,0.4f);

        spriteRenderer.flipY = true;

        boxcollider.enabled = false;

        rigid.AddForce(Vector2.up * 5, ForceMode2D.Impulse);

        Invoke("DeActive", 3);
    }

    void DeActive(){
        gameObject.SetActive(false);
    }

    void start(){
        gameObject.SetActive(true);
    }

    
}
