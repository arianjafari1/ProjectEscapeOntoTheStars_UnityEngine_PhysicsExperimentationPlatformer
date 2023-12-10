using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private sbyte health = 4;
    [SerializeField] private sbyte velocityFallDamage = -8; //the amount of velocity that it takes for the player to fall down, to take damage

    [SerializeField] private Animator animator;
    private Rigidbody2D m_rigidBody; //declared for using unity Rigidbody 2D built in features
    [SerializeField] private GameObject deathTokenObject; //create a referance to the the token prefab
    [SerializeField] private Transform tokenSpawnPoint; //create a referance to the spawn point of token


    [SerializeField] private bool isShooting = false;
    [SerializeField] private bool isIdle = true;

    [SerializeField] private Transform groundCheck; //the object for which we will check ground collision
    [SerializeField] private LayerMask whatIsGround; //here we will assign the layer from the editor to this, so we know when it collides specifically with Ground
    [SerializeField] private float radius = 0f; // we will make the empty object into a circle and assign it a radius
    [SerializeField ]private SpriteRenderer sprite; //reference to the sprite
    private bool hasTouchedGround;


    // Start is called before the first frame update
    void Start()
    {
        m_rigidBody = GetComponent<Rigidbody2D>(); //getting the Rigidbody component from Unity Editor


    }


    private void FixedUpdate()
    {
        hasTouchedGround = Physics2D.OverlapCircle(groundCheck.position, radius, whatIsGround);//1st parameter is where it is located, 2nd the radius, and 3rd the layer mask (Ground)
    }

    // Update is called once per frame
    void Update()
    {
        animator.SetBool("IsShooting", isShooting); //seting the boolean IsShooting from the animator to the one in the script
        animator.SetBool("IsIdle", isIdle); //seting the boolean IsIdle from the animator to the one in the script

        if (hasTouchedGround && m_rigidBody.velocity.y < velocityFallDamage)
        {
            enemyTakesDamage(1);
        }
    }


    public sbyte IsHealth //getters and setters for health
    {
        get
        {
            return this.health;
        }

        set
        {
            this.health = value;
        }
    }

    //function to make enemy flash red
    private IEnumerator flashRed() //special function that exexutes code, then waits for a certain amount, then executes new code
    {
        sprite.color = Color.red;
        yield return new WaitForSeconds(0.1f); //after the seconds mentioned in the parameter, execute the next line of code
        sprite.color = Color.white; //white is just the default color. go back to it

    }

    public void enemyTakesDamage(sbyte damage) //takes damage function for enemy
    {
        if (health > 0)
        {
            health -= damage;
            FindObjectOfType<AudioManager>().playSound("HitEnemy"); //reference my audio manager and call the play function from it to play sound
            StartCoroutine(flashRed()); // calling the special type function Enumerator to flash the enemy red when hit
        }
        animationCheck();
    }

    private void animationCheck() //destroys enemy object if health is 0 or less
    {
        if (health <= 0)
        {
            Destroy(gameObject);


            // we spawn the object token at groundCheck pos with the rotation of shootingStart
            Instantiate(deathTokenObject, tokenSpawnPoint.position, tokenSpawnPoint.rotation); 

        }

        if (isShooting == true)
        {
            isIdle = false;
        } else if (isIdle == true)
        {
            isShooting = false;
        }

    }

}
