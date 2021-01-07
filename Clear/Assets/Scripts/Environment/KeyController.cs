using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class KeyController : MonoBehaviour
{
    public Transform playerTransform;
    public static int keyAvaiable;
    public CanvasGroup canvasGroup;
    public TextMeshProUGUI textMeshProUGUI;

    // Start is called before the first frame update
    void Start()
    {
        textMeshProUGUI.SetText("Press E To Pick Up Key");
        canvasGroup.alpha = 0;
        keyAvaiable = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(Vector2.Distance(transform.position, playerTransform.position) < 5f)
        {
            canvasGroup.alpha = 1;
            if (Input.GetKeyDown(KeyCode.E))
            {
                keyAvaiable++;
                canvasGroup.alpha = 0;
                // Destroy(canvas); somehow can't destroy canvas 
                Destroy(gameObject);
            }
        }
        else
        {
            canvasGroup.alpha = 0;
        }


    }
}
