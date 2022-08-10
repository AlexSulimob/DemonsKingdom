using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
public class Dialog : MonoBehaviour
{
    public GameObject popOutWindow;
    public GameObject TextBox;
    public TMP_Text textBox;

    [TextArea(3,10)]
    public string[] sentence;
    public float speed_reveal = 0.1f;

    private bool _isTyping;
    private int _currentSentence = 0;
    private bool _inTrigger;
    private bool _sentenceRefreshed;
    void Start()
    {
        Singleton<ControlInst>.Instance.Control.Player.Interact.performed += delegate (InputAction.CallbackContext context)
        {
            StopAllCoroutines();
            if (_sentenceRefreshed)
            {
                _inTrigger = false;
                popOutWindow.SetActive(false);
                TextBox.SetActive(false);
                _isTyping = false;
                _currentSentence = 0;
                _sentenceRefreshed = false;
                return;
            }
            if (_inTrigger && !_sentenceRefreshed)
            {
                StartCoroutine("ShowMessage");
            }

        };
        textBox.text = null;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        _inTrigger = true;
        popOutWindow.SetActive(true);
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        _inTrigger = true;
        popOutWindow.SetActive(true);
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        _inTrigger = false;
        popOutWindow.SetActive(false);
        TextBox.SetActive(false);
        _isTyping = false;
        _currentSentence = 0;
    }
    IEnumerator ShowMessage()
    {
        if (_isTyping)
        {
            textBox.text = null;
            textBox.text += sentence[_currentSentence];
            _isTyping = false;
            IncrementSentence();
            yield break;
        }
        _isTyping = true;
        textBox.text = null;
        popOutWindow.SetActive(false);
        TextBox.SetActive(true);

        for (int i = 0; i < sentence[_currentSentence].Length; i++)
        {
            
            textBox.text += sentence[_currentSentence][i];
            yield return new WaitForSeconds(speed_reveal);
        }
        _isTyping = false;
        IncrementSentence();
    }
    void IncrementSentence()
    {
        if (_currentSentence >= sentence.Length-1)
        {
            _currentSentence = 0;
            _sentenceRefreshed = true;           
            
        }
        else { _currentSentence++; }

    }
}
