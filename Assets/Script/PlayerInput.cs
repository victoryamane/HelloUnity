using System;
using UnityEditor;
using UnityEngine;

public class PlayerInput : MonoBehaviour {
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

        Translate();
    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.magenta;
        Gizmos.DrawRay(transform.position, Vector2.down * 1f);
    }

    private void Translate() {
        Vector3 velocity = rigidbody.velocity;
        PlayerState state;

        Quaternion rotation = transform.rotation;
        if (Input.GetKey("right") || Input.GetKey("d")) {
            velocity.x = 1f * speed;
            state = PlayerState.WALK;
            rotation.y = 0f;
            transform.rotation = rotation;
        } else if (Input.GetKey("left") || Input.GetKey("a")) {
            velocity.x = -1f * speed;
            state = PlayerState.WALK;
            rotation.y = 180f;
            transform.rotation = rotation;
        } else {
            velocity.x = 0f;
            state = PlayerState.IDLE;
        }

        if (!isGrounded) {
            state = PlayerState.JUMP;
        }

        jumpBuffer -= Time.fixedDeltaTime;
        bool isUpKeyDown = Input.GetKeyDown("up") || Input.GetKeyDown("w");
        bool isJumpingEnabled = isGrounded && (isUpKeyDown || jumpBuffer > 0f);

        if (isJumpingEnabled) {
            isJumping = true;
            jumpTimer = jumpTime;
            velocity.y = 1f * speed;
            state = PlayerState.JUMP;
            jumpBuffer = 0f;
        } else if (!isGrounded && isUpKeyDown) {
            jumpBuffer = 0.125f;
        } else if (isJumping && (Input.GetKey("up") || Input.GetKey("w"))) {
            if (jumpTimer > 0f) {
                velocity.y = 1f * speed;
                jumpTimer -= Time.fixedDeltaTime;
            } else {
                isJumping = false;
            }
        } else if (Input.GetKeyUp("up") || Input.GetKeyUp("w")) {
            isJumping = false;
        }

        rigidbody.velocity = velocity;
        SetState(state);
    }

    private bool IsJumpEnabled => state != PlayerState.JUMP;

    private void SetState(PlayerState newState) {
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