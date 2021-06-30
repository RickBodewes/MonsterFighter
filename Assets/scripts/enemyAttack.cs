using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyAttack : MonoBehaviour
{
    // main attack stats
    [Header("attack stats")]
    public int damage;
    public int damageVariety;
    public float attackSpeed;

    // defining the region the attack has
    [Header("attack region")]
    public Transform attackPos;
    public float attackRangeX;
    public float attackRangeY;
    public LayerMask target;

    // private variables for the engine
    private float attackSpeedTimer;
    private Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        // checking if an attack is possible
        if (attackSpeedTimer <= 0.0f)
        {
            // deciding the amoount of damage to be dealt
            int attackAmount = Random.Range(damage - damageVariety, damage + damageVariety);

            // checking for the target
            Collider2D checkForTarget = Physics2D.OverlapBox(attackPos.position, new Vector2(attackRangeX, attackRangeY), 0, target);
            if (checkForTarget)
            {
                // if target is found then deal damage and trigger attack animation
                checkForTarget.GetComponent<playerController>().OnDamage(attackAmount);
                anim.SetTrigger("attack");
            }

            attackSpeedTimer = attackSpeed;
        }
        else
        {
            attackSpeedTimer -= Time.deltaTime;
        }
    }

    // this will draw the attack region in the editor
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(attackPos.position, new Vector2(attackRangeX, attackRangeY));
    }
}
