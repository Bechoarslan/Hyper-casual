using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] GameObject bullet;
    [SerializeField] GameObject gameManager;
    public float shootWait = 0.65f;
    public Joystick joystick;
    // Start is called before the first frame update
    void Start()
    {
        transform.LookAt(new Vector3(1, 1, 0), Vector3.forward);
        //StartCoroutine("Shoot");
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(joystick.Direction, Vector3.forward);
        
    }

    public void StopShooting()
    {
        StopCoroutine("Shoot");
    }

    public void StartShooting()
    {
        StartCoroutine("Shoot");
    }

    IEnumerator Shoot()
    {
        while (true)
        {
            yield return new WaitForSeconds(shootWait);
            if (joystick.Direction.x != 0 && joystick.Direction.y != 0)
            {
                //float XDir = 0;
                //float YDir = 0;
                //if (joystick.Direction.x > 0)
                //{
                //    XDir = 1f;
                //}
                //else if(joystick.Direction.x < 0)
                //{
                //    XDir = -1f;
                //}
                //if (joystick.Direction.y > 0)
                //{
                //    YDir = 1f;
                //}
                //else if (joystick.Direction.y < 0)
                //{
                //    YDir = -1f;
                //}
                GameObject n_bullet = Instantiate(bullet, new Vector3((transform.position.x + joystick.Direction.x * 0.7f), (transform.position.y + joystick.Direction.y * 0.7f), 0f), Quaternion.identity);
                n_bullet.GetComponent<Rigidbody2D>().AddForce(joystick.Direction * 450f);
                gameManager.GetComponent<GameManager>().ShootSound();
            }
        }
    }
}
