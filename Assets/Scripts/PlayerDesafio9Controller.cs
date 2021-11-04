using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDesafio9Controller : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    private Vector3 originalSize;
    public Vector3 OriginalSize { get { return originalSize; } }

    void Start()
    {
        originalSize = transform.localScale;
    }

    void Update()
    {
        Move();
    }

    private void Move()
    {
        transform.Translate(new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")) * speed * Time.deltaTime);
    }

    private void OnCollisionEnter(Collision other)
    {
        Debug.Log($"[Player] Colisionó con -> [{other.gameObject.name}]");
    }
}
