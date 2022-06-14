using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class UIMenuManager : MonoBehaviour
{
    [SerializeField] private TMP_InputField inputField;
    [SerializeField] private TMP_Text highestScore;

    //----------------------------------------------//
    private void Awake()
    {
        DataTransfer.instanceDataTransfer.LoadScore();
        SetHighestScore();
    }
    //----------------------------------------------//
    public void StartNew()
    {
        SetPlayerName();
        SceneManager.LoadScene(1);
    }
    //----------------------------------------------//
    public void Exit()
    {
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit(); // original code to quit Unity player
#endif
    }
    //----------------------------------------------//
    public void SetPlayerName()
    {
        DataTransfer.instanceDataTransfer.playerNameCurrent = inputField.text;

    }
    //----------------------------------------------//
    void SetHighestScore()
    {
        highestScore.text = "Highest Score: " + DataTransfer.instanceDataTransfer.playerNameMaxScore + " - " + DataTransfer.instanceDataTransfer.score;
    }
    //----------------------------------------------//
    public void ResetScore()
    {
        DataTransfer.instanceDataTransfer.playerNameCurrent = DataTransfer.instanceDataTransfer.playerNameDefault;
        DataTransfer.instanceDataTransfer.score = 0;

        DataTransfer.instanceDataTransfer.SaveScore();
        SetHighestScore();
    }
}
