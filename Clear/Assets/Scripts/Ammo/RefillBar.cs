using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RefillBar : MonoBehaviour
{
    public AmmoRefill ammoRefill;
    public CanvasGroup canvasGroup;
    public Slider Refillslider;
    public Image fill;
    public Gradient gradient;

    // Start is called before the first frame update
    void Start()
    {
        canvasGroup.alpha = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        refillBar();
    }

    void refillBar()
    {
        if (ammoRefill.isRefilling)
        {
            canvasGroup.alpha = 1f;
        }
        if(canvasGroup.alpha == 1f)
        {
            if (ammoRefill.isRefilling == false)
            {
                canvasGroup.alpha = 0;
            }
/*            else
            {
                transform.position = this.transform.position + transform.up * 2f;
            }*/
        }
    }

    public void setMaxRefillTime(float maxRefillTime)
    {
        Refillslider.maxValue = maxRefillTime;
        Refillslider.value = 0;
        fill.color = gradient.Evaluate(0f);

    }

    public void setRefillTime(float currentRefillTime)
    {
        Refillslider.value = currentRefillTime;
        fill.color = gradient.Evaluate(Refillslider.normalizedValue);
    }
}
