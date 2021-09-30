using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class Gamemanager : MonoBehaviour
{
    public int totalPoint;
    public int stagePoint;
    public int stageIndex;
    public int health;
    public int HpPoint;

    public playermove player;
    public Image[] UIHp;
    public Text UIPoint;
    public GameObject UIRestartBtn;

    public GameObject UIContinueBtn;
    public GameObject UIEndBtn;
    public GameObject Menu;
    public GameObject Tutorial;
    public GameObject MainStage;


    void Awake(){
        health =3;
        UIHp[3].color = new Color(0,0,0,0);
        UIHp[4].color = new Color(0,0,0,0);
        UIHp[5].color = new Color(0,0,0,0);
        UIHp[6].color = new Color(0,0,0,0);
        health=HpPoint;
    }

    void Update() {
        //점수 표시
        UIPoint.text = (totalPoint + stagePoint).ToString();
        
        //메뉴
        if(Input.GetButtonDown("Cancel"))
        {
            if(Menu.activeSelf)
            {
                Menu.SetActive(false);
            }
            else
            {
                Menu.SetActive(true);
            }
            
        }
    }


    
    
    public void NextStage(){
            Tutorial.SetActive(false);
            stageIndex++;
            MainStage.SetActive(true);
            PlayerRespawn();
        

        totalPoint += stagePoint;
        stagePoint = 0;
    }


    //목숨 7개 아이템 먹음
    public void UpHealth(){
        HpPoint =7;
        health=HpPoint;
        UIHp[0].color = new Color(1,1,1,1);
        UIHp[1].color = new Color(1,1,1,1);
        UIHp[2].color = new Color(1,1,1,1);
        UIHp[3].color = new Color(1,1,1,1);
        UIHp[4].color = new Color(1,1,1,1);
        UIHp[5].color = new Color(1,1,1,1);
        UIHp[6].color = new Color(1,1,1,1);
        
    }

    public void HealthDown(){
        if(health > 1){
            --health;
            UIHp[health].color = new Color(1,0,0,0.4f);
        }
        else{
            //플레이어 죽음
            --health;
            UIHp[health].color = new Color(1,0,0,0.4f);
            player.Ondie();
            //Result UI
            Debug.Log("UI 죽음 표시");
            //Retry Button UI
            UIRestartBtn.SetActive(true);
            Text btnText = UIRestartBtn.GetComponentInChildren<Text>();
        }
    }

    //HP 회복 아이템 먹음
    public void HealthUp(){
            health = HpPoint;
            UIHp[0].color = new Color(1,1,1,1);
            UIHp[1].color = new Color(1,1,1,1);
            UIHp[2].color = new Color(1,1,1,1);
            if(HpPoint==7){
                UIHp[3].color = new Color(1,1,1,1);
                UIHp[4].color = new Color(1,1,1,1);
                UIHp[5].color = new Color(1,1,1,1);
                UIHp[6].color = new Color(1,1,1,1);
            }
            
    }

    public void Restart(){
        SceneManager.LoadScene(0);
    }

    void OnTriggerEnter2D(Collider2D collision) {
        //플레이어 떨어짐
        if(collision.gameObject.tag == "Player"||collision.gameObject.tag =="playerdameged"){
            HealthDown();
            if(health >= 1){
                collision.attachedRigidbody.velocity = Vector2.zero;
                collision.transform.position = new Vector3(-5,6,0);
            }
            
        }

        //player Reposition
        
    }
    public void GameExit()
    {
        Application.Quit();
    }

    void PlayerRespawn(){
        player.transform.position = new Vector3(-5,6,0);
        player.VeloccityZero();
    }
}
