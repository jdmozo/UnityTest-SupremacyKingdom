﻿using UnityEngine;

namespace SupremacyKingdom
{
    /// <summary>
    /// To do
    /// </summary>
    public class Destroyer : MonoBehaviour
    {

        void OnTriggerEnter2D(Collider2D col)
        {
            string tag = col.gameObject.tag;
            if (tag == "Bird" || tag == "Pig" || tag == "Brick")
            {
                Destroy(col.gameObject);
            }
        }
    }
}
