using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBoxController : MonoBehaviour
{
    public enum ItemType { SpeedBoost = 0, Heal = 1, IncreaseDamage = 2 }
    private ItemType type;

    private void Awake()
    {
        SetRandomType();
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player")) {
            InventoryManager.AddItem(type);
            CubeExplosion.Explode(gameObject); // Hace una sobrecarga del metodo para pasarle mas config
        }
    }

    private void SetRandomType() {
        ItemType t = (ItemType) Random.Range(0, 2);
        Debug.Log($"Se eligio al azar el tipo {t}");
        type = t;
    }
}
