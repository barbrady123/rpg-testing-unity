using System.Runtime.CompilerServices;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public int MoveSpeed = 4;

    private Rigidbody2D _rigidBody;
    private Animator _animator;

    private bool _isAttacking;
    private float attackDuration = 0.5f;
    private float attackRemaining = 0f;

    // Start is called before the first frame update
    void Start()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        float rawX = Input.GetAxisRaw("Horizontal");
        float rawY = Input.GetAxisRaw("Vertical");

        var movement = HandleMovement(rawX, rawY);
        HandleAttack();
        HandleAnimation(rawX, rawY, movement);
    }

    private Vector3 HandleMovement(float rawX, float rawY)
    {
        var move = Vector3.zero;

        if (!_isAttacking)
        {
            move = new Vector3(rawX, rawY) * this.MoveSpeed;
        }

        _rigidBody.velocity = move;
        return move;
    }

    private void HandleAnimation(float rawX, float rawY, Vector3 movement)
    {
        if ((rawX != 0f) || (rawY != 0f))
        {
            _animator.SetFloat("lastMoveX", rawX);
            _animator.SetFloat("lastMoveY", rawY);
        }

        _animator.SetFloat("posX", movement.x);
        _animator.SetFloat("posY", movement.y);

        _animator.SetBool("isAttacking", _isAttacking);
    }

    private void HandleAttack()
    {
        if (Input.GetButton("Fire1") && (!_isAttacking))
        {
            _isAttacking = true;
            attackRemaining = attackDuration;
        }
        else if (_isAttacking)
        {
            attackRemaining -= Time.deltaTime;
            if (attackRemaining <= 0f)
            {
                _isAttacking = false;
            }
        }
    }
}
