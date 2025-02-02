using Audio;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class playerController : MonoBehaviour
{
    PlayerControls playerControls;

    public Rigidbody2D rb;
    public PlayerInput Input;
    public Vector2 inputVector;
    public Vector3 moveVector;
    public float moveSpeed = 10;
    //attack L
    private GameObject attackAreaL = default;
    private bool attackingL = false;
    private readonly float timeToAttack = 0.25f;
    private float timerL = 0;
    //attack R
    private bool attackingR = false;
    private GameObject attackAreaR = default;
    private float timerR = 0;
    public SoundEffectPlayer soundEffectPlayer;
    private bool isDead;
    void Awake()
    {
        Input = GetComponent<PlayerInput>();
    }
    void OnEnable()
    {
        //playerControls.Enable();
    }

    void OnDisable()
    {
        //playerControls.Disable();
    }
  
    private void OnMove(InputValue InputValue)
    {
        if (isDead) return;
        inputVector = InputValue.Get<Vector2>();
        moveVector.x = inputVector.x;
        moveVector.y = inputVector.y;
       // Debug.Log($"X move: {inputVector.x}");
       // Debug.Log($"Y move: {inputVector.y}");
    }
    private void OnLeftPunch()
    {
        attackingL = true;
        attackAreaL.SetActive(attackingL);
        soundEffectPlayer.PlaySoundEffect(SoundEffectType.PunchAttack, pitch: 1.1f);
    }
    private void OnRightPunch()
    {
        attackingR = true;
        attackAreaR.SetActive(attackingR);
        soundEffectPlayer.PlaySoundEffect(SoundEffectType.PunchAttack, pitch: 0.8f);
    }
    public void Move()
    {
        transform.position = transform.position + (moveVector * (Time.deltaTime * moveSpeed));
    }

    void Start()
    {
        attackAreaL = transform.GetChild(0).gameObject;
        attackAreaR = transform.GetChild(1).gameObject;
    }

    void Update()
    {
        Move();
        if (isDead) return;

        if (attackingL)
        {
            timerL += Time.deltaTime;

            if (timerL >= timeToAttack)
            {
                timerL = 0;
                attackingL = false;
                attackAreaL.SetActive(attackingL);
            }
        }
        if (attackingR)
        {
            timerR += Time.deltaTime;

            if (timerR >= timeToAttack)
            {
                timerR = 0;
                attackingR = false;
                attackAreaR.SetActive(attackingR);
            }
        }
     
    }

    public void Kill()
    {
        isDead = true;
        attackAreaL.SetActive(false);
        attackAreaR.SetActive(false);
        soundEffectPlayer.PlaySoundEffect(SoundEffectType.GameOver);      
        gameObject.GetComponent<BoxCollider2D>().enabled = false;
    }
}
