using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class suAnimasyonu : MonoBehaviour
{
    public Sprite[] animasyonKareleri;
    int animasyonKareleriSayacı = 0;
    SpriteRenderer spriteRenderer;
    float zaman = 0;
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

    }

    void Update()
    {
        zaman += Time.deltaTime;
        if (zaman > 0.05f)
        {
            spriteRenderer.sprite = animasyonKareleri[animasyonKareleriSayacı++];
            if (animasyonKareleri.Length == animasyonKareleriSayacı)
            {
                animasyonKareleriSayacı = 0;
            }
            zaman = 0;
        }
    }
}
