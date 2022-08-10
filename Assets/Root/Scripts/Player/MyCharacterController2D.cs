using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.InputSystem;

public class MyCharacterController2D : MonoBehaviour
{
    
    Rigidbody2D rb;
    PlayerHealth playerHealth;
    Vector2 inputValue;
    Vector2 _direction;
    float _speed;
    public float speed = 10f;

    public float dash_speed = 20f;
    public float dash_time = 1f;

    public float dash_cooldown = 1f;
    bool _dash_cooldowned;

    bool _attack_cooldoned = false;
    public float stopping_time = 0.5f;
    public float attack_cooldown = 0.5f;

    Vector2 _getDamage_flightDirection;
    public float getDamage_flightTime = 0.1f;
    public float getDamage_flightSpeed = 50f;
    public float getDamage_StunTime = 1f;

    public float invincibleTime = 2f;
    bool _isInvincible = false;
    bool _isDead = false;
    
    void Start()
    {      
        rb = GetComponent<Rigidbody2D>();
        playerHealth = GetComponent<PlayerHealth>();
        
        Action<InputAction.CallbackContext> onDash = callback => StartCoroutine("Dash");
        Singleton<ControlInst>.Instance.Control.Player.Dash.performed += onDash;

        Action<InputAction.CallbackContext> onAttack = callback => StartCoroutine("Attack");
        Singleton<ControlInst>.Instance.Control.Player.Attack.performed += onAttack;

        Action<Vector2> OnGethit = direction => { StartCoroutine("GetDamage"); _getDamage_flightDirection = direction; };
        playerHealth.isGetingHit += OnGethit;

        playerHealth.dead +=  delegate ()
        {
            StopAllCoroutines();
            Singleton<PlayerStates>.Instance.currentState = PlayerStates.pStates.Dead;
            _direction = Vector2.zero;
            Singleton<ControlInst>.Instance.Control.Player.Dash.performed -= onDash;
            Singleton<ControlInst>.Instance.Control.Player.Attack.performed -= onAttack;
            playerHealth.isGetingHit -= OnGethit;
        };
        _speed = speed;

    }

    void Update()
    {
        if (Singleton<PlayerStates>.Instance.currentState == PlayerStates.pStates.Dead)
        {
            return;
        }
        inputValue = Singleton<ControlInst>.Instance.Control.Player.Movement.ReadValue<Vector2>();
        if (Singleton<PlayerStates>.Instance.currentState != PlayerStates.pStates.GettingDamage)
        {
            _direction = inputValue;
        }

    }
    private void FixedUpdate()
    {
        rb.velocity = _direction * _speed;
    }
    IEnumerator Dash()
    {
        if (_dash_cooldowned 
            || Singleton<PlayerStates>.Instance.currentState == PlayerStates.pStates.Attacking
            || Singleton<PlayerStates>.Instance.currentState == PlayerStates.pStates.GettingDamage
            )
        {
            yield break;
        }
        Singleton<PlayerStates>.Instance.isInvincible = true;//���������� ����������
        _speed = dash_speed;
        Singleton<PlayerStates>.Instance.currentState = PlayerStates.pStates.Dashing;
        yield return new WaitForSeconds(dash_time);
        Singleton<PlayerStates>.Instance.currentState = PlayerStates.pStates.Walking;
        _speed = speed;
        if (!_isInvincible)
        {
            Singleton<PlayerStates>.Instance.isInvincible = false; //���������� ��������
        }

        StartCoroutine("DashCooldown");
    }
    IEnumerator DashCooldown()
    {
        _dash_cooldowned = true;
        yield return new WaitForSeconds(dash_cooldown);
        _dash_cooldowned = false;
    }
    IEnumerator Attack()
    {
        if (_attack_cooldoned 
            || Singleton<PlayerStates>.Instance.currentState == PlayerStates.pStates.Dashing
            || Singleton<PlayerStates>.Instance.currentState == PlayerStates.pStates.GettingDamage
            )
        {
            yield break;
        }
        Singleton<PlayerStates>.Instance.currentState = PlayerStates.pStates.Attacking;
        _speed = 0f;
        yield return new WaitForSeconds(stopping_time);
        Singleton<PlayerStates>.Instance.currentState = PlayerStates.pStates.Walking;
        _speed = speed;
        StartCoroutine("AttackCooldown");
    }
    IEnumerator AttackCooldown()
    {
        _attack_cooldoned = true;
        yield return new WaitForSeconds(attack_cooldown);
        _attack_cooldoned = false;
    }
    IEnumerator GetDamage()
    {
        StopCoroutine("Dash");
        StopCoroutine("Attack");
        Singleton<PlayerStates>.Instance.currentState = PlayerStates.pStates.GettingDamage;
        Singleton<PlayerStates>.Instance.isInvincible = true;
        _isInvincible = true;
        _direction = _getDamage_flightDirection;
        _speed = getDamage_flightSpeed;
        yield return new WaitForSeconds(getDamage_flightTime);
        _speed = 0f;
        yield return new WaitForSeconds(getDamage_StunTime);
        Singleton<PlayerStates>.Instance.currentState = PlayerStates.pStates.Walking;
        _direction = inputValue;
        _speed = speed;
        StartCoroutine("Invincible");
    }
    IEnumerator Invincible()
    {
        yield return new WaitForSeconds(invincibleTime);
        _isInvincible = false;
        Singleton<PlayerStates>.Instance.isInvincible = false;
    }
}
