using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 *
 *  Tipos de enemigos:
 *  - Pasio: Merondea por ahi, si ve al player cambia a color amarillo, lo va perseguir y aumentar su velocidad
 *  - Agresivo: Ni bien aparece va ir detras del jugador
 *  
 *  Logica de explosion al ser impactado es de: https://youtu.be/s_v9JnTDCCY
 *  Le hice ligeros cambios como el color, que los pedazos sean objeto child y que despawneen en "X" tiempo
 */
public class EnemyController : MonoBehaviour
{
    private enum EnemyType { Pasivo, Agresivo };
    private Transform playerTransform;

    [SerializeField] private EnemyType enemyType;
    [SerializeField] private float speed = 10f;
    private static readonly float DistanceRay = 30f;
    private static readonly Color PasiveColor = Color.yellow;
    private static readonly Color AgresiveColor = Color.red;
    private bool hasExploded = false;

    // Start is called before the first frame update
    void Start()
    {
        playerTransform = GameObject.Find("Player").transform;
        SetMaterialColor(enemyType == EnemyType.Pasivo ? Color.blue : AgresiveColor);
    }

    // Update is called once per frame
    void Update()
    {
        if (!hasExploded) {
            bool isPasiveColor = GetMaterialColor() == PasiveColor;

            if (enemyType == EnemyType.Pasivo)
            {
                // TODO: Movimiento por default, que vaya yendo y viniendo
                OnRaycastHitFollowPlayer();
                OnRaycastHitFollowPlayer();

                if (isPasiveColor) {
                    this.speed += Time.deltaTime;
                }
            }

            if (enemyType == EnemyType.Agresivo || isPasiveColor) {
                FollowPlayer();
                LookAtPlayerLerp();
            }
        }
    }

    private void FollowPlayer()
    {
        transform.position += speed * ((playerTransform.position - transform.position).normalized) * Time.deltaTime;
    }

    private void LookAtPlayerLerp()
    {
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(playerTransform.position - transform.position), 5f * Time.deltaTime);
    }

    private void SetMaterialColor(Color newColor) {
        this.GetComponent<Renderer>().material.SetColor("_Color", newColor);
    }

    private Color GetMaterialColor() {
        return this.GetComponent<Renderer>().material.GetColor("_Color");
    }

    private void OnRaycastHitFollowPlayer() {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, DistanceRay)) {
            if (hit.transform.tag == "Player") {
                SetMaterialColor(Color.yellow);
            }
        }
    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.blue;
        Vector3 direction = transform.TransformDirection(Vector3.forward) * DistanceRay;
        Gizmos.DrawRay(transform.position, direction);
    }

    private void OnCollisionEnter(Collision other) {
        string name = other.gameObject.name;

        if (name == "Player")
        {
            GameManager.instance.RemoveScore();
            Destroy(this.gameObject);
        }
        else if (name.StartsWith("Bullet")) {
            GameManager.instance.AddScore();
            hasExploded = CubeExplosionHandler.Explode(gameObject);
        }
    }

}
