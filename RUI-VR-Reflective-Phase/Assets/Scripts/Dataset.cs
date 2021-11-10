using Boo.Lang;
using UnityEngine;

public class Dataset : MonoBehaviour
{
    public float m_FirstTimeStamp;
    public float m_LastTimeStamp;
    public float m_TimeRange;
    public int m_CurrentTaskNumber;
    public GameObject m_MarksParent;
    public List<GameObject> m_CurrentlyVisibleMarks = new List<GameObject>();

    //private void OnEnable()
    //{
    //    TimeControl.TimeRangeUpdateEvent += DetermineTaskNumber;
    //}

    //private void OnDestroy()
    //{
    //    TimeControl.TimeRangeUpdateEvent -= DetermineTaskNumber;
    //}

    //private void DetermineTaskNumber(float converted)
    //{
    //    UpdateCurrentlyVisibleMarks();
    //    m_CurrentTaskNumber = m_CurrentlyVisibleMarks[m_CurrentlyVisibleMarks.Count-1].GetComponent<Record>().m_TaskNumber;
    //}

    //private List<GameObject> UpdateCurrentlyVisibleMarks()
    //{
    //    m_CurrentlyVisibleMarks.Clear();
    //    for (int i = 0; i < m_MarksParent.transform.childCount; i++)
    //    {
    //        GameObject mark = m_MarksParent.transform.GetChild(i).gameObject;
    //        if (mark.GetComponent<MeshRenderer>().enabled)
    //        {
    //            m_CurrentlyVisibleMarks.Add(mark);
    //        }
    //    }
    //    Debug.Log(m_CurrentlyVisibleMarks.Count);

    //    return m_CurrentlyVisibleMarks;
    //}
}