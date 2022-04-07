using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour
{
    [SerializeField] GameObject bullet;
    public float shootWait = 0.7f;
    public Joystick joystick;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartShooting()
    {
        StartCoroutine("Shoot");
        StartCoroutine("StopWait");
    }

    public void StopShooting()
    {
        StopCoroutine("Shoot");
    }

    IEnumerator StopWait()
    {
        yield return new WaitForSeconds(6f);
        StopShooting();
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
                GameObject n_bullet = Instantiate(bullet, transform.position, Quaternion.identity);
                n_bullet.GetComponent<Rigidbody2D>().AddForce(transform.up * 450f);
            }
        }
    }
}
