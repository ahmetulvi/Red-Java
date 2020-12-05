using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif
public class dusmanKontrol : MonoBehaviour
{
    GameObject[] gidilecekNoktalar;
    bool aradakiMesafeyiBirKereAl = true;
    bool ilerigeri = true;
    Vector3 aradakiMesafe;
    int aradakiMesafeSayacı = 0;
    GameObject karakter;
    RaycastHit2D ray;
    public LayerMask layerMask;
    int hız = 5;

    public Sprite onTaraf;
    public Sprite arkaTaraf;
    SpriteRenderer spriteRenderer;
    public GameObject kursun;
    public float atesZamanı=0;
    void Start()
    {
        gidilecekNoktalar = new GameObject[transform.childCount];
        karakter = GameObject.FindGameObjectWithTag("Player");
        spriteRenderer = GetComponent<SpriteRenderer>();

        for (int i = 0; i < gidilecekNoktalar.Length; i++)
        {
            gidilecekNoktalar[i] = transform.GetChild(0).gameObject;
            gidilecekNoktalar[i].transform.SetParent(transform.parent);
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        beniGordumu();
        if (ray.collider.tag=="Player")
        {
            hız = 8;
            spriteRenderer.sprite = onTaraf;
            atesEt();

        }
        else
        {
            hız=4;
            spriteRenderer.sprite = arkaTaraf;
        }
        noktalaraGit();
    }

    void atesEt()
    {
        atesZamanı += Time.deltaTime;
        if (atesZamanı>Random.Range(0.2f,1))
        {
            Instantiate(kursun, transform.position, Quaternion.identity);
            atesZamanı = 0;
        }

    }
    void beniGordumu()
    {
        Vector3 rayYonum = karakter.transform.position - transform.position;
        ray = Physics2D.Raycast(transform.position, rayYonum, 1000,layerMask);
        Debug.DrawLine(transform.position, ray.point, Color.magenta);
    }
    void noktalaraGit()
    {
        if (aradakiMesafeyiBirKereAl)
        {
            aradakiMesafe = (gidilecekNoktalar[aradakiMesafeSayacı].transform.position - transform.position).normalized;
            aradakiMesafeyiBirKereAl = false;
        }
        float mesafe = Vector3.Distance(transform.position, gidilecekNoktalar[aradakiMesafeSayacı].transform.position);
        transform.position += aradakiMesafe * Time.deltaTime * hız;
        if (mesafe < 0.5f)
        {
            aradakiMesafeyiBirKereAl = true;
            if (aradakiMesafeSayacı == gidilecekNoktalar.Length - 1)
            {
                ilerigeri = false;
            }
            else if (aradakiMesafeSayacı == 0)
            {
                ilerigeri = true;
            }
            if (ilerigeri)
            {
                aradakiMesafeSayacı++;
            }
            else
            {
                aradakiMesafeSayacı--;
            }

        }
    }
   public Vector2 getYon()
    {
        return (karakter.transform.position - transform.position).normalized;
    }
#if UNITY_EDITOR
    public void OnDrawGizmos()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.GetChild(i).transform.position, 1);
        }
        for (int i = 0; i < transform.childCount - 1; i++)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawLine(transform.GetChild(i).transform.position, transform.GetChild(i + 1).transform.position);
        }
    }
#endif
}
#if UNITY_EDITOR
[CustomEditor(typeof(dusmanKontrol))]
[System.Serializable]

class dusmanKontrolEditor : Editor
{
    public override void OnInspectorGUI()
    {
        dusmanKontrol script = (dusmanKontrol)target;
        EditorGUILayout.Space();
        if (GUILayout.Button("ÜRET"))
        {
            GameObject yeniObjem = new GameObject();
            yeniObjem.transform.parent = script.transform;
            yeniObjem.transform.position = script.transform.position;
            yeniObjem.name = script.transform.childCount.ToString();
        }
        EditorGUILayout.Space();
        EditorGUILayout.PropertyField(serializedObject.FindProperty("layerMask"));//Eğer Editör Kodu yazdıysak, bir değişkeni dışarı çıkarmak için bu kodu yazmalıyım.
        EditorGUILayout.PropertyField(serializedObject.FindProperty("onTaraf"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("arkaTaraf"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("kursun"));


        serializedObject.ApplyModifiedProperties();
        serializedObject.Update();

    }                                                                            


}
#endif
