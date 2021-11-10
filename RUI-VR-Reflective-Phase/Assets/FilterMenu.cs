using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using Valve.VR;

public class FilterMenu : MonoBehaviour
{
    public SteamVR_Action_Boolean m_GrabSelect;
    public SteamVR_Input_Sources m_HandType;
    public delegate void Grab();
    public static event Grab GrabEvent;
    // Start is called before the first frame update
    void Start()
    {
        m_GrabSelect.AddOnStateDownListener(SelectMenuItem, m_HandType);
    }

    void SelectMenuItem(SteamVR_Action_Boolean fromAction, SteamVR_Input_Sources fromSource)
    {
        GrabEvent?.Invoke();
    }
}
