using UnityEngine;
using UnityEngine.UI;
using Valve.VR;

public class GetUserInput : MonoBehaviour
{
    public SteamVR_Action_Vector2 m_TouchpadTouch = null;
    public SteamVR_Action_Boolean m_TriggerButton = null;
    public SteamVR_Action_Boolean m_SideGrip = null;
    public SteamVR_Action_Boolean m_GripLong = null;
    public Dataset m_DatasetInformation;
    public BuildTrajectory m_BuildTracjectory;
    public SteamVR_Input_Sources m_HandType;
    public Slider m_Slider;
    public float numerator = 100;
    public float m_SmoothingFactorFast = 1f;
    public float m_SmoothingFactorSlow = .01f;
 

    // Start is called before the first frame update
    private void Start()
    {
        m_TouchpadTouch.AddOnAxisListener(SkipSlider, m_HandType);
        //m_SideGrip.AddOnStateDownListener(StepForward, m_HandType);
        //m_GripLong.AddOnStateDownListener(ChangeSkipFrameDirection, m_HandType);
    }

    private void OnDestroy()
    {
        m_TouchpadTouch.RemoveOnAxisListener(SkipSlider, m_HandType);
        //m_SideGrip.RemoveOnStateDownListener(StepForward, m_HandType);
        //m_GripLong.RemoveOnStateDownListener(ChangeSkipFrameDirection, m_HandType);
    }

 

    private void SkipSlider(SteamVR_Action_Vector2 fromAction, SteamVR_Input_Sources fromSource, Vector2 axis, Vector2 delta)
    {
        bool isGoingFast = m_TriggerButton.GetState(SteamVR_Input_Sources.LeftHand);
        if (!isGoingFast)
        {
            ChangeSlider(axis.x, m_SmoothingFactorSlow);
        }
        else
        {
            ChangeSlider(axis.x, m_SmoothingFactorFast);
        }
        //SetPlayheadArrows(axis.x, isGoingSlow);
    }

    private void ChangeSlider(float x, float factor)
    {
        float normalizer = numerator / m_DatasetInformation.m_TimeRange;
        //Debug.Log("moving slider by: " + x * factor * normalizer);
        m_Slider.value += (x * factor * normalizer);
    }

    //##########################################################################################################
    //###########################SKIP1FRAME IMPLEMENTATION####################################################
    //##########################################################################################################

    //member variables/properties
    //public bool m_GoesForward = true;

    //public delegate void SkipFrame(bool goesForward);

    //public static event SkipFrame SkipFrameEvent;

    //public delegate void SkipFrameDirectionChange(bool direction);

    //public static event SkipFrameDirectionChange SkipFrameDirectionChangeEvent;

    //private void ChangeSkipFrameDirection(SteamVR_Action_Boolean fromAction, SteamVR_Input_Sources fromSource)
    //{
    //    m_GoesForward = !m_GoesForward;
    //    SkipFrameDirectionChangeEvent?.Invoke(m_GoesForward);
    //}

    //private void StepForward(SteamVR_Action_Boolean fromAction, SteamVR_Input_Sources fromSource)
    //{
    //    if (m_SideGrip.GetStateDown(m_HandType))
    //    {
    //        float currentTimeStamp = GetCurrentTimestamp();
    //        // find record with closest timestamp BEFORE...
    //        ChangeSlider(ComputeSliderMovement(currentTimeStamp), 1f);
    //        SkipFrameEvent?.Invoke(m_GoesForward);
    //    }
    //}

    //private float ComputeSliderMovement(float current)
    //{
    //    float nextTimestamp = current;

    //    if (m_GoesForward)
    //    {
    //        for (int i = 0; i < m_BuildTracjectory.m_Marks.Count; i++)
    //        {
    //            if (m_BuildTracjectory.m_Marks[i].GetComponent<Record>().m_TimeStamp > current)
    //            {
    //                //marksAfter.Add(m_BuildTracjectory.m_Marks[i]);
    //                nextTimestamp = m_BuildTracjectory.m_Marks[i].GetComponent<Record>().m_TimeStamp;
    //                break;
    //            }
    //        }
    //    }
    //    else
    //    {
    //        for (int i = m_BuildTracjectory.m_Marks.Count - 1; i >= 0; i--)
    //        {
    //            if (m_BuildTracjectory.m_Marks[i].GetComponent<Record>().m_TimeStamp < current)
    //            {
    //                //marksAfter.Add(m_BuildTracjectory.m_Marks[i]);
    //                nextTimestamp = m_BuildTracjectory.m_Marks[i].GetComponent<Record>().m_TimeStamp;
    //                break;
    //            }
    //        }
    //    }

    //    //get difference between the current and next timestamps in seconds
    //    float difference = nextTimestamp - current;

    //    // convert to slider length aka how much the slider needs to be moved;
    //    float result = difference / (m_DatasetInformation.m_FirstTimeStamp + m_DatasetInformation.m_TimeRange);
    //    return result;
    //}

    //private float GetCurrentTimestamp()
    //{
    //    return (m_DatasetInformation.m_FirstTimeStamp + m_DatasetInformation.m_TimeRange) * m_Slider.value;
    //}

    //float GetNextTimestamp()
    //{
    //    float result;
    //    m_Slider.value *
    //    return result;
    //}

    //##########################################################################################################
    //###########################FAILED IMPLEMENTATION BELOW####################################################
    //##########################################################################################################

    //public enum SkippingSpeed
    //{ TripleBackward, DoubleBackward, SingleBackward, Pause, Single, Double, Triple };

    //public enum InputDirection
    //{ Forward, Backward }

    //public float[] m_PossibleShuttleStepArray = new float[7] { -3f, -2f, -1f, 0f, 1f, 2f, 3f };
    //public float m_ShuttleStep;
    //public float m_ShuttleSpeed;
    //public Text m_SpeedText;

    //public SteamVR_Action_Vector2 m_TouchpadTouchAxis = null;
    //public SteamVR_Action_Boolean m_Grab = null;
    //public bool m_IsTouchingOnFirstFrame = true;

    //public SteamVR_Action_Boolean m_TouchpadRight = null;
    //public SteamVR_Action_Boolean m_TouchpadLeft = null;

    //public SteamVR_Input_Sources m_HandType;
    //public Slider m_Slider;
    //public float m_SmoothingFactor = 1f;
    //public float m_SmoothingFactorFineAdjustment = .01f;
    //public InputDirection m_InputDirection;

    //public SkippingSpeed m_SkippingSpeed = SkippingSpeed.Pause;
    //private SkippingSpeed m_PreviousSkippingSpeed;

    //// Start is called before the first frame update
    //private void OnEnable()
    //{
    //    m_TouchpadRight.AddOnStateDownListener(UpdateSlider, m_HandType);
    //    m_TouchpadLeft.AddOnStateDownListener(UpdateSlider, m_HandType);
    //}

    //private void OnDestroy()
    //{
    //    m_TouchpadRight.RemoveOnStateDownListener(UpdateSlider, m_HandType);
    //    m_TouchpadLeft.RemoveOnStateDownListener(UpdateSlider, m_HandType);
    //}

    ////
    //private void UpdateSlider(SteamVR_Action_Boolean fromAction, SteamVR_Input_Sources fromSource)
    //{
    //    //set direction from user input
    //    SetDirectionFromUserInput(fromAction);

    //    //set new speed based on input direction
    //    SetNewSpeed();

    //    //set coarse slider change from speed
    //    SetSliderChangeRate();

    //    //set fine slider change
    //    AdjustSliderChange();

    //    // actally move slider
    //    ChangeSlider();
    //}

    //private void ChangeSlider()
    //{
    //    StartCoroutine(MoveSlider());
    //}

    //private IEnumerator MoveSlider()
    //{
    //    while (true)
    //    {
    //        m_Slider.value += m_ShuttleSpeed;

    //        if (m_Slider.value == 0f || m_Slider.value == 1f)
    //        {
    //            break;
    //        }
    //        yield return null;
    //    }
    //}

    //private void AdjustSliderChange()
    //{
    //    m_ShuttleSpeed = m_ShuttleStep * m_SmoothingFactor;
    //    Debug.Log("m_ShuttleStep: " + m_ShuttleStep);
    //    Debug.Log("m_ShuttleSpeed: " + m_ShuttleSpeed);
    //}

    //private void SetSliderChangeRate()
    //{
    //    m_ShuttleStep = m_PossibleShuttleStepArray[(int)m_SkippingSpeed];
    //}

    //private void SetNewSpeed()
    //{
    //    switch (m_InputDirection)
    //    {
    //        case InputDirection.Forward:
    //            if (m_SkippingSpeed != SkippingSpeed.Triple)
    //            {
    //                if (m_PreviousSkippingSpeed == SkippingSpeed.SingleBackward || m_PreviousSkippingSpeed == SkippingSpeed.DoubleBackward ||
    //                    m_PreviousSkippingSpeed == SkippingSpeed.TripleBackward)
    //                {
    //                    m_SkippingSpeed = SkippingSpeed.Pause;
    //                }
    //                else
    //                {
    //                    m_SkippingSpeed++;
    //                }
    //            }

    //            break;

    //        case InputDirection.Backward:
    //            if (m_SkippingSpeed != SkippingSpeed.TripleBackward)
    //            {
    //                if (m_PreviousSkippingSpeed == SkippingSpeed.Single || m_PreviousSkippingSpeed == SkippingSpeed.Double ||
    //                    m_PreviousSkippingSpeed == SkippingSpeed.Triple)
    //                {
    //                    m_SkippingSpeed = SkippingSpeed.Pause;
    //                }
    //                else
    //                {
    //                    m_SkippingSpeed--;
    //                }
    //            }
    //            break;

    //        default:
    //            break;
    //    }
    //    m_PreviousSkippingSpeed = m_SkippingSpeed;
    //    Debug.Log("m_SkippingSpeed: " + m_SkippingSpeed);
    //}

    //private void SetDirectionFromUserInput(SteamVR_Action_Boolean fromAction)
    //{
    //    if (fromAction.fullPath.Contains("TouchpadRight"))
    //    {
    //        m_InputDirection = InputDirection.Forward;
    //    }
    //    else if (fromAction.fullPath.Contains("TouchpadLeft"))
    //    {
    //        m_InputDirection = InputDirection.Backward;
    //    }
    //    Debug.Log("m_InputDirection: " + m_InputDirection);
    //}
}