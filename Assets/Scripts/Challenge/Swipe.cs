using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Swipe : MonoBehaviour
{
    // Cached references
    public SpriteRenderer spriteRenderer;
    public Sprite spriteW, spriteR, spriteG, spriteB, spriteY;

    // Public variables
    [Range(0.05f, 1f)]
    public float throwForce = 0.3f;
    public float bounceForce = 0.000000000001f;
    public AudioSource audioSource;
    public AudioClip win;

    // Private variables
    Vector2 startPos, endPos, direction, defaultPos, hmmSpeed, currentPos;
    float touchTimeStart, touchTimeFinish, timeInterval;
    [SerializeField] List<AudioClip> sounds;
    bool hasThrown = false;
    bool hasBounced = false;
    bool hasWon = false;
    bool hasFreeze = false;
    bool hasDied = false;

    // --- START ---
    void Start()
    {
        defaultPos = transform.position;
        Debug.Log(defaultPos);
        this.gameObject.GetComponent<SpriteRenderer>().sprite = spriteW;
        hmmSpeed = new Vector2(10f, 10f);
    }

    //PHONE!
    // Update is called once per frame 
#if UNITY_ANDROID
    void Update()
    {
        currentPos = transform.position;
        //Touching the screen
        if (hasThrown == false && Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            Debug.Log("Mouse Down");
            GetComponent<Rigidbody2D>().isKinematic = false;
            touchTimeStart = Time.time;
            startPos = Input.mousePosition;
        }

        //Releasing
        if (hasThrown == false && Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended)
        {
            Debug.Log("Mouse Up");
            touchTimeFinish = Time.time;
            timeInterval = touchTimeFinish - touchTimeStart;
            endPos = Input.mousePosition;
            direction = startPos - endPos;
            hasFreeze = false;
            GetComponent<Rigidbody2D>().gravityScale = 1;
            GetComponent<Rigidbody2D>().AddForce(-direction / timeInterval * throwForce);
            Debug.Log("Force!!! " + GetComponent<Rigidbody2D>().velocity);
            hasThrown = true;
        }

        //Keeps the ball frozen, this is probably obsolete
        if (hasFreeze == true){
            Debug.Log("Real position: " + currentPos);
            GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            GetComponent<Rigidbody2D>().angularVelocity = 0;
            GetComponent<Rigidbody2D>().gravityScale = 0;
            this.gameObject.GetComponent<SpriteRenderer>().sprite = spriteY;
        }

        //If the ball is fired it will turn white
        if (hasFreeze != true && hasBounced != true && hasDied != true && hasWon != true){
            this.gameObject.GetComponent<SpriteRenderer>().sprite = spriteW;
        }

        if (hasDied == true){
            this.gameObject.GetComponent<SpriteRenderer>().sprite = spriteR;
        }
    }
#endif


#if UNITY_STANDALONE
    void Update(){

        currentPos = transform.position;
        //Mousedown, starts the process of throwing the ball
        if (hasThrown == false && Input.GetMouseButtonDown(0)){
            Debug.Log("Mouse Down");
            GetComponent<Rigidbody2D>().isKinematic = false;
            touchTimeStart = Time.time;
            startPos = Input.mousePosition;
        }

        //Mouseup, finishes the process
        if (hasThrown == false && Input.GetMouseButtonUp(0)){
            Debug.Log("Mouse Up");
            touchTimeFinish = Time.time;
            timeInterval = touchTimeFinish - touchTimeStart;
            endPos = Input.mousePosition;
            direction = startPos - endPos;
            hasFreeze = false;
            GetComponent<Rigidbody2D>().gravityScale = 1;
            GetComponent<Rigidbody2D>().AddForce(-direction / timeInterval * throwForce);
            Debug.Log("Force!!! " + GetComponent<Rigidbody2D>().velocity);
            hasThrown = true;
        }

        //Keeps the ball frozen, this is probably obsolete
        if (hasFreeze == true){
            Debug.Log("Real position: " + currentPos);
            GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            GetComponent<Rigidbody2D>().angularVelocity = 0;
            GetComponent<Rigidbody2D>().gravityScale = 0;
            this.gameObject.GetComponent<SpriteRenderer>().sprite = spriteY;
        }

        //If the ball is fired it will turn white
        if (hasFreeze != true && hasBounced != true && hasDied != true && hasWon != true){
            this.gameObject.GetComponent<SpriteRenderer>().sprite = spriteW;
        }

        if (hasDied == true){
            this.gameObject.GetComponent<SpriteRenderer>().sprite = spriteR;
        }
    }

#endif

    // --- METHODS ---
    void OnCollisionEnter2D(Collision2D coll)
    {
        //Plays collision sound when colliding with anything
        if (hasThrown == true)
        {
            playCollision();
        }

        //Resets playerposition
        if (hasThrown == true && coll.gameObject.tag == "DangerZone")
        {
            StartCoroutine(Death());
        }

        //Gives the player a big boost to his bounce
        if (hasThrown == true && coll.gameObject.tag == "Forcebouncer")
        {
            GetComponent<Rigidbody2D>().AddForce(Vector2.up * hmmSpeed, ForceMode2D.Impulse);
            this.gameObject.GetComponent<SpriteRenderer>().sprite = spriteB;
            hasBounced = true;
        }

        //After ForceBouncer has been in effect, this is the first collision that happens thus will reset the speed and the sprite
        if (hasThrown == true && hasBounced == true && coll.gameObject.tag == "NormalBounce")
        {
            GetComponent<Rigidbody2D>().AddForce(-GetComponent<Rigidbody2D>().velocity / hmmSpeed);
            hasBounced = false;
        }

        //Every bounce on normal surface changes the sprite to the stock White.
        if (coll.gameObject.tag == "NormalBounce")
        {
            Debug.Log("ChangetoWhite!!!");
            this.gameObject.GetComponent<SpriteRenderer>().sprite = spriteW;
        }

        //Lets the player shoot once again and also sticks the ball in place.
        if (hasThrown == true && coll.gameObject.tag == "PlusZone")
        {
            GetComponent<Rigidbody2D>().isKinematic = true;
            Debug.Log("currentPos before hit: " + currentPos);
            transform.position = currentPos;
            Debug.Log("currentPos after hit: " + currentPos);
            hasFreeze = true;
            hasThrown = false;
        }

        //Win!!!
        if (coll.gameObject.tag == "WinZone")
        {
            GetComponent<Rigidbody2D>().isKinematic = true;
            this.gameObject.GetComponent<SpriteRenderer>().sprite = spriteG;
            GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            GetComponent<Rigidbody2D>().angularVelocity = 0;
            if (hasWon == false)
            {
                hasWon = true;
                audioSource.PlayOneShot(win, 2f);
                StartCoroutine(ChangeMap());
            }
        }
    }

    IEnumerator ChangeMap()
    {
        yield return new WaitForSeconds(2.5f);
        if (SceneManager.GetActiveScene().name == "Challenge 5")
        {
            SceneManager.LoadScene("Main Menu");
        }
        else
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }

    IEnumerator Death()
    {
        GetComponent<Rigidbody2D>().isKinematic = true;
        hasDied = true;
        Debug.Log("Die");
        GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        GetComponent<Rigidbody2D>().angularVelocity = 0;
        this.gameObject.GetComponent<SpriteRenderer>().sprite = spriteR;
        yield return new WaitForSeconds(0.3f);
        transform.position = defaultPos;
        GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        GetComponent<Rigidbody2D>().angularVelocity = 0;
        this.gameObject.GetComponent<SpriteRenderer>().sprite = spriteW;
        hasDied = false;
        hasThrown = false;
    }

    private void playCollision()
    {
        if (hasWon == false)
        {
            int randomSound = Random.Range(0, 3);
            audioSource.PlayOneShot(sounds[randomSound], 1f);
        }
    }
}
