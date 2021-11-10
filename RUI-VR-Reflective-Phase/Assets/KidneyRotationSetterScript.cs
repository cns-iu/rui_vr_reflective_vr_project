using UnityEngine;

public class KidneyRotationSetterScript : MonoBehaviour
{
    public BuildTrajectory m_Buildtrajectory;
    public Dataset m_DatasetInformation;

    private void OnEnable()
    {
        TimeControl.TimeRangeUpdateEvent += SetRotation;
    }

    private void OnDestroy()
    {
        TimeControl.TimeRangeUpdateEvent -= SetRotation;
    }

    private void Start()
    {
        SetRotation(m_DatasetInformation.m_LastTimeStamp);
    }

    private void SetRotation(float converted)
    {
        Vector3 rotation = FindClosestKidneyRotationObject(converted);

        //adjust own rotation
        this.transform.rotation = Quaternion.Euler(rotation);

        //float timestamp = GetComponent<Record>().m_TimeStamp;
        //Debug.Log("converted: " + converted + ", my timestamp: " + timestamp);
        ;
        //m_IsInTimeRange = timestamp <= converted;
        //GetComponent<Renderer>().enabled = (m_IsInTimeRange && CheckFlags(new List<bool> { m_IsEncodingVisible, m_IsPhaseVisible, m_IsTaskNumberVisible }));
    }

    private Vector3 FindClosestKidneyRotationObject(float converted)
    {
        Vector3 result = new Vector3();
        float previousDifference = 100f;

        foreach (var item in m_Buildtrajectory.m_RotationList)
        {
            float difference;
            difference = Mathf.Abs(item.GetComponent<Record>().m_TimeStamp - converted);
            if (difference < previousDifference)
            {
                result = item.GetComponent<KidneyRotationRecord>().m_Rotation;
            }

            previousDifference = difference;
        }
        //Debug.Log("converted: " + converted + ", setting rotation to: " + result);
        return result;
    }
}