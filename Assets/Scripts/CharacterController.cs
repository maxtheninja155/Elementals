using UnityEngine;

public class CharacterController : MonoBehaviour
{
    private Rigidbody rb;

    public CharacterDataSO data;

    public ComboTree tree;
    public InputManager inputM;
    public AnimationManager animM;
    private Animator anim;

    //basic movement
    [Header("Movement Modifiers")]
    public float maxSpeed = 7f;
    public float gravityMultiplier = 75f;
    public float accerationMultiplier = 200f;
    [Header("Direction")]
    public bool moveRight = false;
    public bool moveLeft = false;
    public bool playersFlipped = false;

    //jumping + gravity
    [Header("Jumping Modifiers")]
    public float jumpHeight = 50f;
    public bool isGrounded = false;
    private float jumpRayCastOffset = -0.1999f;
    public float jumpCooldownTimer;

    //double Jumping / dashing
    [Header("Double Jump")]
    public bool canDoubleJump = true;
    public float doubleJumpHeight;
    [Header("Dashing")]
    public float forwardDashDist;
    public float backwardDashDist;

    ControlSet controls;

    // Start is called before the first frame update
    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponentInChildren<Animator>();
    }

    void Start()
    {
        InitializeCharacters();
        InitializeCharacterData();
    }

    void OnDisable()
    {
        animM.DestroyGraph();
    }

    public void InitializeCharacters()
    {
        if (CompareTag("Player"))
        {
            controls = ControlSettings.Player_1_Keybinds;

        }
         
        if (CompareTag("Player 2"))
        {
            controls = ControlSettings.Player_2_Keybinds;

        }
          
    }

    public void InitializeCharacterData()
    {
        if (data != null)
        {
            jumpHeight = data.jumpHeight;
            maxSpeed = data.maxSpeed;

            tree = new ComboTree(data.moveList);
            animM = new AnimationManager(anim, data);
            inputM = new InputManager(controls, tree, animM);
            Debug.Log(data.character + " node count: " + tree.GetNodeCount(tree.root));
        }
        else
            Debug.LogWarning("No Character Data has been loaded!!!");
        
    }

    // Update is called once per frame
    void Update()
    {
        //Base Functionality
        JumpCooldown();
        ResetVelocity();
        UpdateInputs();
        inputM.UpdateInput();
        FlipCharacter();
        
    }

    private void FixedUpdate()
    {
        //base functionalty 
        UpdateMovementForces();
        GroundDetection();
        Gravity();

    }

    public void ResetVelocity()
    {
        if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.A)
            /*|| Input.GetKeyUp(KeyCode.D) || Input.GetKeyUp(KeyCode.A)*/)
        {
            rb.linearVelocity = new Vector3(0, rb.linearVelocity.y, rb.linearVelocity.z);
            //Debug.Log("velocity reset");
        }
    }

    public void UpdateInputs()
    {
        if (isGrounded)
        {
            if ((CompareTag("Player") && !playersFlipped)|| (CompareTag("Player 2") && playersFlipped))
            {
                if (Input.GetKey(controls.Right))
                {
                    if (isGrounded)
                    {
                        moveRight = true;
                        //Debug.Log("moveright is activated");
                        if (Input.GetKeyDown(controls.Right))
                            animM.PlayAnimation("Run_Forward");
                    }
                    
                }


                if (Input.GetKey(controls.Left))
                {
                    moveLeft = true;
                    if (Input.GetKeyDown(controls.Left))
                        animM.PlayAnimation("Run_Backward");
                }
            }

            if ((CompareTag("Player 2") && !playersFlipped) || (CompareTag("Player") && playersFlipped))
            {
                if (Input.GetKey(controls.Right))
                {
                    moveRight = true;
                    if (Input.GetKeyDown(controls.Right))
                        animM.PlayAnimation("Run_Backward");
                }


                if (Input.GetKey(controls.Left))
                {
                    moveLeft = true;
                    if (Input.GetKeyDown(controls.Left))
                        animM.PlayAnimation("Run_Forward");
                }
            }


            if (!Input.GetKey(controls.Right))
            {
                moveRight = false;

            }
                

            if (!Input.GetKey(controls.Left))
                moveLeft = false;
        }



        //the input for jumping 
        if (Input.GetKeyDown(controls.Up) && isGrounded)
            Jump();

        if (Input.GetKeyDown(controls.Up) && !isGrounded && canDoubleJump && jumpCooldownTimer <= 0)
        {
            DoubleJump();
            canDoubleJump = false;
        }
            
        if (Input.GetKeyDown(controls.Right) && !isGrounded && canDoubleJump && jumpCooldownTimer <= 0)
        {
            DashRight();
            Debug.Log("you just dashed right");

            canDoubleJump = false;
        }
        if (Input.GetKeyDown(controls.Left) && !isGrounded && canDoubleJump && jumpCooldownTimer <= 0)
        {
            DashLeft();
            Debug.Log("you just dashed left");
            canDoubleJump = false;
        }
        

    }

    public void JumpCooldown()
    {
        if (jumpCooldownTimer > 0)
            jumpCooldownTimer -= Time.deltaTime;
        if (jumpCooldownTimer < 0)
            jumpCooldownTimer = 0;
    }
    public void Jump()
    {
        jumpCooldownTimer = 0.05f;
        isGrounded = false;

        rb.AddForce(Vector3.up * jumpHeight, ForceMode.Impulse);

        if (!playersFlipped)
        {
            if (moveRight)
            {
                rb.AddForce(Vector3.right * jumpHeight * 2f, ForceMode.Impulse);
                moveRight = false;
                Debug.Log("moveright Deactived");
                Debug.Log("forward Jump actived");
            }
            else if (moveLeft)
            {
                rb.AddForce(Vector3.left * jumpHeight, ForceMode.Impulse);
                moveLeft = false;
                Debug.Log("backward Jump actived");
            }
        }
        else
        {
            if (moveLeft)
            {
                rb.AddForce(Vector3.left * jumpHeight, ForceMode.Impulse);
                moveLeft = false;
                Debug.Log("forward Jump actived");
            }
            else if (moveRight)
            {
                rb.AddForce(Vector3.right * jumpHeight, ForceMode.Impulse);
                moveRight = false;
                Debug.Log("backward Jump actived");
            }
        }
        
    }

    public void DoubleJump()
    {
        rb.AddForce(Vector3.up * doubleJumpHeight, ForceMode.Impulse);
    }

    public void DashRight()
    {
        if (!playersFlipped)
        {
            if (CompareTag("Player"))
            {
                rb.AddForce(Vector3.right * forwardDashDist, ForceMode.Impulse);
            }
            else if (CompareTag("Player 2"))
            {
                rb.AddForce(Vector3.right * backwardDashDist, ForceMode.Impulse);
            }
        }
        else
        {
            if (CompareTag("Player"))
            {
                rb.AddForce(Vector3.right * backwardDashDist, ForceMode.Impulse);
            }
            if (CompareTag("Player 2"))
            {
                rb.AddForce(Vector3.right * forwardDashDist, ForceMode.Impulse);
            }
        }
    }

    public void DashLeft()
    {
        if (!playersFlipped)
        {
            if (CompareTag("Player"))
            {
                rb.AddForce(Vector3.left * backwardDashDist, ForceMode.Impulse);
            }
            else if (CompareTag("Player 2"))
            {
                rb.AddForce(Vector3.left * forwardDashDist, ForceMode.Impulse);

            }
        }
        else
        {
            if (CompareTag("Player"))
            {
                rb.AddForce(Vector3.left * forwardDashDist, ForceMode.Impulse);
            }
            if (CompareTag("Player 2"))
            {
                rb.AddForce(Vector3.left * backwardDashDist, ForceMode.Impulse);
            }
        }
    }

    public void UpdateMovementForces()
    {

        if (CameraController.distanceBetweenPlayers >= CameraController.maxXDistance)
        {
            if (CameraController.rawDistance < 0)
            {
                if (CompareTag("Player"))
                {
                    moveLeft = false;
                }
                else
                {
                    moveRight = false;
                }

            }
            else
            {
                if (CompareTag("Player"))
                {
                    moveRight = false;
                }
                else
                {
                    moveLeft = false;
                }

            }
        }

        //input detection
        if (Mathf.Abs(rb.linearVelocity.x) > maxSpeed)
        {
            if (rb.linearVelocity.x < 0)
                rb.linearVelocity = new Vector3(-maxSpeed, rb.linearVelocity.y, rb.linearVelocity.z);
            else
                rb.linearVelocity = new Vector3(maxSpeed, rb.linearVelocity.y, rb.linearVelocity.z);
        }

        if (moveRight)
        {
            rb.AddForce(Vector3.right * accerationMultiplier);
        }

        if (moveLeft)
        {
            rb.AddForce(Vector3.left * accerationMultiplier);
        }
        //Debug.Log("current Speed is " + Mathf.Abs(rb.velocity.x));

        

    }

    //a gravty multiplier that should be changable to how strong we want it
    public void Gravity()
    {
        rb.AddForce(Vector3.down * gravityMultiplier);
    }

    public void GroundDetection()
    {
        

        RaycastHit hit;
        // Does the ray intersect any objects excluding the player layer
        if (Physics.Raycast(new Vector3(transform.position.x, transform.position.y - jumpRayCastOffset,
            transform.position.z), Vector3.down, out hit, 0.2f))
        {
            if (hit.collider.gameObject.CompareTag("Ground") && jumpCooldownTimer <= 0)
            {
                isGrounded = true;
                rb.linearVelocity = new Vector3(rb.linearVelocity.x, 0, rb.linearVelocity.z);
            }

            Debug.DrawRay(new Vector3(transform.position.x, transform.position.y - jumpRayCastOffset,
                transform.position.z), Vector3.down * 0.2f, Color.magenta);
            //Debug.Log("Did Hit");
        }
        else
        {
            isGrounded = false;
            Debug.DrawRay(new Vector3(transform.position.x, transform.position.y - jumpRayCastOffset,
                transform.position.z), Vector3.down * 0.2f, Color.cyan);
            //Debug.Log("Did not Hit");
        }

        if (isGrounded)
        {
            canDoubleJump = true;
        }

    }

    public void FlipCharacter()
    {
        //make it so that it plays an animation of the character switches stances, instead of just rotating.
       

        if (CompareTag("Player") && CameraController.flipP1)
        {
            transform.Rotate(new Vector3(0, 180, 0));
            CameraController.flipP1 = false;
            playersFlipped = !playersFlipped;
            return;
        }
        if (CompareTag("Player 2") && CameraController.flipP2)
        {
            transform.Rotate(new Vector3(0, 180, 0));
            CameraController.flipP2 = false;
            playersFlipped = !playersFlipped;
            return;
        }
        
    }


    public void ShowAnimationGraph()
    {
        animM.ShowGraph();
    }

}
