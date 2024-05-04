using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    UIDisplay UI;
    AudioPlayer audioPlayer;

    private void Awake()
    {
        UI = FindObjectOfType<UIDisplay>();
        audioPlayer = FindObjectOfType<AudioPlayer>();
    }

    void Start()
    {
        if (tag != "Coin")
        {
            tag = "Coin";
            Debug.LogWarning("'Coin' script attached to object not tagged 'Coin', tag added automatically", transform);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            if (UI.coinsCollected == 0)
            {
                UI.PopUp("Great Job! You collected your first time crystal! You must collect them all so they " +
                "don't fall into the wrong hands.");
            }

            audioPlayer.PlayCoinPickupClip();
            Destroy(gameObject);
        }
    }

    void OnDestroy() {
        UI.coinsCollected++;
    }
}
