using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    [SerializeField] private Transform shootingStart; //create a referance to the fire point
    [SerializeField] private GameObject blasterShot; //create a referance to the the blaster shot prefab

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F)) // check when the key f is down and call the shoot function
        {
            shoot();
        }
    }

    private void shoot() //shoot function to instantiate/spawn bullets objects from the gun
    {
        Instantiate(blasterShot, shootingStart.position, shootingStart.rotation); // we spawn the object blasterShot at shootStart pos with the rotation of shootingStart

        FindObjectOfType<AudioManager>().playSound("PlayerShoots"); //reference my audio manager and call the play function from it to play sound

    }



}
