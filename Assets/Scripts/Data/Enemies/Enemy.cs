using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Enemy", menuName = "ScripteableData/Enemy")]
public class Enemy : ScriptableObject
{
    [SerializeField] private EnemyType enemyType;
    [SerializeField] private float speed = 10f;
    [SerializeField] private float maxLife = 100f;
    [SerializeField] private Color color;
    [SerializeField] private float lookDistance = 30f;

    public EnemyType EnemyType { get => enemyType; }
    public float Speed { get => speed; }
    public float MaxLife { get => maxLife; }
    public Color Color { get => color;}
    public float LookDistance { get => lookDistance; }
}
