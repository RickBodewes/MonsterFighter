using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerAttack : MonoBehaviour
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
    public LayerMask enemy;

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
            // when K is pressed go for an attack
            if (Input.GetKeyDown(KeyCode.K))
            {
                // deciding the amoount of damage to be dealt
                int attackAmount = Random.Range(damage - damageVariety, damage + damageVariety);

                // checking for enemies in the attack region and adding them to an array
                Collider2D[] enemiesToDamage = Physics2D.OverlapBoxAll(attackPos.position, new Vector2(attackRangeX, attackRangeY), 0.0f, enemy);

                // looping trough all enemies and dealing the damage
                for (int i = 0; i < enemiesToDamage.Length; i++)
                {
                    enemiesToDamage[i].GetComponent<enemyBehaviour>().OnDamage(attackAmount);
                }

                anim.SetTrigger("attack");
                attackSpeedTimer = attackSpeed;
            }
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
