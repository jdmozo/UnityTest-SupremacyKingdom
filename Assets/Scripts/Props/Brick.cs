using TMPro;
using UnityEngine;

namespace SupremacyKingdom
{
    public class Brick : MonoBehaviour
    {
        public float Health = 70f;
        [SerializeField] private GameObject _textPrefab;

        private void OnEnable() => GameManager.ScoreChange += InstantiateScore;

        private void OnDisable() => GameManager.ScoreChange -= InstantiateScore;

        private void OnCollisionEnter2D(Collision2D col)
        {
            if (col.gameObject.GetComponent<Rigidbody2D>() == null) return;

            float damage = col.gameObject.GetComponent<Rigidbody2D>().velocity.magnitude * 10;

            if (damage >= 10)
            {
                GetComponent<AudioSource>().Play();
                GameManager.instance.AddScore(25);
            }

            Health -= damage;

            if (Health <= 0) 
            {
                Destroy(this.gameObject);
                GameManager.instance.AddScore(50);
            }
        }

        private void InstantiateScore(int score)
        {
            GameObject temp = Instantiate(_textPrefab);
            temp.transform.position = gameObject.transform.position;
            temp.GetComponent<TextMeshPro>().text = $"{score}";
        }

    }
}