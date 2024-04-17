using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementScript : MonoBehaviour
{
    private bool IsClicked = true;//checks if the player clicked
    private bool CanMove;//checks if player can move
    private Collider2D PlayerCollider; //stores the size of the players hockey paddle
    public Rigidbody2D PlayerRigidbody;// Start is called before the first frame update
    public Transform TopBoundaryHolder,RightBoundaryHolder,LeftBoundaryHolder,BottomBoundaryHolder;
    public int PlayerSpeed;
    private ScoreScript ScoreScriptInstance;
    private Boundary PlayerBoundary;
    public int PlayerTouches = 0;
    private Vector2 startpostion;
    public bool isTimerDone = true;
    public bool Lostapoint = false;
    
    public struct Boundary
    {
        public float Top, Right, Left, Bottom;

        public Boundary(float top, float right, float left, float bottom)
        {
            Top = top;
            Right = right;
            Left = left;
            Bottom = bottom;
        }
    }
    void Start()
    {
        new WaitForSecondsRealtime(3);
       // PlayerSize = gameObject.GetComponent<SpriteRenderer>().bounds.extents;//gets the size of the sprite we put in the gameobject
       PlayerCollider = GetComponent<Collider2D>();
        PlayerRigidbody = GetComponent<Rigidbody2D>();//this is store the rigidbody from the gameobject of the script
        startpostion = PlayerRigidbody.position;
        ScoreScriptInstance = FindObjectOfType<ScoreScript>();
        
        PlayerBoundary = new Boundary
            (
            TopBoundaryHolder.GetChild(0).position.y,
            RightBoundaryHolder.GetChild(0).position.x,
            LeftBoundaryHolder.GetChild(0).position.x,
            BottomBoundaryHolder.GetChild(0).position.y
            );
        
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Puck")  )
        {
            PlayerTouches++; 
            
            if (other.gameObject.CompareTag("Puck") && PlayerTouches >= 2 && Lostapoint == false )
            {
                ScoreScriptInstance.Decerement(ScoreScript.Score.PlayerScore);
                PlayerTouches = 0;
                Lostapoint = true;
            }
            
        }

    }

    // Update is called once per frame
    void Update()
    {
    
        
            if (Input.GetMouseButton(0))
            {
                //stores the position of the mouse in the world position of the game and not of the screen
                Vector2 MousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

                    if (PlayerCollider.OverlapPoint(MousePos))
                        // this checks if the mouse position is in the boundary of the x and y position of the paddle
                    {
                        CanMove = true;// the player can now move because the mouse is touching the air hockey paddle
                    }
                    else
                    {
                        CanMove = false;
                    }

                    if (CanMove)//since the player can move the paddle
                    {
                        Vector2 ClampledMousePos = new Vector2(
                            Mathf.Clamp(MousePos.x, PlayerBoundary.Left, PlayerBoundary.Right),
                            Mathf.Clamp(MousePos.y,PlayerBoundary.Bottom,PlayerBoundary.Top));
                        //transform.position = MousePos;//the paddle will know follow wherever the mouse position is
                        PlayerRigidbody.MovePosition(ClampledMousePos);//this makes the paddle move to the mouse with physics elements and with restrictions to boundaries
                 
                    }
     
             
        }
        
    }
    public void ResetPosition()
    {
        PlayerRigidbody.position = startpostion;
    }
}
