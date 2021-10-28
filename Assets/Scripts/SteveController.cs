using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SteveController : MonoBehaviour
{
    [SerializeField] private float speed = 1f;
    private AudioSource footSteps;

    // Start is called before the first frame update
    void Start()
    {
        this.footSteps  = this.gameObject.GetComponent<AudioSource>();
        
    }

    // Update is called once per frame
    void Update()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        transform.Translate(new Vector3(x, 0, z) * speed * Time.deltaTime);

        if (!this.footSteps.isPlaying && (Input.GetButton("Horizontal") || Input.GetButton("Vertical")))
        {
            footSteps.Play();
        }
    }
}
