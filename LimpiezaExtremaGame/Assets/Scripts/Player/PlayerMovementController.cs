using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovementController : MonoBehaviour, IDataPersistence
{
    [Header("Config")]
    [SerializeField] private float moveSpeed = 5f;

    //private Rigidbody2D rb;
    private Vector2 velocity = Vector2.zero;  //Velocidad
    private Animator animator;
    private SpriteRenderer visual;


    //FLECHAS

    private Controls Controls;
    [SerializeField] private Rigidbody2D rb; //rb2D
    

    private Vector2 velocidadObjetivo = Vector2.zero;








    private bool movementDisabled = false;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponentInChildren<Animator>();
        visual = GetComponentInChildren<SpriteRenderer>();


        //FLECHAS
        Controls = new Controls();
    }
    private void OnEnable()
    {
        Controls.Enable();
    }
    private void OnDisable()
    {
        Controls.Enable();
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
        //FLECHAS
        Controls.Enable();
    }

    private void EnablePlayerMovement()
    {
        movementDisabled = false;
        //FLECHAS
        Controls.Enable();
    }

    private void MovePressed(Vector2 moveDir)
    {
        //FLECHAS
        velocidadObjetivo = moveDir; // Se pasa directamente como Vector2
        

    }




    private void Update()
    {
        

        //FLECHAS       
        velocity = Controls.Gameplay.Movement.ReadValue<Vector2>() * moveSpeed;
        if(!movementDisabled) 
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
        //FLECHAS
        MovePressed(velocity * Time.fixedDeltaTime);
        if (movementDisabled)
            return;
        rb.velocity = Vector2.SmoothDamp(rb.velocity, velocidadObjetivo, ref velocity, 0f);


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

        
    }
}














































































//using Cinemachine;
//using System.Collections;
//using System.Collections.Generic;
//using System.Data;
//using Unity.VisualScripting;
//using UnityEngine;

//[RequireComponent(typeof(Rigidbody2D))]
//public class PlayerMovementController : MonoBehaviour, IDataPersistence
//{
//    [Header("Config")]
//    [SerializeField] private float moveSpeed = 5f;

//    //private Rigidbody2D rb;
//    private Vector2 velocity = Vector2.zero;
//    private Animator animator;
//    private SpriteRenderer visual;


//    //JOYSTICK
//    //public VariableJoystick variableJoystick;
//    //private Vector2 keyboardInput = Vector2.zero;

//    //FLECHAS

//    private Controls entradasMovimiento;
//    [SerializeField] private Rigidbody2D rb;





//    private bool movementDisabled = false;

//    private void Awake()
//    {
//        rb = GetComponent<Rigidbody2D>();
//        animator = GetComponentInChildren<Animator>();
//        visual = GetComponentInChildren<SpriteRenderer>();


//        //FLECHAS
//        entradasMovimiento = new Controls();
//    }

//    private void Start()
//    {
//        GameEventsManager.instance.inputEvents.onMovePressed += MovePressed;
//        GameEventsManager.instance.playerEvents.onDisablePlayerMovement += DisablePlayerMovement;
//        GameEventsManager.instance.playerEvents.onEnablePlayerMovement += EnablePlayerMovement;
//    }

//    private void OnDestroy()
//    {
//        GameEventsManager.instance.inputEvents.onMovePressed -= MovePressed;
//        GameEventsManager.instance.playerEvents.onDisablePlayerMovement -= DisablePlayerMovement;
//        GameEventsManager.instance.playerEvents.onEnablePlayerMovement -= EnablePlayerMovement;
//    }

//    private void DisablePlayerMovement()
//    {
//        movementDisabled = true;
//        //FLECHAS
//        entradasMovimiento.Enable();
//    }

//    private void EnablePlayerMovement()
//    {
//        movementDisabled = false;
//        //FLECHAS
//        entradasMovimiento.Enable();
//    }

//    private void MovePressed(Vector2 moveDir)
//    {
//        //FLECHAS
//        Vector2 velocidadObjetivo = moveDir; // Se pasa directamente como Vector2
//        rb.velocity = Vector2.SmoothDamp(rb.velocity, velocidadObjetivo, ref velocity, 0f);

//        //JOYSTICK
//        //keyboardInput = moveDir;
//        //Debug.Log($"Teclado: {moveDir}");



//        //NO JOYSTICK
//        //velocity = moveDir.normalized * moveSpeed;

//        //if (movementDisabled)
//        //{
//        //    velocity = Vector2.zero;
//        //}
//    }




//    private void Update()
//    {
//        UpdateAnimations();

//        //FLECHAS       
//        velocity = entradasMovimiento.Gameplay.Movement.ReadValue<Vector2>() * moveSpeed;
//    }

//    public void LoadData(GameData data)
//    {
//        //this.transform.position = data.playerPosition;
//        transform.position = data.playerPosition;

//        var confiner = FindObjectOfType<CinemachineConfiner>();
//        GameObject mapBoundaryObject = GameObject.Find(data.currentMapBoundary);
//        if (mapBoundaryObject != null)
//        {
//            confiner.m_BoundingShape2D = mapBoundaryObject.GetComponent<PolygonCollider2D>();
//            confiner.InvalidatePathCache(); // Esto fuerza a actualizar la confiner después del cambio
//        }

//        var virtualCam = FindObjectOfType<Cinemachine.CinemachineVirtualCamera>();
//        if (virtualCam != null)
//        {
//            virtualCam.OnTargetObjectWarped(transform, Vector3.zero); // Reajusta internamente sin hacer un “salto” visual
//        }
//    }

//    public void SaveData(ref GameData data)
//    {
//        data.playerPosition = this.transform.position;
//        data.currentMapBoundary = FindObjectOfType<CinemachineConfiner>().m_BoundingShape2D.gameObject.name;
//    }

//    private void FixedUpdate()
//    {
//        //FLECHAS
//        MovePressed(velocity * Time.fixedDeltaTime);



//        //JOYSTICK
//        //if (movementDisabled)
//        //{
//        //    rb.velocity = Vector2.zero;
//        //    velocity = Vector2.zero;
//        //    UpdateAnimations();
//        //    return;
//        //}

//        //Vector2 joystickInput = new Vector2(variableJoystick.Horizontal, variableJoystick.Vertical);
//        //Vector2 finalInput;

//        //// Si el joystick está activo (mayor a 0.1), úsalo. Si no, usa el teclado.
//        //if (joystickInput.magnitude > 0.1f)
//        //{
//        //    finalInput = joystickInput;
//        //}
//        //else
//        //{
//        //    finalInput = keyboardInput;
//        //}

//        //velocity = finalInput * moveSpeed;
//        //rb.velocity = velocity;



//        //NO JOYSTICK
//        //rb.velocity = velocity;


//    }

//    private void UpdateAnimations()
//    {
//        // update the animator parameters
//        bool walking = (velocity.magnitude > 0.01f);
//        animator.SetBool("walking", walking);
//        animator.SetFloat("velocity_x", velocity.x);
//        animator.SetFloat("velocity_y", velocity.y);
//        // facing dir for idle animations
//        if (walking)
//        {
//            int facingX = 0;
//            int facingY = 0;
//            if (velocity.x != 0)
//            {
//                facingX = (int)Mathf.Clamp(velocity.normalized.x, -1, 1);
//            }
//            else if (velocity.y != 0)
//            {
//                facingY = (int)Mathf.Clamp(velocity.normalized.y, -1, 1);
//            }
//            animator.SetInteger("facing_x", facingX);
//            animator.SetInteger("facing_y", facingY);
//        }

//        // flip the sprite appropriately
//        /*if (velocity.x < 0)
//        {
//            visual.flipX = true;
//        }
//        else if (velocity.x > 0)
//        {
//            visual.flipX = false;
//        }*/
//    }
//}
