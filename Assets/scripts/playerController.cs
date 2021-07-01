using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class playerController : MonoBehaviour
{
    // main player values
    [Header("player stats")]
    public int speed;
    public int jumpPower;

    // ground check values (to prevent infinite jump)
    [Header("ground check ")]
    public LayerMask ground;
    public Transform groundCheck;
    public float checkRadius;

    // health and regen
    public int initialHealth;
    public int regenAmount;
    public float regenInterval;
    public Slider healthBar;

    // private variables for the engine
    private Animator anim;
    private float moveInput;
    private Rigidbody2D rb;
    private bool isGrounded;
    private int health;
    private float regenIntervalTimer;

    // Start is called before the first frame update
    void Start()
    {
        // assigning the animator component
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        health = initialHealth;
        regenIntervalTimer = regenInterval;
    }

    // fixed update for walking, using getaxis and checking the ground so the player does not have infinite jump
    void FixedUpdate()
    {
        // getting movement input from horizontal axis (wasd and arrow keys work)
        moveInput = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(moveInput * speed, rb.velocity.y);

        //checking the ground
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, ground);
    }

    void Update()
    {
        // killing the player if health drops below 0 (or equals 0)
        if(health <= 0)
        {
            SceneManager.LoadScene("endScreen");
        }

        // jumping
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            // setting take of animation to trigger
            anim.SetTrigger("takeOf");
            rb.velocity = Vector2.up * jumpPower;
        }

        // regenerating the players health
        if (regenIntervalTimer <= 0)
        {
            if (health + regenAmount <= initialHealth)
            {
                health += regenAmount;
            }
            else{
                health = initialHealth;
            }
            regenIntervalTimer = regenInterval;
        }
        else
        {
            regenIntervalTimer -= Time.deltaTime;
        }

        //displaying health on the healthbar
        healthBar.value = (float)health / (float)initialHealth * 100.0f;

        // updating the animations
        // walking and rotating in the right direction
        if (moveInput > 0)
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
            anim.SetBool("isRunning", true);
        }
        else if (moveInput < 0)
        {
            transform.eulerAngles = new Vector3(0, 180, 0);
            anim.SetBool("isRunning", true);
        }
        else if (moveInput == 0)
        {
            anim.SetBool("isRunning", false);
        }

        // jump animations
        if (isGrounded)
        {
            anim.SetBool("isJumping", false);
        }
        else
        {
            anim.SetBool("isJumping", true);
        }
    }

    // this is fired when the player takes damage
    public void OnDamage(int damage)
    {
        health -= damage;
    }
}
