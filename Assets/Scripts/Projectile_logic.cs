using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile_logic : MonoBehaviour
{

    public float speed = 1;

    public Vector3 movementDirection;

    public float lifetime = 1.0f;

	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        MoveProjectile(2);
        DestroyObject();
    }

    void MoveProjectile(int type)
    {
        if (type == 1)
        {
            transform.Translate(speed, 0, 0);
        }
        else if (type == 2)
        {
            this.GetComponent<Rigidbody2D>().velocity = movementDirection * speed;
        }
    }

    void DestroyObject()
    {
        lifetime -= Time.deltaTime;
        if (lifetime <= 0)
            Destroy(this.gameObject);
    }
}
