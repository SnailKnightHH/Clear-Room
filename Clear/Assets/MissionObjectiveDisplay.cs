using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MissionObjectiveDisplay : MonoBehaviour
{

    public string missionObjectiveText;
    private string currentPrintingContent = "";
    public float printDelay;
    public TextMeshProUGUI textMeshProUGUI;
    public AudioSource audioSource;
    public AudioClip typingSFX;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(PrintObjectives());
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator PrintObjectives()
    {
        yield return new WaitForSeconds(2);
        audioSource.PlayOneShot(typingSFX);
        for (int i = 0; i < missionObjectiveText.Length + 1; i++)
        {
            currentPrintingContent = missionObjectiveText.Substring(0, i);
            textMeshProUGUI.text = currentPrintingContent;
            yield return new WaitForSeconds(printDelay);
        }
        yield return new WaitForSeconds(3);
        //string emptyString = new string(' ', missionObjectiveText.Length);
        for (int i = 0; i < missionObjectiveText.Length ; i++)
        {
                string newString = " " + missionObjectiveText.Substring(i + 1, missionObjectiveText.Length - i - 1);
                textMeshProUGUI.text = newString;
                yield return new WaitForSeconds(printDelay / 5);
        }

    }
}
