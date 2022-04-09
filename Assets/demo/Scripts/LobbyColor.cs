using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
public class LobbyColor : MonoBehaviour
{
    public SpriteRenderer playerSprite;
    public Color defaultColor;
    private void Start()
    {
        playerSprite.color = currentCol();
    }
    private Color currentCol()
    {
        Color col = new Color(PlayerPrefs.GetFloat("ColorR"), PlayerPrefs.GetFloat("ColorG"), PlayerPrefs.GetFloat("ColorB"), 100f);
        if(col == new Color(0f, 0f, 0f, 100f))
        {
            PlayerPrefs.SetFloat("ColorR", defaultColor.r);
            PlayerPrefs.SetFloat("ColorG", defaultColor.g);
            PlayerPrefs.SetFloat("ColorB", defaultColor.b);
        }
        Color finalCol = new Color(PlayerPrefs.GetFloat("ColorR"), PlayerPrefs.GetFloat("ColorG"), PlayerPrefs.GetFloat("ColorB"), 100f);
        return finalCol;
    }
    
    public void UpdateColor(Button sender)
    {
        Color col = sender.gameObject.GetComponent<Image>().color;
        PlayerPrefs.SetFloat("ColorR", col.r);
        PlayerPrefs.SetFloat("ColorG", col.g);
        PlayerPrefs.SetFloat("ColorB", col.b);
        playerSprite.color = currentCol();
    }
}