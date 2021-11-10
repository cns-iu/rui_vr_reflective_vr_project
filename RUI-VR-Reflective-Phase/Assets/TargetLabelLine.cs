using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetLabelLine : MonoBehaviour
{
    public GameObject m_Target;
    public GameObject m_Label;
    public float m_Offset;

    private LineRenderer m_Line;
    

    void OnValidate()
    {
        m_Line = GetComponent<LineRenderer>();
        m_Line.SetPosition(0, new Vector3(m_Label.transform.position.x, m_Label.transform.position.y + m_Offset, m_Label.transform.position.z));
        m_Line.SetPosition(1, m_Target.transform.position);
    }


}
