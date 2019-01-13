using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player_Control : MonoBehaviour
{

    public float xMoving = 1;
    public float yMoving = 1;
    public float zMoving = 1;

    public int movementType = 1;

    public bool upEnabled = true;
    public bool downEnabled = true;
    public bool leftEnabled = true;
    public bool rightEnabled = true;

    public float upBorder = 40;
    public float downBorder = -40;
    public float leftBorder = -40;
    public float rightBorder = 40;

    public GameObject projectile;
    public float shootingSpeed = 0.5f;
    public bool enableShooting = true;

    public float projectilePosY = 1.0f;
    public float projectilePosX = 1.0f;

    public float playerSpeed = 2000.0f;

    public int mouseControlType = 1;

    private float shootingTimer;

    // Use this for initialization
    void Start()
    {
        //StartCoroutine(ProjectileShootTimer());
    }

    // Update is called once per frame
    void Update()
    {
        MoveToMousePosition_ver2();
        MouseControl(3);
    }

    void WASD_constrol(int Type)
    {
        if (Type == 1) //old school
        {
            if (Input.GetKeyDown(KeyCode.W) && upEnabled && transform.position.y <= upBorder)
            {
                transform.Translate(0, yMoving, 0);
            }
            if (Input.GetKeyDown(KeyCode.S) && downEnabled && transform.position.y >= downBorder)
            {
                transform.Translate(0, -yMoving, 0);
            }
            if (Input.GetKeyDown(KeyCode.D) && rightEnabled && transform.position.x <= rightBorder)
            {
                transform.Translate(xMoving, 0, 0);
            }
            if (Input.GetKeyDown(KeyCode.A) && leftEnabled && transform.position.x >= leftBorder)
            {
                transform.Translate(-xMoving, 0, 0);
            }
        }
        else if (Type == 2) // smooth
        {
            if (Input.GetKey(KeyCode.W) && upEnabled && transform.position.y <= upBorder)
            {
                transform.Translate(0, yMoving, 0);
            }
            if (Input.GetKey(KeyCode.S) && downEnabled && transform.position.y >= downBorder)
            {
                transform.Translate(0, -yMoving, 0);
            }
            if (Input.GetKey(KeyCode.D) && rightEnabled && transform.position.x <= rightBorder)
            {
                transform.Translate(xMoving, 0, 0);
            }
            if (Input.GetKey(KeyCode.A) && leftEnabled && transform.position.x >= leftBorder)
            {
                transform.Translate(-xMoving, 0, 0);
            }
            if (Input.GetKeyDown(KeyCode.Space))
            {
                SpawnProjectile(1);
            }
        }
    }

    void MouseControl(int mouseControlType)
    {
        if (mouseControlType == 1)
        {
            if (Input.GetMouseButtonDown(button: 0))
            {
                SpawnProjectile(2);
            }
        }
        else if (mouseControlType == 2)
        {
            if (Input.GetMouseButtonDown(button: 0))
            {
                transform.Translate(xMoving, 0, 0);
            }
            if (Input.GetMouseButtonDown(button: 1))
            {
                transform.Translate(-xMoving, 0, 0);
            }
        }
        if (mouseControlType == 3)
        {
            shootingTimer -= Time.deltaTime;
            if (Input.GetMouseButtonDown(button: 0) && shootingTimer <= 0)
            {
                shootingTimer = shootingSpeed;
                SpawnProjectile(3);
            }
        }
    }

    IEnumerator ProjectileShootTimer()
    {
        while (enableShooting)
        {
            SpawnProjectile(1);
            yield return new WaitForSeconds(shootingSpeed);
        }
    }

    void SpawnProjectile(int projectileType)
    {
        if (projectileType == 1)
        {
            Instantiate(projectile, new Vector3(transform.position.x + projectilePosX, transform.position.y + projectilePosY, 0), Quaternion.identity);
        }
        else if (projectileType == 2)
        {
            GameObject projectileClone;
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            projectileClone = Instantiate(projectile, transform.TransformPoint(Vector3.left + new Vector3(projectilePosX, projectilePosY, 0)), Quaternion.Euler(0,0,transform.eulerAngles.z)) as GameObject;
            Rigidbody2D projectileRigidbody = projectileClone.GetComponent<Rigidbody2D>();
            projectileRigidbody.velocity = mousePosition.normalized * 100.0f;
            projectileRigidbody.gravityScale = 0;
        }
        else if (projectileType == 3)
        {
            Vector2[] directions = new Vector2[] {Vector2.up, Vector2.down, Vector2.left, Vector2.right};

            foreach (Vector2 direction in directions)
            {
                GameObject projectileObject = Instantiate(projectile);
                projectileObject.transform.position = new Vector3(this.transform.position.x + direction.x, this.transform.position.y + direction.y, 0);
                //projectileObject.transform.position = this.transform.position;

                Projectile_logic bullet = projectileObject.GetComponent<Projectile_logic>();
                bullet.movementDirection = direction;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D tempCollision)
    {
        if (tempCollision.gameObject.tag == "Enemy")
        {
            Destroy(this.gameObject);
            ScoreLogic Score = GameObject.FindGameObjectWithTag("GUI").GetComponent<ScoreLogic>();
            Score.GameOver();
            //SceneManager.LoadScene("Scene_01");
        }
    }

    void MoveToMousePosition()
    {
        var tempMousePosition = Input.mousePosition;
        tempMousePosition.z = transform.position.z - Camera.main.transform.position.z;
        tempMousePosition = Camera.main.ScreenToWorldPoint(tempMousePosition);
        transform.position = Vector3.MoveTowards(transform.position, tempMousePosition, playerSpeed * Time.deltaTime);
    }

    void MoveToMousePosition_ver2()
    {
        Vector2 mousePosition = Input.mousePosition;
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(mousePosition);
        this.transform.position = new Vector3(worldPosition.x, worldPosition.y, this.transform.position.z);
    }

    void RotateToMousePosition()
    {
        var tempMousePosition = Input.mousePosition;
        Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(tempMousePosition + Vector3.forward * 10.0f);
        float angle = AngleBetweenPoints(transform.position, mouseWorldPosition);
        transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, angle - 30));
    }

    float AngleBetweenPoints(Vector2 a, Vector2 b)
    {
        return Mathf.Atan2(a.y - b.y, a.x - b.x) * Mathf.Rad2Deg;
    }
}
        
