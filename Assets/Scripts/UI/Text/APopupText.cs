using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class APopupText : MonoBehaviour
{

    protected Text m_text;
    public Animator m_popup_animator_text;


    void Start()
    {
        m_text = m_popup_animator_text.GetComponent<Text>();
        ExtendedStart();
    }

    protected abstract void ExtendedStart();

    public abstract void DestroyText();

    public void SetText(string text)
    {
        m_popup_animator_text.GetComponent<Text>().text = text;
    }

    public virtual void SetColor(Color32 color)
    {
        m_popup_animator_text.GetComponent<Text>().color = color;
    }

}
