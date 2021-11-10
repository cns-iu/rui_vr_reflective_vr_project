using System.Collections;
using UnityEngine;
using Valve.VR;

public class SpeedIndicatorCubes : MonoBehaviour
{
    public GameObject[] m_Cubes;
    public GameObject[] arrows;
    public GameObject[] m_SuperSpeedIndicators;
    public SteamVR_Action_Vector2 m_TouchpadTouch = null;
    public SteamVR_Action_Boolean m_Grip = null;
    public SteamVR_Input_Sources m_HandType;
    public float[] m_Thresholds = new float[6];
    public GameObject[] m_SkipArrows = new GameObject[2];
    public float m_BlipDuration = .3f;
    public GameObject m_TransitionGraphic;

    private void OnEnable()
    {
        m_TouchpadTouch.AddOnUpdateListener(SetCubes, m_HandType);
        //GetUserInput.SkipFrameEvent += ShowSkipArrow;
        //GetUserInput.SkipFrameDirectionChangeEvent += OnTransition;
        //m_TouchpadTouch.AddOnUpdateListener
    }

    private void OnDestroy()
    {
        m_TouchpadTouch.RemoveOnAxisListener(SetCubes, m_HandType);
        //GetUserInput.SkipFrameEvent -= ShowSkipArrow;
        //GetUserInput.SkipFrameDirectionChangeEvent -= OnTransition;
    }

    private void Start()
    {
        DisableObjects(m_SuperSpeedIndicators);
        DisableObjects(m_Cubes);
        DisableObjects(arrows);
        DisableObjects(m_SkipArrows);
    }

    //void OnTransition(bool direction) {
    //    m_TransitionGraphic.transform.GetChild(1).gameObject.SetActive(direction);
    //    m_TransitionGraphic.transform.GetChild(0).gameObject.SetActive(!direction);
    //    StartCoroutine(ShowTransitionAnimation());
    //}

    //private IEnumerator ShowTransitionAnimation()
    //{
    //    m_TransitionGraphic.SetActive(true);
    //    yield return new WaitForSeconds(1f);
    //    m_TransitionGraphic.SetActive(false);
    //}

    //private void ShowSkipArrow(bool goesForward)
    //{
    //    if (goesForward)
    //    {
    //        StartCoroutine(Blip(m_SkipArrows[1]));
    //    }
    //    else
    //    {
    //        StartCoroutine(Blip(m_SkipArrows[0]));
    //    }
    //}

    //private IEnumerator Blip(GameObject arrow)
    //{
    //    arrow.SetActive(true);
    //    yield return new WaitForSeconds(m_BlipDuration);
    //    arrow.SetActive(false);
    //}

    private void SetCubes(SteamVR_Action_Vector2 fromAction, SteamVR_Input_Sources fromSource, Vector2 axis, Vector2 delta)
    {
        for (int i = 4; i < m_Cubes.Length; i++)
        {
            //Debug.Log(axis.x);
            m_Cubes[i].SetActive(axis.x > m_Thresholds[i]);
            arrows[i].SetActive(axis.x > m_Thresholds[i]);
            SetSuperSpeedIndicator(1, axis.x, axis.x > m_Thresholds[i]);
            //arrows[i].GetComponentInChildren<Image>().enabled = axis.x > m_Thresholds[i];
        }

        for (int i = 3; i > -1; i--)
        {
            //Debug.Log(axis.x);
            m_Cubes[i].SetActive(axis.x < m_Thresholds[i]);
            arrows[i].SetActive(axis.x < m_Thresholds[i]);
            SetSuperSpeedIndicator(0, axis.x, axis.x < m_Thresholds[i]);
            //arrows[i].GetComponentInChildren<Image>().enabled = axis.x < m_Thresholds[i];
        }
    }

    private void DisableObjects(GameObject[] gameObjects)
    {
        foreach (var item in gameObjects)
        {
            item.SetActive(false);
        }
    }

    private void SetSuperSpeedIndicator(int index, float x, bool isThresholdReached)
    {
        switch (index)
        {
            case 0:
                if (x < 0f)
                {
                    m_SuperSpeedIndicators[index].SetActive(m_Grip.GetState(m_HandType) && isThresholdReached);
                    m_SuperSpeedIndicators[index + 2].SetActive(m_Grip.GetState(m_HandType) && isThresholdReached);
                }
                break;

            case 1:
                if (x > 0f)
                {
                    m_SuperSpeedIndicators[index].SetActive(m_Grip.GetState(m_HandType) && isThresholdReached);
                    m_SuperSpeedIndicators[index + 2].SetActive(m_Grip.GetState(m_HandType) && isThresholdReached);
                }
                break;

            default:
                break;
        }
    }
}