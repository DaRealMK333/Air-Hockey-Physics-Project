using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class PuckScript : MonoBehaviour
{
    public ScoreScript ScoreScriptInstance;
    public static bool WasGoal { get; private set; }

    private Rigidbody2D PuckRigidBody,PlayerRigidBody,AiRigidbody;
    public GameObject PlayerMovementScript, AiScript;
    
    public float MaxSpeed;

    private PortalSpawnerScript portalSpawnerScript;
    private PortalDestroyer portalDestroyer;

    private PlayerMovementScript _playerMovementScript;
    private AiScript _aiScript;

    private UiManagerScript _uiManagerScript;

    private bool IsPlayersLastTouch;

    public PointDestroy _pointDestroy;

    private ArrowSpawner _arrowSpawner;

    private ArrowDestroyer _arrowDestroyer;

    private PortalSpawnerScript _portalSpawnerScript;

    private PointSpawner _pointSpawner;

    private float Timer = 0;  
   // public AudioManagementScript AudioManger;
    // Start is called before the first frame update
    void Start()
    {
        PuckRigidBody = GetComponent<Rigidbody2D>();
        PlayerRigidBody = PlayerMovementScript.GetComponent<Rigidbody2D>();
        AiRigidbody = AiScript.GetComponent<Rigidbody2D>();
        WasGoal = false;
        portalSpawnerScript = FindObjectOfType<PortalSpawnerScript>();
        portalDestroyer = FindObjectOfType<PortalDestroyer>();
        _playerMovementScript = FindObjectOfType<PlayerMovementScript>();
        _aiScript = FindObjectOfType<AiScript>();
        _uiManagerScript = FindObjectOfType<UiManagerScript>();
        _pointDestroy = FindObjectOfType<PointDestroy>();
        _arrowSpawner = FindObjectOfType<ArrowSpawner>();
        _arrowDestroyer = FindObjectOfType<ArrowDestroyer>();
        _portalSpawnerScript = FindObjectOfType<PortalSpawnerScript>();
        _pointSpawner = FindObjectOfType<PointSpawner>();
        RandomObjectSpawner();
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("AiTouch")) // when the player touches the ball we reset how many times the ai touched the ball
        {
            _playerMovementScript.PlayerTouches = 0;
            IsPlayersLastTouch = false;
            _playerMovementScript.Lostapoint = false; 
        }
        if (other.gameObject.CompareTag("PlayerTouch")) // vice versa
        {
            _aiScript.AiTouches = 0;
            IsPlayersLastTouch = true;
            _aiScript.Lostapoint = false;
        }
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!WasGoal) 
        {
            if (other.gameObject.CompareTag("AiGoal")) //player scored
            {
                ScoreScriptInstance.Increment(ScoreScript.Score.PlayerScore);
                WasGoal = true;
                
                StartCoroutine(ResetPuck(false));
                StartCoroutine(_uiManagerScript.CountDown(true));
                IsPlayersLastTouch = true;
                _aiScript.Lostapoint = false;
                
            }
            else if(other.gameObject.CompareTag("PlayerGoal")) //ai scored
            {
                ScoreScriptInstance.Increment(ScoreScript.Score.AiScore);
                WasGoal = true;
                StartCoroutine(ResetPuck(true));
                StartCoroutine(_uiManagerScript.CountDown(true)); 
                IsPlayersLastTouch = false;
                _playerMovementScript.Lostapoint = false;
                
            }
        }
        
        
        if (PuckRigidBody.position.y < 0 && other.gameObject.CompareTag("Portal")) //checks if the puck hit the portal on the players side
        {
            PuckRigidBody.transform.position = GameObject.Find("AiPortal(Clone)").transform.position;
            
            PortalDestroyer.PortalDestroy();
            Timer = 0;
            
            RandomObjectSpawner();
            
        }
       
        if (PuckRigidBody.position.y > 0 && other.gameObject.CompareTag("Portal"))//checks if the puck hit the portal on the ai side
        {
            PuckRigidBody.transform.position = GameObject.Find("PlayerPortal(Clone)").transform.position;
            PortalDestroyer.PortalDestroy();
            Timer = 0;
           
            RandomObjectSpawner();
        }

       

        if (other.gameObject.CompareTag("PlusOne")) // if the puck hit the plus one the player that hit the puck will get an extra point
        {
            
            if (IsPlayersLastTouch)
            {
                ScoreScriptInstance.Increment(ScoreScript.Score.PlayerScore);
                PointDestroy.PointDestroyer();
                Timer = 0;
                
                RandomObjectSpawner();
            }
            else
            {
                ScoreScriptInstance.Increment(ScoreScript.Score.AiScore);
                PointDestroy.PointDestroyer();
                Timer = 0;
                RandomObjectSpawner();
            }
        }

        if (other.gameObject.CompareTag("Arrow")) //creates
        {
            Debug.Log(_arrowSpawner.Arrow.transform.eulerAngles.z);
            
            switch (_arrowSpawner.Arrow.transform.eulerAngles.z)
            {
                case 0 : PuckRigidBody.velocity = PuckRigidBody.velocity.magnitude * new Vector2(1, 0).normalized;
                    break;
                case 90 : PuckRigidBody.velocity = PuckRigidBody.velocity.magnitude * new Vector2(0, 1).normalized;
                    break;
                case 180 : PuckRigidBody.velocity = PuckRigidBody.velocity.magnitude * new Vector2(-1, 0).normalized;
                    break;
                case 270 : PuckRigidBody.velocity = PuckRigidBody.velocity.magnitude * new Vector2(0, -1).normalized;
                    break;
            }
            ArrowDestroyer.ArrowDestroy();
            Timer = 0;
            
            RandomObjectSpawner();
        }
    }
    
    private IEnumerator ResetPuck(bool DidAiScore)
    {
        yield return new WaitForSecondsRealtime(1);
        PuckRigidBody.velocity = PuckRigidBody.position = new Vector2(0, 0);
        PlayerRigidBody.position = new Vector2(0, -3);
        AiRigidbody.position = new Vector2(0, 3);
        WasGoal = false;
        _playerMovementScript.PlayerTouches = 0;
        _aiScript.AiTouches = 0;
       
        if (DidAiScore)
        {
            PuckRigidBody.position = new Vector2(-0.12f, -1);
            IsPlayersLastTouch = false;
        }
        else
        {
            PuckRigidBody.position = new Vector2(-0.12f, 1);
            IsPlayersLastTouch = true;
        }
        
        
    }

    private void FixedUpdate()
    {
        PuckRigidBody.velocity = Vector2.ClampMagnitude(PuckRigidBody.velocity, MaxSpeed);
    }

    public void CentrePuck()
    {
        PuckRigidBody.position = new Vector2(0, 0);
    }

    private void RandomObjectSpawner()
    {
        int randomNumber = Random.Range(1, 4);
        switch (randomNumber)
        {
            case 1 : _portalSpawnerScript.PortalSpawner();
                break;
            case 2 : _pointSpawner.PointsSpawner();
                break;
            case 3 : _arrowSpawner.ArrowSpawn();
                break;
        }
        
        
        
    }
    // Update is called once per frame
    void Update()
    {
        Timer += Time.deltaTime;

        if (Timer >= 15f) //after 15 seconds if the additional features havent been used after 15 secs they will disapear and another feature will appear randomly
        {
            if (GameObject.Find("Plus_One(Clone)") != null)
            {
                PointDestroy.PointDestroyer();
                Timer = 0;
                RandomObjectSpawner();
            }
            else if (GameObject.Find("PlayerPortal(Clone)") != null || GameObject.Find("AiPortal(Clone)") != null )
            {
                PortalDestroyer.PortalDestroy();
                Timer = 0;
                RandomObjectSpawner();
            }
            else if (GameObject.Find("arrow(Clone)") != null)
            {
                ArrowDestroyer.ArrowDestroy();
                Timer = 0;
                RandomObjectSpawner();
            }
        }
    }
}
