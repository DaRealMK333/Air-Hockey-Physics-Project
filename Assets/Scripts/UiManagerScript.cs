using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;
using UnityEngine.UI ;

public class UiManagerScript : MonoBehaviour
{
    [Header("Canvas")]
    public GameObject CanvasGame;
    public GameObject CanvasRestart;

    [Header("CanvasRestart")]
    public GameObject WinText;
    public GameObject LoseText;
    public GameObject GOCountDownText;
    [Header("other")]
    // public AudioManagementScript audioManger;
    public PuckScript PuckScript;
  public ScoreScript scoreScript;
  public PlayerMovementScript PlayerMovement;
  public AiScript aiScript;
  private int CountDownTimer = 3;
  public Text CountdownText;
  public GameObject PlayerRigidBody , AiRigidbody;
  private AiScript aiScriptRef;
  private PlayerMovementScript playerMovementScriptRef;
  private bool isGoal = false;
  public void ShowRestartCanvas(bool didAiWin)
  {
      Time.timeScale = 0;
      CanvasGame.SetActive(false);
      CanvasRestart.SetActive(true);

      if (didAiWin)
      {
          //audioManger.PlayAudioLostGame();
          WinText.SetActive(false);
          LoseText.SetActive(true);
      }
      else
      {
          //audioManger.PlayAudioWonGame();
          WinText.SetActive(true);
          LoseText.SetActive(false);
      }
  }

  public void RestartGame()
  {
      Time.timeScale = 1;
      
      CanvasGame.SetActive(true);
      CanvasRestart.SetActive(false);
      
     scoreScript.ResetScores();
     PuckScript.CentrePuck();
     PlayerMovement.ResetPosition();
     aiScript.ResetPositon();
     StartCoroutine(CountDown(isGoal));
  }

  private void Start()
  {
      StartCoroutine(CountDown(isGoal));
      //aiScriptRef = FindObjectOfType<AiScript>(); 
      //playerMovementScriptRef = FindObjectOfType<PlayerMovementScript>();
  }

  public IEnumerator CountDown(bool Goal)
  {
      if (Goal)
      {
          yield return new WaitForSecondsRealtime(1.05f); 
      }
      
      GOCountDownText.SetActive(true);
      Time.timeScale = 0f;
        
      // Start the countdown from 3 seconds
      float timeLeft = 3f;
      while (timeLeft > 0)
      {
          // Update the countdown text with the current time left
          CountdownText.text = Mathf.CeilToInt(timeLeft).ToString();

          // Wait for the next frame
          yield return null;

          // Decrement the time left by the time that passed since the last frame
          timeLeft -= Time.unscaledDeltaTime;
      }

 
      CountdownText.text = "0";
      GOCountDownText.SetActive(false);
      Time.timeScale = 1f;
      
  }


    // Update is called once per frame
    void Update()
    {
  
    }
}
