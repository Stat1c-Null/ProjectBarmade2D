using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerUI : MonoBehaviour
{
    public GameObject accessStationText;
    public GameObject DrinkStationUI;
    private bool CanAccessDrinkStation = false;
    private bool DrinkStationOn = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Access Drink Mixing Station
        if(Input.GetKeyDown(KeyCode.E) && CanAccessDrinkStation && !DrinkStationOn) {
            DrinkStationUI.SetActive(true);
            gameObject.GetComponent<PlayerMovement>().movementEnabled = false;
            DrinkStationOn = true;
            accessStationText.SetActive(false);
        } else if(Input.GetKeyDown(KeyCode.E) && CanAccessDrinkStation && DrinkStationOn){
            DrinkStationUI.SetActive(false);
            gameObject.GetComponent<PlayerMovement>().movementEnabled = true;
            DrinkStationOn = false;
            accessStationText.SetActive(true);
        }
    }

    void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.CompareTag("DrinkStation")) {
            accessStationText.SetActive(true);
            CanAccessDrinkStation = true;
        }
    }

    void OnCollisionExit2D(Collision2D other) {
        if (other.gameObject.CompareTag("DrinkStation")) {
            accessStationText.SetActive(false);
            CanAccessDrinkStation = false;
        }
    }
}
