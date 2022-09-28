using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Being
{
#if true
#region Singleton
    private static Player instance;
    private void IntializeSingleton()
    {
        if (instance == null) { instance = this; }
        else { Utils.SingletonErrorMessage(this); }
    }
    public static Player GetInstance() { return instance; }
#endregion
#endif

    protected override void Die()
    {
        isDead = true;
        EventManager.InvokeLose();

        //base.Die();
    }
}
