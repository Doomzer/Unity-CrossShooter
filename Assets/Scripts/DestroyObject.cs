using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyObject : MonoBehaviour {

    ScoreLogic Score;

    public GameObject particles;

    // Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    private void OnCollisionEnter2D(Collision2D tempCollision)
    {
        if(this.gameObject.tag == "Projectile" && tempCollision.gameObject.tag == "Enemy")
        {
            SpawnParticles(tempCollision.transform.position);
            Destroy(tempCollision.gameObject);
            Destroy(this.gameObject);
            Score = GameObject.FindGameObjectWithTag("GUI").GetComponent<ScoreLogic>();
            if (Score != null && Score.scoreType == 1)
                Score.AddToScore();
        }
    }

    void SpawnParticles(Vector2 tempPosition)
    {
        Instantiate(particles,tempPosition,Quaternion.identity);
    }
}
