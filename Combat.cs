using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Net.Mail;
using System.Threading;
using TMPro;
using UnityEditor;
using UnityEngine;

public class Combat : MonoBehaviour
{
    Animator anim;
    public GameObject shield;
    public GameObject disableEffectPrefab;
    public PlayerStats stats;

    public Transform attackPoint;
    public Vector3 upAttackPointPos;
    public float attackRange = 0.5f;
    public LayerMask enemyLayers;
    public float attackRate = 2f;
    float nextAttackTime;

    public bool canAttack = false;

    public static bool inCombat;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        if (Time.time >= nextAttackTime)
        {
            canAttack = true;
            Combat.inCombat = false;
        }

        if (PlayerInput.lookingUp)
        {
            attackPoint.position = transform.position + upAttackPointPos;
        }
        else if (CharacterController.FacingRight)
        {
            attackPoint.position = new Vector3(1, 0, 0) + transform.position;
        }
        else
        {
            attackPoint.position = new Vector3(-1, 0, 0) + transform.position;
        }

    }

    public void Attack()
    {
        PlayerInput.lockPos = true;
        inCombat = true;



        if (canAttack)
        {
            if (!PlayerInput.lookingUp)
            {
                anim.SetTrigger("Attack");
                shield.GetComponent<Animator>().SetTrigger("Attack");
            }
            else
            {
                anim.SetTrigger("UpAttack");
                shield.GetComponent<Animator>().SetTrigger("UpAttack");
            }


            nextAttackTime = Time.time + 1f / attackRate;

            Collider[] hitEnemies = Physics.OverlapSphere(attackPoint.position, attackRange, enemyLayers);

            foreach (Collider enemy in hitEnemies)
            {
                if (enemy.tag == "Slime")
                {
                    enemy.GetComponent<EnemyControllerSlime>().TakeDamage(stats.damage);
                }
            }
        }
        canAttack = false;
        StartCoroutine(UnlockMovement(0.3f)); // length of time of the shield attack animation
    }

    public void SummonShield()
    {
        if (shield.activeSelf)
        {
            shield.SetActive(false);
            var disableEffect = Instantiate(disableEffectPrefab);
            disableEffect.transform.position = shield.transform.position;
        }
        else shield.SetActive(true);
    }

    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null) return;

        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }

    IEnumerator UnlockMovement(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        PlayerInput.lockPos = false;
    }
}
