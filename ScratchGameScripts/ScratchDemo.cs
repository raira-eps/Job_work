using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Security.Cryptography;
using System.IO;
using UnityEngine.SceneManagement;

public class ScratchDemo : MonoBehaviour, IPointerClickHandler, IBeginDragHandler, IDragHandler
{
    [SerializeField]
    private Image image;
    private Texture2D tempTexture2D;
    private RectTransform imageRectTransform;

    private void Start()
    {
        Debug.Log("start");
        if (image == null)
        {
            image = GetComponent<Image>();
        }
        if (image == null)
        {
            Debug.Log("Image not found");
            return;
        }

        imageRectTransform = image.GetComponent<RectTransform>();

        InitTexture(imageRectTransform.sizeDelta);
    }

    private void InitTexture(Vector2 size)
    {
        tempTexture2D = CreateTexture2D((int)size.x, (int)size.y);

        ResetTexture();
    }

    public static Texture2D CreateTexture2D(int width, int height)
    {
        Texture2D texture = new Texture2D(width, height, TextureFormat.RGBA32, false);
        texture.filterMode = FilterMode.Point;

        return texture;
    }

    private void ResetTexture()
    {
        for (int x = 0; x < tempTexture2D.width; x++)
        {
            for (int y = 0; y < tempTexture2D.height; y++)
            {
                Color pixel = Color.white;
                pixel.a = 0f;
                tempTexture2D.SetPixel(x, y, pixel);
                Debug.Log(pixel.a);
            }
        }
        tempTexture2D.Apply();
        image.sprite = Sprite.Create(tempTexture2D, new Rect(0, 0, tempTexture2D.width, tempTexture2D.height), Vector2.zero);
    }

    private void UpdateTexture2D(int xPos, int yPos, float radius)
    {
        if (xPos < 0 || xPos >= tempTexture2D.width || yPos < 0 || yPos >= tempTexture2D.height)
        {
            return;
        }

        for (int x = 0; x < tempTexture2D.width; x++)
        {
            for (int y = 0; y < tempTexture2D.height; y++)
            {
                double dx = x - xPos;
                double dy = y - yPos;
                double distanceSquared = dx * dx + dy * dy;
                if (distanceSquared <= radius * radius)
                {
                    tempTexture2D.SetPixel(x, y, Color.white);
                }
            }
        }
        tempTexture2D.Apply();
        image.sprite = Sprite.Create(tempTexture2D, new Rect(0, 0, tempTexture2D.width, tempTexture2D.height), Vector2.zero);
    }


    public void OnPointerClick(PointerEventData pointerEventData)
    {
        Debug.Log("OnPointerClick");
        if (pointerEventData.button == PointerEventData.InputButton.Left)
        {
            UpdateByWorldSpacePos(pointerEventData.position, 30);
        }
        else
        {
            ResetTexture();
        }
    }

    public void OnBeginDrag(PointerEventData pointerEventData)
    {
        Debug.Log("OnBeginDrag");
        UpdateByWorldSpacePos(pointerEventData.position, 30);
    }

    public void OnDrag(PointerEventData pointerEventData)
    {
        Debug.Log("OnDrag");
        UpdateByWorldSpacePos(pointerEventData.position, 30);
    }

    public void UpdateByWorldSpacePos(Vector2 pos, int radius)
    {
        var loaclPosition = transform.InverseTransformPoint(pos);
        var coordinate = new Vector2(loaclPosition.x + (imageRectTransform.sizeDelta.x * imageRectTransform.pivot.x), loaclPosition.y + (imageRectTransform.sizeDelta.y * imageRectTransform.pivot.y));
        UpdateTexture2D((int)coordinate.x, (int)coordinate.y, radius);
    }


}
