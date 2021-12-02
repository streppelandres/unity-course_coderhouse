using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private GameObject scoreUi; // FIXME: Esto debería ser por un singleton
    [SerializeField] protected Enemy enemyScripteable;

    private Transform playerTransform;
    private bool hasExploded = false;
    private bool playerFound = false;

    // Start is called before the first frame update
    void Start()
    {
        playerTransform = GameObject.Find("Player").transform;
        SetMaterialColor(enemyScripteable.Color);
    }

    // Update is called once per frame
    void Update()
    {
        if (!hasExploded)
        {

            if (enemyScripteable.EnemyType == EnemyType.Agresive || playerFound) {
                FollowPlayer();
                LookAtPlayerLerp();
            } else if (enemyScripteable.EnemyType == EnemyType.Pasive && !playerFound) {
                OnRaycastFindPlayer();
            }

        }
    }

    private void FollowPlayer()
    {
        transform.position += enemyScripteable.Speed * ((playerTransform.position - transform.position).normalized) * Time.deltaTime;
    }

    private void LookAtPlayerLerp()
    {
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(playerTransform.position - transform.position), 5f * Time.deltaTime);
    }

    private void SetMaterialColor(Color newColor)
    {
        this.GetComponent<Renderer>().material.SetColor("_Color", newColor);
    }

    private void OnRaycastFindPlayer()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, enemyScripteable.LookDistance))
        {
            if (hit.transform.tag == "Player")
            {
                playerFound = true;
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Vector3 direction = transform.TransformDirection(Vector3.forward) * enemyScripteable.LookDistance;
        Gizmos.DrawRay(transform.position, direction);
    }

    private void OnCollisionEnter(Collision other)
    {
        string name = other.gameObject.name;

        if (name == "Player")
        {
            GameManager.instance.RemoveScore();
            Destroy(this.gameObject);
        }
        else if (name.StartsWith("Bullet"))
        {
            // TODO: Fijate de hacer mejor esto:
            float damage = playerTransform.Find("WeaponSpot").GetComponent<WeaponController>().GunScripteable.DamagePerShoot;
            enemyScripteable.Life -= damage;
            Debug.Log($"El enemigo recibió [{damage}] puntos de daño, vida restante [{enemyScripteable.Life}]");

            if (enemyScripteable.Life <= 0) {
                Debug.Log($"Enemigo sin vida, eliminado");

                // Sumo al score
                GameManager.instance.AddScore();
                scoreUi.GetComponent<ScoreController>().SetScore(GameManager.GetScore());

                // Destruyo el object
                hasExploded = CubeExplosionHandler.Explode(gameObject);
            }
        }
    }

}
