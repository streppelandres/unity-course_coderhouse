using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    public float speed = 10;
    public Vector3 direction = Vector3.down;
    public int damage = 10;
    public float lifeTime = 10.0f;

    void Update()
    {
        this.transform.Translate(this.direction * this.speed * Time.deltaTime);

        this.lifeTime -= Time.deltaTime;

        if (lifeTime <= 0)
        {
            Destroy(this.gameObject);
        }

        if(Input.GetKeyDown(KeyCode.Space))
        {
            this.transform.localScale = this.transform.localScale * 2;
        }
    }
}