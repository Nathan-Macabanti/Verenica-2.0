using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Player))]
public class PlayerMovement : MonoBehaviour
{
#if false
    #region OnMove
        public delegate void PlayerMove();
        public event PlayerMove OnPlayerMove;
        public void InvokeMove()
        {
            OnPlayerMove?.Invoke();
        }
    #endregion
#endif
    [SerializeField] private Transform[] slidePoints;
    [SerializeField] private float jumpHeight = 0.5f;

    [Header("Cooldown times")]
    [SerializeField] private float startingCoolDown = 0.2f;
    [SerializeField] private float minCoolDownTime = 0.1f;
    [SerializeField] private float maxCoolDownTime = 0.25f;
    public float coolDown { get; private set; }
    public float coolDownTimer { get; private set; }
    public float coolDownTimerPercent { get { return coolDownTimer / coolDown; } }

    private float airTime = 0.125f;

    private uint currentPosition = 1;
    public bool canMove { get; private set; } = true;
    private bool isJumping = false;

    private Player player;
    private Transform _playerTransform;
    
    // Start is called before the first frame update
    void Start()
    {
        Initialize();
        CenterCharacter();
    }

    private void Initialize()
    {
        player = GetComponent<Player>();
        _playerTransform = transform;
        if(slidePoints.Length == 0)
        {
            NoteSpawnerGroup noteSpawnerGroup = NoteSpawnerGroup.GetInstance();
            NoteSpawner[] noteSpawners = noteSpawnerGroup.GetNoteSpawners();
            int count = noteSpawners.Length;
            //Debug.Log(count);
            slidePoints = new Transform[count];
            for (int i = 0; i < count; i++)
            {
                slidePoints[i] = noteSpawners[i].GetPath().destination;
            }
        }

        coolDown = startingCoolDown;
        coolDown = Mathf.Clamp(coolDown, minCoolDownTime, maxCoolDownTime);
    }

    private void Update()
    {
        //Position will update when the currentPosition changes
        if (player.IsDead())
        {
            StopAllCoroutines(); //Will stop all courotines in this class
            isJumping = false;
            canMove = false;
            return;
        }    
    }

    public void Move(uint index)
    {
        //if the the place you are moving too is the place is the same as where we are do not MOVE
        if (slidePoints[currentPosition].localPosition == slidePoints[index].localPosition) return;
        if (!canMove || isJumping)//If you cannot move then don't move
        {
            //Debug.Log("You Cannot Move");
            return; 
        }

        //Zoom to this position
        currentPosition = index;
        _playerTransform.position = slidePoints[currentPosition].localPosition;
        StartCoroutine(Cooldown());
    }

    public void Jump()
    {
        if (!canMove || isJumping) return;

        StartCoroutine("JumpCoroutine");
    }

    IEnumerator JumpCoroutine()
    {
        float x = _playerTransform.position.x;
        float y = jumpHeight;
        float z = _playerTransform.position.z;
        _playerTransform.position = new Vector3(x, y, z);

        isJumping = true;
        yield return new WaitForSeconds(airTime);
        isJumping = false;

        _playerTransform.position = slidePoints[currentPosition].localPosition;
    }

    public IEnumerator Cooldown()
    {
        canMove = false;
        coolDownTimer = coolDown;
        while (coolDownTimer > 0)
        {
            //Debug.Log(coolDownTimerPercent.ToString());
            coolDownTimer -= Time.deltaTime;
            yield return null;
        }
        coolDownTimer = 0;
        canMove = true;
    }

    public void CenterCharacter()
    {
        if (currentPosition == 1) return;

        currentPosition = 1;
        _playerTransform.position = slidePoints[currentPosition].localPosition;
    }

    public bool SetCoolDown(float newCoolDown)
    {
        if (coolDown <= minCoolDownTime || coolDown >= maxCoolDownTime)
            return false;

        coolDown = newCoolDown;
        coolDown = Mathf.Clamp(coolDown, minCoolDownTime, maxCoolDownTime);

        return true;
    }

    public void OnPhaseChange(Phase phase)
    {
        CenterCharacter();
    }

    private void OnEnable()
    {
        EventManager.OnPhaseChange += OnPhaseChange;
    }
    private void OnDisable()
    {
        EventManager.OnPhaseChange -= OnPhaseChange;
        StopAllCoroutines();
    }

    public uint GetPosIndex() { return currentPosition; }
}
