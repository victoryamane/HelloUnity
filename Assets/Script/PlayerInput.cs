using System;
using UnityEditor;
using UnityEngine;

public class PlayerInput : MonoBehaviour {
    [SerializeField] private Rigidbody2D rigidbody;
    [SerializeField] private Animator animator;

    [SerializeField] private float speed;
    [SerializeField] private float jump;
    [SerializeField] private LayerMask[] layerHit;

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
    }

    private void FixedUpdate() {
        Translate();
        // 1   0b1
        // 2   0b10
        // 16  0b10000
        // 15 << 2  0b111100 =  4 + 8 + 16 + 32  = 60      
        //  1 << 9  0b1000000000 = 512
        // int ground = 1 << 9; // 0b01000000000
        // int enemy = 1 << 10; // 0b10000000000
        // int mask = ground | enemy; // 0b11000000000 512 + 1024  = 1536


        int numberOfContacts = Physics2D.RaycastNonAlloc(transform.position, Vector2.down, hits, 1f, mask);
        for (int index = 0; index < numberOfContacts; index += 1) {
            RaycastHit2D hit = hits[index];
        }
    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.magenta;
        Gizmos.DrawRay(transform.position, Vector2.down * 1f);
    }

    private void Translate() {
        Vector3 velocity = rigidbody.velocity;
        PlayerState state;

        if (Input.GetKey("right") || Input.GetKey("d")) {
            velocity.x = 1f * speed;
            state = PlayerState.WALK;
        } else if (Input.GetKey("left") || Input.GetKey("a")) {
            velocity.x = -1f * speed;
            state = PlayerState.WALK;
        } else {
            velocity.x = 0f;
            state = PlayerState.IDLE;
        }

        if (Input.GetKey("up") || Input.GetKey("w")) {
            velocity.y = 1f * speed;
            state = PlayerState.JUMP;
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