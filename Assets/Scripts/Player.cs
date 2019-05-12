using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    float DeltaX = 0f;
    float DeltaY = 0f;
    float MaxX=0f;
    float MaxY=0f;
    float MinY=0f;
    float MinX=0f;
    bool pressed = false;
    [SerializeField] float speed = 6f;
    [SerializeField] float padding = 1f;
    [SerializeField] float projectileSpeed = 5f;
    [SerializeField] GameObject LaserPrefab = null;
    [SerializeField] float projectileFiringPeriod = 0.2f;
    [SerializeField] float Health = 200f;
    [SerializeField] AudioClip playerFiring = null;
    [SerializeField] AudioClip playerDeath = null;
    // Start is called before the first frame update


    Coroutine fireCoroutine;

    void Start()
    {
        SetUpMovingBoundaries();
        Level level = FindObjectOfType<Level>();
        if(level)
        {
            level.DisplayHealth();
        }
    }

    public float getHealth()
    {
        return Health;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        DamageDealer damageDealer = collision.gameObject.GetComponent<DamageDealer>();
        Health -= damageDealer.GetDamage();
        damageDealer.Hit();
        Level level = FindObjectOfType<Level>();

        if (Health <= 0)
        {
            Health = 0f;
            AudioSource.PlayClipAtPoint(playerDeath, transform.position);
            Destroy(gameObject);
            FindObjectOfType<Level>().LoadOver();
        }

        if (level)
        {
            level.DisplayHealth();
        }
    }

    private void SetUpMovingBoundaries()
    {
        Camera gameCamera = Camera.main;
        MinX = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).x + padding;
        MaxX = gameCamera.ViewportToWorldPoint(new Vector3(1, 0, 0)).x - padding;
        MinY = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).y + padding;
        MaxY = gameCamera.ViewportToWorldPoint(new Vector3(0, 1, 0)).y -padding;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.anyKey)
        {
            DeltaX = Input.GetAxis("Horizontal")*Time.deltaTime*speed;
            DeltaY = Input.GetAxis("Vertical")*Time.deltaTime*speed;
            Move(DeltaX,DeltaY);
        }
        if(Input.GetButtonDown("Fire1") && !pressed)
        {
            fireCoroutine =StartCoroutine(Fire());
            pressed = true;
        }
        if(Input.GetButtonUp("Fire1"))
        {
            StopCoroutine(fireCoroutine);
            pressed = false;
        }
    }

    IEnumerator Fire()
    {
        while (true)
        {
            GameObject Laser = Instantiate(LaserPrefab, transform.position, Quaternion.identity) as GameObject;
            AudioSource.PlayClipAtPoint(playerFiring, transform.position);
            Rigidbody2D rigidbody = Laser.GetComponent<Rigidbody2D>();
            rigidbody.velocity = new Vector2(0, projectileSpeed);
            yield return new WaitForSeconds(projectileFiringPeriod);
        }
    }

    private void Move(float DeltaX, float DeltaY)
    {
        float newX = Mathf.Clamp(transform.position.x + DeltaX, MinX, MaxX);
        float newY = Mathf.Clamp(transform.position.y + DeltaY, MinY, MaxY);
        transform.position = new Vector2(newX, newY);
    }
}
