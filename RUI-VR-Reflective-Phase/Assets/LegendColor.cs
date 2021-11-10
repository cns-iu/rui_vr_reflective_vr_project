using UnityEngine;

public class LegendColor : MonoBehaviour
{
    public BuildTrajectory m_BuildTractory;
    public Color m_StartColor;
    public Color m_EndColor;
    public GameObject[] m_LegendCubes;
    public float m_Offset;
    public GameObject[] m_Labels;

    private const float m_MinAngle = 0f;
    private const float m_MaxAngle = 180f;

    private void Start()
    {
        GetColors();
        SetColors();
        //SetPositions();
    }

    private void GetColors()
    {
        m_StartColor = m_BuildTractory.m_StartColor;
        m_EndColor = m_BuildTractory.m_EndColor;
    }

    private void SetColors()
    {
        for (int i = 0; i < m_LegendCubes.Length; i++)
        {
            Color color = Color.Lerp(m_StartColor, m_EndColor, (i * 1.2f) / m_LegendCubes.Length);
            m_LegendCubes[i].GetComponent<MeshRenderer>().material.color = color;
        }
    }

    private void SetPositions()
    {
        for (int i = 0; i < m_LegendCubes.Length; i++)
        {
            
            m_LegendCubes[i].transform.position = new Vector3(
                m_LegendCubes[i].transform.position.x + m_Offset * i,
                m_LegendCubes[i].transform.position.y,
                m_LegendCubes[i].transform.position.z
                );
            Debug.Log("setting cube at " + m_LegendCubes[i].transform.position + " with x-pos: "+ m_LegendCubes[i].transform.position.x);
        }
    }
}