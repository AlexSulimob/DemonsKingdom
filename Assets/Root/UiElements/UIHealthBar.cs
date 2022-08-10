using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIHealthBar : MonoBehaviour
{
    public PlayerHealth playerHealth;

    public Image[] hearts;
    public Sprite fullHeart;
    public Sprite emptySprite;
    private void Start()
    {
        playerHealth.isGetingHit += dir => UpdateHealth();
        playerHealth.RestoreHealth += UpdateHealth;
        playerHealth.dead += UpdateHealth;
    }
    void Update()
    {

    }
    void UpdateHealth()
    {

        for (int i = 0; i < hearts.Length; i++)
        {
            if (i < playerHealth.CurrentHealth)
            {
                hearts[i].sprite = fullHeart;
            }
            else
            {
                hearts[i].sprite = emptySprite;
            }

            if (i < playerHealth.maxHealth)
            {
                hearts[i].enabled = true;
            }
            else
            {
                hearts[i].enabled = false;
            }
        }
    }
}
