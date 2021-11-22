using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float movementSpeed = 5f;
    private bool isAiming = false;
    private WeaponController weaponController;

    private void Awake()
    {
        weaponController = transform.Find("WeaponSpot").gameObject.GetComponent<WeaponController>();
    }

    void Update()
    {
        CheckIfPlayerIsAiming();
        if (isAiming && Input.GetMouseButtonDown(0)) weaponController.ShootHandler();
    }

    private void FixedUpdate()
    {
        PlayerMovementHandler.MainMovementHanlder(transform, movementSpeed, isAiming);
    }

    private void CheckIfPlayerIsAiming()
    {
        if (Input.GetMouseButtonDown(1))
        {
            isAiming = true;
        }
        else if (Input.GetMouseButtonUp(1))
        {
            isAiming = false;
        }

        CursorManager.SetCursor(isAiming ? CursorManager.CursorType.Aiming : CursorManager.CursorType.Default);
    }
}
