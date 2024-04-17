using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class AiScript : MonoBehaviour
{
    public float MaxSpeed;
    private Rigidbody2D AiRigidBody;
    private Vector2 StartingPos;
    public Rigidbody2D Puck;

    public Transform TopAiBoundaryHolder, RightAiBoundaryHolder, LeftAiBoundaryHolder, BottomAiBoundaryHolder;
    public PlayerMovementScript.Boundary AiBoundary;
    
    public Transform TopPuckBoundaryHolder, RightPuckBoundaryHolder, LeftPuckBoundaryHolder, BottomPuckBoundaryHolder;
    public PlayerMovementScript.Boundary PuckBoundary;

    private bool isAifirstTouch =true;
    private float OffsetX;

    private Vector2 TargetPos;
    public int AiTouches = 0 ;
    private ScoreScript ScoreScriptInstance;

    public bool isTimerDone = true;

    public bool Lostapoint = false;
    // Start is called before the first frame update
    private void Start()
    {
        
        AiRigidBody = GetComponent<Rigidbody2D>();
        StartingPos = AiRigidBody.position;
        
        AiBoundary = new PlayerMovementScript.Boundary // gets the boundaries of Ai
        (
            TopAiBoundaryHolder.GetChild(0).position.y,
            RightAiBoundaryHolder.GetChild(0).position.x,
            LeftAiBoundaryHolder.GetChild(0).position.x,
            BottomAiBoundaryHolder.GetChild(0).position.y
        );
        
        PuckBoundary = new PlayerMovementScript.Boundary // gets the boundaries of the puck
        (
            TopPuckBoundaryHolder.GetChild(0).position.y,
            RightPuckBoundaryHolder.GetChild(0).position.x,
            LeftPuckBoundaryHolder.GetChild(0).position.x,
            BottomPuckBoundaryHolder.GetChild(0).position.y
        );
        ScoreScriptInstance = FindObjectOfType<ScoreScript>();
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Puck")) // if Ai touches the ball more than 1 it will lost 1 point , i restricted so that the player may only lose one point until the other player touches the ball
        {
            AiTouches++;
           // Debug.Log("touch");
            if (other.gameObject.CompareTag("Puck") && AiTouches >= 2 && Lostapoint == false)
            {
                ScoreScriptInstance.Decerement(ScoreScript.Score.AiScore);
                AiTouches = 0;
                Lostapoint = true;
            }
        }
    }

    // Update is called once per frame
    private void FixedUpdate()
    {

            if (!PuckScript.WasGoal) // restrict the ai to only moving when its not a goal
            {
                float MovementSpeed;
                if (Puck.position.y < PuckBoundary.Bottom) //ai will only move when the puck is on its side
                {
                
                    if (isAifirstTouch)
                    {
                        isAifirstTouch = false;
                        OffsetX = Random.Range(-0.5f, 0.5f);
                    }

                    MovementSpeed = MaxSpeed * Random.Range(0.5f, 1f);
                    TargetPos = new Vector2(Mathf.Clamp(Puck.position.x + OffsetX, AiBoundary.Left, AiBoundary.Right), StartingPos.y);
                }
                else
                {
                 
                    isAifirstTouch = true;
                    MovementSpeed = Random.Range(MaxSpeed * 0.4f, MaxSpeed);
                    TargetPos = new Vector2(Mathf.Clamp(Puck.position.x + OffsetX, AiBoundary.Left, AiBoundary.Right)
                                            ,Mathf.Clamp(Puck.position.y, AiBoundary.Bottom, AiBoundary.Top));

                }

                AiRigidBody.MovePosition(Vector2.MoveTowards(AiRigidBody.position, TargetPos,
                    MovementSpeed * Time.fixedDeltaTime));
            }
        
            
    }

    public void ResetPositon()
    {
        AiRigidBody.position = StartingPos;
    }
}
