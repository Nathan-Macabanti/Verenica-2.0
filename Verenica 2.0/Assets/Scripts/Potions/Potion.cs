using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public abstract class Potion
{
    public virtual bool UseMe() { return true; }
    public virtual bool UseMe(GameObject obj) { return true; }
}
