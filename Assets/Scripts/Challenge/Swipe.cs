using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Swipe : MonoBehaviour
{

    public SpriteRenderer spriteRenderer;
    public Sprite spriteW, spriteR, spriteG, spriteB, spriteY;

    Vector2 startPos, endPos, direction, defaultPos, oldSpeed, newSpeed, hmmSpeed, currentPos;
    float touchTimeStart, touchTimeFinish, timeInterval;

    [Range(0.05f, 1f)]
    public float throwForce = 0.3f;
    public float bounceForce = 0.000000000001f;

    public AudioSource audioSource;
    [SerializeField] List<AudioClip> sounds;
    public AudioClip win;

    bool hasThrown = false;
    bool hasBounced = false;
    bool hasWon = false;
    bool hasFreeze = false;
    bool hasDied = false;

    void Start(){
        defaultPos = transform.position;
        Debug.Log(defaultPos);
        this.gameObject.GetComponent<SpriteRenderer>().sprite = spriteW;
        hmmSpeed = new Vector2(10f, 10f);
    }

    //PHONE!
    // Update is called once per frame 
#if UNITY_ANDROID
    void FixedUpdate () {
	    
        //Touching the screen
        if(hasThrown == false && Input.touchCount > 0 && Input.GetTouch (0).phase == TouchPhase.Began){
            touchTimeStart = Time.time;
            startPos = Input.GetTouch(0).position;
        }

        //Releasing
        if(hasThrown == false && Input.touchCount > 0 && Input.GetTouch(0).phase  == TouchPhase.Ended){
            touchTimeFinish = Time.time;
            timeInterval = touchTimeFinish - touchTimeStart;
            endPos = Input.GetTouch(0).position;
            direction = startPos - endPos;
            GetComponent<Rigidbody2D>().AddForce(-direction / timeInterval * throwForce);
            hasThrown = true;
        }
	}
#endif


#if UNITY_STANDALONE
    void Update(){

        currentPos = transform.position;
        if (hasThrown == false && Input.GetMouseButtonDown(0)){
            touchTimeStart = Time.time;
            startPos = Input.mousePosition;
        }

        if (hasThrown == false && Input.GetMouseButtonUp(0)){
            touchTimeFinish = Time.time;
            timeInterval = touchTimeFinish - touchTimeStart;
            endPos = Input.mousePosition;
            direction = startPos - endPos;
            GetComponent<Rigidbody2D>().gravityScale = 1;
            GetComponent<Rigidbody2D>().AddForce(-direction / timeInterval * throwForce);
            hasThrown = true;
            hasFreeze = false;
        }

        if (hasFreeze == true){
            GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            GetComponent<Rigidbody2D>().angularVelocity = 0;
            GetComponent<Rigidbody2D>().gravityScale = 0;
            this.gameObject.GetComponent<SpriteRenderer>().sprite = spriteY;
        }

        if (hasFreeze != true && hasBounced != true && hasDied != true){
            this.gameObject.GetComponent<SpriteRenderer>().sprite = spriteW;
        }
        if (hasDied == true){
            this.gameObject.GetComponent<SpriteRenderer>().sprite = spriteR;
        }
    }
#endif

    void OnCollisionEnter2D(Collision2D coll){
        //Plays collision sound when colliding with anything
        if (hasThrown == true){
            playCollision();
        }

        //Resets playerposition
        if (hasThrown == true && coll.gameObject.tag == "DangerZone"){
            StartCoroutine(Death());
        }

        //Gives the player a big boost to his bounce
        if (hasThrown == true && coll.gameObject.tag == "Forcebouncer"){
            oldSpeed = GetComponent<Rigidbody2D>().velocity;
            GetComponent<Rigidbody2D>().AddForce(Vector2.up * hmmSpeed, ForceMode2D.Impulse);
            newSpeed = GetComponent<Rigidbody2D>().velocity;
            this.gameObject.GetComponent<SpriteRenderer>().sprite = spriteB;
            hasBounced = true;
        }

        //After ForceBouncer has been in effect, this is the first collision that happens thus will reset the speed and the sprite
        if (hasThrown == true && hasBounced == true && coll.gameObject.tag == "NormalBounce"){
            GetComponent<Rigidbody2D>().AddForce(-GetComponent<Rigidbody2D>().velocity / hmmSpeed);
            hasBounced = false;
        }

        if(coll.gameObject.tag == "NormalBounce")
        {
            Debug.Log("ChangetoWhite!!!");
            this.gameObject.GetComponent<SpriteRenderer>().sprite = spriteW;
        }
        
        if (hasThrown == true && coll.gameObject.tag == "PlusZone"){
            transform.position = currentPos;
            hasFreeze = true;
           hasThrown = false;
        }

        //Win!!!
        if (coll.gameObject.tag == "WinZone"){
            GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            GetComponent<Rigidbody2D>().angularVelocity = 0;
            if (hasWon == false){
                hasWon = true;
                audioSource.PlayOneShot(win, 2f);
                StartCoroutine(ChangeMap());
            }
        }

    }

    IEnumerator ChangeMap(){
        yield return new WaitForSeconds(2.5f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
    }

    IEnumerator Death(){
        hasDied = true;
        Debug.Log("Should wait here");
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

    private void playCollision(){
        if (hasWon == false){
            int randomSound = Random.Range(0, 3);
            audioSource.PlayOneShot(sounds[randomSound], 1f);
        }
    }

}
