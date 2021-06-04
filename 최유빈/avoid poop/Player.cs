using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class Player : MonoBehaviour
{
	public int Health;
	public int MaxHealth;
	public int Speed;

	void Start()
	{
		MaxHealth = 3;
		Health = MaxHealth;
	}
	
	
	void Update()
	{
		
            
		if (Health == 0)
		{
			SceneManager.LoadScene("End");
		}


		if (Input.GetKey(KeyCode.A))
		{
			transform.Translate(Vector3.left * Speed * Time.deltaTime);
		}

		if (Input.GetKey(KeyCode.D))
		{
			transform.Translate(Vector3.right * Speed * Time.deltaTime);
		}

		if (Input.GetKeyDown(KeyCode.Escape))
		{	
			Application.Quit();
		}

	}
}
