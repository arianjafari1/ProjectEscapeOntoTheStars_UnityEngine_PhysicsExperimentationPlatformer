using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour //script for bullet prefab
{

    [SerializeField] private float speed = 20f; //speed for the bullet
    [SerializeField] private Rigidbody2D rigidBody; //reference to the rigid body from unity editor
    [SerializeField] private sbyte damage = 1; //speed for the bullet

    // Start is called before the first frame update
    private void Start()
    {
        rigidBody.velocity = transform.right * speed;
    }




    //collision info for storing collision information of the objects we hit
    private void OnTriggerEnter2D(Collider2D collisionInfo) //using this built in Unity function to trigger events of our choosing in case of collision
    {
        Wall wallCollision = collisionInfo.GetComponent<Wall>(); //reference the wall object for collision and storing it in this variable
        Enemy EnemyTakesDamage = collisionInfo.GetComponent<Enemy>(); //reference the enemy object for collision and storing it in this variable

        Destroy(gameObject); //destroy the bullet if it hits something


        //if bullet collides with brick wall, then brick wall falls apart, sets isWallHit to true from the Wall.cs
        if (wallCollision != null) // if wallCollision isn't equal to null, so we actually found a Wall component when colliding
        {
            wallCollision.IsWallHit = true;
        }

        //if bullet collides with enemy, then enemy takes damage
        if (EnemyTakesDamage != null) // if enemycollision isn't equal to null, so we actually found an enemy component when colliding
        {
            EnemyTakesDamage.enemyTakesDamage(damage);
        }

        //Debug.Log(collisionInfo.name); //print out in the console the names of the objects we hit


        //if (collisionInfo.name == "BrickOne" || collisionInfo.name == "BrickTwo" || collisionInfo.name == "BrickThree" || collisionInfo.name == "BrickFour" || collisionInfo.name == "BrickFive")
        //{
        //    Debug.Log("It works");

        //}
    }


}
