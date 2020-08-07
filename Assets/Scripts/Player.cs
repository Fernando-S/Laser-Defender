using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Player")]
    public float moveSpeed = 10f;
    [SerializeField] int health = 100;
    [SerializeField] GameObject explosionFX;
    [SerializeField] float explosionDuration;
    [SerializeField] AudioClip explosionSound;
    [SerializeField] [Range(0, 1)] float explosionSoundVolume = 0.7f;


    [Header("Projectile")]
    [SerializeField] float projectileSpeed = 20f;
    [SerializeField] float projectileFiringCooldown = 0.1f;
    [SerializeField] GameObject laserPrefab;
    [SerializeField] AudioClip laserSound;
    [SerializeField] [Range(0, 1)] float laserSoundVolume = 0.08f;


    float xMin, xMax, yMin, yMax;
    SpriteRenderer spriteRenderer;
    Coroutine firingCoroutine;
    

    // Start is called before the first frame update
    void Start()
    {
        SetUpMoveBoundaries();
    }


    // Update is called once per frame
    void Update()
    {
        PlayerMovement();
        ImFiringMyLaser();
    }


    // Player movement control
    private void PlayerMovement()
    {
        var deltaX = Input.GetAxis("Horizontal") * Time.deltaTime * moveSpeed;
        var newXPos = Mathf.Clamp(transform.position.x + deltaX, xMin, xMax);
        var deltaY = Input.GetAxis("Vertical") * Time.deltaTime * moveSpeed;
        var newYPos = Mathf.Clamp(transform.position.y + deltaY, yMin, yMax);

        transform.position = new Vector2(newXPos, newYPos);
    }


    // Limiting player movement in screen
    private void SetUpMoveBoundaries()
    {
        Camera gameCamera = Camera.main;
        xMin = gameCamera.ViewportToWorldPoint(new Vector3(0,0,0)).x + GetComponent<SpriteRenderer>().size.x / 2;
        xMax = gameCamera.ViewportToWorldPoint(new Vector3(1,0,0)).x - GetComponent<SpriteRenderer>().size.x / 2;
        yMin = gameCamera.ViewportToWorldPoint(new Vector3(0,0,0)).y + GetComponent<SpriteRenderer>().size.y / 2;
        yMax = gameCamera.ViewportToWorldPoint(new Vector3(0,1,0)).y - GetComponent<SpriteRenderer>().size.y / 2;
    }


    // Controls player fire
    private void ImFiringMyLaser()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            firingCoroutine = StartCoroutine(FiringContinuously());
        }
        if (Input.GetButtonUp("Fire1"))
        {
            StopCoroutine(firingCoroutine);
        }
    }


    // Cooldown for firerate
    IEnumerator FiringContinuously()
    {
        while (true)
        {
            GameObject laser = Instantiate(laserPrefab, transform.position, Quaternion.identity) as GameObject;
            laser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, projectileSpeed);
            AudioSource.PlayClipAtPoint(laserSound, Camera.main.transform.position, laserSoundVolume);

            yield return new WaitForSeconds(projectileFiringCooldown);
        }
        
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


    // Method to process every hit the player takes
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

        // Loading Game Over scene
        FindObjectOfType<LevelManager>().LoadGameOver();
    }


    // Getters and Setters
    public int GetHealth(){
        return health;
    }
}
