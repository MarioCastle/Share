using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Player : MonoBehaviour
{

    [SerializeField] float moveSpeed = 6f;
    [SerializeField] PlayerInput input;

    new Rigidbody2D rigidbody;

    void Awake(){
        rigidbody = GetComponent<Rigidbody2D>();

    }
    void OnEnable()
    {
        input.onMove += Move;
        input.onStopMove += StopMove;
    }
    
    void OnDisable()
    {
        input.onMove -= Move;
        input.onStopMove -= StopMove;
    }
    void Start()
    {
        rigidbody.gravityScale = 0f;
        input.EnableGameplayInput();
    }


    void Move(Vector2 moveInput)
    {
        // Vector2 moveAmount = moveInput * moveSpeed;
        // rigidbody.velocity = moveAmount;
        rigidbody.velocity = moveInput * moveSpeed;
        StartCoroutine(MovePositionLimitCoroutine());
    }

    void StopMove()
    {
        rigidbody.velocity = Vector2.zero;
        StopCoroutine(MovePositionLimitCoroutine());
    }

    IEnumerator MovePositionLimitCoroutine(){
        while(true){
            
            transform.position = Viewport.Instance.PlayerMoveablePosition(transform.position);

            yield return null;
        }
    }
}
