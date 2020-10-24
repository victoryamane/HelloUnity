using UnityEngine;

public class PlayerMovement : MonoBehaviour {
    [SerializeField] private Rigidbody2D rigidbody;
    [SerializeField] private Animator animator;

    [SerializeField] private float speed;
    [SerializeField] private float jumpTime;
    [SerializeField] private LayerMask[] layerHit;

    private bool isGrounded;
    private bool isJumping;
    private float jumpTimer;
    private float jumpBuffer;

    private int mask = 0;


    private PlayerState state = PlayerState.IDLE;

    private RaycastHit2D[] hits = new RaycastHit2D[3];

    // Start is called before the first frame update
    private void Start() {
        for (int index = 0; index < layerHit.Length; index += 1) {
            mask |= layerHit[index].value;
        }
    }

    // Update is called once per frame
    private void Update() {
        /*if (!ismoving)
        {
            setstate(playerstate.idle);
        }*/
        // transform.Translate(new Vector3(5f * Time.unscaledDeltaTime, 0f));
    }

    private void FixedUpdate() {
        int numberOfContacts = Physics2D.RaycastNonAlloc(transform.position, Vector2.down, hits, 1f, mask);
        isGrounded = numberOfContacts > 0;
        jumpBuffer -= Time.fixedDeltaTime;

        if (isGrounded && jumpBuffer > 0f) {
            Jump();
        }
    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.magenta;
        Gizmos.DrawRay(transform.position, Vector2.down * 1f);
    }

    public void Move(float horizontal) {
        Vector3 velocity = rigidbody.velocity;
        Quaternion rotation = transform.rotation;

        velocity.x = horizontal * speed;
        rotation.y = horizontal < 0f ? 180f : 0f;

        SetState(PlayerState.WALK);
        transform.rotation = rotation;
        rigidbody.velocity = velocity;
    }

    public void Stop() {
        Vector3 velocity = rigidbody.velocity;
        velocity.x = 0f;

        SetState(PlayerState.IDLE);
        rigidbody.velocity = velocity;
    }

    public void Jump() {
        if (isGrounded) {
            Vector3 velocity = rigidbody.velocity;

            isJumping = true;
            jumpTimer = jumpTime;
            velocity.y = 1f * speed;
            jumpBuffer = 0f;

            SetState(PlayerState.JUMP);
            rigidbody.velocity = velocity;
        } else {
            jumpBuffer = 0.125f;
        }
    }

    public void KeepJumping() {
        if (!isJumping) return;

        if (jumpTimer > 0f) {
            Vector3 velocity = rigidbody.velocity;

            velocity.y = 1f * speed;
            jumpTimer -= Time.fixedDeltaTime;

            rigidbody.velocity = velocity;
        } else {
            isJumping = false;
        }
    }

    public void StopJumping() {
        isJumping = false;
    }

    private bool IsJumpEnabled => state != PlayerState.JUMP;

    private void SetState(PlayerState newState) {
        if (!isGrounded) {
            newState = PlayerState.JUMP;
        }

        if (state == newState) {
            return; // Guard Clause (Clausula de garantia)
        }


        state = newState;
        if (state == PlayerState.IDLE) {
            animator.SetTrigger("idle");
        } else if (state == PlayerState.WALK) {
            animator.SetTrigger("walk");
        } else if (state == PlayerState.JUMP) {
            animator.SetTrigger("jump");
        }
    }
}