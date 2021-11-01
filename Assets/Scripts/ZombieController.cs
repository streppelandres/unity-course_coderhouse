using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieController : MonoBehaviour
{
    private enum EnemyType { Type1, Type2 };
    private Transform playerTransform;

    [SerializeField] private float speed = 5f;
    [SerializeField] private float lookSpeed = 5f;
    [SerializeField] private float playerDistance = 10f;
    [SerializeField] private EnemyType enemyType;

    void Start() {
        playerTransform = GameObject.Find("Player").transform;
    }

    void Update() {
        // Un enemigo (enemigo 1) que mire al jugador siempre, con rotación suave (lerp)
        if (enemyType == EnemyType.Type1) {
            LookAtPlayerLerp();
        }

        // Un enemigo (enemigo 2) que persigue al jugador siempre, pero se detenga al estar a una distancia menor a 2 unidades.
        else if (enemyType == EnemyType.Type2) {
            LookAtPlayer();
            FollowPlayer();
        }
    }

    private void LookAtPlayer() {
        transform.rotation = Quaternion.LookRotation(playerTransform.position - transform.position);
    }

    private void LookAtPlayerLerp() {
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(playerTransform.position - transform.position), lookSpeed * Time.deltaTime);
    }

    private void FollowPlayer() {
        if ((playerTransform.position - transform.position).magnitude > playerDistance) {
            transform.position += speed * ((playerTransform.position - transform.position).normalized) * Time.deltaTime;
        }
    }
}
