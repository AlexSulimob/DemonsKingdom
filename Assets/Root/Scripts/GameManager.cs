using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class GameManager : Singleton<GameManager>
{
    public GameObject PlayerPrefab;
    public GameObject DefaultRespawn;
    public PlayerHealth playerHealth;
    public GameObject DeadScreen;
    [HideInInspector]
    public PlayerSave playerSave;

    public GameObject PauseMenu;

    private bool _gameIsPaused;
    void Start()
    {
        playerHealth.dead += () => DeadScreen.SetActive(true);
        
        Singleton<ControlInst>.Instance.Control.Player.Pause.performed += _ => PauseGame(); 
        //PauseButton.performed += _ => PauseGame();
    }
    private void Awake()
    {

        
        if (PlayerPrefs.HasKey("PlayerSave"))
        {

            playerSave = JsonUtility.FromJson<PlayerSave>(PlayerPrefs.GetString("PlayerSave"));
            //Debug.Log(playerSave.currentRespwanPoint.name);
            if (playerSave.currentRespwanPoint!=null)
            {

                PlayerPrefab.transform.position = playerSave.currentRespwanPoint;
            }

        } else
        {
            PlayerPrefab.transform.position = DefaultRespawn.transform.position;
        }



    }

    public void MakeSave()
    {
        PlayerPrefs.SetString("PlayerSave", JsonUtility.ToJson(playerSave));//������ ��������
    }
    // Update is called once per frame

    public void SavePlayer()
    {
        string json = JsonUtility.ToJson(playerSave);
    }
    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        PauseMenu.SetActive(false);
        _gameIsPaused = false;
        Time.timeScale = 1f;
    }

    public void PauseGame()
    {
        if (_gameIsPaused)
        {
            PauseMenu.SetActive(false);
            _gameIsPaused = false;
            Time.timeScale = 1f;

        }
        else
        {
            PauseMenu.SetActive(true);
            _gameIsPaused = true;
            Time.timeScale = 0f;

        }
    }
}
[Serializable]
public class PlayerSave
{
    public Vector3 currentRespwanPoint;
}
