using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : Player
{
    private float movement;

    private float jumpLittleTimer = 0;

    private Vector2 delta;

    //WALL JUMP & WALL SLIDE STUFF
    private float wallSlideSpeed = 0.01f;
    private bool isTouchingWall;
    private bool isWallSliding;
    [SerializeField] Transform wallCheckPoint;
    [SerializeField] Vector2 wallJumpAngle;
    private bool isAbleWallJump = true;

    public override void Start()
    {
        base.Start();
        wallJumpAngle.Normalize();
    }

    public override void Update()
    {
        //MOVE
        movement = Input.GetAxis("Horizontal");
        Move();

        //JUMP && WALLJUMP
        if (GroundCheck.OnGround)
        {
            isAbleWallJump = true;
        }

        if (Input.GetButtonDown("Jump") && GroundCheck.OnGround && jumpLittleTimer < 0)
        {
            Jump();
        }
        else if(Input.GetButtonDown("Jump") && isAbleWallJump && isTouchingWall)
        {
            WallJump();
        }

        WallSlideAnimation();

        jumpLittleTimer -= Time.deltaTime;
        JumpAnimations();

        //FLIP
        Flip();

        //WALL SLIDE
        isTouchingWall = Physics2D.OverlapBox(wallCheckPoint.position, Vector2.zero, 0, LayerMask.GetMask("Ground"));
        WallSlide();
       
    }

    private void Move()
    {
        if (!Physics2D.Raycast(transform.position, Vector2.right * movement,
            PlayerStats.CurrentSpeed * Time.deltaTime + Col.bounds.extents.x, LayerMask.GetMask("Ground", "Enemy")))
        {
            transform.position += new Vector3(movement, 0, 0) * Time.deltaTime * PlayerStats.CurrentSpeed;
            Animator.SetFloat("RUN", Mathf.Abs(movement));
        }
    }

    private void Jump()
    {    
        Rb.AddForce(new Vector2(0, PlayerStats.CurrentJump), ForceMode2D.Impulse);
        jumpLittleTimer = 0.3f;
    }

    private void JumpAnimations()
    {
        if (GroundCheck.OnGround)
        {
            Animator.SetBool("JUMP", false);
        }
        else
        {
            Animator.SetBool("JUMP", true);
        }
        Animator.SetFloat("YVelocity", Rb.velocity.y);

    }

    private void Flip()
    {
        delta = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 characterScale = transform.localScale;

        FlipAnimations(characterScale);

        if (delta.x > transform.position.x)
        {
            characterScale.x = 1;
        }
        if (delta.x < transform.position.x)
        {
            characterScale.x = -1;
        }

        transform.localScale = characterScale;
    }

    private void FlipAnimations(Vector3 characterScale)
    {
        if (movement == 0f)
            Animator.SetBool("FLIPPED", false);
        if (characterScale.x == 1 && movement > 0.01)
            Animator.SetBool("FLIPPED", false);
        if (characterScale.x == -1 && movement > 0.01)
            Animator.SetBool("FLIPPED", true);
        if (characterScale.x == 1 && movement < -0.01)
            Animator.SetBool("FLIPPED", true);
        if (characterScale.x == -1 && movement < -0.01)
            Animator.SetBool("FLIPPED", false);
    }

    private void WallSlide()
    {
        if (isTouchingWall && !GroundCheck.OnGround && Rb.velocity.y < 0)
        {
            isWallSliding = true;
        }
        else
        {
            isWallSliding = false;
        }

        if (isWallSliding && isAbleWallJump)
        {
            Rb.velocity = new Vector2(Rb.velocity.x, wallSlideSpeed);
        }
    }

    private void WallJump()
    {
        Rb.AddForce(new Vector2(0, PlayerStats.CurrentJump * wallJumpAngle.y), ForceMode2D.Impulse);
        isAbleWallJump = false;
    }

    private void WallSlideAnimation()
    {
        if(isAbleWallJump && isTouchingWall)
            Animator.SetBool("WALL SLIDING", true);
        else
            Animator.SetBool("WALL SLIDING", false);
    }
}
