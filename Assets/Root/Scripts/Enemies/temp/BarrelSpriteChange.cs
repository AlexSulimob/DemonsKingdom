using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrelSpriteChange : MonoBehaviour
{
    public Sprite DamagedSprite;
    public SpriteRenderer spriteRenderer;
    public EnemyHealth enemyHealth;
    void Start()
    {
        enemyHealth.isGetingHit += _ => spriteRenderer.sprite = DamagedSprite;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
