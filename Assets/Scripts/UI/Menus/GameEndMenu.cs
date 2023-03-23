using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace SupremacyKingdom
{
    /// <summary>
    /// To do
    /// </summary>
    public class GameEndMenu : MonoBehaviour
    {
        [SerializeField] private GameObject _menuContainer;
        [SerializeField] private TMP_Text title;
        [SerializeField] private TMP_Text score;
        [SerializeField] private Button _buttonReset;

        [Header("Win Message")]
        [SerializeField][TextArea(1, 2)] private string titleWin;
        [Header("Lose Message")]
        [SerializeField][TextArea(1, 2)] private string titleLose;

        private void OnEnable() => GameManager.OnGameEnd += GameEnd;

        private void OnDisable() => GameManager.OnGameEnd -= GameEnd;

        private void Start() => _buttonReset.onClick.AddListener(() => SceneManager.LoadScene(0));

        private void GameEnd(bool status)
        {
            score.text = $"{GameManager.instance.Score} pts";
            _menuContainer.SetActive(true);

            if (status)
                title.text = titleWin;
            else
                title.text = titleLose;
        }
    }
}
