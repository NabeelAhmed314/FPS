using UnityEngine;

public class Target : MonoBehaviour
{
    public float health = 50f;
    public void takeDamage(float amount)
    {
        health -= amount;
        if(health <= 0f)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log("Destroyed : "  + transform.name);
        Destroy(gameObject);
    }
    
}
