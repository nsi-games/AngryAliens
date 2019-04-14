using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AngryAliens
{
    public class Health : MonoBehaviour
    {
        public float maxHealth = 100f;
        public float damage = 50f;
        public float painThreshold = 4f;

        public Sprite[] damageSprites;
        public GameObject deathEffect;
        public SpriteRenderer rend;

        private float health = 100f;
        private int spriteIndex = 0;

        void OnCollisionEnter2D(Collision2D col)
        {
            // Has the relative velocity reached the pain threshold?
            if (col.relativeVelocity.magnitude > painThreshold)
            {
                // Deal damage with the script's stored damage
                DealDamage(damage);
            }
        }

        private void Start()
        {
            health = maxHealth;
        }
        private void Die()
        {
            // Spawn death effect (particles)
            Instantiate(deathEffect, transform.position, Quaternion.identity);
            // Destroy self
            Destroy(gameObject);
        }

        public void DealDamage(float damage)
        {
            // Reduce health by damage
            health -= damage;

            // Get percentage of health
            float percentage = health / maxHealth;
            // Use percentage to get index
            spriteIndex = (int)Mathf.Lerp(0, damageSprites.Length, percentage);
            // Update renderer's sprite to correspond with damage
            rend.sprite = damageSprites[spriteIndex];

            // Is there no more health?
            if (health <= 0)
            {
                // Just die.
                Die();
            }
        }
    }
}