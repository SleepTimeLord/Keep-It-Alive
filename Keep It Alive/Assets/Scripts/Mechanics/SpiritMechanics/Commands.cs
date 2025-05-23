using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;
using Image = UnityEngine.UI.Image;
public enum Commands
{
    Rally,
    Chase,
    Dash,
    StandStill
}

[CreateAssetMenu(menuName = "Scriptable Objects/Commands")]
public class HeroCommand : ScriptableObject
{
    public Sprite imageIcon;
    public float commandCooldownAmount;
    public bool isOnCooldown;
    public Commands typeOfCommand;
}