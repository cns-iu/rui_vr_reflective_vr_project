using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public enum Condition { standup, tabletop, desktop }

public class BuildTrajectory : MonoBehaviour
{
    public float m_PercentageofDataShown = 1;
    public Condition m_Condition = Condition.standup;
    public string m_Filename;
    public int[] m_ColumnNumbers = new int[9];
    public Dataset m_DatasetInformation;

    //public Dictionary<string, int> m_ColumnNamesAndNumber = new Dictionary<string, int>();
    public bool m_NeedKidneyRotation = false;
    //public Dictionary<float, Vector3> m_KidneyrotationDict = new Dictionary<float, Vector3>();
    public List<GameObject> m_RotationList = new List<GameObject>();

    public GameObject m_HeadMark;
    public Sprite head;
    public GameObject m_RHandMark;
    public GameObject m_LHandMark;
    public GameObject m_SliverMark;
    public GameObject m_KidneyRotationObject;
    public GameObject m_ParentObject;
    public Color m_StartColor;
    public Color m_EndColor;

    public List<GameObject> m_Marks = new List<GameObject>();

    public bool m_ShowTutorialAndPlateau = true;

    // Start is called before the first frame update
    private void Awake()
    {
        ReadCSV();
    }

    private void ReadCSV()
    {
        int counter = 0;
        StreamReader streamReader = new StreamReader(Application.dataPath + "/Data/RUI_VR/source_control/" + m_Condition + "/" + m_Filename + ".csv");
        bool endOfFile = false;
        string dataString = streamReader.ReadLine();
        while (!endOfFile)
        {
            dataString = streamReader.ReadLine();
            if (dataString == null)
            {
                endOfFile = true;
                GetFirstandLastTimeStamps();
                break;
            }
            string[] dataValues = dataString.Split(',');
            //Debug.Log(dataValues.ToString
            Vector3 spawnPosition1 = new Vector3(
                    ToFloat(dataValues[m_ColumnNumbers[0]]), // standup: 6, tabletop: same, desktop: same
                    ToFloat(dataValues[m_ColumnNumbers[1]]), // standup: 7, tabletop: same, desktop: same
                    ToFloat(dataValues[m_ColumnNumbers[2]]) // standup: 8, tabletop: same, desktop: same
                    );

            Vector3 spawnPosition2 = new Vector3(
                    ToFloat(dataValues[m_ColumnNumbers[3]]), // standup: 9, tabletop: same
                    ToFloat(dataValues[m_ColumnNumbers[4]]), // standup: 10, tabletop: same
                    ToFloat(dataValues[m_ColumnNumbers[5]]) // standup: 11, tabletop: same
                    );

            Vector3 spawnPosition3 = new Vector3(
                    ToFloat(dataValues[m_ColumnNumbers[6]]), // standup: 12, tabletop: same
                    ToFloat(dataValues[m_ColumnNumbers[7]]), // standup: 13, tabletop: same
                    ToFloat(dataValues[m_ColumnNumbers[8]]) // standup: 14, tabletop: same
                    );
            Vector3 spawnPosition4 = new Vector3(
                    ToFloat(dataValues[m_ColumnNumbers[9]]), // standup: 17, tabletop: 23
                    ToFloat(dataValues[m_ColumnNumbers[10]]), // standup: 18, tabletop: 24
                    ToFloat(dataValues[m_ColumnNumbers[11]]) // standup: 19, tabletop: 25
                    );
            Vector3 kidneyRotation = new Vector3();
            if (m_NeedKidneyRotation)
            {
                kidneyRotation = new Vector3(
                    ToFloat(dataValues[17]),
                    ToFloat(dataValues[18]),
                    ToFloat(dataValues[19])
                    );
                //Debug.Log(kidneyRotation);
            }
            //Debug.Log(counter % (1 / m_PercentageofDataShown));
            if (counter % (1 / m_PercentageofDataShown) == 0)
            {
                if (dataValues[4] == DataOrigin.Complexity.ToString())
                {
                    GameObject sphere = SpawnSphere(m_HeadMark, spawnPosition1);
                    AddData(sphere, dataValues[m_ColumnNumbers[12]], dataValues[m_ColumnNumbers[13]], dataValues[m_ColumnNumbers[14]]);  // standup: (5,4,2), tabletop: same

                    GameObject leftSphere = SpawnSphere(m_LHandMark, spawnPosition2);
                    AddData(leftSphere, dataValues[m_ColumnNumbers[12]], dataValues[m_ColumnNumbers[13]], dataValues[m_ColumnNumbers[14]]);  // standup: (5,4,2), tabletop: same

                    GameObject rightSphere = SpawnSphere(m_RHandMark, spawnPosition3);
                    AddData(rightSphere, dataValues[m_ColumnNumbers[12]], dataValues[m_ColumnNumbers[13]], dataValues[m_ColumnNumbers[14]]);  // standup: (5,4,2), tabletop: same

                    GameObject sliverCube = SpawnCube(m_SliverMark, spawnPosition4);
                    AddData(sliverCube, dataValues[m_ColumnNumbers[12]], dataValues[m_ColumnNumbers[13]], dataValues[m_ColumnNumbers[14]], dataValues[m_ColumnNumbers[15]]);  // standup: (5,4,2, 38), tabletop: (5,4,2, 44)

                    if (m_NeedKidneyRotation)
                    {
                        GameObject kidneyRotationObject = Instantiate(m_KidneyRotationObject, m_ParentObject.transform.position, Quaternion.Euler(kidneyRotation));
                        AddData(kidneyRotationObject, dataValues[m_ColumnNumbers[12]], dataValues[m_ColumnNumbers[13]], dataValues[m_ColumnNumbers[14]], kidneyRotation);
                    }
                }
            }

            counter++;
        }
        HideTutorialAndPlateauIfDesired();
    }

    private void HideTutorialAndPlateauIfDesired()
    {
        if (!m_ShowTutorialAndPlateau)
        {
            foreach (var item in m_Marks)
            {
                if (item.GetComponent<Record>().m_Phase == DataOrigin.Plateau || item.GetComponent<Record>().m_Phase == DataOrigin.Tutorial)
                {
                    item.SetActive(false);
                }
            }
        }
    }

    private void AddData(GameObject mark, string timestamp, string dataOrigin, string taskNumber)
    {
        mark.transform.parent = m_ParentObject.transform;
        mark.AddComponent(typeof(Record));
        mark.GetComponent<Record>().m_TimeStamp = float.Parse(timestamp);
        mark.GetComponent<Record>().m_Phase = (DataOrigin)Enum.Parse(typeof(DataOrigin), dataOrigin);
        mark.GetComponent<Record>().m_TaskNumber = Convert.ToInt32(taskNumber);
        m_Marks.Add(mark);
    }

    // overload for tissue block marks
    private void AddData(GameObject mark, string timestamp, string dataOrigin, string taskNumber, string angle)
    {
        mark.transform.parent = m_ParentObject.transform;
        mark.AddComponent(typeof(Record));
        mark.GetComponent<Record>().m_TimeStamp = float.Parse(timestamp);
        mark.GetComponent<Record>().m_Phase = (DataOrigin)Enum.Parse(typeof(DataOrigin), dataOrigin);
        mark.GetComponent<Record>().m_TaskNumber = Convert.ToInt32(taskNumber);
        mark.GetComponent<MeshRenderer>().material.color = Color.Lerp(m_StartColor, m_EndColor, float.Parse(angle) / 180f);
        m_Marks.Add(mark);
    }

    //overload for kidney rotation in Tabletop
    private void AddData(GameObject kidneyRotationObject, string timestamp, string dataOrigin, string taskNumber, Vector3 rotation)
    {
        kidneyRotationObject.transform.parent = m_ParentObject.transform;
        kidneyRotationObject.AddComponent(typeof(Record));
        kidneyRotationObject.GetComponent<Record>().m_TimeStamp = float.Parse(timestamp);
        kidneyRotationObject.GetComponent<Record>().m_Phase = (DataOrigin)Enum.Parse(typeof(DataOrigin), dataOrigin);
        kidneyRotationObject.GetComponent<Record>().m_TaskNumber = Convert.ToInt32(taskNumber);

        kidneyRotationObject.AddComponent(typeof(KidneyRotationRecord));
        kidneyRotationObject.GetComponent<KidneyRotationRecord>().m_Rotation = rotation;
        m_RotationList.Add(kidneyRotationObject);
    }

    private float ToFloat(string input)
    {
        return float.Parse(input);
    }

    private GameObject SpawnSphere(GameObject mark, Vector3 spawnPosition)
    {
        GameObject create = Instantiate(mark, spawnPosition, Quaternion.identity);
        return (create);
    }

    private GameObject SpawnCube(GameObject mark, Vector3 spawnPosition)
    {
        GameObject create = Instantiate(mark, spawnPosition, Quaternion.identity);
        return (create);
    }

    //private GameObject SpawnKidneyRotationObject(GameObject kidneyRotation, Vector3 spawnRotation)
    //{
    //    GameObject create =
    //    return (create);
    //}

    private void GetFirstandLastTimeStamps()
    {
        //Debug.Log(marks[100].GetComponent<Record>().m_TimeStamp);
        List<float> timestamps = new List<float>();
        foreach (var item in m_Marks)
        {
            //Debug.Log(item.GetComponent<Record>().m_TimeStamp);
            float value = item.GetComponent<Record>().m_TimeStamp;
            timestamps.Add(value);
        }
        //Debug.Log(timestamps.Count);
        m_DatasetInformation.m_LastTimeStamp = Mathf.Max(timestamps.ToArray());
        m_DatasetInformation.m_FirstTimeStamp = Mathf.Min(timestamps.ToArray());
        m_DatasetInformation.m_TimeRange = m_DatasetInformation.m_LastTimeStamp - m_DatasetInformation.m_FirstTimeStamp;
        //Debug.Log(Mathf.Min(timestamps.ToArray()));
        //Debug.Log( m_DatasetInformation.lastTimeStamp);
    }
}