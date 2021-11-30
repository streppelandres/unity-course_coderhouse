using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefab;
    public enum WeaponType { Pistol, Shotgun, Submachine };
    private Transform shootOrigenTransform;

    // La logica la podes hacer en torno a que si agarras otra arma, lo unico que vas hacer es cambiar este tipo
    // De ahi podes hacer un metodo en el collider, que cambie este tipo y el modelo del arma
    [SerializeField] private WeaponType weaponType;
    private static Dictionary<WeaponType, WeaponSpecifics> weaponSepecificationDictionary;

    private bool firstTimeShooting = true; // Busca otra forma de no tener que hacer una flag asi
    private WeaponSpecifics weaponSpecifics = null;
    private float timerCooldownPerShoot = 0f;

    // TODO: Hacer algo estatico y por eventos
    [SerializeField] private GameObject reloadBar;

    private void Awake()
    {
        // Esto podría ser mejor, quizás cada arma sea una clase a parte que herede de esta
        weaponSepecificationDictionary = new Dictionary<WeaponType, WeaponSpecifics> {
            { WeaponType.Pistol, new WeaponSpecifics(15, 1f, 2.5f, 1.5f) },
            { WeaponType.Shotgun, new WeaponSpecifics(6, 2.5f, 4f, 10f) },
            { WeaponType.Submachine, new WeaponSpecifics(30, 0.5f, 3f, 1f) }
        };

        shootOrigenTransform = transform.Find("ShootOrigin");
    }

    private void Update()
    {
        if (weaponSpecifics != null && timerCooldownPerShoot < weaponSpecifics.CooldownPerShootTime) timerCooldownPerShoot += Time.deltaTime;
    }

    public void ShootHandler() {
        // Si todavia no lo tengo cargado, traigo las especificaciones del arma
        if (weaponSpecifics == null) weaponSpecifics = weaponSepecificationDictionary[weaponType];

        reloadBar.GetComponent<ReloadController>().SetMax(weaponSpecifics.CooldownPerShootTime);

        if (firstTimeShooting || timerCooldownPerShoot > weaponSpecifics.CooldownPerShootTime)
        {
            reloadBar.GetComponent<ReloadController>().ResetValue();
            InstantiateBullet();
            timerCooldownPerShoot = 0f;
            firstTimeShooting = false;
        }
        else {
            Debug.Log($"No se pudo disparar. Weapon Cooldown [{weaponSpecifics.CooldownPerShootTime}], timerShoot: [{timerCooldownPerShoot}]");
        }
    }

    private void InstantiateBullet() {
        GameObject bullet = Instantiate(bulletPrefab, shootOrigenTransform.position, Quaternion.identity) as GameObject;
        bullet.GetComponent<Rigidbody>().AddForce(shootOrigenTransform.TransformDirection(Vector3.forward) * 20f, ForceMode.Impulse);
        Destroy(bullet, 5f);
    }

    public void ChangeWeaponType(WeaponType newWeaponType)
    {
        weaponType = newWeaponType;
        // Al cambiar de arma le cargo las especificaciones nuevas
        weaponSpecifics = weaponSepecificationDictionary[weaponType];
        firstTimeShooting = true;
        reloadBar.GetComponent<ReloadController>().SetMax(weaponSpecifics.CooldownPerShootTime);
    }
}
