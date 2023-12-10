using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{
    private const float targetRotation = 0f;
    [SerializeField] private Rigidbody2D rigidBody;
    [SerializeField] private float force = 0f;
    private bool isHitByBullet =  false; //boolean used to decide whether to use the balance method from update
    [SerializeField] private sbyte damageToEnemy = 1; //speed for the bullet

    // Start is called before the first frame update
    private void Start()
    {
        rigidBody = gameObject.GetComponent<Rigidbody2D>();
    }

    // will use the update function to constantly rotate the wall bricks so they stay straight, I call this the "balance" method
    private void Update()
    {
        if (isHitByBullet == false)
        {
            //balance method for jointed 2D Sprites
            rigidBody.MoveRotation(Mathf.LerpAngle(rigidBody.rotation, targetRotation, force * Time.deltaTime));
        }

        //Debug.Log(isHit);
        //if (Input.GetKeyDown(KeyCode.F)) // check when the key f is down and and change isWallHit to true to test the de-balance method
        //{
        //    IsWallHit = true;
        //}
    }

    private void OnTriggerEnter2D(Collider2D collisionInfoWall) //collision check to check whether wall collides with enemy
    {
        Enemy enemy = collisionInfoWall.GetComponent<Enemy>();

        //if wall collides with enemy, then enemy takes damage
        if (enemy != null) // if enemy collision isn't equal to null, so we actually found an enemy component when colliding
        {
            enemy.enemyTakesDamage(damageToEnemy);
        }


    }


    //getter and setters for the private variable of isHit to use in the Bullet script in case of collision with bullet to turn off balance and make the wall collapse
    public bool IsWallHit
    {
        get 
        { 
            return this.isHitByBullet; 
        }

        set
        {
            this.isHitByBullet = true;
        }
    }

}
