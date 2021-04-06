using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public int maxHealth = 5;
    public int health = 5;
    public int mana = 100;
    public int damage = 1;

    public Healthbar healthbar;
    public SpriteRenderer spriteRenderer;

    public Color red;
    public Color white;

    private void Awake()
    {
        healthbar = GameObject.Find("Healthbar").GetComponent<Healthbar>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Start()
    {
        health = maxHealth;
        healthbar.SetMaxHealth(maxHealth);
    }

    private void Update()
    {
        healthbar.SetHealth(health);

        if (spriteRenderer.color != white)
        {
            spriteRenderer.color = Color.Lerp(spriteRenderer.color, white, 2 * Time.deltaTime);
        }
    }

    public void TakeDamage(int damage)
    {
        health = health - damage;
        spriteRenderer.color = red;
        Debug.Log("Taken damage! Current health : " + health);
        
    }

}
