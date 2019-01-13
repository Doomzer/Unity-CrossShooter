using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLogic : MonoBehaviour
{

    public float enemySpeed = 0.5f;

    public Vector3 movementDirection;

	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        MoveToDirection();
    }

    void MoveToDirection()
    {
        this.GetComponent<Rigidbody2D>().velocity = movementDirection * enemySpeed;
    }

    void MoveDown()
    {
        transform.Translate(0,-enemySpeed,0);
    }
    void MoveLeft()
    {
        transform.Translate(-enemySpeed, 0, 0);
    }

    void MoveToPlayer()
    {
        transform.position = Vector3.Slerp(transform.position, new Vector3(0,0,0), enemySpeed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision.name);
        if (collision.gameObject.GetComponent<DestroyObject>() == null)
        {
            if (collision.gameObject.GetComponent<Projectile_logic>() != null)
            {
                Destroy(this.gameObject);
                Destroy(collision.gameObject);
            }
            if (collision.gameObject.GetComponent<Player_Control>() != null)
            {
                Destroy(collision.gameObject);
            }
        }
    }
}
