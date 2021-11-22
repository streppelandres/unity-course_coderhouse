using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSpecifics
{
    public int MagazineCapacity { get; private set; }
    public float CooldownPerShootTime { get; private set; }
    public float ReloadTime { get; private set; }
    public float DamagePerShoot { get; private set; }

    public WeaponSpecifics(int magazineCapacity, float cooldownPerShootTime, float reloadTime, float damagePerShoot)
    {
        this.MagazineCapacity = magazineCapacity;
        this.CooldownPerShootTime = cooldownPerShootTime;
        this.ReloadTime = reloadTime;
        this.DamagePerShoot = damagePerShoot;
    }
}
