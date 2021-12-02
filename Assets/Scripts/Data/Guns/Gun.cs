using UnityEngine;

[CreateAssetMenu(fileName = "New Gun", menuName = "Gun")]
public class Gun : ScriptableObject
{
    [SerializeField] private GunType type;
    [SerializeField] private int magazineCapacity;
    [SerializeField] private float cooldownPerShootTime;
    [SerializeField] private float reloadTime;
    [SerializeField] private int bulletsPerShoot;
    [SerializeField] private float damagePerShoot;
    [SerializeField] private float criticalChance;
    [SerializeField] private Color color;

    public GunType Type { get => type; set => type = value; }
    public int MagazineCapacity { get => magazineCapacity; set => magazineCapacity = value; }
    public float CooldownPerShootTime { get => cooldownPerShootTime; set => cooldownPerShootTime = value; }
    public float ReloadTime { get => reloadTime; set => reloadTime = value; }
    public int BulletsPerShoot { get => bulletsPerShoot; set => bulletsPerShoot = value; }
    public float DamagePerShoot { get => damagePerShoot; set => damagePerShoot = value; }
    public float CriticalChance { get => criticalChance; set => criticalChance = value; }
    public Color Color { get => color; set => color = value; }
}
