using UnityEngine;

namespace Enemy
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class EnemyPatrol : MonoBehaviour
    {
        public float moveSpeed = 4f;
        public LayerMask whatIsGround;
    
        public Transform forwardPoint;
        private Rigidbody2D _rigidBody2D;
    

        private void Start()
        { _rigidBody2D = GetComponent<Rigidbody2D>(); }

        private void FixedUpdate()
        {
            if (!IsGrounded()) return;
            _rigidBody2D.velocity = new Vector2(transform.localScale.x * moveSpeed, _rigidBody2D.velocity.y);
        }

        private void LateUpdate()
        {
            if (!IsGrounded()) return;
            if (DetectWallOrFall())
            {
                transform.localScale = new Vector3(-transform.localScale.x, 1f,1f);
            }
        }
    
        private bool IsGrounded()
        {
            return Physics2D.Raycast(transform.position, Vector2.down, 0.5f, whatIsGround);
        }

        private bool DetectWallOrFall()
        {
            RaycastHit2D hit;
            return !Physics2D.Raycast(forwardPoint.position, Vector2.down, 0.5f, whatIsGround)
                   || Physics2D.Raycast(transform.position, Vector2.right * transform.localScale.x, 0.8f, whatIsGround);
        }
    }
}