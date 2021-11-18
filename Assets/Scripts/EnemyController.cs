using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 *  Que tenga dos tipos
 *  - Uno que sea agresivo (rojo)
 *  - Otro que sea pasivo (amarillo), pero si llega a ver o tocar al player se vuelve agresivo (rojo)
 *  - Al impactar con el player restar un punto en el score
 *  - Al morir sumar un punto al score del player
 */
public class EnemyController : MonoBehaviour
{
    private enum EnemyType { Pasivo, Agresivo };
    private Transform playerTransform;

    [SerializeField] private EnemyType enemyType;
    [SerializeField] private float speed = 10f;
    private float distanceRay = 30f;

    // Start is called before the first frame update
    void Start()
    {
        playerTransform = GameObject.Find("Player").transform;

        if (enemyType == EnemyType.Pasivo) {
            ChangeMaterialColor(Color.grey);
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        bool isYellow = this.GetComponent<Renderer>().material.GetColor("_Color") == Color.yellow;

        if (enemyType == EnemyType.Pasivo)
        {
            OnRaycastHitFollowPlayer();

            if (isYellow) {
                this.speed += Time.deltaTime;
            }
        }

        if (enemyType == EnemyType.Agresivo || isYellow) {
            FollowPlayer();
            LookAtPlayerLerp();
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

    private void ChangeMaterialColor(Color newColor) {
        this.GetComponent<Renderer>().material.SetColor("_Color", newColor);
    }

    private void OnRaycastHitFollowPlayer() {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, distanceRay)) {
            if (hit.transform.tag == "Player") {
                ChangeMaterialColor(Color.yellow);
            }
        }
    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.blue;
        Vector3 direction = transform.TransformDirection(Vector3.forward) * distanceRay;
        Gizmos.DrawRay(transform.position, direction);
    }

    private void OnCollisionEnter(Collision other) {
        if (other.gameObject.name == "Player")
        {
            GameManager.instance.RemoveScore();
            Destroy(this.gameObject);
        }
        else if (other.gameObject.name == "Bullet") {
            GameManager.instance.AddScore();
            Destroy(this.gameObject);
        }
    }
}
