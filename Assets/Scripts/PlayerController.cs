using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float m_movementSpeed = 30f; //declare variable float for left and right speed to be edited in Unity editor
    [SerializeField] private float m_jumpForce = 30f; //float var for jump force of char to be also set in Unity editor
    private float horizontalMovement = 0f; //place holder variable to be set in update
    protected bool spaceBarDown = false; //boolean for checking if space bar is down
    private bool move = false; //bolean to check if player moves
    private bool facingRight = true; //boolean to check if player if facing the right direction
    private sbyte deathTokensCollected = 0; //tokens collected by Player
    private bool gameIsWon = false;


    [SerializeField] private TextMeshProUGUI countDownText;
    [SerializeField] private TextMeshProUGUI scoreText;
    private bool isGrounded; //checking if the player is grounded based on an empty object that we will create in the editor and place at the feet of the player
    [SerializeField] private Transform groundCheck; //the object for which we will check ground collision
    [SerializeField] private Transform playerPosition;
    [SerializeField] private float radius = 0f; // we will make the empty object into a circle and assign it a radius
    [SerializeField] private LayerMask whatIsGround; //here we will assign the layer from the editor to this, so we know when it collides specifically with Ground
    private sbyte extraJumps;
    [SerializeField] private sbyte extraJumpsValue = 2;

    private Rigidbody2D m_rigidBody; //declared for using unity Rigidbody 2D built in features
    [SerializeField] private Animator animator;

    [SerializeField] private GameOverScreen GameOverScreen;
    [SerializeField] private GameWonScreen GameWonScreen;

    float currentTime = 0f;
    public float startingTime = 10f;



    // Start is called before the first frame update
    private void Start()
    {
        m_rigidBody = GetComponent<Rigidbody2D>(); //getting the Rigidbody component from Unity Editor
        currentTime = startingTime;
    }



    // Update is called once per frame
    private void Update()
    {
        //checkShootKey();
        checkSpaceBarDownAndExtraJumps();
        checkMoveKeys(); // function called to check if movement keys are pressed
        animator.SetBool("move", move); //seting the boolean move from the animator to the one in the script
        countDown();

        gameWon();

        if (gameIsWon)
        {
            currentTime = 600;
        }
    }

    private void FixedUpdate()
    {
        //OverlapCircle to check if the collider is within the area of the circle
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, radius, whatIsGround);//1st parameter is where it is located, 2nd the radius, and 3rd the layer mask (Ground)

        movePlayer(); //function to move the Player being called
        jumpPlayer(); //function being called to make player jump (with extra jump if changed in editor)


    }

    private void gameWon()
    {
        if (playerPosition.transform.position.x >= 131) 
        {
            GameWonScreen.Setup(deathTokensCollected);
                gameIsWon = true;
        }
        
    }


    private void OnTriggerEnter2D(Collider2D collisionInfo)
    {

        if (collisionInfo.gameObject.CompareTag("Token"))
        {
            Destroy(collisionInfo.gameObject);
            deathTokensCollected += 1; // add one to the deathtokens count if twe collide
            FindObjectOfType<AudioManager>().playSound("TokenCollected"); //reference my audio manager and call the play function from it to play sound
            scoreText.text = "Tokens Collected: " + deathTokensCollected.ToString();

        }



    }

    private void gameOver()
    {
        
        GameOverScreen.Setup(deathTokensCollected);
    }

    private void countDown()
    {
        currentTime -= 1 * Time.deltaTime; //delta time so it decreases it only every second, not every frame
        countDownText.text = "Time count time: " + currentTime.ToString("0");

        if (currentTime <= 0)
        {
            currentTime = 0;

            gameOver();
        }
    }




    //this is a function created to change the boolean spaceBarDown to true if the button is down and manipulate extrajumps in order to achieve multiple jumps
    private void checkSpaceBarDownAndExtraJumps()
    {
        if (isGrounded)
        {
            extraJumps = extraJumpsValue;
        }

        //get input down for the space bar and set it to boolean "spaceBarDown"
        //doing this in fixed update caused delay, so it worked in update
        if (Input.GetButtonDown("Jump") && extraJumps > 0)
        {
            spaceBarDown = true;
            extraJumps--;

        } else if (Input.GetButtonDown("Jump") && extraJumps == 0 && isGrounded)
        {
            spaceBarDown = true;
        }
    }


    //function if spacebar is pressed according to boolean and the player is on ground jump, if player is in air and spacebar pressed, jump, but with less force
    private void jumpPlayer()
    {
        if (spaceBarDown && isGrounded)
        {
            m_rigidBody.AddForce(new Vector2(0, m_jumpForce), ForceMode2D.Impulse);//ading force on the y axis with Vector2 (impulse force seems to be good for insant application)
            FindObjectOfType<AudioManager>().playSound("jumpSoundEffect"); //reference my audio manager and call the play function from it to play sound
        } else if (spaceBarDown && isGrounded == false)
        {
            m_rigidBody.AddForce(new Vector2(0, m_jumpForce / 1.5f), ForceMode2D.Impulse);
        }
            spaceBarDown = false;


    }


    //function for checking if movement keys are pressed
    private void checkMoveKeys() //doing it this way will make my update function more readable
    { // check if horizontal movement keys are down and if so, assign true to move boolean
        if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.D))
        {
            move = true;
        }
        else if (Input.GetKeyUp(KeyCode.RightArrow) || Input.GetKeyUp(KeyCode.LeftArrow) || Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.D)) 
            // if those keys are up, then assign false to move
        {
            move = false;
        }
    }




    //function to move the player
    private void movePlayer()
    {            
        //assign the already declared "horzontalMovement" variable the input for horizontal movement   
        horizontalMovement = Input.GetAxis("Horizontal");

        if (move)
        {

            //manipulating position of character using transform to move it left and right
            transform.position += new Vector3(horizontalMovement, 0, 0) * Time.fixedDeltaTime * m_movementSpeed; //creating a Vector 3 with the x set to the input value
        }

        // if facingRight is false (facing left) and the horizontalMovement is higher than 0, then we know the charcter is moving right and supposed to face right
        if (facingRight == false && horizontalMovement > 0) 
        {
            flipPlayer();
        } else if(facingRight == true && horizontalMovement < 0) //same thing applies for this, but the other way around
        {
            flipPlayer();
        }
    }




    private void flipPlayer() //using the function to flip the player from facing right to left and vice versa
    {
        facingRight = !facingRight; // if facingright is false, when this line of code runs, then facingRight will become equal to true and vice versa

        ////Vector3 playerPositions = transform.localScale; // getting the x, y and z axis of the player character
        ////playerPositions.x *= -1; // multiply x with -1 to change the position x of the player to the exact negative or positive equivalent of that number
        ////transform.localScale = playerPositions;

        transform.Rotate(0f, 180f, 0f); //we rotate the character by 0 on the x, 180 on the y, and 0 on the z, much simpler code and fixes the bullet starting point issue
                                        //(issue was that the empty obj from where the bullet fires wasn't facing left when rotating, this fixes it)

    }

}
