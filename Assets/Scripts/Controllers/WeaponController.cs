using UnityEngine;

public class WeaponController : MonoBehaviour
{
    [SerializeField] private Weapon gunScripteable;
    public Weapon GunScripteable { get => gunScripteable; }
    [SerializeField] private GameObject bulletPrefab;

    private Transform shootOrigenTransform;
    private bool firstTimeShooting = true; // Busca otra forma de no tener que hacer una flag asi
    private float timerCooldownPerShoot = 0f;

    private void Awake()
    {
        shootOrigenTransform = transform.Find("ShootOrigin");
        ChangeGunColor(gunScripteable.Color);
    }

    private void Start()
    {
        
    }

    private void Update()
    {
        if (timerCooldownPerShoot < gunScripteable.CooldownPerShootTime) timerCooldownPerShoot += Time.deltaTime;
    }

    public void ShootHandler() {
        ReloadUI.instance.SetMax(gunScripteable.CooldownPerShootTime);

        if (firstTimeShooting || timerCooldownPerShoot > gunScripteable.CooldownPerShootTime)
        {
            ReloadUI.instance.ResetValue();
            InstantiateBullet();
            timerCooldownPerShoot = 0f;
            firstTimeShooting = false;
        }
        else {
            Debug.Log($"No se pudo disparar. Weapon Cooldown [{gunScripteable.CooldownPerShootTime}], timerShoot: [{timerCooldownPerShoot}]");
        }
    }

    private void InstantiateBullet() {
        GameObject bullet = Instantiate(bulletPrefab, shootOrigenTransform.position, Quaternion.identity) as GameObject;
        bullet.GetComponent<Rigidbody>().AddForce(shootOrigenTransform.TransformDirection(Vector3.forward) * 20f, ForceMode.Impulse);
        Destroy(bullet, 5f);
    }

    public void ChangeWeaponType(Weapon newGunScripteable)
    {
        gunScripteable = newGunScripteable;
        firstTimeShooting = true;
        ReloadUI.instance.SetMax(gunScripteable.CooldownPerShootTime);
        ChangeGunColor(gunScripteable.Color);
    }

    private void ChangeGunColor(Color newColor) {
        transform.Find("Weapon").GetComponent<Renderer>().material.SetColor("_Color", newColor);
    }
}
