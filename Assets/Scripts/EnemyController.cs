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
    private float distanceRay = 30f;
    private Color pasiveColor = Color.yellow;
    private Color agresiveColor = Color.red;

    // TODO: Todo lo de la explosión estaría bueno que este en un utils a parte
    #region Cube explosion variables
    private float cubeSize = 0.2f;
    private int cubesInRow = 5;
    private float cubesPivotDistance;
    private Vector3 cubesPivot;
    private float explosionForce = 50f;
    private float explosionRadius = 4f;
    private float explosionUpward = 0.4f;
    private bool hasExploded = false;
    private float explodedTimer = 0f;
    private float explodedPiecesDespawnTime = 10f;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        playerTransform = GameObject.Find("Player").transform;
        SetMaterialColor(enemyType == EnemyType.Pasivo ? Color.blue : agresiveColor);

        // Calculate pivot distance
        cubesPivotDistance = cubeSize * cubesInRow / 2;
        // Use this value to create pivot vector)
        cubesPivot = new Vector3(cubesPivotDistance, cubesPivotDistance, cubesPivotDistance);

    }

    // Update is called once per frame
    void Update()
    {
        if (!hasExploded) {
            bool isPasiveColor = GetMaterialColor() == pasiveColor;

            if (enemyType == EnemyType.Pasivo)
            {
                // TODO: Movimiento por default, que vaya yendo y viniendo
                OnRaycastHitFollowPlayer();

                if (isPasiveColor) {
                    this.speed += Time.deltaTime;
                }
            }

            if (enemyType == EnemyType.Agresivo || isPasiveColor) {
                FollowPlayer();
                LookAtPlayerLerp();
            }

            ExplodeTimerHandler();
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
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, distanceRay)) {
            if (hit.transform.tag == "Player") {
                SetMaterialColor(Color.yellow);
            }
        }
    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.blue;
        Vector3 direction = transform.TransformDirection(Vector3.forward) * distanceRay;
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
            Explode();
        }
    }

    #region Cube explosion methods
    private void ExplodeTimerHandler() {
        if (hasExploded) {
            explodedTimer += Time.deltaTime;
            if (explodedTimer > explodedPiecesDespawnTime) {
                Destroy(this.gameObject);
            }
        }
    }

    private void Explode()
    {
        // Make object disappear
        hasExploded = true;

        // Disable components
        GetComponent<MeshRenderer>().enabled = false;
        GetComponent<BoxCollider>().enabled = false;
        GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;

        // Loop 3 times to create 5x5x5 pieces in x,y,z coordinates
        for (int x = 0; x < cubesInRow; x++)
        {
            for (int y = 0; y < cubesInRow; y++)
            {
                for (int z = 0; z < cubesInRow; z++)
                {
                    CreatePiece(x, y, z);
                }
            }
        }

        // Get explosion position
        Vector3 explosionPos = transform.position;
        // Get colliders in that position and radius
        Collider[] colliders = Physics.OverlapSphere(explosionPos, explosionRadius);
        // Add explosion force to all colliders in that overlap sphere
        foreach (Collider hit in colliders)
        {
            // Get rigidbody from collider object
            Rigidbody rb = hit.GetComponent<Rigidbody>();
            if (rb != null)
            {
                // Add explosion force to this body with given parameters
                rb.AddExplosionForce(explosionForce, transform.position, explosionRadius, explosionUpward);
            }
        }
    }

    private void CreatePiece(int x, int y, int z)
    {
        // Create piece
        GameObject piece = GameObject.CreatePrimitive(PrimitiveType.Cube);
        // Make child piece's
        piece.transform.parent = transform;

        // Set piece position and scale
        piece.transform.position = transform.position + new Vector3(cubeSize * x, cubeSize * y, cubeSize * z) - cubesPivot;
        piece.transform.localScale = new Vector3(cubeSize, cubeSize, cubeSize);

        // Add color
        piece.GetComponent<Renderer>().material.SetColor("_Color", GetMaterialColor());

        // Add rigidbody and set mass
        piece.AddComponent<Rigidbody>();
        piece.GetComponent<Rigidbody>().mass = cubeSize;
    }
    #endregion

}
