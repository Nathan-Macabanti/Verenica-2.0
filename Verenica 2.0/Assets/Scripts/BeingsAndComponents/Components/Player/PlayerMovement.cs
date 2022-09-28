using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Player))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Transform[] slidePoints;
    [SerializeField] private float airTime = 0.2f;
    [SerializeField] private float coolDown = 0.1f;

    private int currentPosition;
    private bool canMove;
    private bool isJumping;

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

        //If currentPosition is within bounds 
        if (currentPosition < slidePoints.Length)
            this.transform.position = slidePoints[currentPosition].position;
        else
            CenterCharacter();
    }

    public void Slide(int index)
    {
        if (!canMove) return;

        //Zoom to this position
        currentPosition = index;
    }

    public IEnumerator Jump()
    {
        isJumping = true;
        yield return new WaitForSeconds(airTime);
        isJumping = false;
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
    }
}
