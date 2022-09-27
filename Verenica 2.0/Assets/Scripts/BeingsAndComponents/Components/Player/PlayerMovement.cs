using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Player))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Transform[] slidePoints;
    [SerializeField] private float airTime = 0.2f;

    private int currentPosition;
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

        this.transform.position = slidePoints[currentPosition].position;
    }

    public void Slide(int value)
    {
        currentPosition += value;
    }

    public IEnumerator Jump()
    {
        isJumping = true;
        yield return new WaitForSeconds(airTime);
        isJumping = false;
    }

    public void CenterCharacter()
    {
        currentPosition = 1;
        
    }
}
