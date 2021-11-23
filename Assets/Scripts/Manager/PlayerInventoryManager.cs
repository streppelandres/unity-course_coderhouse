using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;

public static class PlayerInventoryManager
{
    private static List<ItemBoxController.ItemType> itemsList = new List<ItemBoxController.ItemType>();

    public static void AddItem(ItemBoxController.ItemType item)
    {
        if (!itemsList.Contains(item))
        {
            Debug.Log($"Se va agregar al inventario [{item}]");
            itemsList.Add(item);
            DebugLogIventory();
        }
        else {
            Debug.Log($"No se pudo agregar el item [{item}], porque ya esta en el inventario");
        }
    }

    public static void RemoveItem(ItemBoxController.ItemType item)
    {
        Debug.Log($"Se va eliminar del inventario [{item}]");
        itemsList.Remove(item);
        DebugLogIventory();
    }

    // Despues con interfaz esto deberia volar
    public static void DebugLogIventory()
    {
        if (!HasItems()) return;

        StringBuilder sb = new StringBuilder();

        itemsList.ForEach(delegate (ItemBoxController.ItemType item)
        {
            sb.Append($"[{item}] ");
        });

        Debug.Log($"Inventario actualizado: {sb.ToString().Trim()}.");
    }

    public static void UseItem() {

        if (HasItems())
        {
            Debug.Log($"Se va usar el item {itemsList.ToArray()[0]}");
            itemsList.RemoveAt(0);
            DebugLogIventory();
        }
        else {
            Debug.Log("No hay items en el inventario para consumir");
        }
    }

    private static bool HasItems() {
        return itemsList.Count > 0;
    }
}
