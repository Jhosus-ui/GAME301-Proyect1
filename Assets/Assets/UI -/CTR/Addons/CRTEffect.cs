
using UnityEngine;
using UnityEngine.UI;

public class CRTEffect : MonoBehaviour
{
    [Header("UI Elements")]
    [SerializeField] private RawImage scanlines;
    [SerializeField] private RawImage interference;

    [Header("CRT Settings")]
    [SerializeField, Range(0f, 1f)] private float scanlineOpacity = 0.18f;
    [SerializeField] private float interferenceSpeed = 45f;
    [SerializeField, Range(0f, 1f)] private float flickerAmount = 0.08f;

    private Texture2D scanlineTexture;
    private Texture2D interferenceTexture;
    private float originalAlpha;

    private void Start()
    {

        scanlineTexture = new Texture2D(2, 4, TextureFormat.RGBA32, false);
        scanlineTexture.filterMode = FilterMode.Point;
        scanlineTexture.wrapMode = TextureWrapMode.Repeat;

        for (int y = 0; y < 4; y++)
        {
            Color pixelColor = y < 2
                ? new Color(0f, 0f, 0f, 0.8f)
                : Color.clear;

            for (int x = 0; x < 2; x++)
                scanlineTexture.SetPixel(x, y, pixelColor);
        }

        scanlineTexture.Apply();

        scanlines.texture = scanlineTexture;
        scanlines.uvRect = new Rect(0f, 0f, 1f, 50f);
        scanlines.color = new Color(1f, 1f, 1f, scanlineOpacity);
        scanlines.raycastTarget = false;
        interferenceTexture = new Texture2D(2, 32, TextureFormat.RGBA32, false);
        interferenceTexture.filterMode = FilterMode.Point;
        interferenceTexture.wrapMode = TextureWrapMode.Clamp;

        for (int y = 0; y < 32; y++)
        {
            float distance = Mathf.Abs(y - 16f) / 16f;
            float alpha = (1f - distance) * 0.18f;

            for (int x = 0; x < 2; x++)
                interferenceTexture.SetPixel(x, y, new Color(0.6f, 1f, 0.75f, alpha));
        }

        interferenceTexture.Apply();

        interference.texture = interferenceTexture;
        interference.color = Color.white;
        interference.raycastTarget = false;

        originalAlpha = scanlineOpacity;
    }

    private void Update()
    {
        RectTransform rect = interference.rectTransform;

        float height = scanlines.rectTransform.rect.height;
        float position = Mathf.Repeat(Time.unscaledTime * interferenceSpeed, height + 40f);

        rect.anchoredPosition = new Vector2(0f, height * 0.5f - position);
        float flicker = Random.Range(-flickerAmount, flickerAmount);
        float alpha = Mathf.Clamp01(originalAlpha + flicker);

        scanlines.color = new Color(1f, 1f, 1f, alpha);
    }

    private void OnDestroy()
    {
        if (scanlineTexture != null)
            Destroy(scanlineTexture);

        if (interferenceTexture != null)
            Destroy(interferenceTexture);
    }
}
