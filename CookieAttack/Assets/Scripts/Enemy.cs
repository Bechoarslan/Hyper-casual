using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] GameObject enemyCookie;
    [SerializeField] GameObject enemyDonut;
    [SerializeField] GameObject enemyChef;
    [SerializeField] GameObject gameManager;
    [SerializeField] GameObject hitFX;
    [SerializeField] GameObject dieFX;
    
    [SerializeField] float speed;
    public bool isTripled = false;
    public bool isFrozen = false;
    public int health = 3;
    float startVeloX;
    float startVeloY;
    Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        if (GetComponent<Chef>())
        {
            StartCoroutine("SpawnEnemy");
        }
        gameManager = FindObjectOfType<GameManager>().gameObject;
        gameManager.GetComponent<GameManager>().numEnemies++;
        rb = GetComponent<Rigidbody2D>();
        transform.LookAt(new Vector2(0, 0));
        rb.velocity = transform.forward * speed * Random.Range(0.8f, 1f);
        startVeloX = rb.velocity.x;
        startVeloY = rb.velocity.y;
    }

    // Update is called once per frame
    void Update()
    {
        if (gameManager.GetComponent<GameManager>().isDead)
        {
            StopCoroutine("SpawnEnemy");
        }
        
        if (gameManager.GetComponent<GameManager>().isLaserOn)
        {
            GetComponent<CircleCollider2D>().isTrigger = true;
        }
        else if (gameManager.GetComponent<GameManager>().isLaserOn == false)
        {
            GetComponent<CircleCollider2D>().isTrigger = false;
        }
        if (rb.velocity != new Vector2(startVeloX,startVeloY) && isFrozen == false)
        {
            rb.velocity = new Vector2(startVeloX, startVeloY);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<Bullet>())
        {
            Instantiate(dieFX, collision.transform.position, Quaternion.identity);
            gameManager.GetComponent<GameManager>().numEnemies--;
            Destroy(gameObject);
        }
        if (collision.gameObject.GetComponent<CFX_AutoDestructShuriken>())
        {
            print("diedinexplosion");
            gameManager.GetComponent<GameManager>().numEnemies--;
            Destroy(gameObject);
        }
    }

    IEnumerator SpawnEnemy()
    {
        while (true)
        {
            yield return new WaitForSeconds(10f);
            int rnd = Random.Range(0, 3);
            switch (rnd)
            {
                case 0:
                    Instantiate(enemyDonut, new Vector2(transform.position.x + rnd/10, transform.position.y + rnd/10), Quaternion.identity);
                    break;
                case 1:
                    Instantiate(enemyCookie, new Vector2(transform.position.x + rnd / 10, transform.position.y + rnd / 10), Quaternion.identity);
                    break;
                case 2:
                    Instantiate(enemyDonut, new Vector2(transform.position.x - rnd / 10, transform.position.y - rnd / 10), Quaternion.identity);
                    break;
                default:
                    break;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<Player>())
        {
            gameManager.GetComponent<GameManager>().Die();
        }
        if (collision.gameObject.GetComponent<Bullet>())
        {
            health--;
            Instantiate(hitFX, collision.transform.position, Quaternion.identity);
            //gameManager.GetComponent<GameManager>().HitSound();
        }
        if (collision.gameObject.GetComponent<Bullet>() && GetComponent<Cookie>() && health <= 0)
        {
            collision.gameObject.GetComponent<Bullet>().hitCookie = true;
            if (isTripled == false && health <= 0)
            {
                Instantiate(dieFX, collision.transform.position, Quaternion.identity);
                print("tripled");
                GameObject c1 = Instantiate(enemyCookie, transform.position, Quaternion.identity);
                c1.transform.localScale = new Vector3(c1.transform.localScale.x / 2, c1.transform.localScale.y / 2, c1.transform.localScale.z / 2);
                c1.GetComponent<Enemy>().isTripled = true;
                c1.transform.LookAt(new Vector2(0, 0));
                c1.GetComponent<Rigidbody2D>().velocity = transform.forward * speed;
                GameObject c2 = Instantiate(enemyCookie, transform.position, Quaternion.identity);
                c2.transform.localScale = new Vector3(c2.transform.localScale.x / 2, c2.transform.localScale.y / 2, c2.transform.localScale.z / 2);
                c2.GetComponent<Enemy>().isTripled = true;
                c2.transform.LookAt(new Vector2(0, 0));
                c2.GetComponent<Rigidbody2D>().velocity = transform.forward * speed;
                GameObject c3 = Instantiate(enemyCookie, transform.position, Quaternion.identity);
                c3.transform.localScale = new Vector3(c3.transform.localScale.x / 2, c3.transform.localScale.y / 2, c3.transform.localScale.z / 2);
                c3.GetComponent<Enemy>().isTripled = true;
                c3.transform.LookAt(new Vector2(0, 0));
                c3.GetComponent<Rigidbody2D>().velocity = transform.forward * speed;
                Destroy(collision.gameObject);
                gameManager.GetComponent<GameManager>().numEnemies--;
                gameManager.GetComponent<GameManager>().killedEnemies++;
                gameManager.GetComponent<GameManager>().KillSound();
                Destroy(gameObject);
            }
            else if (collision.gameObject.GetComponent<Bullet>() && GetComponent<Cookie>() && health <= 0)
            {
                Instantiate(dieFX, collision.transform.position, Quaternion.identity);
                gameManager.GetComponent<GameManager>().numEnemies--;
                gameManager.GetComponent<GameManager>().killedEnemies++;
                gameManager.GetComponent<GameManager>().KillSound();
                Destroy(collision.gameObject);
                Destroy(gameObject);
            }
        }
        else if (collision.gameObject.GetComponent<Bullet>() && health <= 0 && GetComponent<Donut>())
        {
            Instantiate(dieFX, collision.transform.position, Quaternion.identity);
            gameManager.GetComponent<GameManager>().numEnemies--;
            gameManager.GetComponent<GameManager>().killedEnemies++;
            gameManager.GetComponent<GameManager>().KillSound();
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
        else if (collision.gameObject.GetComponent<Bullet>() && health <= 0 && GetComponent<Chef>())
        {
            Instantiate(dieFX, collision.transform.position, Quaternion.identity);
            gameManager.GetComponent<GameManager>().numEnemies--;
            gameManager.GetComponent<GameManager>().killedEnemies++;
            gameManager.GetComponent<GameManager>().KillSound();
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
    }

    public void Explode()
    {
        Instantiate(dieFX, transform.position, Quaternion.identity);
        GetComponentInChildren<SpriteRenderer>().enabled = false;
    }

    public void FreezeAndContinue()
    {
        Vector2 currentVelocity = GetComponent<Rigidbody2D>().velocity;
        isFrozen = true;
        GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        GetComponent<Rigidbody2D>().drag = 100;
        StartCoroutine(ContinueVelocity(currentVelocity));
    }

    IEnumerator ContinueVelocity(Vector2 currentVelocity)
    {
        yield return new WaitForSeconds(3f);
        isFrozen = false;
        GetComponent<Rigidbody2D>().drag = 0;
        GetComponent<Rigidbody2D>().velocity = currentVelocity;
    }
}
