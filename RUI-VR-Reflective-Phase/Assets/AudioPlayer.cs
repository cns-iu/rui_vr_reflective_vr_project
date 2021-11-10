using System.Collections;
using UnityEngine;

public class AudioPlayer : MonoBehaviour
{
    public float m_Delay;
    public AudioSource m_Source;

    // Start is called before the first frame update
    private void Start()
    {
        StartCoroutine(PlayAudioAfterDelay());
    }

    private IEnumerator PlayAudioAfterDelay()
    {
        yield return new WaitForSeconds(m_Delay);
        m_Source.Play();
    }
}