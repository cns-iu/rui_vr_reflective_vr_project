using UnityEngine;
using UnityEngine.UI;

public class TimeControl : MonoBehaviour
{
    public Slider m_Slider;
    public Dataset m_DatasetInformation;
    public BuildTrajectory m_BuildTrajectory;
    public float converted;
    // Start is called before the first frame update

    public delegate void TimeRangeUpdate(float converted);

    public static event TimeRangeUpdate TimeRangeUpdateEvent;



    private void Start()
    {
        converted = ConvertSliderValueToTimeStamp(m_Slider.value);
        //Debug.Log("sliderValue:" + m_Slider.value);
        //Debug.Log("m_DatasetInformation.m_FirstTimeStamp: " + m_DatasetInformation.m_FirstTimeStamp);

        m_Slider.onValueChanged.AddListener(
            delegate
            {
                converted = ConvertSliderValueToTimeStamp(m_Slider.value);
                //Debug.Log(converted);
                UpdateRangeShown(converted);
            }
            );
    }

    private void UpdateRangeShown(float converted)
    {
        TimeRangeUpdateEvent?.Invoke(converted);
    }

    private float ConvertSliderValueToTimeStamp(float sliderValue)
    {
        float result = (m_DatasetInformation.m_TimeRange) * sliderValue + m_DatasetInformation.m_FirstTimeStamp;
        return result;
    }
}