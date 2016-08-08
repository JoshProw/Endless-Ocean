using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

    //Movement Variables

    public float movementSpeed;


    Rigidbody rigidbody;
    Animator animator;

    bool facingRight;

    //Bool that indicates whether or not the gameobject is touching the ground.
    bool onGround = false;
    //An array that contains the collision objects that the circle collides with when jumping.
    Collider[] groundCollisions;
    //The radius of the cirle to check for objects in the ground layer when jumping.
    float groundCheckRadius = 0.2f;
    //A layer mask that filters out game objects that are not in the ground layer.
    public LayerMask groundLayerMask;
    //The transform of a gameobject used to position the cicle used to determine if the game object is on the ground when jumping.
    public Transform groundCheck;
    //The height the player will jump when the user makes them jump.
    public float jumpHeight;

    // Use this for initialization
    void Start () {
        //Retrieving components from the game objects this script is attatched to.
        this.rigidbody = this.GetComponent<Rigidbody>();
        //this.animator = this.GetComponent<Animator>();
        this.facingRight = true;
    }
	
	// Update is called once per frame
	void Update ()
    {
	    
	}
    
    /// <summary>
    /// Runs before every frame. Performs physics calculates for game objects to be displayed when the next frame is rendered and updates the animator.
    /// </summary>
    void FixedUpdate()
    {
        if(onGround && (Input.GetAxis("Jump") > 0))
        {
            this.onGround = false;
           // this.animator.SetBool("grounded", this.onGround);
            this.rigidbody.AddForce(new Vector3(0, jumpHeight, 0));
        }

        groundCollisions = Physics.OverlapSphere(this.groundCheck.position, this.groundCheckRadius, this.groundLayerMask);
        if(groundCollisions.Length > 0)
        {
            this.onGround = true;
        }
        else
        {
            this.onGround = false;
        }

        //this.animator.SetBool("grounded", this.onGround);

        //Getting horizontal movement from the user.
        float move = Input.GetAxis("Horizontal");
        //animator.SetFloat("speed",  Mathf.Abs(move));
        rigidbody.velocity = new Vector3(move * movementSpeed, this.rigidbody.velocity.y, 0);
        //If the game object starts moving left and is facing right turn the object around.
        if (move > 0 && !facingRight)
        {
            this.turnAround();
        }
        //If the game object starts moving right and is facing left turn the object around.
        if (move < 0 && facingRight)
        {
            this.turnAround();
        }
    }
    
    /// <summary>
    /// This function flips the game object when the user turns it around my moving it.
    /// </summary>
    void turnAround()
    {
        this.facingRight = !facingRight;
        Vector3 reverseScale = transform.localScale;
        reverseScale.z *= -1;
        transform.localScale = reverseScale;
    }
}
