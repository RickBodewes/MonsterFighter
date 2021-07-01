using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyBehaviour : MonoBehaviour
{
    // main enemy stats, all enemies have the same script just different stats
    [Header("stats")]
    public int initialHealth;
    public int speed;
    // knockback stats for when the enemy is attacked
    public int knockback;
    public int knockbackHeight;

    // misc stats like the health bar object
    [Header("misc")]
    public float stoppingDistance;
    public int scoreAmount;
    public Transform healthBar;

    // private variables for the engine
    private int health;
    private Animator anim;
    private Rigidbody2D rb;
    private Transform target;
    private float distanceToTarget;

    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.Find("player").GetComponent<Transform>();
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        health = initialHealth;
    }

    void FixedUpdate()
    {
        // determining the distance to the target
        distanceToTarget = target.position.x - transform.position.x;

        // moving towards the target
        if (distanceToTarget < -stoppingDistance)
        {
            rb.velocity = new Vector2(-speed, rb.velocity.y);
        }
        else if (distanceToTarget > stoppingDistance)
        {
            rb.velocity = new Vector2(speed, rb.velocity.y);
        }
        else
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
        }
    }

    // Update is called once per frame
    void Update()
    {
        // when health drops below 0 kill the enemy (and remove)
        if(health <= 0)
        {
            // TODO: add spawn manager
            // sending the score gained to the score manager
            GameObject.Find("scoreManager").GetComponent<scoreManager>().scoreIncrease(scoreAmount);

            // destroying the game object
            Destroy(gameObject);
        }

        // activating the animation
        if (rb.velocity.x > 0 || rb.velocity.x < 0)
        {
            anim.SetBool("isRunning", true);
        }
        else
        {
            anim.SetBool("isRunning", false);
        }

        // flipping the character while also unflipping the healthbar
        if (distanceToTarget > 0)
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
            healthBar.eulerAngles = new Vector3(0, 0, 0);
            healthBar.localPosition = new Vector2(-1.5f, 4);
        }
        else if (distanceToTarget < 0)
        {
            transform.eulerAngles = new Vector3(0, 180, 0);
            healthBar.eulerAngles = new Vector3(0, 0, 0);
            healthBar.localPosition = new Vector2(1.5f, 4);
        }
    }

    // when damage is taken this function is called (see playerAttack)
    public void OnDamage(int damage)
    {
        // dealing the damage and updating the healthbar
        health -= damage;
        healthBar.localScale = new Vector3((float) health / (float)initialHealth * 0.5f, 0.5f, 1);

        // dealing knocback
        if (distanceToTarget > 0)
        {
            rb.velocity = new Vector2(-knockback, knockbackHeight);
        }
        else if (distanceToTarget < 0)
        {
            rb.velocity = new Vector2(knockback, knockbackHeight);
        }
    }
}
