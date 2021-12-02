using UnityEngine;

/**
 *  La lógica es sacada de: https://youtu.be/s_v9JnTDCCY
 *  Le hice ligeros cambios como el color, que los pedazos sean objeto child y que despawneen en "X" tiempo
 */
public class CubeExplosion
{
    private static readonly float DespawnTimeAfterBeenDestroyed = 5f;
    private static readonly float CubeSize = 0.2f;
    private static readonly int CubesInRow = 5;
    private static readonly float ExplosionForce = 50f;
    private static readonly float ExplosionRadius = 4f;
    private static readonly float ExplosionUpward = 0.4f;

    private static Vector3 CalculatePivotDistance()
    {
        // Calculate pivot distance
        float cubesPivotDistance = CubeSize * CubesInRow / 2;
        // Use this value to create pivot vector
        return new Vector3(cubesPivotDistance, cubesPivotDistance, cubesPivotDistance);
    }

    private static void CreatePiece(int x, int y, int z, Transform targetTransform, Color targetColor, Vector3 cubesPivot)
    {
        // Create piece
        GameObject piece = GameObject.CreatePrimitive(PrimitiveType.Cube);

        // Make child piece's
        piece.transform.parent = targetTransform.transform;

        // Set piece position and scale
        piece.transform.position = targetTransform.transform.position + new Vector3(CubeSize * x, CubeSize * y, CubeSize * z) - cubesPivot;
        piece.transform.localScale = new Vector3(CubeSize, CubeSize, CubeSize);

        // Add color
        piece.GetComponent<Renderer>().material.SetColor("_Color", targetColor);

        // Add rigidbody and set mass
        piece.AddComponent<Rigidbody>();
        piece.GetComponent<Rigidbody>().mass = CubeSize;
    }

    public static bool Explode(GameObject target)
    {
        // Get pivot vector
        Vector3 cubesPivot = CalculatePivotDistance();

        // Disable components
        target.GetComponent<MeshRenderer>().enabled = false;
        target.GetComponent<BoxCollider>().enabled = false;
        target.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;

        // Get target color for child's cubes
        Color targetColor = target.GetComponent<Renderer>().material.GetColor("_Color");

        // Loop 3 times to create 5x5x5 pieces in x,y,z coordinates
        for (int x = 0; x < CubesInRow; x++)
        {
            for (int y = 0; y < CubesInRow; y++)
            {
                for (int z = 0; z < CubesInRow; z++)
                {
                    CreatePiece(x, y, z, target.transform, targetColor, cubesPivot);
                }
            }
        }

        // Get explosion position
        Vector3 explosionPos = target.transform.position;
        // Get colliders in that position and radius
        Collider[] colliders = Physics.OverlapSphere(explosionPos, ExplosionRadius);
        // Add explosion force to all colliders in that overlap sphere
        foreach (Collider hit in colliders)
        {
            // Get rigidbody from collider object
            Rigidbody rb = hit.GetComponent<Rigidbody>();
            if (rb != null)
            {
                // Add explosion force to this body with given parameters
                rb.AddExplosionForce(ExplosionForce, target.transform.position, ExplosionRadius, ExplosionUpward);
            }
        }

        // Destroy enemy
        GameObject.Destroy(target, DespawnTimeAfterBeenDestroyed);

        return true;
    }
}
