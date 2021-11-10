using UnityEngine;
using UnityEngine.UI;
using Valve.VR;

public class Arrows : MonoBehaviour
{
    public GameObject[] arrows = new GameObject[4];
    public SteamVR_Action_Vector2 m_TouchpadTouch = null;
    public SteamVR_Action_Boolean m_Grip = null;

    private void OnEnable()
    {
        //m_TouchpadTouch.onAxis += ShowArrows;
        //m_TouchpadTouch.onUpdate += HideArrows;

        m_TouchpadTouch.AddOnAxisListener(ShowArrows, SteamVR_Input_Sources.LeftHand);
        m_TouchpadTouch.AddOnUpdateListener(HideAllArrows, SteamVR_Input_Sources.LeftHand);
        m_Grip.AddOnStateUpListener(HideFastArrows, SteamVR_Input_Sources.LeftHand);
    }

    private void OnDisable()
    {
        //m_TouchpadTouch.onAxis -= ShowArrows;
        //m_TouchpadTouch.onUpdate -= HideArrows;
    }

    private void Awake()
    {
        foreach (var item in arrows)
        {
            item.GetComponent<Image>().enabled = false;
        }
    }

    private void ShowArrows(SteamVR_Action_Vector2 fromAction, SteamVR_Input_Sources fromSource, Vector2 axis, Vector2 delta)
    {
        //arrows[0].GetComponent<Image>().enabled = (axis.x < 0f);
        //arrows[1].GetComponent<Image>().enabled = axis.x > 0f;
        //Debug.Log(m_Grip.state);

        if (m_Grip.GetState(SteamVR_Input_Sources.LeftHand))
        {
            arrows[0].GetComponent<Image>().enabled = (axis.x < 0f);
            arrows[1].GetComponent<Image>().enabled = axis.x > 0f;
        }
        else
        {
            arrows[2].GetComponent<Image>().enabled = (axis.x < 0f);
            arrows[3].GetComponent<Image>().enabled = axis.x > 0f;
        }
    }

    private void HideAllArrows(SteamVR_Action_Vector2 fromAction, SteamVR_Input_Sources fromSource, Vector2 axis, Vector2 delta)
    {
        foreach (var item in arrows)
        {
            if (axis.x == 0f)
            {
                item.GetComponent<Image>().enabled = false;
            }
        }
    }

    private void HideFastArrows(SteamVR_Action_Boolean fromAction, SteamVR_Input_Sources fromSource)
    {
        arrows[0].GetComponent<Image>().enabled = false;
        arrows[1].GetComponent<Image>().enabled = false;
    }
}