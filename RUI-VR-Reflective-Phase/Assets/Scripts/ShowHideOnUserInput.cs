using UnityEngine;
using Valve.VR;

public class ShowHideOnUserInput : MonoBehaviour
{
    public SteamVR_Action_Boolean m_Menu = null;
    public SteamVR_Input_Sources m_Source;
    public GameObject m_Kidney;

    // Start is called before the first frame update
    private void Start()
    {
        m_Source = GetComponent<SteamVR_Behaviour_Pose>().inputSource;
        m_Menu.AddOnStateDownListener(ShowHide, m_Source);
        m_Menu.AddOnStateUpListener(ShowHide, m_Source);
    }

    private void OnDestroy()
    {
        m_Menu.onStateDown -= ShowHide;
    }

    private void ShowHide(SteamVR_Action_Boolean fromAction, SteamVR_Input_Sources fromSource)
    {
        m_Kidney.gameObject.SetActive(!fromAction.GetState(m_Source));
    }
}