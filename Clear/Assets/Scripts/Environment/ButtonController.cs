using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ButtonController : MonoBehaviour
{
    public TextMeshProUGUI textMeshProUGUI;
    public CanvasGroup canvasGroup;
    public GameObject turret;
    public static int buttonActivationNumber = 0;
    public SpriteRenderer spriteRenderer;
    private bool pressedAlready = false;

    // Start is called before the first frame update
    void Start()
    {
        canvasGroup.alpha = 0;
        textMeshProUGUI.SetText("Press E To Activate Button.");
    }

    // Update is called once per frame
    void Update()
    {
        if(turret.gameObject != null)
        {
            spriteRenderer.enabled = false;
        }
        else
        {
            spriteRenderer.enabled = true;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        PlayerController player = collision.GetComponent<PlayerController>();
        if(player != null)
        {          
            canvasGroup.alpha = 1;
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (!pressedAlready)
                {
                    buttonActivationNumber++;
                    textMeshProUGUI.SetText("Button Activated");
                    spriteRenderer.color = Color.green;
                    pressedAlready = true;
                }
            }
        }
        else
        {
            canvasGroup.alpha = 0;
        }
    }

}
