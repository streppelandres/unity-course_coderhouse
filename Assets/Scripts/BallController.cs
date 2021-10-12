using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
    public Vector3 initScale = new Vector3(1.0f, 1.0f, 1.0f);
    public Vector3 position = new Vector3(0, 1, 0);
    public float speed = 100.0f;

    // Start is called before the first frame update
    void Start()
    {
        transform.localScale = initScale;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += (position * speed * Time.deltaTime);
    }
}
