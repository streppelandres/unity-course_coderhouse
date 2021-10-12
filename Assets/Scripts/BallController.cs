using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
    public const int MAX_LIFE = 100;
    public const int MIN_LIFE = 1;

    public float speed = 100.0f;
    public int life = 50;
    private Vector3 position = new Vector3(0, 1, 0);

    void Start()
    {
        Debug.Log($"Vida antes de modificar {this.life}");
        this.ChangeLife(-15); // Da�o la vida
        this.ChangeLife(15); // Curo la vida

        this.ChangeLife(-(MAX_LIFE * 9)); // Me paso del limite
        this.ChangeLife(MAX_LIFE * 9); // Me paso del limite

        this.MovementHandler(new Vector3(0, 15, 0));
    }

    void Update()
    {
        
    }

    // M�todo que cura/da�a al jugador
    public void ChangeLife(int lifePoints) {
        if (this.life + lifePoints >= MIN_LIFE && this.life + lifePoints <= MAX_LIFE)
        {
            this.life += lifePoints;
            Debug.Log($"Se modific� la vida por [{lifePoints}] puntos, la nueva vida es de [{this.life}]");
        }
        else
        {
            Debug.Log($"No se puede moficiar la vida fuera del m�nimo [{MIN_LIFE}] y el m�ximo [{MAX_LIFE}]");
        }
    }

    // No s� bien a que se refiere la ppt
    // M�todo que controla el movimiento
    public void MovementHandler(Vector3 newPosition)
    {
        transform.position += (newPosition * this.speed * Time.deltaTime);
    }
}
