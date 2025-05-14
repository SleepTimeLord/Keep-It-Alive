using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class SpiritMovement : MonoBehaviour
{
    [SerializeField]
    private InputActionReference teleport, cursorPosition;
    private Transform spirit;

    [Header("Movement Settings")]
    public float teleportCooldown = 1;
    [SerializeField]
    private bool canTeleport = true;

    PlayerInput playerInput;
    private void OnEnable()
    {
        teleport.action.performed += SpiritTeleport;
    }
    private void OnDisable()
    {
        teleport.action.performed -= SpiritTeleport;
    }

    void Start()
    {
        spirit = GetComponent<Transform>();
    }

    // tps the spirit to cursor position
    private void SpiritTeleport(InputAction.CallbackContext context)
    {
        if (canTeleport)
        {
            Debug.Log(context);
            spirit.position = CursorPosition();
            canTeleport = false;
            StartCoroutine(StartCooldown());
        }
    }

    private Vector2 CursorPosition()
    {
        Vector3 mousePos = cursorPosition.action.ReadValue<Vector2>();
        // do this because screentoworldpoint is a vector 3 and considers depth.
        // this gets the depth distance of the mousePos
        mousePos.z = Camera.main.nearClipPlane;
        return Camera.main.ScreenToWorldPoint(mousePos);
    }

    IEnumerator StartCooldown()
    {
        yield return new WaitForSeconds(teleportCooldown);
        canTeleport = true;
    }
}
