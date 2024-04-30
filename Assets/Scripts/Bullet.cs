using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    AudioSource _bullet;

    void Start()
    {
        _bullet = GetComponent<AudioSource>();
    }

    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Road"))
        {
            _bullet.Play();
        }

        if (!_bullet.isPlaying)
        {
            Destroy(gameObject,2f);
        }


    }
}
