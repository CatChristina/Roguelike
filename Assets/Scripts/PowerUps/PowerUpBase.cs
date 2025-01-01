using UnityEngine;

public enum PowerUps
{
    XP,
    Health,
    Speed,
    Jump,
    Ammo
}

[CreateAssetMenu(fileName = "New Power Up", menuName = "Scriptable Object / PowerUp")]
public class PowerUpBase : ScriptableObject
{
    public PowerUps powerUps;
    public float valueIncrease;
}
