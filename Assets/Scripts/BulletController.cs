using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    public float speed = 10;
    public Vector3 direction = Vector3.down;
    public int damage = 10;

    void Update()
    {
        this.transform.Translate(this.direction * this.speed * Time.deltaTime);
    }
}