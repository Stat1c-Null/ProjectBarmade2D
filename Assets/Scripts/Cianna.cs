using UnityEngine;
using UnityEngine.UI;

// MAJOR TODO: This script needs to be renamed to DraftBeerMachine
public class DraftBeerMachine : MonoBehaviour
{
    public GameObject beerSelectionUI; // Assign the UI Panel in the Inspector
    public Text logText; // Assign a Text UI element to display selected beer 

    private bool isPlayerNear = false; // TODO: this should be removed. We should have a central way of detecting player
    private string[] beers = { "Lager", "IPA", "Stout", "Pilsner", "Porter", "Wheat Beer", "Amber Ale", "Pale Ale" }; // TODO: We should not be storing beer information in the script. 

    void Update()
    {
        if (isPlayerNear && Input.GetKeyDown(KeyCode.E))
        {
            ShowBeerMenu();
        }
    }

    public void SelectBeer(int beerIndex)
    {
        if (beerIndex >= 0 && beerIndex < beers.Length)
        {
            Debug.Log("Player selected: " + beers[beerIndex]);
            if (logText != null)
                logText.text = "Selected Beer: " + beers[beerIndex];
            beerSelectionUI.SetActive(false); // Hide UI after selection
        }
    }

    private void ShowBeerMenu()
    {
        beerSelectionUI.SetActive(true);
    }

    // TODO: Code below should be replaced with connection to InteractionController
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            isPlayerNear = true;
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            isPlayerNear = false;
            beerSelectionUI.SetActive(false);
        }
    }
}