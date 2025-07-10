using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// TODO: Make clear the difference between ice tray and ice machine 
// TODO: This PlayerIce script should not be a child of PlayerBartender
public class PlayerIce : MonoBehaviour // TODO: What is the purpose of this script? Should it be renamed IceMachineController?
{
    public bool withIce = false;
    public GameObject Icetray;
    public GameObject IceMachine;

    private bool CollidingWithIceTray = false; // TODO: We should not be handling collisions here
    private bool CollidingWithIceMachine = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && CollidingWithIceMachine)
        {
            FromIceMachine();
            withIce = true;
            //Debug.Log("Refilling from ice machine");
        }
        if (Input.GetKeyDown(KeyCode.E) && CollidingWithIceTray)
        {
            if (withIce)
            {
                RefillIceTray();
                withIce = false;
                Debug.Log("refilling ice tray...");
            }
            else
            {
                Debug.Log("need ice from machine first....");
            }
        }
    }

    // TODO: Should be using InteractionController
    void OnCollisionEnter2D(Collision2D other)
    {

        if (other.gameObject.CompareTag("Ice Tray"))
        {
            CollidingWithIceTray = true;
        }
        if (other.gameObject.CompareTag("Ice Machine"))
        {
            CollidingWithIceMachine = true;
        }
    }

    void OnCollisionExit2D(Collision2D other)
    {

        if (other.gameObject.CompareTag("Ice Tray"))
        {
            CollidingWithIceTray = false;
        }
        if (other.gameObject.CompareTag("Ice Machine")) // TODO: Make difference between ice machine and ice tray clear
        {
            CollidingWithIceMachine = false;
        }
    }

    void RefillIceTray()
    {
        //this refills the ice trays
        Icetray.GetComponent<IceTray>().iceTrayVolume = 100f;
    }

    void FromIceMachine()
    {
        //this refills the ice from machine
        Debug.Log("Refilling from ice machine");
        //Icetray.GetComponent<IceTray>().iceMachineVolume = 0f;
    }
}
