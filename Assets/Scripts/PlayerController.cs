using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float movementSpeed = 5f;
    [SerializeField] private GameObject bulletPrefab;
    private bool isAiming = false;

    void Start()
    {

    }

    void Update()
    {
        CheckIsHoldingRightClick();
        if (isAiming) CanShoot();
    }

    private void FixedUpdate()
    {
        PlayerMovementHandler.MainMovementHanlder(transform, movementSpeed, isAiming);
    }

    private void CheckIsHoldingRightClick()
    {
        if (Input.GetMouseButtonDown(1))
        {
            isAiming = true;
            CursorManager.SetCursor(CursorManager.CursorType.Aiming);
        }
        else if (Input.GetMouseButtonUp(1))
        {
            isAiming = false;
            CursorManager.SetCursor(CursorManager.CursorType.Default);
        }
    }

    private void CanShoot()
    {
        if (Input.GetMouseButtonDown(0))
        {
            GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity) as GameObject;
            bullet.GetComponent<Rigidbody>().AddForce(transform.TransformDirection(Vector3.forward) * 20f, ForceMode.Impulse);
        }
    }
}
