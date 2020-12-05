using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityStandardAssets.CrossPlatformInput;

public class karakterKontrol : MonoBehaviour
{
    public Sprite[] yürümeAnim;
    public Sprite[] zıplamaAnim;
    public Sprite[] beklemeAnim;

    public Image siyahArkaPlan;
    public float arkaPlanSayacı;
    public Text canText;
    public Text altınText;
    int can = 100;
    int altınsayacı = 0;



    SpriteRenderer spriteRenderer;

    float horizontal = 0;
    float beklemeAnimZaman = 0;
    float yurumeAnimZaman = 0;
    
    Rigidbody2D fizik;

    Vector3 vec;
    Vector3 kameraİlkPoz;
    Vector3 kameraSonPoz;

    bool birKereZıpla = true;

    GameObject Kamera;

    int beklemeanimSayac = 0;
    int yurumeAnimSayac = 0;
    float anaMenuyeDonzaman = 0;
    void Start()
    {
        siyahArkaPlan.gameObject.SetActive(false);
        Time.timeScale = 1;
        spriteRenderer = GetComponent<SpriteRenderer>();
        fizik = GetComponent<Rigidbody2D>();
        Kamera = GameObject.FindGameObjectWithTag("MainCamera");
        if (SceneManager.GetActiveScene().buildIndex > PlayerPrefs.GetInt("kacıncı level"));
        {
            PlayerPrefs.SetInt("kaçıncılevel", SceneManager.GetActiveScene().buildIndex);

        }

        kameraİlkPoz = Kamera.transform.position - transform.position;

        canText.text = "Can  " + can;
        altınText.text = "30 - " + altınsayacı;

    }
    private void Update()
    {
        if (CrossPlatformInputManager.GetButtonDown("Jump"))
        {
            if (birKereZıpla==true)
            {
                fizik.AddForce(new Vector2(0, 500));
                birKereZıpla = false;
            }
        }
        
    }

    void FixedUpdate()
    {
        KarakterHareket();
        Animasyon();
        if (can<=0)
        {
            Time.timeScale = 0.4f;
            siyahArkaPlan.gameObject.SetActive(true);
            canText.enabled = false;
            arkaPlanSayacı += 0.03f;
            siyahArkaPlan.color = new Color(0,0,0,arkaPlanSayacı);
            anaMenuyeDonzaman += Time.deltaTime;
            if (anaMenuyeDonzaman > 1)
            {
                SceneManager.LoadScene("anamenu");
            }
        }
        
    }
    private void LateUpdate()
    {
        KameraKontrol();
    }
    void KarakterHareket()
    {
        horizontal = CrossPlatformInputManager.GetAxisRaw("Horizontal"); //GetAxis gittikçe artan GetAxisRaw direk geçiş yapan.
        vec = new Vector3(horizontal * 10, fizik.velocity.y, 0);
        fizik.velocity = vec;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        birKereZıpla = true;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag=="kursun")
        {
            can--;
            canText.text = "CAN " + can;
        }
        if(collision.gameObject.tag == "dusman")
        {
            can-=10;
            canText.text = "CAN " + can;
        }
        if (collision.gameObject.tag == "testere")
        {
            can -= 10;
            canText.text = "CAN " + can;
        }
        if (collision.gameObject.tag == "levelbitsin")
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
        if (collision.gameObject.tag == "canver")
        {
           
            if (can <= 90)
            {
                can += 10;
                canText.text = "CAN " + can;
            }
            else
            {
                can = 100;
                canText.text = "CAN " + can;
            }
            collision.GetComponent<BoxCollider2D>().enabled = false;
            collision.GetComponent<canver>().enabled = true;
            Destroy(collision.gameObject,3);
        }
        if (collision.gameObject.tag=="altın")
        {
            altınsayacı++;
            altınText.text = "30 - " + altınsayacı;
            Destroy(collision.gameObject);
        }
        if (collision.gameObject.tag=="su")
        {
            can = 0;
        }

    }
    void KameraKontrol()
    {
        kameraSonPoz = kameraİlkPoz + transform.position;
        Kamera.transform.position = Vector3.Lerp(Kamera.transform.position, kameraSonPoz, 0.1f); // lerp ile kamera pozisyonunu yumuşatıyoruz
    }
    void Animasyon()
    {
        if (birKereZıpla)
        {
            if (horizontal == 0)
            {
                beklemeAnimZaman += Time.deltaTime;
                if (beklemeAnimZaman > 0.05f)
                {
                    spriteRenderer.sprite = beklemeAnim[beklemeanimSayac++];
                    if (beklemeanimSayac == beklemeAnim.Length)
                    {
                        beklemeanimSayac = 0;
                    }
                    beklemeAnimZaman = 0;

                }

            }
            else if (horizontal > 0)
            {
                yurumeAnimZaman += Time.deltaTime;
                if (yurumeAnimZaman > 0.05f)
                {
                    spriteRenderer.sprite = yürümeAnim[yurumeAnimSayac++];
                    if (yurumeAnimSayac == yürümeAnim.Length)
                    {
                        yurumeAnimSayac = 0;
                    }
                    yurumeAnimZaman = 0;
                    transform.localScale = new Vector3(1, 1, 1);

                }

            }
            else if (horizontal < 0)
            {
                yurumeAnimZaman += Time.deltaTime;
                if (yurumeAnimZaman > 0.1f)
                {
                    spriteRenderer.sprite = yürümeAnim[yurumeAnimSayac++];
                    if (yurumeAnimSayac == yürümeAnim.Length)
                    {
                        yurumeAnimSayac = 0;
                    }
                    yurumeAnimZaman = 0;
                    transform.localScale = new Vector3(-1, 1, 1);

                }

            }
        }
        else
        {
            if (fizik.velocity.y>0)
            {
                spriteRenderer.sprite = zıplamaAnim[0];
            }
            else
            {
                spriteRenderer.sprite = zıplamaAnim[1];
            }
            if (horizontal>0)
            {
                transform.localScale=new Vector3(1, 1, 1);
            }
            else if(horizontal<0)
            {
                transform.localScale = new Vector3(-1, 1, 1);

            }
        }
    }
      
}
