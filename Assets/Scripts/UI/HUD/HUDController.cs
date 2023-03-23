using TMPro;
using UnityEngine;

namespace SupremacyKingdom
{
    /// <summary>
    /// To do
    /// </summary>
    public class HUDController : MonoBehaviour
    {
        [SerializeField] private TMP_Text _score;
        [SerializeField] private GameObject _addScorePrefab;
        [SerializeField] private Transform _scoreParent;

        private void OnEnable() => GameManager.ScoreChange += InstantiateScore;

        private void OnDisable() => GameManager.ScoreChange -= InstantiateScore;

        private void InstantiateScore(int score)
        {
            _score.text = $"Score: {GameManager.instance.Score}";

            GameObject temp = Instantiate(_addScorePrefab, _scoreParent);
            temp.GetComponent<TextMeshPro>().text = $"{score}";
        }

    }
}
