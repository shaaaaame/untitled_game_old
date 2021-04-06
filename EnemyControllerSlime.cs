using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEditor.Android;
using UnityEngine;

public class EnemyControllerSlime : MonoBehaviour
{
    public Enemy enemyObject;
    public GameObject player;
    public Animator anim;
    public Rigidbody rb;
    public GameObject deathEffectPrefab;

    public int health;
    public int damage;
    public float speed;

    public float verticalJumpForce;
    public float horizontalJumpForce;
    public float knockbackForce;

    float nextDamageTime;

    int groundLayerIndex = 9;
    int playerLayerIndex = 10;

    private void Awake()
    {
        GetComponent<SpriteRenderer>().sprite = enemyObject.sprite;
        player = FindObjectOfType<PlayerStats>().gameObject;
        this.damage = enemyObject.enemyDamage;
        this.health = enemyObject.enemyHealth;
        this.speed = enemyObject.enemySpeed;
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.layer == groundLayerIndex)
        {
            rb.velocity = new Vector3(0, 0, 0);
            StartCoroutine(WaitForJump(0.1f));
        } else if (collision.gameObject.layer == playerLayerIndex)
        {
            if (Time.time > nextDamageTime)
            {
                collision.gameObject.GetComponent<PlayerStats>().TakeDamage(damage);
                collision.gameObject.GetComponent<Rigidbody>().AddForce(new Vector3(knockbackForce, knockbackForce/3, 0), ForceMode.Impulse);
                nextDamageTime = Time.time + 1.5f;
            }
        }
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        var deathEffect  = Instantiate(deathEffectPrefab);
        deathEffect.transform.position = new Vector3(transform.position.x, transform.position.y + 0.4f, transform.position.z);
        StartCoroutine(WaitForDestroy(1f, deathEffect));
        Destroy(gameObject);
        Debug.Log("You have killed : " + enemyObject.name);
    }

    void Jump()
    {
        Vector3 dir;

        if (player.transform.position.x - gameObject.transform.position.x < 0)
        {
            if (horizontalJumpForce > 0)
            {
                horizontalJumpForce = -horizontalJumpForce;
            }

            gameObject.transform.localScale = new Vector3(-1, transform.localScale.y, transform.localScale.z);
        } else
        {
            if (horizontalJumpForce > 0)
            {
                horizontalJumpForce = -horizontalJumpForce;
            }
            horizontalJumpForce = -1 * horizontalJumpForce;
            gameObject.transform.localScale = new Vector3(1, transform.localScale.y, transform.localScale.z);
        }
        dir = new Vector3(horizontalJumpForce * rb.mass, verticalJumpForce * rb.mass, 0);

        rb.AddForce(dir);
    }

    IEnumerator WaitForJump(float seconds)
    {
        anim.SetTrigger("Landing");
        yield return new WaitForSeconds(seconds);
        anim.SetTrigger("Jump");

        Jump();
    }

    IEnumerator WaitForDestroy(float seconds, GameObject deathEffect)
    {
        yield return new WaitForSeconds(seconds);
        Destroy(deathEffect);
    }
}
