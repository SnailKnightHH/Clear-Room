using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class turretHealthBar : MonoBehaviour
{
    public Slider slider;
    public Gradient gradient;
    public Image fill;
    public Transform turret;
    public SpriteRenderer turretSpriteRenderer;
    public CanvasGroup canvasGroup;


    void Start()
    {
        canvasGroup.alpha = 0;
    }

    private void Update()
    {
        if (turret != null)
        {
            if(turretSpriteRenderer.enabled == true)
            {
                canvasGroup.alpha = 1;
                transform.position = turret.position + transform.up * 3f;
            }
            else
            {
                canvasGroup.alpha = 0;
            }
            
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
