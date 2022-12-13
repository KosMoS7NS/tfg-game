using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    float bulletSpeed = 60;
    public GameObject bullet;

    void Fire()
    {
        GameObject tempBullet = Instantiate(bullet, transform.position, transform.rotation) as GameObject;
        Rigidbody tempRigidBodyBullet = tempBullet.GetComponent<Rigidbody>();
        tempRigidBodyBullet.AddForce(tempRigidBodyBullet.transform.forward * bulletSpeed);
        Destroy(tempBullet, 5f);
    }


    void Update()
    {
        if (Input.GetMouseButtonDown(0)) Fire();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy")) Destroy(other.gameObject);
    }
}
