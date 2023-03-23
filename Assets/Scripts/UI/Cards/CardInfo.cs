using UnityEngine;

namespace SupremacyKingdom
{
    [CreateAssetMenu(fileName = "newBirdInfo", menuName = "Card/BirdInfo", order = 2)]
    public class CardInfo : ScriptableObject
    {
        public string birdName;
        public Color cardColor = new Color(255,255,255);
        public Sprite birdImage;
        public GameObject birdPrefab;
    }
}
