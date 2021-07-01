using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class portalSpawner : MonoBehaviour
{
    // main attack stats
    [Header("attack stats")]
    public int damage;
    public float attackSpeed;

    // defining the region the attack has
    [Header("attack region")]
    public float attackRangeX;
    public float attackRangeY;
    public LayerMask target;

    // defining values for spawning
    public int maxEnemies;
    public float startInterval;
    public float intervalDecrease;
    public float minInterval;

    // all enemies;
    public GameObject zombie;
    public GameObject witch;
    public GameObject devil;
    public GameObject clown;

    // private variables for the engine
    private float attackSpeedTimer;
    private float currentEnemies;
    private float intervalTimer;
    private int totalSpawns;
    private GameObject[] enemies;

    // Start is called before the first frame update
    void Start()
    {
        intervalTimer = 3;
        currentEnemies = 0;

        enemies = new GameObject[] { zombie, witch, devil, clown };
    }

    // Update is called once per frame
    void Update()
    {
        // checking if its possible to spawn an enemy
        if (intervalTimer <= 0 && currentEnemies < maxEnemies)
        {
            // spawning the enemy
            Instantiate(enemies[Random.Range(0, 4)], transform.position, Quaternion.identity);

            currentEnemies++;
            totalSpawns++;

            // decreasing the timer
            if (startInterval - (totalSpawns * intervalDecrease) <= minInterval)
            {
                intervalTimer = minInterval;
            }
            else
            {
                intervalTimer = startInterval - (totalSpawns * intervalDecrease);
            }
        }
        else
        {
            if (intervalTimer > 0)
            {
                intervalTimer -= Time.deltaTime;
            }
        }

        // checking if portal damage is available
        if (attackSpeedTimer <= 0.0f)
        {
            // checking for the target
            Collider2D checkForTarget = Physics2D.OverlapBox(transform.position, new Vector2(attackRangeX, attackRangeY), 0, target);
            if (checkForTarget)
            {
                // if target is found then deal damage 
                checkForTarget.GetComponent<playerController>().OnDamage(damage);
            }

            attackSpeedTimer = attackSpeed;
        }
        else
        {
            attackSpeedTimer -= Time.deltaTime;
        }

        // rotating the portal
        transform.Rotate(Vector3.forward * -15 * Time.deltaTime);
    }

    public void onEnemyKilled()
    {
        currentEnemies--;
    }

    // this will draw the attack region in the editor
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, new Vector2(attackRangeX, attackRangeY));
    }
}
