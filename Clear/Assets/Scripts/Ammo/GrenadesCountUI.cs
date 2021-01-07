using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GrenadesCountUI : MonoBehaviour
{
    public TextMeshProUGUI textMeshProUGUI;
    public ThrowingGrenades throwingGrenades;

    // Start is called before the first frame update
    void Start()
    {
               
    }

    // Update is called once per frame
    void Update()
    {
        textMeshProUGUI.SetText(throwingGrenades.currentGrenadesCount.ToString());    
    }
}
