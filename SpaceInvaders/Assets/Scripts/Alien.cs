using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Alien : MonoBehaviour
{
    public float speed = 10f;
    private new Rigidbody2D rigidbody;
    public Sprite startImg;
    public Sprite altImg;
    public Sprite explosionSprite;

    private SpriteRenderer spriteRenderer;
    

    public float secBeforeSpriteChange = 0.5f;

    public GameObject alienBullets;

    public float minFireRateTime = 1.0f;

    public float maxFireRateTime = 3.0f;

    public float baseFireWaitTime = 3.0f;

    


    // Use this for initialization
    void Start () {
        rigidbody = GetComponent<Rigidbody2D>();
        rigidbody.velocity = new Vector2(1, 0) * speed;

        spriteRenderer = GetComponent<SpriteRenderer>();

        StartCoroutine(ChangeSprite());
        

        baseFireWaitTime += UnityEngine.Random.Range(minFireRateTime, maxFireRateTime);

    }

    //turn opposite direction
    void Turn(int direction)
    {
        Vector2 newVelocity = rigidbody.velocity;
        newVelocity.x = speed * direction;//will be 1 or -1 rt or lft respectively

        rigidbody.velocity = newVelocity;
    }


    //move down after hit wall
    void MoveDown()
    {
        Vector2 position = transform.position;
        position.y -= 1;

        transform.position = position;
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.name == "lftWall")
        {
            Turn(1);
            MoveDown();
        }
        if (col.gameObject.name == "rtWall")
        {
            Turn(-1);
            MoveDown();
        }
        if (col.gameObject.tag == "Bullet")
        {
            SoundManager.Instance.PlayOneShot(SoundManager.Instance.AlienDies);
            Destroy(gameObject);
        }
    }

    public IEnumerator ChangeSprite()
    {
        while (true)
        {
            if (spriteRenderer.sprite == startImg)
            {
                spriteRenderer.sprite = altImg;
                //SoundManager.Instance.PlayOneShot(SoundManager.Instance.AlienBuzz1);
            }
            else
            {
                spriteRenderer.sprite = startImg;
                SoundManager.Instance.PlayOneShot(SoundManager.Instance.AlienBuzz2);
            }
            yield return new WaitForSeconds(secBeforeSpriteChange);
            SoundManager.Instance.PlayOneShot(SoundManager.Instance.AlienBuzz1);
        }
        
    }

    private void FixedUpdate()
    {
        if (Time.time > baseFireWaitTime)
        {
            baseFireWaitTime += UnityEngine.Random.Range(minFireRateTime, maxFireRateTime);
            Instantiate(alienBullets, transform.position, Quaternion.identity);
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            SoundManager.Instance.PlayOneShot(SoundManager.Instance.ShipExplosion);
            col.GetComponent<SpriteRenderer>().sprite = explosionSprite;
            Destroy(gameObject);
            Destroy(col.gameObject, 0.5f);
        }
    }
}
