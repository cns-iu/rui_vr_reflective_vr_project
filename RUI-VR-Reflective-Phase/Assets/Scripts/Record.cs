using UnityEngine;

public enum DataOrigin { Tutorial, Complexity, Plateau, None }

public class Record : MonoBehaviour
{
    public float m_TimeStamp;
    public DataOrigin m_Phase;
    public int m_TaskNumber;
}