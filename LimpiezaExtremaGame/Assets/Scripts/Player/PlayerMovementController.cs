using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovementController : MonoBehaviour, IDataPersistence
{
    [Header("Config")]
    [SerializeField] private float moveSpeed = 5f;

    private Rigidbody2D rb;
    private Vector2 velocity = Vector2.zero;
    private Animator animator;
    private SpriteRenderer visual;

    private bool movementDisabled = false;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponentInChildren<Animator>();
        visual = GetComponentInChildren<SpriteRenderer>();
    }

    private void Start()
    {
        GameEventsManager.instance.inputEvents.onMovePressed += MovePressed;
        GameEventsManager.instance.playerEvents.onDisablePlayerMovement += DisablePlayerMovement;
        GameEventsManager.instance.playerEvents.onEnablePlayerMovement += EnablePlayerMovement;
    }

    private void OnDestroy()
    {
        GameEventsManager.instance.inputEvents.onMovePressed -= MovePressed;
        GameEventsManager.instance.playerEvents.onDisablePlayerMovement -= DisablePlayerMovement;
        GameEventsManager.instance.playerEvents.onEnablePlayerMovement -= EnablePlayerMovement;
    }

    private void DisablePlayerMovement()
    {
        movementDisabled = true;
    }

    private void EnablePlayerMovement()
    {
        movementDisabled = false;
    }

    private void MovePressed(Vector2 moveDir)
    {
        velocity = moveDir.normalized * moveSpeed;

        if (movementDisabled)
        {
            velocity = Vector2.zero;
        }
    }

    private void Update()
    {
        UpdateAnimations();
    }

    public void LoadData(GameData data)
    {
        //this.transform.position = data.playerPosition;
        transform.position = data.playerPosition;

        var confiner = FindObjectOfType<CinemachineConfiner>();
        GameObject mapBoundaryObject = GameObject.Find(data.currentMapBoundary);
        if (mapBoundaryObject != null)
        {
            confiner.m_BoundingShape2D = mapBoundaryObject.GetComponent<PolygonCollider2D>();
            confiner.InvalidatePathCache(); // Esto fuerza a actualizar la confiner después del cambio
        }

        var virtualCam = FindObjectOfType<Cinemachine.CinemachineVirtualCamera>();
        if (virtualCam != null)
        {
            virtualCam.OnTargetObjectWarped(transform, Vector3.zero); // Reajusta internamente sin hacer un “salto” visual
        }
    }

    public void SaveData(ref GameData data)
    {
        data.playerPosition = this.transform.position;
        data.currentMapBoundary = FindObjectOfType<CinemachineConfiner>().m_BoundingShape2D.gameObject.name;
    }

    private void FixedUpdate()
    {
        rb.velocity = velocity;
    }

    private void UpdateAnimations()
    {
        // update the animator parameters
        bool walking = (velocity.magnitude > 0.01f);
        animator.SetBool("walking", walking);
        animator.SetFloat("velocity_x", velocity.x);
        animator.SetFloat("velocity_y", velocity.y);
        // facing dir for idle animations
        if (walking)
        {
            int facingX = 0;
            int facingY = 0;
            if (velocity.x != 0)
            {
                facingX = (int)Mathf.Clamp(velocity.normalized.x, -1, 1);
            }
            else if (velocity.y != 0)
            {
                facingY = (int)Mathf.Clamp(velocity.normalized.y, -1, 1);
            }
            animator.SetInteger("facing_x", facingX);
            animator.SetInteger("facing_y", facingY);
        }

        // flip the sprite appropriately
        /*if (velocity.x < 0)
        {
            visual.flipX = true;
        }
        else if (velocity.x > 0)
        {
            visual.flipX = false;
        }*/
    }
}
