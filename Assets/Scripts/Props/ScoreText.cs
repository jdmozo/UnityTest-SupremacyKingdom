using UnityEngine;

namespace SupremacyKingdom
{
    /// <summary>
    /// The score which is shown when the bird collide with a pig or brick.
    /// </summary>

    public class ScoreText : MonoBehaviour
    {
        [SerializeField] private float _time = 2;
        [SerializeField] private float _distance = 2;
        void Start()
        {
            gameObject.transform.localPositionTo(_time, new Vector2(transform.position.x, transform.position.y + _distance)).setOnCompleteHandler((x) =>
            {
                x.complete();
                x.destroy();
                Destroy(gameObject);
            });
        }
    }
}
