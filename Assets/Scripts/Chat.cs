using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;
using TMPro;
using Crosstales.BWF.Model;
using Crosstales.BWF.Util;
using Crosstales.BWF.Manager;
public class Chat : MonoBehaviourPun, IPunObservable
{
    public ManagerMask BadwordManager = ManagerMask.BadWord;
    public ManagerMask DomManager = ManagerMask.Domain;
    public ManagerMask CapsManager = ManagerMask.Capitalization;
    public ManagerMask PuncManager = ManagerMask.Punctuation;
    public System.Collections.Generic.List<string> Sources = new System.Collections.Generic.List<string>(30);

    public GameObject notifDot;
    public static Chat instance;
    public ScrollRect rect;
    public RectTransform content;
    public float offset;
    public float poffset;
    public float lineHeight;

    public TextMeshProUGUI chat;
    public InputField input;
    private bool show;
    private Animator anim()
    {
        return this.GetComponent<Animator>();
    }
    private void Awake()
    {
        instance = this;
    }
    public void ShowNotification()
    {
        if(show)
        {
            notifDot.SetActive(false);
        }
        else
        {
            notifDot.SetActive(true);
        }
    }
    private void OnDisable()
    {

    }
    public void enable(bool value)
    {
        notifDot.transform.parent.gameObject.SetActive(value);
        if(!value)
        {
            chat.text = "";
            notifDot.SetActive(false);
            if(show)
            {
                anim().SetTrigger("out");
                show = false;
            }
        }
    }
    public void Toggle()
    {
        show = !show;
        if(!show)
        {
            anim().SetTrigger("out");
        }
        else
        {
            anim().SetTrigger("in");
            ShowNotification();
        }
    }
    public void Start()
    {
        PlayerPrefs.SetInt("chatcensoring", 1);
    }
    public void _SendMessage()
    {
        string sender =  PlayerPrefs.GetString("nickname");
        string message = input.text;
        if(isValid(message))
        {
            //Debug.LogError(PlayerPrefs.GetFloat("chatcensoring"));
            //if(PlayerPrefs.GetFloat("chatcensoring") == 2)
            //{
            //    message = Crosstales.BWF.BWFManager.ReplaceAll(message, BadwordManager | DomManager | CapsManager | PuncManager, Sources.ToArray());
            //}
            //message = Crosstales.BWF.BWFManager.ReplaceAll(message, BadwordManager | DomManager | CapsManager | PuncManager, Sources.ToArray());
            message = Crosstales.BWF.BWFManager.ReplaceAll(message, BadwordManager | DomManager | CapsManager | PuncManager, Sources.ToArray());
            photonView.RPC("ReceiveMessage", RpcTarget.All, sender, message);
        }
    }

    private bool isValid(string text)
    {
        bool value = true;
        if(string.IsNullOrWhiteSpace(text))value=false;
        return value;
    }
    [PunRPC]
    public void ReceiveMessage(string sender, string value)
    {
        if(chat.text != "")
        {
            chat.text = string.Format("{0}\n<color=#fcc203>{1}: </color>{2}", chat.text,sender, value);
        }
        else
        {
            chat.text = string.Format("{0}<color=#fcc203>{1}: </color>{2}", chat.text,sender, value);
        }
        content.sizeDelta = new Vector2(0, 0 + offset * CountLines(chat.text));
        content.localPosition = new Vector3(3.5f, (0 + poffset * CountLines(chat.text)), 0);
        rect.verticalNormalizedPosition = 0f;
        input.text = "";
        ShowNotification();
    }
    [PunRPC]
    public void LeftPartyMember(string name, bool removed)
    {
        if(removed)
        {
            if(chat.text != "")
            {
                chat.text = string.Format("{0}\n<color=#fc0303>{1} was removed from the party.</color>",chat.text, name);
            }
            else
            {
                chat.text = string.Format("{0}<color=#fc0303>{1} was removed from the party.</color>",chat.text, name);
            }
        }
        else
        {
            if(chat.text != "")
            {
                chat.text = string.Format("{0}\n<color=#fc0303>{1} left the party.</color>",chat.text, name);
            }
            else
            {
                chat.text = string.Format("{0}<color=#fc0303>{1} left the party.</color>",chat.text, name);
            }
        }
        content.sizeDelta = new Vector2(0, 0 + offset * CountLines(chat.text));
        content.localPosition = new Vector3(3.5f, (0 + poffset * CountLines(chat.text)), 0);
        rect.verticalNormalizedPosition = 0f;
        ShowNotification();
    }
    [PunRPC]
    public void NewPartyMember(string name)
    {
        if(chat.text != "")
        {
            chat.text = string.Format("{0}\n<color=#BFFF00>{1} joined the party.</color>",chat.text, name);
        }
        else
        {
            chat.text = string.Format("{0}<color=#BFFF00>{1} joined the party.</color>",chat.text, name);
        }
        content.sizeDelta = new Vector2(0, 0 + offset * CountLines(chat.text));
        content.localPosition = new Vector3(3.5f, (0 + poffset * CountLines(chat.text)), 0);
        rect.verticalNormalizedPosition = 0f;
        ShowNotification();
    }
    [PunRPC]
    public void PartyCreated(string name)
    {
        if(chat.text != "")
        {
            chat.text = string.Format("{0}\n<color=#BFFF00>{1} created the party.</color>",chat.text, name);
        }
        else
        {
            chat.text = string.Format("{0}<color=#BFFF00>{1} created the party.</color>",chat.text, name);
        }
        content.sizeDelta = new Vector2(0, 0 + offset * CountLines(chat.text));
        content.localPosition = new Vector3(3.5f, (0 + poffset * CountLines(chat.text)), 0);
        rect.verticalNormalizedPosition = 0f;
        ShowNotification();
    }
    private int CountLines(string str)
    {
       return Mathf.RoundToInt(chat.GetComponent<RectTransform>().sizeDelta.y/lineHeight);
    }
    IEnumerator UpdateData() 
	{
		while(true)
		{
			yield return null;
		}
	}
	
	public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
	{
	}
}
