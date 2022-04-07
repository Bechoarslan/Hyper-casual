using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    [SerializeField] bool faster = false;
    [SerializeField] bool laser = false;
    [SerializeField] bool explosion = false;
    [SerializeField] bool triple = false;
    [SerializeField] bool frozen = false;
    [SerializeField] GameObject powerUpFX;
    [SerializeField] GameObject explosivePowerUpFX;
    GameObject gameManager;
    GameObject player;
    [SerializeField] GameObject bullet;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>().gameObject;
        player = FindObjectOfType<Player>().gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator LaserOn()
    {
        print("laseron");
        gameManager.GetComponent<GameManager>().isLaserOn = true;
        gameManager.GetComponent<GameManager>().LaserIsOn();
        yield return new WaitForSeconds(3f);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<Bullet>() && faster)
        {
            Instantiate(powerUpFX, Vector2.zero, Quaternion.identity);
            player.GetComponent<Player>().shootWait = player.GetComponent<Player>().shootWait * 0.81f;
            gameManager.GetComponent<GameManager>().PowerUpSound();
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }

        else if (collision.gameObject.GetComponent<Bullet>() && laser)
        {
            Instantiate(powerUpFX, Vector2.zero, Quaternion.identity);
            StartCoroutine("LaserOn");
            player.GetComponent<Player>().shootWait = player.GetComponent<Player>().shootWait / 4f;
            gameManager.GetComponent<GameManager>().PowerUpSound();
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }

        else if (collision.gameObject.GetComponent<Bullet>() && explosion)
        {
            GameObject expl = Instantiate(explosivePowerUpFX, transform.position, Quaternion.identity);
            gameManager.GetComponent<GameManager>().PowerUpSound();
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }

        else if (collision.gameObject.GetComponent<Bullet>() && triple)
        {
            Instantiate(powerUpFX, Vector2.zero, Quaternion.identity);
            foreach (Fire namlular in FindObjectsOfType<Fire>())
            {
                namlular.StartShooting();
            }
            gameManager.GetComponent<GameManager>().PowerUpSound();
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }

        else if (collision.gameObject.GetComponent<Bullet>() && frozen)
        {
            Instantiate(powerUpFX, Vector2.zero, Quaternion.identity);
            foreach (Enemy en in FindObjectsOfType<Enemy>())
            {
                en.FreezeAndContinue();
            }
            gameManager.GetComponent<GameManager>().PowerUpSound();
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
    }
}
