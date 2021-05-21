using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Run : MonoBehaviour
{
    private float GRAVITY = 9.8f;
    
        private float _mVelocity = 0f;
        
        void Start()
        {
        }
        
        void Update()
        {
        
            var transform1 = this.transform;
            Vector3 current = transform1.position;

            _mVelocity += GRAVITY * Time.deltaTime;

            current.y -= _mVelocity * Time.deltaTime;
            transform1.position = current;
            
        }
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Player"))
            {
                Debug.Log($"{collision.name}");
                SceneManager.LoadScene("End");
            }
            Destroy(this.gameObject);
        }
}
