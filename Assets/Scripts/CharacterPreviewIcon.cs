using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterPreviewIcon : MonoBehaviour
{
    private Image m_image;

    private void Awake()
    {
        m_image = GetComponent<Image>();
    }

    private void OnEnable()
    {
        m_image.sprite = UIManager.Instance.playerImage.sprite;
    }
}
