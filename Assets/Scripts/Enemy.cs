using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    [SerializeField] float health = 100;
    [SerializeField] float ShotCounter;
    [SerializeField] float minTimeBetweenShots = 0.2f;
    [SerializeField] float maxTimeBetweenShots = 3f;
    [SerializeField] float ProjectileSpeed = 10f;
    [SerializeField] float Points = 100;
    [SerializeField] GameObject Projectile = null;
    [SerializeField] GameObject DeathVFX = null;
    [SerializeField] AudioClip enemyDeath = null;
    [SerializeField] AudioClip enemyFiring = null;
    // Start is called before the first frame update
    void Start()
    {
        ShotCounter = UnityEngine.Random.Range(minTimeBetweenShots, maxTimeBetweenShots);
    }

    // Update is called once per frame
    void Update()
    {
        CountdownAndShoot();
    }

    private void CountdownAndShoot()
    {
        ShotCounter -= Time.deltaTime;
        if(ShotCounter <= 0)
        {
            Fire();
            ShotCounter = UnityEngine.Random.Range(minTimeBetweenShots, maxTimeBetweenShots);
        }
    }

    private void Fire()
    {
        GameObject laser = Instantiate(Projectile, transform.position, Quaternion.identity) as GameObject;
        laser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -ProjectileSpeed);
        AudioSource.PlayClipAtPoint(enemyFiring, transform.position);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        DamageDealer damageDealer = collision.gameObject.GetComponent<DamageDealer>();
        if (damageDealer)
        {
            health -= damageDealer.GetDamage();
            damageDealer.Hit();
        }
        if(health <=0)
        {
            AudioSource.PlayClipAtPoint(enemyDeath,transform.position);
            Destroy(gameObject);
            GameObject particle = Instantiate(DeathVFX, transform.position,transform.rotation);
            Level level = FindObjectOfType<Level>();
            if(level)
            {
                level.IncreaseScore(Points);
            }
            Destroy(particle, 1f);
        }
    }
}
