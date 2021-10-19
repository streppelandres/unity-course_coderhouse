using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanonController : MonoBehaviour
{
    public GameObject bulletPrefab;
    public float delayTime = 3f;

    void Start()
    {
        InvokeRepeating("ShotBullet", 1f, this.delayTime);
    }

    #pragma warning disable IDE0051 // Se usa en InvokeRepeating
    private void ShotBullet()
    {
        Instantiate(this.bulletPrefab, this.transform.position, this.bulletPrefab.transform.rotation);
    }
}