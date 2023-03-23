using TMPro;
using UnityEngine;

namespace SupremacyKingdom
{
    public class HUDController : MonoBehaviour
    {
        [SerializeField] private TMP_Text _score;

        private void OnEnable() => GameManager.ScoreChange += InstantiateScore;

        private void OnDisable() => GameManager.ScoreChange -= InstantiateScore;

        private void InstantiateScore(int score)
        {
            _score.text = $"Score: {GameManager.instance.Score}";
        }

    }
}
