using UnityEngine;
using Valve.VR;

public class ReferenceSystem : MonoBehaviour
{
    public GameObject[] m_ReferenceSystemElements = new GameObject[2];
    public SteamVR_Action_Boolean m_MenuPress = null;
    bool m_ShowReferenceSystem = true;
    // Start is called before the first frame update
    void OnEnable()
    {
        m_MenuPress.onStateDown += Toggle;
    }

    private void OnDisable()
    {
        m_MenuPress.onStateDown -= Toggle;
    }

    void Toggle(SteamVR_Action_Boolean fromAction, SteamVR_Input_Sources fromSource)
    {
       
            m_ShowReferenceSystem = !m_ShowReferenceSystem;
            foreach (var item in m_ReferenceSystemElements)
            {
                item.SetActive(m_ShowReferenceSystem);
            }

    }
}
