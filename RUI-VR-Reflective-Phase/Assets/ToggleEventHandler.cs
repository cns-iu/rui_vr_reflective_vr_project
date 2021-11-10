using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public enum EncodeType { head, right, tissue, left, none }

public class ToggleEventHandler : MonoBehaviour //, IPointerClickHandler
{
    //public GameObject m_EventListener;
    //public List<GameObject> m_EventListeners = new List<GameObject>();
    public delegate void ToggleByEncode(EncodeType type, bool isOn);

    public static event ToggleByEncode ToggleByEncodeEvent;

    public EncodeType m_Type;

    public delegate void ToggleByPhase(DataOrigin phase, bool isOn);

    public static event ToggleByPhase ToggleByPhaseEvent;

    public DataOrigin m_Phase;

    public int m_TaskNumber = -1;

    public delegate void ToggleByTaskNumber(int taskNumber, bool isOn);

    public static event ToggleByTaskNumber ToggleByTaskNumberEvent;

    private Toggle m_Toggle;

    private void Awake()
    {
        m_Toggle = GetComponent<Toggle>();
    }

    private void Start()
    {
        m_Toggle.onValueChanged.AddListener(delegate
        {
            if (m_Type != EncodeType.none)
            {
                //Debug.LogFormat("Raising event ToggleByEncodeEvent() with args: {0}, {1}", m_Type, m_Toggle.isOn);
                ToggleByEncodeEvent?.Invoke(m_Type, m_Toggle.isOn);
            }
            else if (m_Phase != DataOrigin.None)
            {
                //Debug.LogFormat("Raising event ToggleByPhaseEvent() with args: {0}, {1}", m_Phase, m_Toggle.isOn);
                ToggleByPhaseEvent?.Invoke(m_Phase, m_Toggle.isOn);
            }
            else if (m_TaskNumber != -1)
            {
                ToggleByTaskNumberEvent?.Invoke(m_TaskNumber, m_Toggle.isOn);
            }
        });
    }

    //void IPointerClickHandler.OnPointerClick(PointerEventData eventData)
    //{
    //    if (m_Type != EncodeType.none)
    //    {
    //        //Debug.LogFormat("Raising event ToggleByEncodeEvent() with args: {0}, {1}", m_Type, m_Toggle.isOn);
    //        ToggleByEncodeEvent?.Invoke(m_Type, m_Toggle.isOn);
    //    }
    //    else
    //    {
    //        //Debug.LogFormat("Raising event ToggleByPhaseEvent() with args: {0}, {1}", m_Phase, m_Toggle.isOn);
    //        ToggleByPhaseEvent?.Invoke(m_Phase, m_Toggle.isOn);
    //    }
    //}
}