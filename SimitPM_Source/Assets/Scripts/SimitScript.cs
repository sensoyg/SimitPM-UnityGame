using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SimitScript : MonoBehaviour
{
    // Karakterimizin fizikleriyle ilgili tanımlamalar.
    Rigidbody2D simitBody;
    public float speed = 10f;
    public float jumpHeight = 8f;
    public float donusHiz = 250f;
    private float donus;
    public float gravityIncrease = 0.2f;
    private float gravity;

    // Oyundaki bazı animasyonlarla ilgili tanımlamalar.
    public Animator simitAnim;
    public Animator ayranAnim;
    public Animator cayAnim;
    public Animator peynirAnim;

    // Özel güçlerimizin tanımlandığı satırlar.
    public GameObject peynir;
    public GameObject ayran;
    public GameObject cay;

    // Karakterimizin zıplaması ile ilgili değişkenler.
    public bool isGround;
    public float radius = 1.30f;
    public Transform groundCheck;
    public LayerMask groundLayer;

    
    // Skor sayacı ile ilgili değişkenler.
    public Text scoreText;
    public Text highScoreText;
    internal float decimalScore;
    internal int highScore;
    internal int score;
    public float skorCarpan = 5f;

    // Hızın arttırılması ile ilgili tanımlamalar.
    private bool isEnableForSpeed;
    private float speedUp;
    // Oyunun durdurulması ile ilgili tanımlamalar.
    private float speedTemp;
    private bool isContinue = false;

    //Oyunun başlaması ve bitmesi ile ilgili tanımlamalar.
    private int enterCounter = 0;
    public GameObject gameOverPrefab;

    void Start()
    {
        gravity = GetComponent<Rigidbody2D>().gravityScale;
        ayran.GetComponent<SpriteRenderer>().sortingOrder = -1;
        cay.GetComponent<SpriteRenderer>().sortingOrder = -1;
        peynir.GetComponent<SpriteRenderer>().sortingOrder = -1;
        // Fareyi gizleyen satırlar.
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        simitBody = gameObject.GetComponent<Rigidbody2D>();
        // En yüksek skoru cache içerisine kaydeden satır.
        highScore = PlayerPrefs.GetInt("HighScore");

        highScoreText.text = highScore.ToString();
        // Karakterimizin gitgide hızlanmasını sağlayan fonksiyonu çağıran satır.
        StartCoroutine(faster());

        // Oyunun en başında karakterimizin hareket etmemesini ve skor saymamasını sağlayan satırlar.
        skorCarpan = 0f;
        donusHiz = 0f;
        speed = 0f;
        gravityIncrease = 0f;
        jumpHeight = 0f;
        isEnableForSpeed = true;
        isContinue = false;
    }

    // Karakterimizin gitgide hızlanmasını sağlayan fonksiyon.
    public IEnumerator faster()
    {
        while (true)
        {
            speed += speedUp;
            gravity += gravityIncrease;
            yield return new WaitForSeconds(10f);
        }
    }

    // Zıplama özel gücünü aldıktan 10 saniye sonra zıplamanın normale dönmesini sağlayan fonksiyon.
    public void jumpIncrease()
    {
        jumpHeight = 16;
        ayran.GetComponent<SpriteRenderer>().sortingOrder = -1;
        ayranAnim.SetBool("isAyran", false);
    }

    // Hız yavaşlatma özel gücünü aldıktan 10 saniye sonra hızın normale dönmesini sağlayan fonksiyon.
    public void speedIncrease()
    {
        speed *= 1.4f;
        cay.GetComponent<SpriteRenderer>().sortingOrder = -1;
        cayAnim.SetBool("isAyran", false);
    }

    // Skor çarpanını arttıran özel gücü aldıktan 10 saniye sonra skor çarpanının normale dönmesini sağlayan fonksiyon.
    public void scoreIncrease()
    {
        skorCarpan /= 2;
        peynir.GetComponent<SpriteRenderer>().sortingOrder = -1;
        peynirAnim.SetBool("isPeynir", false);
    }
    
    void Update()
    {
        // Karakterimizin fiziklerini ayarlayan satırlar.
        donus = donusHiz * Time.deltaTime * -1;
        transform.Rotate(0, 0, donus);
        simitBody.velocity = new Vector2(speed, simitBody.velocity.y);
        isGround = Physics2D.OverlapCircle(groundCheck.position, radius, groundLayer);

        //Karakterimizin; ölme, oyunun durdululması, oyunun başlamaması gibi durumlarda hızlanmamasını sağlayan if yapısı.
        if (isEnableForSpeed)
        {
            speedUp = 0f;
            gravityIncrease = 0f;
        }
        else if (!isEnableForSpeed)
        {
            speedUp = 1f;
            gravityIncrease = 0.2f;
        }

        // Zıplama tuşunu kontrol eden if yapısı.
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (enterCounter < 1)
            {

                skorCarpan = 5f;
                donusHiz = 250f;
                speed = 6f;
                gravityIncrease = 0.2f;
                jumpHeight = 16f;
                isEnableForSpeed = false;
                enterCounter++;
            }
            if (isGround)
            {
                SoundController.PlaySound("jump");
                simitBody.velocity = Vector2.up * jumpHeight;
            }
        }

        // Bir diğer zıplama tuşunu kontrol eden if yapısı
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (isGround)
            {
                SoundController.PlaySound("jump");
                simitBody.velocity = Vector2.up * jumpHeight;
            }
        }

        // Durdurma tuşunu kontrol eden if yapısı.
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            if (speed != 0)
            {
                speedTemp = speed;
            }
            if (isContinue)
            {
                
                skorCarpan = 0f;
                donusHiz = 0f;
                speed = 0f;
                gravityIncrease = 0f;
                jumpHeight = 0f;
                isEnableForSpeed = true;
                isContinue = false;
            }
            else if (!isContinue)
            {
                
                skorCarpan = 5f;
                donusHiz = 250f;
                speed = speedTemp;
                jumpHeight = 16f;
                isEnableForSpeed = false;
                isContinue = true;
            }
        }
        
        // Oyunu başlatma tuşunu kontrol eden if yapısı.
        if (Input.GetKeyDown(KeyCode.Return))
        {
            if(enterCounter < 1)
            {
                
                skorCarpan = 5f;
                donusHiz = 250f;
                speed = 6f;
                gravityIncrease = 0.2f;
                jumpHeight = 16f;
                isEnableForSpeed = false;
                enterCounter++;
            }
            
        }

        //Skorun oluşturulması ve basılması ile ilgili satırlar. 
        decimalScore += skorCarpan * Time.deltaTime;
        score = Mathf.RoundToInt(decimalScore);
        scoreText.text = score.ToString();

        // En yüksek skorun belirlenmesini sağlayan if yapısı.
        if(score > highScore)
        {
            PlayerPrefs.SetInt("HighScore", score);
        }

    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Karakterimizin zıplama süper gücüne değince olacakları kontrol eden if yapısı.
        if (other.gameObject.tag == "Ayran")
        {
            ayran.GetComponent<SpriteRenderer>().sortingOrder = 1;
            ayranAnim.SetBool("isAyran", true);
            SoundController.PlaySound("power");
            Destroy(other.gameObject);
            jumpHeight = 18.5f;
            Invoke("jumpIncrease", 10);
        }

        // Karakterimizin yavaşlama süper gücüne değince olacakları kontrol eden if yapısı.
        if (other.gameObject.tag == "Cay")
        {
            cay.GetComponent<SpriteRenderer>().sortingOrder = 1;
            cayAnim.SetBool("isAyran", true);
            SoundController.PlaySound("power");
            Destroy(other.gameObject);
            speed /= 1.4f;
            Invoke("speedIncrease", 10);
        }

        // Karakterimizin skor çarpanını arttıran süper güce değince olacakları kontrol eden if yapısı.
        if (other.gameObject.tag == "Peynir")
        {
            peynir.GetComponent<SpriteRenderer>().sortingOrder = 1;
            peynirAnim.SetBool("isAyran", true);
            SoundController.PlaySound("power");
            Destroy(other.gameObject);
            skorCarpan *= 2;
            Invoke("scoreIncrease", 10);

        }

        // Karakterimizin bir düşmana değince olacakları kontrol eden if yapısı.
        if (other.gameObject.tag == "Enemy")
        {
            SoundController.PlaySound("death");
            isEnableForSpeed = true;
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            skorCarpan = 0f;
            donusHiz = 0f;
            speed = 0f;
            jumpHeight = 0f;
            simitAnim.SetBool("isDead", true);
            GameObject instantiatedGameOver = Instantiate(gameOverPrefab, new Vector2(0, 0), Quaternion.identity);
            instantiatedGameOver.transform.SetParent(GameObject.Find("Canvas").transform, false);

        }
        
    }

    // Oyun bitince yeniden başlatmayı sağlayan fonksiyon.
    public void RestartGame()
    {
        SceneManager.LoadScene(0);
    }
    
}
