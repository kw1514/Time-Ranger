using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIDisplay : MonoBehaviour
{
    [Header("Health")]
    [SerializeField] Slider healthSlider;
    [SerializeField] Health playerHealth;

    [Header("PopUp")]
    [SerializeField] TextMeshProUGUI popUpText;
    [SerializeField] GameObject popUpPanel;
    [SerializeField] float duration = 5f;
    [SerializeField] bool popUpActive;
    private static float startDuration;
    private string text;

    [Header("Interact")]
    [SerializeField] GameObject interactKey;
    // [SerializeField] GameObject interactPanel;
    // [SerializeField] TextMeshProUGUI interactText;

    [Header("Charge")]
    [SerializeField] Slider chargeSlider;


    [Header("Coins")]
    [SerializeField] TextMeshProUGUI coinCount;
    [SerializeField] bool allCoinsCollected;
    public int coinsCollected;
    private int coinsInLevel;

    LevelManager levelManager;
    Shooting shooting;
    PlayerMovement player;
 
    void Start()
    {
        levelManager = FindObjectOfType<LevelManager>();
        shooting = FindObjectOfType<Shooting>();
        player = FindObjectOfType<PlayerMovement>();

        healthSlider.maxValue = playerHealth.GetHealth();
        chargeSlider.maxValue = shooting.timeBetweenTravel;
        chargeSlider.value = shooting.timeBetweenTravel;
        interactKey.SetActive(false);
        // interactPanel.SetActive(false);
        // interactText.text = "";
        allCoinsCollected = false;

        if (popUpPanel != null && popUpText != null)
        {
            popUpPanel.SetActive(false);
            popUpText.text = "";

            popUpActive = false;
            startDuration = duration;
        }

        //Get the total amount of coins in the scene.
        coinsInLevel = GameObject.FindGameObjectsWithTag("Coin").Length;
        coinsCollected = 0;
        coinCount.text = (coinsCollected + " of " + coinsInLevel);
    }


    void Update()
    {
        healthSlider.value = playerHealth.GetHealth();
        coinCount.text = (coinsCollected + " of " + coinsInLevel);
    
        if (shooting.canTravel == false && player.timeTravelUnlocked == true)
        {
            chargeSlider.value = shooting.travelTimer;
        }
        else if (shooting.canTravel == true && player.timeTravelUnlocked == true)
        {
            chargeSlider.value = shooting.timeBetweenTravel;
        }
        else if (player.timeTravelUnlocked == false)
        {
            chargeSlider.value = 0;
        }
        
        
        if (popUpActive)
        {
            SetPopUp();
            duration -= Time.deltaTime;

            if (duration <= 0f)
            {
                popUpPanel.SetActive(false);
                popUpText.text = "";

                duration = startDuration;
                popUpActive = false;
            }
        }

        if (coinsCollected == coinsInLevel)
        {
            allCoinsCollected = true;
        }

        if (allCoinsCollected)
        {
            PopUp("Congratulations! You found all of the time crystals. Now no one can use them to rewrite time!");
            levelManager.LoadTitleScreen();
        }
    }

    private void SetPopUp()
    {
        popUpPanel.SetActive(true);
        popUpText.text = text;
    }

    public void PopUp(string str)
    {
        text = str;
        popUpActive = true;
    }

    public void setInteract(bool active)
    {
        interactKey.SetActive(active);
        // interactPanel.SetActive(active);
        // interactText.text = text;
    }
}