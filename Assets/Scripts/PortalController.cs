using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalController : MonoBehaviour
{
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player")) {
            PlayerDesafio9Controller player = other.gameObject.GetComponent<PlayerDesafio9Controller>();

            if (player.OriginalSize == player.transform.localScale)
            {
                player.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
            }
            else
            {
                player.transform.localScale = player.OriginalSize;
            }
        }
    }

}
