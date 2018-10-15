using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Swipe : MonoBehaviour {

    Vector2 startPos, endPos, direction, defaultPos, oldSpeed, newSpeed, hmmSpeed;
    float touchTimeStart, touchTimeFinish, timeInterval;

    [Range(0.05f, 1f)]
    public float throwForce = 0.3f;
    public float bounceForce = 0.000000000001f;

    public AudioSource audioSource;
    [SerializeField] List<AudioClip> sounds;

    bool hasThrown = false;
    bool hasBounced = false;

    void Start(){
        defaultPos = transform.position;
        hmmSpeed = new Vector2(10f, 10f);
    }
    /*
    //PHONE!
	// Update is called once per frame 
	void FixedUpdate () {
	    
        //Touching the screen
        if(Input.touchCount > 0 && Input.GetTouch (0).phase == TouchPhase.Began){
            touchTimeStart = Time.time;
            startPos = Input.GetTouch(0).position;
        }

        //Releasing
        if(Input.touchCount > 0 && Input.GetTouch(0).phase  == TouchPhase.Ended){
            touchTimeFinish = Time.time;
            timeInterval = touchTimeFinish - touchTimeStart;
            endPos = Input.GetTouch(0).position;
            direction = startPos - endPos;
            GetComponent<Rigidbody2D>().AddForce(-direction / timeInterval * throwForce);
        }
	}
    */

    private void Update(){

        if(hasThrown == false && Input.GetMouseButtonDown(0)){
            touchTimeStart = Time.time;
            startPos = Input.mousePosition;
        }

        if(hasThrown == false && Input.GetMouseButtonUp(0))
        {
            touchTimeFinish = Time.time;
            timeInterval = touchTimeFinish - touchTimeStart;
            endPos = Input.mousePosition;
            direction = startPos - endPos;
            GetComponent<Rigidbody2D>().AddForce(-direction / timeInterval * throwForce);
            hasThrown = true;
        }
        
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        if(hasThrown == true){
            playSound();
        }
        if (hasThrown == true && coll.gameObject.tag == "DangerZone"){
            GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            GetComponent<Rigidbody2D>().angularVelocity = 0;
            hasThrown = false;
            GetComponent<Rigidbody2D>().MovePosition(defaultPos);
        }

        if(hasThrown == true && coll.gameObject.tag == "Forcebouncer"){
            oldSpeed = GetComponent<Rigidbody2D>().velocity;
            GetComponent<Rigidbody2D>().AddForce(Vector2.up * hmmSpeed, ForceMode2D.Impulse);
            newSpeed = GetComponent<Rigidbody2D>().velocity;
            hasBounced = true;
        }

        if(hasThrown == true && hasBounced == true && coll.gameObject.tag == "NormalBounce"){
            GetComponent<Rigidbody2D>().AddForce(-GetComponent<Rigidbody2D>().velocity /hmmSpeed);
            Debug.Log("normalbounce reached");
            hasBounced = false;
        }

        if(coll.gameObject.tag == "WinZone")
        {
            SceneManager.LoadScene("Challenge 2");
        }
       
    }
   
    private void playSound()
    {
        int randomSound = Random.Range(0, 3);
        audioSource.PlayOneShot(sounds[randomSound], 1f);
    }

}
