using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class SpiritCommand : MonoBehaviour
{
    [Header("Spirit Input")]
    [SerializeField]
    private InputActionReference attack, rally, dodge, standstill;

    private Hero _hero;

    private void Start()
    {
        _hero = GetComponentInParent<Hero>();
    }
    private void OnEnable()
    {
        attack.action.performed += StartHeroAttack;
        rally.action.performed += StartHeroRally;
        dodge.action.performed += StartHeroDodge;
        standstill.action.performed += StartHeroStandStill;
    }

    private void OnDisable()
    {
        attack.action.performed -= StartHeroAttack;
        rally.action.performed -= StartHeroRally;
        dodge.action.performed -= StartHeroDodge;
        standstill.action.performed -= StartHeroStandStill;
    }

    // if preformed a different input, stops the previous trigger
    private void StartHeroAttack(InputAction.CallbackContext context)
    {
        _hero.ActionCommanded("attack");
    }

    private void StartHeroRally(InputAction.CallbackContext context)
    {
        _hero.ActionCommanded("rally");
    }

    private void StartHeroDodge(InputAction.CallbackContext context)
    {
        _hero.ActionCommanded("dodge");
    }

    private void StartHeroStandStill(InputAction.CallbackContext context)
    {
        _hero.ActionCommanded("standstill");
    }
}
