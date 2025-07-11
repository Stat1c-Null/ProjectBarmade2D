using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    public GameObject accessStationText; // TODO: Simplify variables
    public GameObject DrinkStationUI;
    public GameObject DialogueUI;
    public TextMeshProUGUI npcNameText;
    public TextMeshProUGUI dialogueText;
    public GameObject choicesPanel;
    public Button[] choiceButtons;
    public GameObject pressEText;//event handler for dialogue

    private bool CanOpenDialogue = false;
    private bool CanAccessDrinkStation = false;
    private bool DrinkStationOn = false;

    //It�s a placeholder for �what to do when a player clicks a choice button."
    private System.Action<int> currentChoiceCallback; // TODO: This should be handled in dialogue script, not here

    // Update is called once per frame
    void Update()
    {
        //Access Drink Mixing Station
        if(Input.GetKeyDown(KeyCode.E) && CanAccessDrinkStation && !DrinkStationOn) { // TODO: Remove Game functionality from UI code
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

    //This function is called to show the dialogue text without choices
    public void ShowDialogue(string npcName, string message) // TODO: We should not be handling this here
    {
        DialogueUI.SetActive(true);
        gameObject.GetComponent<PlayerMovement>().movementEnabled = false;
        npcNameText.text = npcName;
        dialogueText.text = message;
        choicesPanel.SetActive(false);
    }

    public void HideDialogue() // TODO: We should not be handling this here
    {
        DialogueUI.SetActive(false);  // Hides the whole dialogue panel
        choicesPanel.SetActive(false);   // Hides the choices if they�re visible
        gameObject.GetComponent<PlayerMovement>().movementEnabled = true; // Re-enable player movement
    }

    //This function is called when the player chooses a dialogue option with choices
    public void ShowChoices(string[] choices, System.Action<int> onChoiceSelected) // TODO: We should not be handling this here
    {
        choicesPanel.SetActive(true);
        currentChoiceCallback = onChoiceSelected;

        for (int i = 0; i < choiceButtons.Length; i++)
        {
            if (i < choices.Length)
            {
                choiceButtons[i].gameObject.SetActive(true);
                choiceButtons[i].GetComponentInChildren<TextMeshProUGUI>().text = choices[i];
                int index = i;
                choiceButtons[i].onClick.RemoveAllListeners();
                choiceButtons[i].onClick.AddListener(() => OnChoiceSelected(index));
            }
            else
            {
                choiceButtons[i].gameObject.SetActive(false);
            }
        }
    }

    public void ShowPressE()
    {
        pressEText.SetActive(true);
    }

    public void HidePressE()
    {
        pressEText.SetActive(false);
    }

    //This function is called when the player selects a choice from the dialogue options
    private void OnChoiceSelected(int index) // TODO: Again, there should be a seperate DialogueUI script handling this
    {
        choicesPanel.SetActive(false);
        currentChoiceCallback?.Invoke(index);
    }

    void OnCollisionEnter2D(Collision2D other) { // TODO: Why is a UI script handling collisions??
        if (other.gameObject.CompareTag("DrinkStation")) {
            accessStationText.SetActive(true);
            CanAccessDrinkStation = true;
        }

        if (other.gameObject.CompareTag("NPC"))
        {
            CanOpenDialogue = true;
        }
    }

    void OnCollisionExit2D(Collision2D other) {
        if (other.gameObject.CompareTag("DrinkStation")) {
            accessStationText.SetActive(false);
            CanAccessDrinkStation = false;
        }

        if (other.gameObject.CompareTag("NPC"))
        {
            CanOpenDialogue = false;
        }
    }
}
