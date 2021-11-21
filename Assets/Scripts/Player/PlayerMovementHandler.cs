using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementHandler
{
    private static readonly float RotationSpeed = 720f;

    public static void MainMovementHanlder(Transform transform, float movementSpeed, bool isAiming)
    {
        Vector3 movementDirection = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));

        if (isAiming)
        {
            AimingMovement(transform, movementDirection, movementSpeed);
            AimingRotation(transform);
        }
        else
        {
            DefaultMovement(transform, movementDirection, movementSpeed);
        }
    }

    // Rotaci�n en base al mouse de iKabyLake30: http://answers.unity.com/answers/1699638/view.html
    private static void AimingRotation(Transform transform)
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

    private static void AimingMovement(Transform transform, Vector3 movementDirection, float movementSpeed)
    {
        transform.Translate(movementDirection * movementSpeed * Time.deltaTime, Space.World);
    }

    // Movimiento del personaje con rotaci�n de Ketra Games: https://youtu.be/BJzYGsMcy8Q
    private static void DefaultMovement(Transform transform, Vector3 movementDirection, float movementSpeed)
    {
        movementDirection.Normalize();
        transform.Translate(movementDirection * movementSpeed * Time.deltaTime, Space.World);

        if (movementDirection != Vector3.zero)
        {
            Quaternion toRotation = Quaternion.LookRotation(movementDirection, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, RotationSpeed * Time.deltaTime);
        }
    }
}
