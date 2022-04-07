using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    GameManager gameManager;
    public bool hitCookie = false;
    [SerializeField] Sprite[] bullets;
    
    SpriteRenderer spr;
    private void Awake()
    {
        spr = GetComponent<SpriteRenderer>();

        int rnd = Random.Range(0, 18);
        switch (rnd)
        {
            case 0:
                spr.sprite = bullets[0];
                break;
            case 1:
                spr.sprite = bullets[1];
                break;
            case 2:
                spr.sprite = bullets[2];
                break;
            case 3:
                spr.sprite = bullets[3];
                break;
            case 4:
                spr.sprite = bullets[4];
                break;
            case 5:
                spr.sprite = bullets[5];
                break;
            case 6:
                spr.sprite = bullets[6];
                break;
            case 7:
                spr.sprite = bullets[7];
                break;
            case 8:
                spr.sprite = bullets[8];
                break;
            case 9:
                spr.sprite = bullets[9];
                break;
            case 10:
                spr.sprite = bullets[10];
                break;
            case 11:
                spr.sprite = bullets[11];
                break;
            case 12:
                spr.sprite = bullets[12];
                break;
            case 13:
                spr.sprite = bullets[13];
                break;
            case 14:
                spr.sprite = bullets[14];
                break;
            case 15:
                spr.sprite = bullets[15];
                break;
            case 16:
                spr.sprite = bullets[16];
                break;
            case 17:
                spr.sprite = bullets[17];
                break;
            default:
                break;
        }
        
    }
    // Start is called before the first frame update
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        Destroy(gameObject, 2f);
    }

    // Update is called once per frame
    void Update()
    {
        if (gameManager.isLaserOn)
        {
            GetComponent<BoxCollider2D>().isTrigger = true;
        }
        else
        {
            GetComponent<BoxCollider2D>().isTrigger = false;
        }
    }

    
}
