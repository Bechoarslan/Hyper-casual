using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    AudioSource au;

    [SerializeField] Sprite brokoliSad;
    [SerializeField] Sprite brokoliAngry;
    [SerializeField] Sprite brokoliDead;
    [SerializeField] Sprite brokoliHappy;

    [SerializeField] GameObject enemyCookie;
    [SerializeField] GameObject enemyDonut;
    [SerializeField] GameObject enemyChef;
    [SerializeField] GameObject powerUp;
    [SerializeField] GameObject powerUp2;
    [SerializeField] GameObject powerUp3;
    [SerializeField] GameObject powerUp4;
    [SerializeField] GameObject powerUp5;
    [SerializeField] GameObject player;

    [SerializeField] AudioClip music;
    [SerializeField] AudioClip killSound;
    [SerializeField] AudioClip hitSound;
    [SerializeField] AudioClip fireSound;
    [SerializeField] AudioClip powerUpSound;
    [SerializeField] AudioClip gameOver;
    [SerializeField] AudioClip button;

    [SerializeField] GameObject GameOverMenu;
    [SerializeField] TMP_Text scoreText;
    [SerializeField] TMP_Text oldHighScoreText;
    [SerializeField] TMP_Text scoreTextMenu;
    [SerializeField] TMP_Text highScoreText;
    [SerializeField] TMP_Text highScoreAlertText;

    public float spawnWait = 1.75f;
    public bool isLaserOn = false;
    public bool isDead = false;
    [SerializeField] bool isPaused = false;

    [SerializeField] int level;
    [SerializeField] int levelOld;
    public int numEnemies;
    public int killedEnemies;
    // Start is called before the first frame update
    void Start()
    {
        highScoreAlertText.gameObject.SetActive(false);
        au = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerPrefs.GetInt("HighScore") < killedEnemies)
        {
            PlayerPrefs.SetInt("HighScore", killedEnemies);
            highScoreAlertText.gameObject.SetActive(true);
        }

        scoreText.text = killedEnemies.ToString();
        scoreTextMenu.text = "SCORE\n" + killedEnemies.ToString();
        oldHighScoreText.text = "HIGH SCORE\n" + PlayerPrefs.GetInt("HighScore").ToString();
        if (highScoreText != null)
        {
            highScoreText.text = "High Score: " + PlayerPrefs.GetInt("HighScore").ToString();
        }
        
        if (spawnWait >= 0.5f)
        {
            spawnWait -= Time.realtimeSinceStartup / 6000000;
        }
        
        if (numEnemies >= 10 && level == levelOld)
        {
            level++;
            float posX = Random.Range(-2f, 2f);
            float posY = Random.Range(-4f, 4f);
            int rnd = Random.Range(0, 5);
            GameObject pwr;
            switch (rnd)
            {
                case 0:
                    pwr = Instantiate(powerUp, new Vector2(posX, posY), Quaternion.identity);
                    pwr.transform.LookAt(new Vector2(0, 0));
                    break;
                case 1:
                    pwr = Instantiate(powerUp2, new Vector2(posX, posY), Quaternion.identity);
                    pwr.transform.LookAt(new Vector2(0, 0));
                    break;
                case 2:
                    pwr = Instantiate(powerUp3, new Vector2(posX, posY), Quaternion.identity);
                    pwr.transform.LookAt(new Vector2(0, 0));
                    break;
                case 3:
                    pwr = Instantiate(powerUp4, new Vector2(posX, posY), Quaternion.identity);
                    pwr.transform.LookAt(new Vector2(0, 0));
                    break;
                case 4:
                    pwr = Instantiate(powerUp5, new Vector2(posX, posY), Quaternion.identity);
                    pwr.transform.LookAt(new Vector2(0, 0));
                    break;
                default:
                    break;
            }
            
            StartCoroutine(WaitToChangeLevel());
        }
    }

    public void ButtonSound()
    {
        au.PlayOneShot(button);
    }

    public void HitSound()
    {
        au.PlayOneShot(hitSound);
    }

    public void PowerUpSound()
    {
        au.PlayOneShot(powerUpSound, 0.5f);
    }

    public void KillSound()
    {
        au.PlayOneShot(killSound, 0.3f);
    }

    public void ShootSound()
    {
        au.PlayOneShot(fireSound);
    }

    public void PauseGame()
    {
        float spawnWaitOnPause = 1.75f;
        if (isPaused)
        {
            spawnWait = spawnWaitOnPause;
            Time.timeScale = 1;
            player.GetComponent<Player>().StartShooting();
            //StartCoroutine(SpawnEnemy());
        }
        else if (isPaused == false)
        {
            spawnWaitOnPause = spawnWait;
            Time.timeScale = 0;
            player.GetComponent<Player>().StopShooting();
            //StopCoroutine(SpawnEnemy());
        }
        isPaused = !isPaused;
    }

    public void ResetGame()
    {
        player.GetComponentInChildren<SpriteRenderer>().sprite = brokoliHappy;
        print("resetted");
        level = 0;
        levelOld = 0;
        player.GetComponent<Player>().shootWait = 0.65f;
        foreach (var item in FindObjectsOfType<Enemy>())
        {
            Destroy(item.gameObject);
        }
        foreach (var item in FindObjectsOfType<PowerUp>())
        {
            Destroy(item.gameObject);
        }
        spawnWait = 1.75f;
        isDead = false;
        highScoreAlertText.gameObject.SetActive(false);
        numEnemies = 0;
        killedEnemies = 0;
        //StopCoroutine("DieOneByOne");
        //StopCoroutine("SpawnEnemy");
    }

    public void StartGame()
    {
        highScoreAlertText.gameObject.SetActive(false);
        player.GetComponentInChildren<SpriteRenderer>().sprite = brokoliHappy;
        StopAllCoroutines();
        StartCoroutine(SpawnEnemy());
        player.GetComponent<Player>().StartShooting();
        spawnWait = 1.75f;
    }

    public void Exit()
    {
        Application.Quit();
    }

    public void LaserIsOn()
    {
        StartCoroutine("TurnLaserOff");
    }

    IEnumerator TurnLaserOff()
    {
        yield return new WaitForSeconds(2.5f);
        isLaserOn = false;
        player.GetComponent<Player>().shootWait = player.GetComponent<Player>().shootWait * 4f;
    }

    IEnumerator WaitToChangeLevel()
    {
        int rnd = Random.Range(0, 2);
        if (rnd == 0)
        {
            player.GetComponentInChildren<SpriteRenderer>().sprite = brokoliAngry;
        }
        else if (rnd == 1)
        {
            player.GetComponentInChildren<SpriteRenderer>().sprite = brokoliSad;
        }
        yield return new WaitForSeconds(9f);
        levelOld = level;
    }

    public void Die()
    {
        player.GetComponentInChildren<SpriteRenderer>().sprite = brokoliDead;
        au.PlayOneShot(gameOver);
        foreach (var item in FindObjectsOfType<Bullet>())
        {
            Destroy(item);
        }
        if (isDead == false)
        {
            GameOverMenu.GetComponent<Animator>().SetBool("Open", true);
        }
        isDead = true;
        player.GetComponent<Player>().StopShooting();
        StartCoroutine("DieOneByOne");
    }

    public void DieInGame()
    {
        foreach (var item in FindObjectsOfType<Bullet>())
        {
            Destroy(item);
        }
        isDead = true;
        player.GetComponent<Player>().StopShooting();
    }

    IEnumerator DieOneByOne()
    {
        foreach (Enemy en in FindObjectsOfType<Enemy>())
        {
             en.Explode();
             yield return new WaitForSeconds(0.05f);
        }
    }

    IEnumerator SpawnEnemy()
    {
        print("startspawning");
        while (true)
        {
            yield return new WaitForSeconds(spawnWait);
            if (isDead == false)
            {
                float posX = Random.Range(-2.8f, 2.8f);
                float posY = Random.Range(-5.5f, 5.5f);
                if (posX < 0 && posY < 5.5 && posY > -5.5)
                {
                    posX = -2.8f;
                }
                else if (posX > 0 && posY < 5.5 && posY > -5.5)
                {
                    posX = 2.8f;
                }
                else if (posY < 0 && posX < 2.8 && posX > -2.8)
                {
                    posY = -5.5f;
                }
                else if (posY > 0 && posX < 2.8 && posX > -2.8)
                {
                    posY = 5.5f;
                }
                int rnd;
                rnd = Random.Range(0, 6);
                if (rnd < 3)
                {
                    Instantiate(enemyDonut, new Vector3(posX, posY, 0f), Quaternion.identity);
                }
                else if (rnd < 5)
                {
                    Instantiate(enemyCookie, new Vector3(posX, posY, 0f), Quaternion.identity);
                }
                else
                {
                    Instantiate(enemyChef, new Vector3(posX, posY, 0f), Quaternion.identity);
                }
                //switch (rnd)
                //{
                //    case 0:
                //        Instantiate(enemyCookie, new Vector3(posX, posY, 0f), Quaternion.identity);
                //        break;
                //    case 1:
                //        Instantiate(enemyDonut, new Vector3(posX, posY, 0f), Quaternion.identity);
                //        break;
                //    case 2:
                //        Instantiate(enemyChef, new Vector3(posX, posY, 0f), Quaternion.identity);
                //        break;
                //    default:
                //        break;
                //}
            }
        }
    }
}
