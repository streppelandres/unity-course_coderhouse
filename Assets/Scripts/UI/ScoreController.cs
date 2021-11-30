using UnityEngine;
using UnityEngine.UI;

public class ScoreController : MonoBehaviour
{
    [SerializeField] private Text scoreText;

    public void SetScore(int score) {
        scoreText.text = $"Score: {score}";
    }
}
