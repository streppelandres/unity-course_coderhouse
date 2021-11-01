using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    private float rotationX = -90f;
    private AudioSource footSteps;

    void Start() {
        this.footSteps  = this.gameObject.GetComponent<AudioSource>();
    }

    void Update() {
        Move();
        PlaySteepsSound();
        Rotate();
    }

    private void Move() {
        transform.Translate(new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")) * speed * Time.deltaTime);
    }

    private void PlaySteepsSound() {
        if (!this.footSteps.isPlaying && (Input.GetButton("Horizontal") || Input.GetButton("Vertical"))) {
            footSteps.Play();
        }
    }

    private void Rotate() {
        transform.localRotation = Quaternion.Euler(0, (rotationX += Input.GetAxis("Mouse X")), 0);
    }
}
