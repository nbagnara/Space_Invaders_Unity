using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceShip : MonoBehaviour
{
    
    public float speed = 30f;
    public GameObject theBullet;

    private void FixedUpdate()
    {
        float horzMvmnt = Input.GetAxisRaw("Horizontal");
        GetComponent<Rigidbody2D>().velocity = new Vector2(horzMvmnt, 0) * speed;
    }

    
	
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetButtonDown("Jump"))
        {
            Instantiate(theBullet, transform.position, Quaternion.identity);
            SoundManager.Instance.PlayOneShot(SoundManager.Instance.BulletFire);
        }
	}
}
