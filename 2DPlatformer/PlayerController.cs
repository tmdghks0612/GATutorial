using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    #region MEMBER_VARIABLES

    // RigidBody 는 물리엔진 계산의 본체가 되는 컴포넌트. 중력 등 여러 힘을 가하거나 물체의 충돌에 대한 반응 등에 영향을 준다고 생각하면 편합니다.
    private Rigidbody2D rigidBodyComponent;

    /*
     * 가로 방향의 속도를 저장하는 변수. tutorial 에서는 public 으로 사용했지만, 현 프로젝트에서는 private 으로 유지하며 Get/Set function 을 통해 접근할 것입니다.
     * 그럼에도 불구하고 프로젝트에서 값을 자주 바꾸며 관찰하고 싶은 경우 아래처럼 SerializeField 를 사용합시다.(보안/관리에 더 좋습니다)
     */
    [SerializeField]
    private float verticalSpeed;
    [SerializeField]
    private float jumpForce;
    [SerializeField]
    private float jumpTime;
    private float jumpTimeCounter;
    private bool isJumping = false;

    // 들어온 입력을 저장하기 위한 변수. 누르고 있는 시간이나 방향 등을 저장하기 위함.
    private float moveInput;

    private bool isOnGround = true;

    [SerializeField]
    private Transform feetPos;
    [SerializeField]
    private float checkRadius;
    [SerializeField]
    private LayerMask groundType;

    #endregion


    #region MEMBER_FUNCTIONS

    /*
     * 현재 스크립트에서 초기화를 담당하는 함수. Tutorial 에서는 Start 함수를 사용했지만,
     *  이는 프로젝트 규모가 커질 경우 여러 버그를 일으킬 수 있어, 직접 다른 이름의 함수로 지정해 호출할 것입니다.
     */
    public void InitPlayerController()
    {
        rigidBodyComponent = GetComponent<Rigidbody2D>();
    }

    /*
     * Update 함수와 유사하게 프레임마다 호출되는 함수라고 생각하면 됩니다.
     * 값의 변화를 언제든 알아차릴 수 있어 유용하지만, 이 함수가 너무 커지면 게임의 성능이 나빠질 수도 있으니 유의합시다.
     */
    private void FixedUpdate()
    {
        // GetAxisRaw 는 GetAxis 와 유사하지만, 키를 누른 감도와 상관없이 0 1 -1 만을 반환합니다.
        moveInput = Input.GetAxisRaw("Horizontal");

        // Unity 에서는 vector 에 특정 값을 더하는 기능이 없습니다. 매번 기존 vector 값을 이용해 새로운 벡터를 지정해줍시다.
        rigidBodyComponent.velocity = new Vector2(moveInput * verticalSpeed, rigidBodyComponent.velocity.y);
    }

    /*
     * 
     */
    private void Update()
    {
        // 반대 방향 입력이 들어오면 캐릭터를 뒤집어주는 작업
        if(moveInput < 0)
        {
            transform.eulerAngles = new Vector3(0, 180, 0);
        } else if(moveInput > 0)
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
        }


        #region JUMP

        // 발이 땅에 닿아 있는 것을 판별하는 정도입니다.
        isOnGround = Physics2D.OverlapCircle(feetPos.position, checkRadius, groundType);

        // 지정해둔 땅에 있으면서 스페이스바를 누르고 있고, 발이 충분히 땅에 닿아 있으면 점프를 합니다.
        if (isOnGround && Input.GetKeyDown(KeyCode.Space))
        {
            isJumping = true;
            jumpTimeCounter = jumpTime;
            rigidBodyComponent.velocity = Vector2.up * jumpForce;
        }

        // 스페이스바를 누르고 있으면 더 높이 뛰도록 만들어줍니다. 만약 뛰지 못했다면 곱한 값도 0이 되니 상관이 없을 것입니다.
        if (Input.GetKey(KeyCode.Space) && isJumping)
        {
            if(jumpTimeCounter > 0)
            {
                rigidBodyComponent.velocity = Vector2.up * jumpForce;
                jumpTimeCounter -= Time.deltaTime;
            }
            else
            {
                isJumping = false;
            }
        }

        // 스페이스바 키를 떼는 순간 점프가 끊기도록 합니다.
        if (Input.GetKeyUp(KeyCode.Space))
        {
            isJumping = false;
        }

        #endregion
    }
    #endregion
}
