using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndLevel : MonoBehaviour
{
    public TextMeshProUGUI textMeshProUGUI;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        StartCoroutine(EndLevelWait());
        
    }

    IEnumerator EndLevelWait()
    {
        textMeshProUGUI.SetText("Mission Accomplished! \nGood Job Soldier!");
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene(3);
    }
}
