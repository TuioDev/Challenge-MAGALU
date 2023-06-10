using UnityEngine;

public abstract class Brain : ScriptableObject
{
    //Here we can put all the basic attributes of each movement class
    //for example: velocity
    public abstract void Think(Enemy enemy, float time);
}
