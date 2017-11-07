using UnityEngine;

public class PlayerController : MonoBehaviour 
{
	[Header("Speed settings")]
	public float horizontalSpeed;
	public float verticalSpeed;

	[Header("Grounded check settings")]
	public LayerMask groundMask;

    [Header("Sounds")]
    public AudioClip jumpSound;
    public AudioClip hurtSound;

	private bool isFacingRight;
	private bool isGrounded;
    private bool allowDoubleJump;
    private Rigidbody2D rigidBody;
	private Animator animator;
    private Camera mainCamera;
    private float cameraWidth;
    private SpriteRenderer sprite;
    private float radius;

    private AudioSource jumpAudio;
    public AudioSource hurtAudio;

    private Transform feetPoint;
    private Transform headPoint;

    private const float MAX_VERTICAL_VELOCITY = -10.0f;

    public void Awake () 
	{
		rigidBody = GetComponent<Rigidbody2D>();
		animator = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        mainCamera = Camera.main.GetComponent<Camera>();
        cameraWidth = mainCamera.orthographicSize * Screen.width / Screen.height;
        isFacingRight = true;
        radius = GetComponent<CapsuleCollider2D>().bounds.size.x / 2.0f;
        InitializeAudioSources();

        feetPoint = new GameObject("Feet Point").transform;
        feetPoint.position = transform.position;
        feetPoint.parent = transform;

        headPoint = new GameObject("Head Point").transform;
        headPoint.position = new Vector3(transform.position.x, transform.position.y + sprite.bounds.size.y, transform.position.z);
        headPoint.parent = transform;
	}

	public void Update () 
	{
        //game is paused so don't update
        if (Time.timeScale == 0.0f)
            return;

        //limit vertical velocity
        if (rigidBody.velocity.y < MAX_VERTICAL_VELOCITY)
            rigidBody.velocity = new Vector2(rigidBody.velocity.x, MAX_VERTICAL_VELOCITY);

        //horizontal bounds check
        HorizontalBoundsCheck();

        //check if player is standing on something
		isGrounded = Physics2D.OverlapCircle(feetPoint.position, radius, groundMask);

        //reset double jump flag if player touched the ground
		if (isGrounded)
			allowDoubleJump = true;

		var hSpeed = Input.GetAxisRaw("Horizontal"); 

        //only move if player is not crouched
		rigidBody.velocity = new Vector2 (hSpeed * horizontalSpeed, rigidBody.velocity.y);

        //flip player's sprite if he changed the direction of movement
        if ((hSpeed < 0 && isFacingRight) || (hSpeed > 0 && !isFacingRight))
        {
            sprite.flipX = !sprite.flipX;
            isFacingRight = !isFacingRight;
        }
		
        //check if jump button was pressed and if jumping is allowed 
        if (Input.GetButtonDown("Jump"))
            if (isGrounded)
                Jump();
		    else if (allowDoubleJump) {
                Jump();
				allowDoubleJump = false;
			}

        //update animation controller variables
		animator.SetInteger ("hSpeed", (int) rigidBody.velocity.x);
		animator.SetInteger ("vSpeed", (int) rigidBody.velocity.y);
		animator.SetBool ("isGrounded", isGrounded);
	}

    private void Jump()
    {
        rigidBody.isKinematic = false;
        rigidBody.velocity = new Vector2 (rigidBody.velocity.x, 0.0f);
        rigidBody.AddForce(new Vector2(0.0f, verticalSpeed));
        jumpAudio.Play();
    }

    private void HorizontalBoundsCheck()
    {
        var cameraPositionX = mainCamera.transform.position.x;
        var playerPosition = transform.position;

        playerPosition.x = Mathf.Clamp(
            playerPosition.x,
            cameraPositionX - cameraWidth + sprite.bounds.size.x / 2,
            cameraPositionX + cameraWidth - sprite.bounds.size.x / 2
        );

        transform.position = playerPosition;
    }

    private void InitializeAudioSources()
    {
        jumpAudio = gameObject.AddComponent<AudioSource>();
        jumpAudio.playOnAwake = false;
        jumpAudio.clip = jumpSound;

        hurtAudio = gameObject.AddComponent<AudioSource>();
        hurtAudio.playOnAwake = false;
        hurtAudio.clip = hurtSound;
    }
}