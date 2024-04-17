using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI ;

public class ScoreScript : MonoBehaviour
{

    public enum Score
    {
        AiScore , PlayerScore
    }

    public Text AiScoreText, PlayerScoreText;

    public int MaxScore;

    public UiManagerScript UiManager;
    
    private int AiScore, PlayerScore;
        
    private int aiScore
    {
        get { return AiScore;
        }
        set
        {
            AiScore = value;
            if (value == MaxScore)
            {
                UiManager.ShowRestartCanvas(true);
            }
        }
    }
    
    private int playerScore
    {
        get { return PlayerScore;
        }
        set
        {
            PlayerScore = value;
            if (value == MaxScore)
            {
                UiManager.ShowRestartCanvas(false);
            }
        }
    }
    

    public void Increment(Score whichscore)
    {
        if (whichscore == Score.AiScore)
        {
            AiScoreText.text = (++aiScore).ToString();
        }
        else
        {
            PlayerScoreText.text = (++playerScore).ToString();
        }
    } 
    
    public void Decerement(Score whichscore)
    {
        if (whichscore == Score.AiScore)
        {
            if (aiScore== 0)
            {
                aiScore = 0;
            }
            else 
                AiScoreText.text = (--aiScore).ToString();
        }
        else
        {
            if (playerScore == 0)
            {
                playerScore = 0;
            }
            else 
                PlayerScoreText.text = (--playerScore).ToString();
        }
    }

    public void ResetScores()
    {
        aiScore = playerScore = 0;
        AiScoreText.text = PlayerScoreText.text = "0";
    }

    public void CloseApplication()
    {
        Application.Quit();
    }
    // Start is called before the first frame update
    void Start()
    {
      
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
