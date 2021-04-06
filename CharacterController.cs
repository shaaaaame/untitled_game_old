using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class CharacterController : MonoBehaviour
{
    [SerializeField] private float JumpForce;
    [SerializeField] private LayerMask GroundLayers;
    [SerializeField] private Transform GroundCheck;
    [SerializeField] [Range(0, 1)] private float Smoothing;
    [SerializeField] private float speed;
    [SerializeField] private float jumpSpeed;

    private float GroundedRadius = 0.06f;
    public bool isGrounded;
    public static bool FacingRight = true;
    private Rigidbody Rigidbody;
    private Vector3 Velocity = Vector3.zero;

    private float fallMultiplier = 2.5f;
    private float lowJumpMultiplier = 2f;

    private void Awake()
    {
        Rigidbody = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        isGrounded = false;

        Collider[] colliders = Physics.OverlapSphere(GroundCheck.position, GroundedRadius, GroundLayers);
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].gameObject != gameObject)
            {
                isGrounded = true;
            }
        }
    }

    private void Update()
    {
        if (Rigidbody.velocity.y < 0)
            Rigidbody.velocity += Vector3.up * Physics.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        else if (Rigidbody.velocity.y > 0)
            Rigidbody.velocity += Vector3.up * Physics.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
    }

    public void Move(float vel, bool jump)
    {
        Vector3 targetVel = new Vector3(vel * 10f * speed * Time.deltaTime, Rigidbody.velocity.y, Rigidbody.velocity.z);
        Rigidbody.velocity = Vector3.SmoothDamp(Rigidbody.velocity, targetVel, ref Velocity, Smoothing);

        if (vel > 0 && !FacingRight)
        {
            Flip();
        }

        else if (vel < 0 && FacingRight)
        {
            Flip();
        }


        if (jump)
        {
            // jump
            Rigidbody.velocity = new Vector3(Rigidbody.velocity.x, jumpSpeed * Time.deltaTime * 10, 0);
        }
    }

    private void Flip()
    {
        FacingRight = !FacingRight;
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }
}
