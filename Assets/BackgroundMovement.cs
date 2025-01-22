using UnityEngine;

public class BackgroundMovement : MonoBehaviour
{
    public float initialSpeed = 5f; // 초기 배경 속도
    public float acceleration = 0.1f; // 가속도
    public float maxSpeed = 20f; // 최대 속도 (상한치)
    public float resetPoint = -20f; // 배경이 반복될 위치

    private float currentSpeed; // 현재 배경 속도
    private Vector3 startPosition; // 배경의 초기 위치

    void Start()
    {
        // 초기 속도 설정
        currentSpeed = initialSpeed;

        // 배경의 초기 위치 기록
        startPosition = transform.position;
    }

    void Update()
    {
        // 게임이 시작되었을 때만 배경 이동
        if (GameManager.Instance != null && GameManager.Instance.isGameRunning)
        {
            transform.Translate(Vector3.back * currentSpeed * Time.deltaTime);

            // 속도 증가 (가속도 적용)
            currentSpeed += acceleration * Time.deltaTime;

            // 속도 상한치 적용
            currentSpeed = Mathf.Min(currentSpeed, maxSpeed); // currentSpeed가 maxSpeed를 초과하지 않도록 제한

            // 배경이 지정된 위치를 지나면
            if (transform.position.z <= resetPoint)
            {
                transform.position = startPosition; // 배경을 초기 위치로 리셋
            }
        }
    }
}