using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Enemy")]
    [SerializeField] int health = 100;
    [SerializeField] int gimmePoints = 100;
    [SerializeField] GameObject explosionFX;
    [SerializeField] float explosionDuration;
    [SerializeField] AudioClip explosionSound;
    [SerializeField] [Range(0, 1)] float explosionSoundVolume = 0.7f;

    [Header("Enemy Projectile")]
    [SerializeField] float shotCounter;
    [SerializeField] float minTimeBetweenShots = 3f;
    [SerializeField] float maxTimeBetweenShots = 8f;
    [SerializeField] float projectileSpeed;
    [SerializeField] GameObject projectile;
    [SerializeField] AudioClip laserSound;
    [SerializeField] [Range(0,1)] float laserSoundVolume = 0.08f;


    // Start is called before the first frame update
    void Start()
    {
        shotCounter = Random.Range(minTimeBetweenShots, maxTimeBetweenShots);
    }


    // Update is called once per frame
    void Update()
    {
        CountDownAndShoot();
        DieOut();
    }


    // Enemy firerate
    private void CountDownAndShoot()
    {
        shotCounter -= Time.deltaTime;

        if (shotCounter <= 0f)
        {
            Fire();
            shotCounter = Random.Range(minTimeBetweenShots, maxTimeBetweenShots);
        }
    }


    // Enemy shooting
    private void Fire()
    {
        GameObject laser = Instantiate(projectile, transform.position, Quaternion.identity) as GameObject;
        laser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -projectileSpeed);
        AudioSource.PlayClipAtPoint(laserSound, Camera.main.transform.position, laserSoundVolume);
    }


    // Method to deal with enemy colliding with player projectiles
    private void OnTriggerEnter2D(Collider2D collider)
    {
        // Get what collided with this enemy
        DamageDealer damageDealer = collider.gameObject.GetComponent<DamageDealer>();

        // Checks if it has a DamageDealer component
        if (damageDealer)
        {
            // Process damage
            ProcessHit(damageDealer);
        }
    }


    // Method to process every hit an enemy takes
    private void ProcessHit(DamageDealer damageDealer)
    {
        // Damage gameObject
        health -= damageDealer.GetDamage();

        // Destroy gameObject that collided
        damageDealer.Hit();

        // Destroy this gameObject if it has no health
        if (health <= 0)
        {
            // Calls death ripper
            DieBitch();
        }
    }


    // Method do trigger death visual effect and destroy gameObject
    private void DieBitch()
    {
        // Kill the bastard
        Destroy(gameObject);

        // Instanciate explosion particles
        GameObject explosion = Instantiate(explosionFX, transform.position, transform.rotation);

        // Destroy explosion particles
        Destroy(explosion, explosionDuration);

        // Death dound
        AudioSource.PlayClipAtPoint(explosionSound, Camera.main.transform.position, explosionSoundVolume);

        // Add points to score
        FindObjectOfType<GameSession>().AddToScore(gimmePoints);
    }


    // Method to destroy if out of scren
    private void DieOut()
    {
        if (this.GetComponent<Pathing>().GetMustDie())
        {
            Destroy(gameObject);
        }
    }
}
