using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasAnimationControl : MonoBehaviour
{
    public static CanvasAnimationControl Instance;

    private Animator anim;
    public GameObject dancePlayer;
    public int m_current = 1;

    public void LateUpdate()
    {
        dancePlayer.SetActive(false);
    }
    private void Awake(){
        anim = GetComponent<Animator>();
        Instance = this;
    }

    //MARKER When You PRESS the selectButton on the FIRST SCREEN 
    public void SelectButton(){
        m_current = 2;
        anim.SetTrigger("ToSelection");

        if (dancePlayer)
            dancePlayer.SetActive(false);
    }

    //MARKER WHen Player Select One of Character on the DISPLAY(SECOND) CANVAS, MVOE to the DETAIL(THIRD) SCREEN
    //MARKER HAS BEEN CALLED Inside SelectCharacter Function from CharacterSelection script
    public void SelectHero(){
        m_current = 3;
        //anim.SetTrigger("ToSelection");

        if (dancePlayer)
            dancePlayer.SetActive(false);
    }
    public void SelectItem(){
        m_current = 5;
        anim.SetTrigger("ToSelection");
        GameManager.instance.fromCharacter = true;

        if (dancePlayer)
            dancePlayer.SetActive(false);

        UIManager.Instance.PressSelectonHeroButton();
    }
    public void SelectWeapon(){
        m_current = 6;
        anim.SetTrigger("ToSelection");
        GameManager.instance.fromCharacter = true;

        if (dancePlayer)
            dancePlayer.SetActive(false);

        UIManager.Instance.PressSelectonHeroButton();
    }
    public void SelectWeapon_FromMainMenu(){
        m_current = 6;
        anim.SetTrigger("ToSelection");
        GameManager.instance.fromMainMenu = true;

        if (dancePlayer)
            dancePlayer.SetActive(false);

        UIManager.Instance.PressSelectonHeroButton();
    }
    public void SelectItem_FromMainMenu(){
        m_current = 5;
        anim.SetTrigger("ToSelection");
        GameManager.instance.fromMainMenu = true;

        if (dancePlayer)
            dancePlayer.SetActive(false);

        UIManager.Instance.PressSelectonHeroButton();
    }

    public void Select_I_W(){
        m_current = 6;
        anim.SetTrigger("ToSelection");

        if (dancePlayer)
            dancePlayer.SetActive(false);
    }

    //BACK to the DISPLAY(SECOND) SCREEN, ATTCHED TO BACKBUTTON ON THIRD CANVAS
    public void BackButtonToSelection(bool debug){
        m_current = 3;
        if(!debug)
        {
            anim.SetTrigger("ToSelection");
        }
        if (dancePlayer)
            dancePlayer.SetActive(false);
    }
    public void BackButtonTo_I_W(){
        m_current = 6;
        anim.SetTrigger("ToSelection");
    }
    public void BackButton_SelectHero(){
        m_current = 3;
        anim.SetTrigger("ToSelection");

        if (dancePlayer)
            dancePlayer.SetActive(false);
    }

    //BACK to the Menu(FIRST) SCREEN, ATTACH TO BACKBUTTON ON SECOND CANVAS
    public void BackButtonToMain(){
        m_current = 1;
        anim.SetTrigger("ToMenu");

        if (dancePlayer)
            dancePlayer.SetActive(true);
    }
    public void BackButton(){
        if (GameManager.instance.fromCharacter == true && GameManager.instance.fromMainMenu != true){
            m_current = 6;
            anim.SetTrigger("MoveSix");
            GameManager.instance.fromCharacter = false;
        }
        else if(GameManager.instance.fromMainMenu == true && GameManager.instance.fromCharacter != true){
            m_current = 1;
            anim.SetTrigger("BackFirst");

            if (dancePlayer)
                dancePlayer.SetActive(true);
            GameManager.instance.fromMainMenu = false;
        }
    }
}
