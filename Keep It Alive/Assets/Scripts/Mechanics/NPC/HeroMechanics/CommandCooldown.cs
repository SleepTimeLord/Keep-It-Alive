using UnityEngine;
using Image = UnityEngine.UI.Image;
public class CommandCooldown : MonoBehaviour
{
/*    [SerializeField]
    private Image numberCooldown;*/
    private float cooldownTimer;

    public HeroCommand heroCommand;
    public Image imageIconPlaceholder;

    public Image imageCooldown;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        imageCooldown.fillAmount = 0;
        imageIconPlaceholder.sprite = heroCommand.imageIcon;
    }

    // Update is called once per frame
    void Update()
    {
        if (heroCommand.isOnCooldown)
        {
            ApplyCooldown();
        }
    }

    void ApplyCooldown()
    {
        // subtract time 
        cooldownTimer -= Time.deltaTime;

        if (cooldownTimer < 0)
        {
            heroCommand.isOnCooldown = false;
            imageCooldown.fillAmount = 0;
        }
        else
        {
            CommandCooldownStart();
        }
    }

    private void CommandCooldownStart()
    {
        // new system
        imageCooldown.fillAmount = cooldownTimer / heroCommand.commandCooldownAmount;
        heroCommand.isOnCooldown = true;
    }

    public void UseCommand()
    {
        // if is in cooldown it doesn't use the command
        if (heroCommand.isOnCooldown)
        {
            Debug.Log($"{heroCommand.typeOfCommand} is on cooldown.");
        }
        // if not, it sets the cooldowntimer to the cooldown amount and it sets isOnCooldown to true
        else
        {
            cooldownTimer = heroCommand.commandCooldownAmount;
            heroCommand.isOnCooldown = true;
        }
    }
}
