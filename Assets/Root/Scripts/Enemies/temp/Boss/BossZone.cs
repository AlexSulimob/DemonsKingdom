using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class BossZone : MonoBehaviour
{
    public GameObject UIobject;
    public Slider slider;
    public GameObject WinScreen;
    public EnemyHealth bossHealth;
    public GameObject gate;

    public GameObject gateOn;
    bool _inTrigger;
    void Start()
    {
        bossHealth.isGetingHit += _ => slider.value = bossHealth.currentHealth;
        //bossHealth.dead += () => Win();
        //bossHealth.dead += () => gate.transform.position = new Vector2(43f, 6f);
    }

    void Update()
    {
        if (bossHealth.currentHealth == 0)
        {
            WinScreen.SetActive(true);
            //gateOn.SetActive(false);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        gateOn.SetActive(true);
        if (bossHealth.currentHealth>0)
        {

            UIobject.SetActive(true);
            slider.maxValue = bossHealth.maxHealth;
            slider.value = bossHealth.currentHealth;
        }


    }

}
