using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestHelper : MonoBehaviour
{
    public GameObject CanvasIndicator;
    public Chest Chest;
    private bool _keyDowned;
    private bool _isOpened = false;
    public Animator animator;

    bool _isWalking;
    bool _inTrigger;
    private void Start()
    {
        Singleton<ControlInst>.Instance.Control.Player.Interact.performed += ef => _keyDowned = true;
        Singleton<ControlInst>.Instance.Control.Player.Interact.canceled += ef => _keyDowned = false;
        Chest.ChestOpen += () => { _isOpened = true; animator.SetBool("isOpened", true); };
    }
    private void Update()
    {


        if (_isOpened)
        {
            CanvasIndicator.SetActive(false);
            animator.SetBool("isOpened", true);
            return;
        }
        _isWalking = Vector2.zero != Singleton<ControlInst>.Instance.Control.Player.Movement.ReadValue<Vector2>() ? true : false;
        if (_inTrigger)
        {
            CanvasIndicator.SetActive(true);
            if (_keyDowned && !_isOpened && !_isWalking)
            {
                animator.SetBool("isOpening", true);
            }
            else
            {
                animator.SetBool("isOpening", false);
            }
        }else
        {
            CanvasIndicator.SetActive(false);
        }

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        _inTrigger = true;
    }
    private void OnTriggerStay2D(Collider2D collision)
    {


    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        _inTrigger = false;
    }

}
