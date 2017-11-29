using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextManager : MonoBehaviour
{

    private static GameObject canvas;
    private static APopupText m_dmg_text;
    private static APopupText m_conversation_text;
    private static APopupText m_cry_text;
    //private static IPopupText m_conversation_text;
    //private list<> m_ongoing_conversation;


/// <summary>
/// IMPORTANT: There must be a canvas with the tag Canvas on the scene. 
/// Initialize, which will be called from the GameManager, load the popuptext resources.
/// To create a text, call TextManager.Create<typeoftext>Text(string Text, Transform location, float? range).
/// location is the position of the created text, range is a random range x interal which the text will be created around
///</summary>
    public static void Initialize()
    {
				
			canvas = GameObject.FindWithTag("Canvas"); // TODO change to a better solution?
			if (canvas == null) {
				Debug.LogError("Could not find a canvas with the tag Canvas in the scene");
			}

			m_dmg_text = Resources.Load<APopupText>("Prefabs/Text/DmgTextParent");
			m_conversation_text = Resources.Load<APopupText>("Prefabs/Text/DmgTextParent");
			m_cry_text = Resources.Load<APopupText>("Prefabs/Text/DmgTextParent");
    }

    public static void CreateDmgText(string text, Transform location, float? range)
    {
        float random_range = range != null ? Random.Range(-(float)range, (float)range) : 0;

        CreatePopupText(text, new Vector2(location.position.x + random_range, location.position.y), m_dmg_text);
    }

    //TODO add different conversation text.
    public static void CreateConversationText(string text, Transform location)
    {
        CreatePopupText(text, location.position, m_conversation_text);
    }

		//TODO, make it better and more flexible.
    /*
	public static void DestroyConversationText(string text, Transform location)
    {
		DestroyPopupText(text, location, m_dmg_text); // needed since conversation text doesn't destroy on set time.
	}*/

    public static void CreateCryText(string text, Transform location)
    {
        CreatePopupText(text, location.position, m_cry_text);
    }

    private static APopupText CreatePopupText(string text, Vector2 position, APopupText popuptext)
    {
        APopupText instance = Instantiate(popuptext);
        Vector2 screen = Camera.main.WorldToScreenPoint(position);
        instance.transform.SetParent(canvas.transform, false);
        instance.transform.position = screen;
        instance.SetText(text);
        return instance;
    }
}
