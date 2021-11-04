using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallController : MonoBehaviour
{
    [SerializeField] private Vector3 positionToTransport = new Vector3(4.12f, 2.5f, -2.85f);
    [SerializeField] private float timeToStay = 2f;
    private float timer = 0f;

    private void OnCollisionStay(Collision collision)
    {
        timer += Time.deltaTime;
        Debug.Log($"[WallController] Tiempo en colision -> [{timer}].");

        if (timer > timeToStay)
        {
            Debug.Log($"[WallController] Se va transportar al jugar a la posición -> [{positionToTransport}].");
            collision.gameObject.transform.position = positionToTransport;
        }

    }

    private void OnCollisionExit(Collision collision)
    {
        Debug.Log($"[WallController] Se salió de la colisión, se va resetear el timer.");
        timer = 0;
    }

}
