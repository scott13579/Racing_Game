using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 10f; // 자동차의 초기 좌우 이동 속도
    public float laneOffset = 2.5f; // 차선 간 간격 (중앙, 왼쪽, 오른쪽)
    public float speedIncreaseRate = 0.05f; // 이동 속도 증가 비율

    public LayerMask gasItemLayer;
    
    private int currentLane = 1; // 현재 차선 (0: 왼쪽, 1: 중앙, 2: 오른쪽)
    private Vector3 targetPosition; // 목표 위치
    private float distanceTraveled = 0f; // 이동한 거리

    void Start()
    {
        UpdateTargetPosition();
    }

    void Update()
    {
        if (GameManager.Instance != null && GameManager.Instance.isGameRunning)
        {
            HandleInput();
            MoveToLane();
            UpdateDistanceTraveled(); // 이동 거리 업데이트
        }
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
        // 현재 차선에 맞는 목표 위치 설정
        targetPosition = new Vector3((currentLane - 1) * laneOffset, transform.position.y, transform.position.z);
    }

    private void MoveToLane()
    {
        // 부드럽게 차선 변경
        transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * moveSpeed);
    }

    private void UpdateDistanceTraveled()
    {
        // 이동 거리 계산
        distanceTraveled += moveSpeed * Time.deltaTime;

        // 이동 속도 증가 (게임 시간이 지날수록 이동 속도가 빨라짐)
        moveSpeed += speedIncreaseRate * Time.deltaTime;
    }

    public void ResetDistance()
    {
        distanceTraveled = 0f; // 게임 시작 시 이동 거리 초기화
    }

    public float GetDistance()
    {
        return distanceTraveled; // 이동 거리 반환
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
