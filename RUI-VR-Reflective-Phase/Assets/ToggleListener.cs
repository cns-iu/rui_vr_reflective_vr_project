using System.Collections.Generic;
using UnityEngine;

public class ToggleListener : MonoBehaviour
{
    public EncodeType m_ColorType;
    public DataOrigin m_PhaseType;
    public int m_TaskNumber;

    public bool m_IsEncodingVisible = true;
    public bool m_IsInTimeRange = true;
    public bool m_IsPhaseVisible = true;
    public bool m_IsTaskNumberVisible = true;

    private void OnEnable()
    {
        ToggleEventHandler.ToggleByEncodeEvent += SetVisibility;
        ToggleEventHandler.ToggleByPhaseEvent += SetVisibility;
        ToggleEventHandler.ToggleByTaskNumberEvent += SetVisibility;
        TimeControl.TimeRangeUpdateEvent += SetVisibility;
    }

    private void OnDestroy()
    {
        ToggleEventHandler.ToggleByEncodeEvent -= SetVisibility;
        ToggleEventHandler.ToggleByPhaseEvent -= SetVisibility;
        ToggleEventHandler.ToggleByTaskNumberEvent -= SetVisibility;
        TimeControl.TimeRangeUpdateEvent -= SetVisibility;
    }

    private void Start()
    {
        m_PhaseType = GetComponent<Record>().m_Phase;
        m_TaskNumber = GetComponent<Record>().m_TaskNumber;
    }

    private void SetVisibility(int taskNumber, bool isOn)
    {
        //Debug.LogFormat("SetVisibility called with ars: {0}, {1}; expression evaluates to {2}", taskNumber, isOn, taskNumber == m_TaskNumber);
        if (taskNumber == m_TaskNumber)
        {
            m_IsTaskNumberVisible = isOn;
            GetComponent<MeshRenderer>().enabled = (m_IsTaskNumberVisible && CheckFlags(new List<bool> { m_IsPhaseVisible, m_IsInTimeRange, m_IsEncodingVisible }));
        }
    }

    private void SetVisibility(EncodeType type, bool isOn)
    {
        //Debug.LogFormat("called SetVisibility() with args: {0}, {1} ", type, isOn);
        if (type == m_ColorType)
        {
            m_IsEncodingVisible = isOn;
            GetComponent<MeshRenderer>().enabled = (m_IsEncodingVisible && CheckFlags(new List<bool> { m_IsPhaseVisible, m_IsInTimeRange, m_IsTaskNumberVisible }));
        }
    }

    private void SetVisibility(float converted)
    {
        //Debug.LogFormat("called SetVisibility() with args: {0}", converted);
        float timestamp = GetComponent<Record>().m_TimeStamp;
        m_IsInTimeRange = timestamp <= converted;
        GetComponent<Renderer>().enabled = (m_IsInTimeRange && CheckFlags(new List<bool> { m_IsEncodingVisible, m_IsPhaseVisible, m_IsTaskNumberVisible }));
    }

    private void SetVisibility(DataOrigin phase, bool isOn)
    {
        //Debug.LogFormat("called SetVisibility() with args: {0}, {1} ", phase, isOn);
        if (m_PhaseType == phase)
        {
            m_IsPhaseVisible = isOn;
            GetComponent<MeshRenderer>().enabled = (m_IsPhaseVisible && CheckFlags(new List<bool> { m_IsEncodingVisible, m_IsInTimeRange, m_IsTaskNumberVisible }));
        }
    }

    private bool CheckFlags(List<bool> flags)
    {
        for (int i = 0; i < flags.Count; i++)
        {
            if (flags[i] == false)
            {
                return false;
            }
        }
        return true;
    }
}