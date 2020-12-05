using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ayarlar : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public static void low()
    {
        QualitySettings.SetQualityLevel(0);
        
    }
    public static void med()
    {
        QualitySettings.SetQualityLevel(3);
    }
    public static void high()
    {
        QualitySettings.SetQualityLevel(6);
    }
    public void geri()
    {
        SceneManager.LoadScene(0);
    }
}
