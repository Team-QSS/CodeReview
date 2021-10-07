using UnityEngine;

public class Player : MonoBehaviour
{
	public int hp;
	public int damage;
	
	void Start()
	{
		hp = 10;
		damage = 2;
	}
	
	public bool RecieveAttack(int damage)
	{
		hp -= damage;
		if (hp <= 0)
		{
			return false;
		}
		return true;
	}
	
	private void Update()
	{
		Transform tf = GetComponent<Transform>();
		
		if (Input.GetKey(KeyCode.W))
		{
			tf.Translate(0, 1, 0);
		}
		if (Input.GetKey(KeyCode.A))
		{
			tf.Translate(-1, 0, 0);
		}
		if (Input.GetKey(KeyCode.S))
		{
			tf.Translate(0, -1, 0);
		}
		if (Input.GetKey(KeyCode.D))
		{
			tf.Translate(1, 0, 0);
		}
	}
	
	private void OnTriggerStay2D(Collider2D col)
	{
		if (col.gameObject.tag == "Player")
		{
			if (Input.GetKeyDown(KeyCode.Space))
			{
				col.GetComponent<Player>().RecieveAttack(damage);
			}
		}
	}
}