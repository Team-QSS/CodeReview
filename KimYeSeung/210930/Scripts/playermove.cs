using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;



public class playermove : MonoBehaviour
{
      public Gamemanager gameManager;
      public canscript canScript;
      public BGM bgm;
      public sevenHP Seven;

      public AudioClip DioJump;
      public AudioClip DioMove;

      AudioSource audioSource;


      public float maxSpeed;
      public float jumpPower;
      public float SecJumpPower;
      public float JumpHap;
      public float NoDamageTime;
      public int JumpCount=2;
      public int check = 1;



      Rigidbody2D rigid;
      SpriteRenderer spriteRenderer;
      Animator anim;
      BoxCollider2D boxcollider;
      int Q = 0;

      float timecheck = 0;



      void Awake() {
      rigid = GetComponent<Rigidbody2D>();
      spriteRenderer = GetComponent<SpriteRenderer>();
      anim = GetComponent<Animator>();
      boxcollider = GetComponent<BoxCollider2D>();
      this.audioSource=GetComponent<AudioSource>();

      }

      void PlaySound(string action) {
            switch(action){
            case "JUMP":
                  audioSource.clip = DioJump;
                  break;
            
            case "MOVE":
                  audioSource.clip = DioMove;
                  break;
            }
            audioSource.Play();

      }

      // void PlayBGM(string action){
      //       switch(action){
      //       case "0":
      //             audioSource.clip = DioJump;
      //             break;
            
      //       case "1":
      //             audioSource.clip = DioMove;
      //             break;
      //       }
      //       audioSource.Play();

      // }

      private void Update() {
            float h = Input.GetAxisRaw("Horizontal");

            if(JumpCount < 0){
                  JumpCount = 0;
            }

            if(rigid.velocity.y < -20){
                  timecheck += 1;
            }


            //점프
            if(JumpCount > 0){
                  if(Input.GetButtonDown("Jump")){
                        PlaySound("JUMP");
                        if(JumpCount == 1){
                              if(rigid.velocity.y < 0){
                                    rigid.AddForce(Vector2.up * JumpHap, ForceMode2D.Impulse);
                              }
                              rigid.AddForce(Vector2.up * SecJumpPower, ForceMode2D.Impulse);
                              anim.SetBool("jump", true);
                        }
                        else{
                              rigid.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
                              anim.SetBool("jump", true);
                        }
                        --JumpCount;
                  }
            }

            //방향전환
            if(Input.GetButton("Horizontal")){
                  spriteRenderer.flipX = Input.GetAxisRaw("Horizontal") == -1;        
            }

            //움직임 애니메이션
            if(h>0||h<0){
                  anim.SetBool("moving", true);
            } 
            else{
            anim.SetBool("moving", false);

            } 


            //게임 재시작
            if(Input.GetKeyDown(KeyCode.R)){
                  SceneManager.LoadScene(0);
            }



      }



      void FixedUpdate() {
            float h = Input.GetAxisRaw("Horizontal");
            //Move By Key Control

            if(check == 1){
                  
                  rigid.AddForce(Vector2.right * h, ForceMode2D.Impulse);

                  if(rigid.velocity.x > maxSpeed){
                        rigid.velocity = new Vector2(maxSpeed, rigid.velocity.y);
                  }
                  else if(rigid.velocity.x < maxSpeed*(-1)){
                        rigid.velocity = new Vector2(maxSpeed*(-1), rigid.velocity.y);
                  }
            }


            //Landing Platform
            if(rigid.velocity.y < -1){
                  Vector2 downVec = new Vector2(rigid.position.x , rigid.position.y - 0.5f);
                  Debug.DrawRay(downVec, Vector3.down, new Color(0, 1, 0));
                  RaycastHit2D rayHit = Physics2D.Raycast(downVec, Vector3.down, 1, LayerMask.GetMask("Platform"));
                  if(rayHit.collider != null){
                        anim.SetBool("jump", false);
                        timecheck = 0;
                  }        
            }
            if(rigid.velocity.y < -1){
                  Vector2 downVec = new Vector2(rigid.position.x , rigid.position.y - 0.5f);
                  Debug.DrawRay(downVec, Vector3.down * 2, new Color(0, 1, 0));
                  RaycastHit2D rayHit = Physics2D.Raycast(downVec, Vector3.down, 1, LayerMask.GetMask("Platform"));
                  if(rayHit.collider == null && timecheck > 25){
                        check = 0;
                        Debug.Log("움직임이 멈춤");
                  }         
            }
      }
      void OnCollisionEnter2D(Collision2D collision){

             //hit
            if(collision.gameObject.tag == "Enemy"){
                  timecheck = 0;
                  check = 1;
                  //공격
                  if(transform.position.y > collision.transform.position.y && rigid.velocity.y > 0){
                        JumpCount =0;
                        OnAttack(collision.transform);

                  }
                  //맞음
                  else{
                        OnDamaged(collision.transform.position);
                  }
            }

            //더블점프 카운트
            //평평한 땅에 닿음
            if(collision.gameObject.tag == "Ground"){
                  JumpCount = 2;
                  check = 1;
                  timecheck = 0;

            }
            //기울어진 땅에 닿음
            else if(collision.gameObject.tag == "rldnf"){
                  JumpCount = 2;
                  timecheck = 0;
            }
      }

      void OnTriggerEnter2D(Collider2D collider){
            
            //캔(HP 회복) 줍줍
            if(collider.gameObject.tag == "can"){
                  anim.SetTrigger("pick");
                  PickUpCan();
                  PointUp(300);
            }
            //도착
            else if(collider.gameObject.tag == "Finish"){
                  gameManager.NextStage();
                  Debug.Log("anj");
            }
            //목숨 7개 아이템 줍줍
            else if(collider.gameObject.tag == "seven"){
                  anim.SetTrigger("pick");
                  Seven.gone();
                  gameManager.UpHealth();
                  PointUp(500);
            
            }

            //에리어에 따라 bgm이 달라짐
            if(collider.gameObject.tag == "MS"){
                  if(rigid.velocity.y < 0){
                        bgm.Ms1();
                  }
                  else{
                        bgm.Ms2();
                  }
            }

            if(collider.gameObject.tag == "MS2"){
                  if(rigid.velocity.y < 0){
                        bgm.Ms2();
                  }
                  else{
                        bgm.Ms3();
                  }
            }


      }
      void PickUpCan(){
            canScript.gone();
            gameManager.HealthUp();        
      }

      void OnAttack(Transform mouse){
            rigid.AddForce(Vector2.up *10 , ForceMode2D.Impulse);
            mousemove MouseMove = mouse.GetComponent<mousemove>();
            MouseMove.OnDamaged();
            PointUp(100);
      }

      void OnDamaged(Vector2 targetPos){

            //Health Down
            gameManager.HealthDown();

            //무적
            gameObject.layer = 10;

            //적을 밝앗을때 튕기기
            int dirc = transform.position.x-targetPos.x > 0 ? 1 : -1;
            rigid.AddForce(new Vector2(dirc,1)*10, ForceMode2D.Impulse);

            //맞는 애니메이션 
            anim.SetTrigger("hit");

            spriteRenderer.color = new Color(1,1,1,0.4f);

            //무적시간
            Invoke("OffDamaged", NoDamageTime);
      }

      void OffDamaged(){
            gameObject.layer = 6;
            spriteRenderer.color = new Color(1,1,1,1);
            if(Q==1){
            spriteRenderer.color = new Color(1,1,1,0.4f);
            Q=0;
            }
      }

      public void Ondie(){

            Q = 1;


            spriteRenderer.flipY = true;

            boxcollider.enabled = false;

            rigid.AddForce(Vector2.up * 5, ForceMode2D.Impulse);
            spriteRenderer.color = new Color(1,1,1,0.4f);
      }

      public void VeloccityZero(){
            rigid.velocity = Vector2.zero;
      }

      void PointUp(int changeValue){
            gameManager.stagePoint += changeValue;
      }


}
