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
    [SerializeField] private float airTime = 0.2f;
    [SerializeField] private float coolDown = 0.1f;

    private uint currentPosition = 1;
    private bool canMove = true;
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
            NoteSpawnerGroup noteSpawnerGroup = FindObjectOfType<NoteSpawnerGroup>();
            NoteSpawner[] noteSpawners = noteSpawnerGroup.GetNoteSpawners();
            int count = noteSpawners.Length;
            //Debug.Log(count);
            slidePoints = new Transform[count];
            for (int i = 0; i < count; i++)
            {
                slidePoints[i] = noteSpawners[i].GetPath().destination;
            }
        }
    }

    private void Update()
    {
        //Position will update when the currentPosition changes
        if (player.IsDead())
        {
            StopAllCoroutines(); //Will stop all courotines in this class
            return;
        }    
    }

    public void Move(uint index)
    {
        if (!canMove || isJumping)//If you cannot move then don't move
        {
            Debug.Log("You Cannot Move");
            return; 
        }

        //Zoom to this position
        currentPosition = index;
        _playerTransform.position = slidePoints[currentPosition].localPosition;
        StartCoroutine(Cooldown());
        //InvokeMove();
    }

    public void Jump()
    {
        if (!canMove || isJumping) return;

        StartCoroutine("JumpCoroutine");
        //InvokeMove();
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
        yield return new WaitForSeconds(coolDown);
        canMove = true;
    }

    public void CenterCharacter()
    {
        if (currentPosition == 1) return;

        currentPosition = 1;
        _playerTransform.position = slidePoints[currentPosition].localPosition;
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
