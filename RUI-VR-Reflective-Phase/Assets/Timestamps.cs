using Boo.Lang;
using UnityEngine;
using UnityEngine.UI;

public class Timestamps : MonoBehaviour
{
    public Button[] m_Buttons;
    public GameObject m_Parent;
    public Slider m_Slider;
    public Dataset m_Dataset;
    public BuildTrajectory m_BuildTrajectory;

    public enum TimestampStartType { Tutorial, Complexity, Plateau }

    public TimestampStartType m_TimestampStart;

    public delegate void TimestampJump(TimestampStartType start);

    public static event TimestampJump TimestampJumpEvent;

    private List<GameObject> m_Marks = new List<GameObject>();

    private const int m_TutorialStartTaskNumber = 0;
    private const int m_ComplexityStartTaskNumber = 1;
    private const int m_PlateauStartTaskNumber = 15;
    private float toTimestamp;

    // Start is called before the first frame update
    private void Start()
    {
        foreach (var item in m_Buttons)
        {
            item.onClick.AddListener(
               delegate
               {
                   //Debug.Log("clicked " + item.transform.parent.name);
                   //Debug.Log(item.name.Substring(0, 2));
                   switch (item.name.Substring(0, 2))
                   {
                       case "PR":
                           //Debug.Log(item.transform.parent.name.Substring(9));
                           int arg;
                           arg = System.Convert.ToInt32(item.transform.parent.name.Substring(9));
                           //Debug.Log(arg);
                           JumpTo(arg);
                           break;

                       case "BT":
                           switch (item.name)
                           {
                               case "BTN_GoToTutorial":
                                   m_TimestampStart = TimestampStartType.Tutorial;
                                   break;

                               case "BTN_GoToComplexity":
                                   m_TimestampStart = TimestampStartType.Complexity;
                                   break;

                               case "BTN_GoToPlateau":
                                   m_TimestampStart = TimestampStartType.Plateau;
                                   break;

                               default:
                                   break;
                           }
                           JumpTo(m_TimestampStart);
                           break;

                       default:
                           break;
                   }
               }
                );
        }

        GetMarks();

        //Debug.Log(m_Marks.Count);
    }

    private void JumpTo(TimestampStartType start)
    {
        FindTimestampsByPhase(start);
        //Debug.Log(start);
        m_Slider.value = ComputeNewSliderValue();
    }

    private void JumpTo(int taskNumber)
    {
        FindTimestampsByTaskNumber(taskNumber);

        //move slider
        m_Slider.value = ComputeNewSliderValue();
    }

    private void FindTimestampsByTaskNumber(int taskNumber)
    {
        GetFirstRecord(taskNumber);
    }

    private float ComputeNewSliderValue()
    {
        return (toTimestamp - m_Dataset.m_FirstTimeStamp) / m_Dataset.m_TimeRange;
    }

    private void FindTimestampsByPhase(TimestampStartType start)
    {
        int startNumber = 0;

        switch (start)
        {
            case TimestampStartType.Tutorial:
                startNumber = m_TutorialStartTaskNumber;
                break;

            case TimestampStartType.Complexity:
                startNumber = m_ComplexityStartTaskNumber;
                break;

            case TimestampStartType.Plateau:
                startNumber = m_PlateauStartTaskNumber;
                break;

            default:
                break;
        }

        GetFirstRecord(startNumber);
    }

    private void GetFirstRecord(int number)
    {
        foreach (var item in m_BuildTrajectory.m_Marks)
        {
            if (item.GetComponent<Record>().m_TaskNumber == number)
            {
                toTimestamp = item.GetComponent<Record>().m_TimeStamp;
                //Debug.Log("wanting to skip to: " + toTimestamp);
                //Debug.Log(toTimestamp);
                return;
            }

            //return;
            //Debug.Log(item.GetComponent<Record>().m_TaskNumber + " " + item.GetComponent<Record>().m_TimeStamp);
        }
    }

    private void GetMarks()
    {
        for (int i = 0; i < m_Parent.transform.childCount; i++)
        {
            m_Marks.Add(m_Parent.transform.GetChild(i).gameObject);
        }
    }
}