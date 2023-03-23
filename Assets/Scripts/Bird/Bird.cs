using UnityEngine;
using System.Collections;

namespace SupremacyKingdom
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Bird : MonoBehaviour
    {
        public BirdState State { get; private set; }
        [SerializeField] private float explotionDuration = 1f;
        [SerializeField] private GameObject explosionPrefab;

        public void OnBirdShoot()
        {
            GetComponent<AudioSource>().Play(); GetComponent<TrailRenderer>().enabled = true; 
            GetComponent<Rigidbody2D>().isKinematic = false; 
            GetComponent<CircleCollider2D>().radius = Constants.BirdColliderRadiusNormal; 
            State = BirdState.Thrown;
        }

        IEnumerator DestroyAfter(float seconds)
        { 
            yield return new WaitForSeconds(seconds);
            Destroy(gameObject);
        }

        private void Update()
        {
            if (State == BirdState.Thrown && GetComponent<Rigidbody2D>().velocity.sqrMagnitude <= Constants.Min_Velocity)
                StartCoroutine(DestroyAfter(2));

            if (State == BirdState.Thrown && Input.GetMouseButtonUp(0) && GameManager.instance.slingshot.slingshotState == SlingshotState.BirdFlying)
                Explotion();
        }

        private void Start()
        {
            GetComponent<TrailRenderer>().enabled = false;
            GetComponent<TrailRenderer>().sortingLayerName = "Foreground";
            GetComponent<Rigidbody2D>().isKinematic = true;
            GetComponent<CircleCollider2D>().radius = Constants.Bird_Collider_Radius_Big;
            State = BirdState.BeforeThrown;
        }

        private void Explotion()
        {
            Instantiate(explosionPrefab, transform.position, Quaternion.identity);
            GetComponent<CircleCollider2D>().radius = Constants.Bird_Collider_Radius_Explotion;
            StartCoroutine(DestroyAfter(0));
        }
    }
}