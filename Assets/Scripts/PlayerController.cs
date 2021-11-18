using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed = 15f;
    [SerializeField] private GameObject projectile;
    // private float rotationX = -90f;
    // private AudioSource footSteps;

    void Start() {
        // this.footSteps  = this.gameObject.GetComponent<AudioSource>();
    }

    void Update() {
        Move();
        // PlaySteepsSound();
        // Rotate();
        Shoot();
    }

    private void Move() {
        transform.Translate(new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")) * speed * Time.deltaTime);
    }

    private void Shoot() {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            GameObject bullet = Instantiate(projectile, transform.position, Quaternion.identity) as GameObject;
            bullet.GetComponent<Rigidbody>().AddForce(transform.forward * 10);
        }
    }

    /*private void PlaySteepsSound() {
        if (!this.footSteps.isPlaying && (Input.GetButton("Horizontal") || Input.GetButton("Vertical"))) {
            footSteps.Play();
        }
    }*/

    /*private void Rotate() {
        transform.localRotation = Quaternion.Euler(0, (rotationX += Input.GetAxis("Mouse X")), 0);
    }*/
}
