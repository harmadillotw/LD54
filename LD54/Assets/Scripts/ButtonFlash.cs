using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonFlash : MonoBehaviour
{
    public int buttonNumber;
    Color origColor;
    float timer;
    float flashRate = 0.5f;
    Image image;
    // Start is called before the first frame update
    void Start()
    {
        timer = 0f;
        image = GetComponent<Image>();
        origColor = image.color;
    }

    // Update is called once per frame
    void Update()
    {
        if (buttonNumber == GlobalValues.previousStation)
        {
            timer += Time.deltaTime;
            if (timer > flashRate)
            {
                timer = 0f;
                if (image.color == origColor)
                {
                    image.color = Color.red;
                }
                else
                {
                    image.color = origColor;
                }
            }
        }
    }
}
