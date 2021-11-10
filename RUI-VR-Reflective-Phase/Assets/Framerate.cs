using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class Framerate : MonoBehaviour
{
    public enum Percentage { hundred, fifty, twenty_five};
    public Percentage m_Percentage = Percentage.hundred;
    float m_FrameRate;
    float m_Max;
    [Range(5f,10f)]

    List<float> m_FpsValues = new List<float>();
    // Update is called once per frame

    void Awake()
    {
        m_FrameRate = RetrieveFramerate();
        StartCoroutine(CheckMaximum());
        SetupCSV();
    }
    void Update()
    {
 
        m_FrameRate = RetrieveFramerate();
        m_FpsValues.Add(m_FrameRate);
        Debug.Log("Current ramerate: " + m_FrameRate);
        Debug.Log("Maximum fps: " + m_Max);
        AddRecord(Application.dataPath + "/Data/RUI_VR/fps_test_" + m_Percentage + ".csv", m_FrameRate, ComputeAverage());
    }

    float RetrieveFramerate()
    {
        return 1 / Time.deltaTime;
    }

    IEnumerator CheckMaximum()
    {
        while (true)
        {

            Debug.Log("starting coroutinessssssssssssssssssssssssssssssssssssssssssssssssssssss");
            yield return new WaitForSeconds(3);
            if (m_FrameRate >= m_Max)
            {
                m_Max = m_FrameRate;
            }
            Debug.Log("Maximum fps: " + m_Max);
            yield return null;
        }

    }

    void OnApplicationQuit() {
        ComputeAverage();
    }

    float ComputeAverage()
    {
      
        float total = 0f;
        float mean;

        for (int i = 0; i < m_FpsValues.Count; i++)
        {
            total += m_FpsValues[i];
        }
        mean = total / m_FpsValues.Count;
        Debug.LogFormat("Logged {0} values with average {1}", m_FpsValues.Count, mean);
        return mean;
       
    }

    public void SetupCSV()
    {
        using (StreamWriter file = new StreamWriter(Application.dataPath + "/Data/RUI_VR/fps_test_" + m_Percentage + ".csv", true))
        {
            file.WriteLine(
               "current" + ","
               + "average");
        }
    }

    public void AddRecord(string filepath, float current, float average)
    {
        using (StreamWriter file = new StreamWriter(filepath, true))
        {
            file.WriteLine(current + "," + average);
        }
    }
}
