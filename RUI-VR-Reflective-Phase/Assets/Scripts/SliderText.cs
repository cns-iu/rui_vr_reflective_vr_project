using System;
using UnityEngine;
using UnityEngine.UI;

public class SliderText : MonoBehaviour
{
    public Slider m_Slider;
    public TimeControl m_TimeControl;
    public Dataset m_DatasetInformation;
    public Text m_Text;

    private void Awake()
    {
        m_Text.text = ConstructText();
    }

    // Update is called once per frame
    void Update()
    {
        m_Text.text = ConstructText();
    }

    string ConstructText()
    {
        string result = "";
        result = SecondsToMinutes(m_TimeControl.converted);

        return result;
    }

    string SecondsToMinutes(float totalTime)
    {
        
        TimeSpan t = TimeSpan.FromSeconds(totalTime);

        string result = string.Format("{0:D2}m:{1:D2}s",
                        t.Minutes,
                        t.Seconds);
        return result;
    }

}
