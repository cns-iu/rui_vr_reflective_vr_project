using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class LegendCanvasEventHandler : MonoBehaviour
{
    public Toggle[] m_Toggles = new Toggle[4];

    // Start is called before the first frame update
    void Start()
    {
        foreach (var toggle in m_Toggles)
        {
            toggle.onValueChanged.AddListener(
            delegate
            {
                SetLegend(toggle, toggle.isOn);
                SetVisualization(toggle, toggle.isOn);
            }
            );
        }

    }

    void SetLegend(Toggle toggle, bool isOn)
    {
        //Debug.Log(toggle.gameObject.name + " is on: " + isOn);
    }

    void SetVisualization(Toggle toggle, bool isOn)
    {
        //Debug.Log(toggle.gameObject.name + " is on: " + isOn);
    }
}
