using UnityEngine;

[System.Serializable]
public class SpeedPotion : Potion
{
    [Range(0.0f, 1.0f)] [SerializeField] private float multipler = 0.7f;

    public override bool UseMe(GameObject obj)
    {
        if (obj.TryGetComponent<Player>(out Player player))
        {
            PlayerMovement playerMovement = player.playerMovement;
            if (playerMovement.SetCoolDown(playerMovement.coolDown * (1 - multipler)))
            {
                Debug.Log("Speed enhanced");
                return true;
            }
            else
            {
                Debug.Log("Speed already enhanced");
                return false;
            }
        }

        return false;
    }
}
