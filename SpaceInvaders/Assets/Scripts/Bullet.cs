using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bullet : MonoBehaviour
{
    public float speed = 30f;

    private Rigidbody2D rigidBody;

    public Sprite explodedAlienImage;

	// Use this for initialization
	void Start () {
        rigidBody = GetComponent<Rigidbody2D>();
        rigidBody.velocity = Vector2.up * speed;
	}

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Wall")
        {
            Destroy(gameObject);//gameObject is always a reference to whatever the script is attached
        }
        if (col.tag == "Alien")
        {
            SoundManager.Instance.PlayOneShot(SoundManager.Instance.AlienDies);
            IncreaseTextUIScore();
            col.GetComponent<SpriteRenderer>().sprite = explodedAlienImage;

            Destroy(gameObject);

            Destroy(col.gameObject, 0.5f);

        }
        if (col.tag == "Shield")
        {
            Destroy(gameObject);
            Destroy(col.gameObject);
        }
    }

    void IncreaseTextUIScore()
    {
        var textUIcomp = GameObject.Find("Score").GetComponent<Text>();
        int score = int.Parse(textUIcomp.text);
        score += 10;
        textUIcomp.text = score.ToString();
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
