using System.Collections;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class Logger : MonoBehaviour
{
    public enum Condition { Standup, Tabletop, Desktop };

    public enum ReflectionPhasePart { Intro, Main };

    public Condition m_Condition;
    public ReflectionPhasePart m_ReflectionPhasePart;
    public string userName;
    public GameObject headset;
    public GameObject controllerLeft;
    public GameObject controllerRight;
    public GameObject m_Kidney;
    public float logInterval = .1f;
    public bool isDataLogOn = false;
    public Toggle[] m_LegendToggles;

    private float m_TotalDistanceTraveled;
    private Vector3 m_PreviousPosition;
    private bool isExperimentRunning = true;
    private const string m_ExperimentState = "Reflective";
    private float elapsedTime;
    private string m_ButtonName = "";

    //private string type = "";
    private string m_State = "";

    private string dateTimeAtStart;
    private bool isRunning = false;
    private TimeControl m_TimeControl;

    private void OnEnable()
    {
        ClickEventHandler.ButtonClickEvent += ButtonListener;
    }

    private void OnDestroy()
    {
        ClickEventHandler.ButtonClickEvent -= ButtonListener;
    }

    private void OnEndOfSessionStopLogging()
    {
        isExperimentRunning = false;
    }

    public void ButtonListener(string buttonName, string state)
    {
        m_ButtonName = buttonName;
        m_State = state;
        AddRecord(GetPath());
        ResetButtonData();
    }

    private void Start()
    {
        //get start time/date for file name
        dateTimeAtStart = GiveDateTime();

        //initialize variables for user input
        InitializeVariables();

        //set up CSV file if logging is on
        if (isDataLogOn)
        {
            SetupCSV();
        }
    }

    private void Update()
    {
        UpdateData();

        if (isDataLogOn && isExperimentRunning)
        {
            if (!isRunning) StartCoroutine(LogMessage());
        }

        m_PreviousPosition = headset.transform.position;
    }

    public void UpdateData()
    {
        elapsedTime += Time.deltaTime;
        m_TotalDistanceTraveled += Vector3.Distance(headset.transform.position, m_PreviousPosition);
    }

    public IEnumerator LogMessage()
    {
        AddRecord(GetPath());
        isRunning = true;
        yield return new WaitForSeconds(logInterval);
        isRunning = false;
    }

    private void ResetButtonData()
    {
        m_ButtonName = "";
        m_State = "";
    }

    public void AddRecord(string filepath)
    {
        using (StreamWriter file = new StreamWriter(filepath, true))
        {
            file.WriteLine(
           FormatMessage()
            );
        }
    }

    private string FormatMessage()
    {
        return
            //general info
            userName + ","
            + m_ExperimentState + ","
            + m_ReflectionPhasePart + ","
            + m_Condition + ","
            + elapsedTime + ","
            + m_TotalDistanceTraveled + ","

            //position
            + GetPosition(headset) + ","
            + GetPosition(controllerLeft) + ","
            + GetPosition(controllerRight) + ","

            //rotation
            + GetRotation(headset) + ","
            + GetRotation(controllerLeft) + ","
            + GetRotation(controllerRight) + ","

            //user input
            // get time slider
            //raw slider avlue
            + m_TimeControl.m_Slider.value + ","
            + m_TimeControl.converted + ","

            // get kidney on/off status
            + m_Kidney.gameObject.activeSelf + ","

            // get status for all 7 filters
            + GetLegendToggleStatus()
            + m_ButtonName + ","
            + m_State

            //// get all button inputs individually AS THEY HAPPEN: time slider, kidney on/off, 7 filters in legend
            //+ button + ","
            //+ side + ","
            //+ status
            ;
    }

    private void InitializeVariables()
    {
        m_TimeControl = controllerLeft.GetComponent<TimeControl>();
    }

    private string GetPosition(GameObject gameObject)
    {
        string result =
            gameObject.transform.position.x + ","
            + gameObject.transform.position.y + ","
            + gameObject.transform.position.z;
        return result;
    }

    private string GetRotation(GameObject gameObject)
    {
        string result =
            gameObject.transform.rotation.eulerAngles.x + ","
            + gameObject.transform.rotation.eulerAngles.y + ","
            + gameObject.transform.rotation.eulerAngles.z + ","
            + UnityEditor.TransformUtils.GetInspectorRotation(gameObject.transform).x + ","
            + UnityEditor.TransformUtils.GetInspectorRotation(gameObject.transform).y + ","
            + UnityEditor.TransformUtils.GetInspectorRotation(gameObject.transform).z;

        return result;
    }

    private string GetLegendToggleStatus()
    {
        string result = "";

        foreach (var entry in m_LegendToggles)
        {
            result += entry.isOn + ",";
        }

        return result;
    }

    public void SetupCSV()
    {
        using (StreamWriter file = new StreamWriter(GetPath(), true))

        {
            file.WriteLine(
           //general info
           "userName" + ","
           + "ExperimentState" + ","
           + "ReflectionPhasePart" + ","
           + "condition" + ","
           + "elapsedTime" + ","
           + "distanceTraveled" + ","
           //position
           + "headsetPosX" + ","
           + "headsetPosY" + ","
           + "headsetPosZ" + ","
           + "ControllerLeftPosX" + ","
           + "ControllerLeftPosY" + ","
           + "ControllerLeftPosZ" + ","
           + "ControllerRightPosX" + ","
           + "ControllerRightPosY" + ","
           + "ControllerRightPosZ" + ","
           //rotation
           + "headsetRotEulerX" + ","
           + "headsetRotEulerY" + ","
           + "headsetRotEulerZ" + ","
           + "headsetRotInspectorX" + ","
           + "headsetRotInspectorY" + ","
           + "headsetRotInspectorZ" + ","
           + "controllerLeftRotEulerX" + ","
           + "controllerLeftRotEulerY" + ","
           + "controllerLeftRotEulerZ" + ","
           + "controllerLeftRotInspectorX" + ","
           + "controllerLeftRotInspectorY" + ","
           + "controllerLefttRotInspectorZ" + ","
           + "controllerRightRotEulerX" + ","
           + "controllerRightRotEulerY" + ","
           + "controllerRightRotEulerZ" + ","
           + "controllerRightRotInspectorX" + ","
           + "controllerRightRotInspectorY" + ","
           + "controllerRighttRotInspectorZ" + ","
           //user input
           + "rawSliderValue" + ","
            + "convertedSliderValue" + ","
            // get kidney on/off status
            + "isKidneyVisible" + ","
            // get status for all 7 filters
            + "ToggleHead" + ","
            + "ToggleRight" + ","
            + "ToggleLeft" + ","
            + "ToggleTissue" + ","
            + GetTaskLabelsWithNumbers()
            + "buttonName" + ","
            + "state"
           // get all button inputs individually AS THEY HAPPEN
           //+ "button" + ","
           //+ "side" + ","
           //+ "status"
           );
        }
    }

    private string GetTaskLabelsWithNumbers()
    {
        string result = "";
        for (int i = 4; i < m_LegendToggles.Length; i++)
        {
            result += "TaskVisible" + m_LegendToggles[i].name.Substring(9) + ",";
        }

        return result;
    }

    private string GetPath()
    {
        return Application.dataPath + "/Data/RUI_VR/experiment/" + m_Condition + "/" + userName + " " + m_Condition + " " + m_ExperimentState + " " +m_ReflectionPhasePart+ " " + dateTimeAtStart + ".csv";
    }

    private string GiveDateTime()
    {
        dateTimeAtStart = System.DateTime.Now.ToString();
        dateTimeAtStart = dateTimeAtStart.Replace(':', '_');
        dateTimeAtStart = dateTimeAtStart.Replace('/', '.');
        return dateTimeAtStart;
    }
}