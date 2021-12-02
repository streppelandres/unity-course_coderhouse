using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Enemy", menuName = "Enemy")]
public class Enemy : ScriptableObject
{
    [SerializeField] private EnemyType enemyType;
    [SerializeField] private float speed = 10f;
    [SerializeField] private float life = 100f;
    [SerializeField] private Color color;
    [SerializeField] private float lookDistance = 30f;

    public EnemyType EnemyType { get => enemyType; set => enemyType = value; }
    public float Speed { get => speed; set => speed = value; }
    public float Life { get => life; set => life = value; }
    public Color Color { get => color; set => color = value; }
    public float LookDistance { get => lookDistance; set => lookDistance = value; }
}
