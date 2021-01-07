using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthBar : MonoBehaviour
{
    public Slider slider;
    public Gradient gradient;
    public Image fill;

    public Transform enemy;
    public EnemyController enemyControl;

    void Start()
    {

    }

    private void Update()
    {
        if (enemyControl.enemyCurrentHealth > 0)
        {
            transform.position = enemy.position + transform.up * 2f;
        }
        else
        {
            Destroy(gameObject);
        }
        
    }

    public void SetEnemyMaxHealth(int maxHealth)
    {
        slider.maxValue = maxHealth;
        slider.value = maxHealth;
        fill.color = gradient.Evaluate(1f);
    }

    public void SetEnemyHealth(int health)
    {
        slider.value = health;
        fill.color = gradient.Evaluate(slider.normalizedValue);
    }
}
