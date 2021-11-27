using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieHealth : MonoBehaviour
{
    [SerializeField]
    private int startingHealth = 30;
    [SerializeField]
    AudioSource zombiehit;

    private int currentHealth;
    int destroyed = 1;

    public void OnEnable()
    {
        currentHealth = startingHealth;
    }

    public void Awake()
    {

    }
    public int getHealth()
    {
        return startingHealth;
    }
    public void TakeDamage(int damageAmount)
    {
        zombiehit.Play();
        currentHealth -= damageAmount;
        if (currentHealth <= 0)
        {
            die();
        }
    }
    public void die()
    {
        Destroy(gameObject);
    }

}
