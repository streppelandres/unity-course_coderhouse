using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float movementSpeed = 5f;
    private bool isAiming = false;
    private WeaponController weaponController;
    // public PlayerInventoryManager InventoryManager { get; private set; } // Es estatico no lo necesitas instanciar

    private void Awake()
    {
        // InventoryManager = new PlayerInventoryManager(); // Es estatico no lo necesitas instanciar
        weaponController = transform.Find("WeaponSpot").gameObject.GetComponent<WeaponController>();
    }

    void Update()
    {
        CheckIfPlayerIsAiming();
        if (isAiming && Input.GetMouseButtonDown(0)) weaponController.ShootHandler();
        if (Input.GetKeyDown(KeyCode.E)) PlayerInventoryManager.UseItem();
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
