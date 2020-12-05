using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

[RequireComponent(typeof(RectTransform), typeof(Image))]
public class scratchsystemDemo : MonoBehaviour
{
    public int scratchRadius = 10;
    public UnityEvent OnScratch;

    Texture2D maskTexture;
    RectTransform rectTransform;
    private int width;
    private int height;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        width = (int)rectTransform.sizeDelta.x;
        height = (int)rectTransform.sizeDelta.y;
        Reset();
    }

    private void Reset()
    {
        maskTexture = new Texture2D(width,height);
        Color32[] cols = maskTexture.GetPixels32();
        for (int i = 0; i < cols.Length; i++)
        {
            cols[i] = new Color32(0, 0, 0, 255);
        }
        maskTexture.SetPixels32(cols);
        GetComponent<Image>().material.mainTexture = maskTexture;
        maskTexture.Apply(false);
    }

    public void Scratch(int xCenter, int yCenter)
    {
        int xOffset, yOffset, xPos, yPos, yRange;
        Color32[] tempArray = maskTexture.GetPixels32();
        bool hasChanged = false;
        for(xOffset = -scratchRadius; xOffset <= scratchRadius; xOffset++)
        {
            yRange = (int)Mathf.Ceil(Mathf.Sqrt(scratchRadius * scratchRadius - xOffset * xOffset));
            for(yOffset = -yRange; yOffset <= yRange; yOffset++)
            {
                xPos = xCenter + xOffset;
                yPos = yCenter + yOffset;
                hasChanged = TryScratchPixel(xPos, yPos, ref tempArray) || hasChanged;
            }
        }
        if(hasChanged)
        {
            OnScratch.Invoke();
            maskTexture.SetPixels32(tempArray);
            maskTexture.Apply(false);
        }
    }

    public bool TryScratchPixel(int xPos, int yPos, ref Color32[] pixels)
    {
        if (xPos >= 0 && xPos < width && yPos >= 0 && yPos < height)
        {
            int index = yPos * width + xPos;
            if(pixels[index].a != 0)
            {
                pixels[index].a = 0;
                return true;
            }
        }
        return false;
    }
}