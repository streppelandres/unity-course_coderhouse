using UnityEngine;

[CreateAssetMenu(fileName = "New Gun", menuName = "ScripteableData/Gun")]
public class Weapon : ScriptableObject
{
    [SerializeField] private WeaponType type;
    [SerializeField] private int magazineCapacity;
    [SerializeField] private float cooldownPerShootTime;
    [SerializeField] private float reloadTime;
    [SerializeField] private int bulletsPerShoot;
    [SerializeField] private float damagePerShoot;
    [SerializeField] private float criticalChance;
    [SerializeField] private Color color;

    public WeaponType Type { get => type; }
    public int MagazineCapacity { get => magazineCapacity; }
    public float CooldownPerShootTime { get => cooldownPerShootTime; }
    public float ReloadTime { get => reloadTime; }
    public int BulletsPerShoot { get => bulletsPerShoot; }
    public float DamagePerShoot { get => damagePerShoot; }
    public float CriticalChance { get => criticalChance; }
    public Color Color { get => color; }
}
