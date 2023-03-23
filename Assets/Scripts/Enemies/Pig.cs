using TMPro;
using UnityEngine;

namespace SupremacyKingdom
{
    /// <summary>
    /// To do
    /// </summary>
    public class Pig : MonoBehaviour
    {
        public float Health = 150f;
        public Sprite SpriteShownWhenHurt;
        private float ChangeSpriteHealth;
        [SerializeField] private bool _showText;
        [SerializeField] private GameObject _textPrefab;

        private void OnEnable() => GameManager.ScoreChange += InstantiateScore;

        private void OnDisable() => GameManager.ScoreChange -= InstantiateScore;

        private void Start() => ChangeSpriteHealth = Health - 30f;

        private void OnCollisionEnter2D(Collision2D col)
        {
            if (col.gameObject.GetComponent<Rigidbody2D>() == null) return;

            if (col.gameObject.tag == "Bird")
            {
                GetComponent<AudioSource>().Play();
                Destroy(gameObject);
                GameManager.instance.AddScore(150);
            }
            else
            {
                float damage = col.gameObject.GetComponent<Rigidbody2D>().velocity.magnitude * 10;
                Health -= damage;
                if (damage >= 10)
                    GetComponent<AudioSource>().Play();

                if (Health < ChangeSpriteHealth)
                {
                    GetComponent<SpriteRenderer>().sprite = SpriteShownWhenHurt;
                    GameManager.instance.AddScore(50);
                }

                if (Health <= 0)
                {
                    Destroy(this.gameObject);
                    GameManager.instance.AddScore(150);
                }
            }
        }

        private void InstantiateScore(int score)
        {
            if (_showText)
            {
                GameObject temp = Instantiate(_textPrefab);
                temp.transform.position = gameObject.transform.position;
                temp.GetComponent<TextMeshPro>().text = $"{score}";
            }
        }

    }
}