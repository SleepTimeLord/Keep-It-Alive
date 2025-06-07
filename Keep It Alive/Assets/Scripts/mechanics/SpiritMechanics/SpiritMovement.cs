using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class SpiritMovement : MonoBehaviour
{
    [SerializeField]
    private InputActionReference teleport, cursorPosition;
    private Transform spirit;
    private Hero hero;
    private SpriteRenderer spriteRenderer;

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
        hero = FindAnyObjectByType<Hero>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    private void Update()
    {
        Vector3 diff = hero.transform.position - transform.position;
        diff.Normalize();
        float rot_Z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, rot_Z - 180);

        // get the Z-rotation in degrees (0 → 360)
        float zAngle = transform.eulerAngles.z;

        spriteRenderer.flipY = (zAngle > 90f && zAngle < 270f);
    }

    // tps the spirit to cursor position
    private void SpiritTeleport(InputAction.CallbackContext context)
    {
        if (canTeleport)
        {
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
