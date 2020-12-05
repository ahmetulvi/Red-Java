using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class canver : MonoBehaviour
{
    public Sprite []animasyonKareleri;
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
        if (zaman>0.1f)
        {
            spriteRenderer.sprite = animasyonKareleri[animasyonKareleriSayacı++];
            if (animasyonKareleri.Length==animasyonKareleriSayacı)
            {
                animasyonKareleriSayacı = animasyonKareleri.Length-1;
            }
            zaman = 0;
        }
        
    }
}
