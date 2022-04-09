using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageAlphaPulsate : MonoBehaviour
{
    private Image m_image;

    void Awake()
    {
        m_image = GetComponent<Image>();
    }

    void Update()
    {
        Color color = m_image.color;
        color.a = Mathf.PingPong(Time.time, 1);
        m_image.color = color;
    }
}
