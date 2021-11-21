using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed = 15f;
    [SerializeField] private GameObject bulletPrefab;
    private bool isHoldingRightClick = false;

    void Start()
    {

    }

    void Update()
    {
        CheckIsHoldingRightClick();

        if (isHoldingRightClick)
        {
            Shoot();
        }

    }

    private void FixedUpdate()
    {
        Move();

        if (isHoldingRightClick)
        {
            RotateUsingRaycast();
        }

    }

    private void CheckIsHoldingRightClick()
    {
        if (Input.GetMouseButtonDown(1))
        {
            isHoldingRightClick = true;
            CursorManager.SetCursor(CursorManager.CursorType.Aiming);
        }
        else if (Input.GetMouseButtonUp(1))
        {
            isHoldingRightClick = false;
            CursorManager.SetCursor(CursorManager.CursorType.Default);
        }
    }

    private void Move()
    {
        transform.Translate(new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical")) * speed * Time.deltaTime);
    }

    private void Shoot()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity) as GameObject;
            bullet.GetComponent<Rigidbody>().AddForce(transform.TransformDirection(Vector3.forward) * 20f, ForceMode.Impulse);
        }
    }

    // Código de iKabyLake30: http://answers.unity.com/answers/1699638/view.html
    private void RotateUsingRaycast()
    {
        Ray cameraRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        Plane groundPlane = new Plane(Vector3.up, Vector3.zero);
        float rayLength;
        if (groundPlane.Raycast(cameraRay, out rayLength))
        {
            Vector3 pointToLook = cameraRay.GetPoint(rayLength);
            Debug.DrawLine(cameraRay.origin, pointToLook, Color.cyan);
            transform.LookAt(new Vector3(pointToLook.x, transform.position.y, pointToLook.z));
        }
    }
}
