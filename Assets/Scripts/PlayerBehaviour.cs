using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    public bool isGround;
    public bool canJump = true;
    private Rigidbody2D rb2;
    private GameController gameController;
    private AudioSource audioSource;
    [SerializeField] private PhysicsMaterial2D bounceMat;
    [SerializeField] private PhysicsMaterial2D normalMat;
    public LayerMask groundMask;
    public float jumpForce;
    public float jumpFar;
    public float jumpHigh;
    [SerializeField] private GameObject groundCheck;
    [SerializeField] private float groundCheckRadius=0.1f;
    [SerializeField] private Animator playerAnim;

    public AudioClip bump;
    public AudioClip jump;
    public AudioClip land;
    public AudioClip splat;

    // Start is called before the first frame update
    void Awake()
    {   
        audioSource = GetComponent<AudioSource>();
        gameController = GameObject.Find("GameController").GetComponent<GameController>();
        rb2 = this.gameObject.GetComponent<Rigidbody2D>();
        jumpForce = 0.0f;
        LoadPlayerData();
    }

    // Update is called once per frame
    void Update()
    {
        //Check is Grounded
        isGround = Physics2D.OverlapBox(groundCheck.transform.position, groundCheck.GetComponent<BoxCollider2D>().size,1.57f, groundMask);
        // isGround = Physics2D.OverlapCircle(groundCheck.transform.position, 
        // groundCheckRadius,groundMask);
        
        //Bounce force
        if(jumpForce > 0 && isGround == false)
        {
            rb2.sharedMaterial = bounceMat;
        }
        else
        {
            rb2.sharedMaterial = normalMat;
        }


        //Set player's animations
        if(isGround == true)
        {
            playerAnim.SetBool("Ground",true);
            playerAnim.SetBool("Jumping",false);
        }
        if(isGround == false)
        {
            playerAnim.SetBool("Ground",false);
        }
        if(canJump == false && isGround == false)
        {
            playerAnim.SetBool("Jumping",true);
        }
    }

  

    //Reset jump when player land
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Ground") && isGround)
        {
            audioSource.PlayOneShot(land);
            rb2.velocity = Vector2.zero;
            // rb2.angularVelocity = 0f;
            canJump = true;    
            jumpForce = 0.0f;

            //save position when player lands on ground
            SavePlayerPositionData();
        }
        else
        {
            audioSource.PlayOneShot(bump);
        }
    }

    //Check floor player is landing
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Floor"))
        {
            gameController.changeFloorText(collision.gameObject.name);
        }
    }

    //Player jump behaviour
    public void Jump(float forceJump,float jumpDir)
    {
        playerAnim.SetBool("Charging",false);
        audioSource.PlayOneShot(jump);
        jumpForce = forceJump;
        Debug.Log("Jump Dir " + jumpDir +  "   Jump Force: " + jumpForce);
        if(isGround)
        {
            rb2.velocity = new Vector2(jumpDir*jumpForce,jumpForce);    
            canJump = false;
        }
        if(jumpDir > 0)
        {
            this.gameObject.transform.Find("Skin").rotation = Quaternion.Euler(0f, 0f, 0f);
        }
        else
        {
            this.gameObject.transform.Find("Skin").rotation = Quaternion.Euler(0f, 180f, 0f);
        }
    }

    //Save load system
    public void SavePlayerPositionData()
    {
        SaveLoadData.SavePositionData(this);
    }

    public void LoadPlayerData()
    {
        if(PlayerPrefs.HasKey("x"))
        {
            this.gameObject.transform.position = new Vector3(PlayerPrefs.GetFloat("x"),PlayerPrefs.GetFloat("y"),PlayerPrefs.GetFloat("z"));
        }

        if(PlayerPrefs.HasKey("Skin"))
        {
            gameController.GetComponent<ChangeSkinsController>().skinChange(PlayerPrefs.GetInt("Skin"));
        }

        if(PlayerPrefs.HasKey("High"))
        {
            jumpHigh = PlayerPrefs.GetFloat("High");
        }
        else
        {
            jumpHigh = 0.5f;
        }

        if(PlayerPrefs.HasKey("Far"))
        {
            jumpFar = PlayerPrefs.GetFloat("Far");
        }
        else
        {
            jumpFar = 0.5f;
        }
    } 

}
