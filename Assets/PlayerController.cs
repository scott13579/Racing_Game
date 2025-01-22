using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 10f; // 자동차의 좌우 이동 속도
    public float laneOffset = 2.5f; // 차선 간 간격 (중앙, 왼쪽, 오른쪽)
    public LayerMask gasItemLayer; // 가스 아이템의 레이어

    private int currentLane = 1; // 현재 차선 (0: 왼쪽, 1: 중앙, 2: 오른쪽)
    private Vector3 targetPosition; // 목표 위치

    void Start()
    {
        // 초기 위치는 중앙 차선
        UpdateTargetPosition();
    }

    void Update()
    {
        HandleInput();
        MoveToLane();
    }

    private void HandleInput()
    {
        // 좌우 이동 입력 처리
        if (Input.GetKeyDown(KeyCode.LeftArrow) && currentLane > 0)
        {
            currentLane--;
            UpdateTargetPosition();
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow) && currentLane < 2)
        {
            currentLane++;
            UpdateTargetPosition();
        }
    }

    private void UpdateTargetPosition()
    {
        // 현재 차선을 기준으로 목표 위치 설정
        targetPosition = new Vector3((currentLane - 1) * laneOffset, transform.position.y, transform.position.z);
    }

    private void MoveToLane()
    {
        // 부드럽게 차선 변경
        transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * moveSpeed);
    }

    private void OnTriggerEnter(Collider other)
    {
        // 가스 아이템 충돌 처리 (레이어를 활용한 충돌 검사)
        if (((1 << other.gameObject.layer) & gasItemLayer) != 0)
        {
            GameManager.Instance.AddGas(30f); // 가스 30 증가
            Destroy(other.gameObject); // 아이템 제거
        }
    }
}