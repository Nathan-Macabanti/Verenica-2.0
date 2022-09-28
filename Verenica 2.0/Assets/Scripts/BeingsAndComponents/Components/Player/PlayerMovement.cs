using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Player))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Transform[] slidePoints;
    [SerializeField] private float jumpHeight = 0.5f;
    [SerializeField] private float airTime = 0.2f;
    [SerializeField] private float coolDown = 0.1f;

    private int currentPosition = 1;
    private bool canMove = true;
    private bool isJumping = false;

    private Player player;
    
    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<Player>();
        CenterCharacter();
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

    public void Move(int index)
    {
        if (!canMove || isJumping)//If you cannot move then don't move
        {
            Debug.Log("You Cannot Move");
            return; 
        }

        //Zoom to this position
        currentPosition = index;
        this.transform.position = slidePoints[currentPosition].localPosition;
        StartCoroutine(Cooldown());
    }

    public void Jump()
    {
        if (!canMove || isJumping) return;

        StartCoroutine("JumpCoroutine");
    }

    IEnumerator JumpCoroutine()
    {
        float x = transform.position.x;
        float y = jumpHeight;
        float z = transform.position.z;
        transform.position = new Vector3(x, y, z);

        isJumping = true;
        yield return new WaitForSeconds(airTime);
        isJumping = false;

        transform.position = slidePoints[currentPosition].localPosition;
    }

    public IEnumerator Cooldown()
    {
        canMove = false;
        yield return new WaitForSeconds(coolDown);
        canMove = true;
    }

    public void CenterCharacter()
    {
        currentPosition = 1;
        transform.position = slidePoints[currentPosition].localPosition;
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }

    public int GetPosIndex() { return currentPosition; }
}
