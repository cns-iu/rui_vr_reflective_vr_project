using UnityEngine;
using UnityEngine.UI;

public class TaskNumberIndicator : MonoBehaviour
{
    public Text m_TaskNumberText;
    public Dataset m_Dataset;

    // Update is called once per frame
    private void Update()
    {
        m_TaskNumberText.text = m_Dataset.m_CurrentTaskNumber.ToString();
    }
}