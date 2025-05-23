using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class SpiritCommand : MonoBehaviour
{
    [Header("Spirit Input")]
    [SerializeField]
    private InputActionReference attack, rally, dodge, standstill;

    private Hero _hero;

    [Header("Command Types")]
    public CommandCooldown rallyCooldown;
    public CommandCooldown attackCooldown;
    public CommandCooldown dodgeCooldown;
    public CommandCooldown standstillCooldown;

    private void Start()
    {
        _hero = GetComponentInParent<Hero>();
    }
    private void OnEnable()
    {
        attack.action.performed += StartHeroAttack;
        rally.action.performed += StartHeroRally;
        dodge.action.performed += StartHeroDodgeorDash;
        standstill.action.performed += StartHeroStandStill;
    }

    private void OnDisable()
    {
        attack.action.performed -= StartHeroAttack;
        rally.action.performed -= StartHeroRally;
        dodge.action.performed -= StartHeroDodgeorDash;
        standstill.action.performed -= StartHeroStandStill;
    }
    // if preformed a different input, stops the previous trigger
    private void StartHeroAttack(InputAction.CallbackContext context)
    {
        if (!_hero.isInAttackRange && !attackCooldown.heroCommand.isOnCooldown)
        {
            _hero.ActionCommanded("chase");
            attackCooldown.UseCommand();
            rallyCooldown.UseCommand();
        }
    }

    private void StartHeroRally(InputAction.CallbackContext context)
    {
        if (!rallyCooldown.heroCommand.isOnCooldown)
        {
            _hero.ActionCommanded("rally");
            _hero.initialSpiritPos = _hero.spirit.transform.position;
            attackCooldown.UseCommand();
            rallyCooldown.UseCommand();
        }
    }

    private void StartHeroDodgeorDash(InputAction.CallbackContext context)
    {
        if(!dodgeCooldown.heroCommand.isOnCooldown)
        {
            if (_hero.isMoving)
            {
                _hero.ActionCommanded("dash");
            }
            else
            {
                _hero.ActionCommanded("dodge");
            }
            dodgeCooldown.UseCommand();
        }
    }

    private void StartHeroStandStill(InputAction.CallbackContext context)
    {
        if (!standstillCooldown.heroCommand.isOnCooldown)
        {
            _hero.ActionCommanded("standstill");
            standstillCooldown.UseCommand();
        }
    }
}
