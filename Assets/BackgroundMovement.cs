using UnityEngine;

public class BackgroundMovement : MonoBehaviour
{
    public float initialSpeed = 5f; // 초기 배경 속도
    public float acceleration = 0.1f; // 가속도
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
        // 배경을 -Z축 방향으로 이동
        transform.Translate(Vector3.back * currentSpeed * Time.deltaTime);

        // 속도 증가 (가속도 적용)
        currentSpeed += acceleration * Time.deltaTime;

        // 배경이 지정된 위치를 지나면
        if (transform.position.z <= resetPoint)
        {
            // 배경을 초기 위치로 리셋
            transform.position = startPosition;
        }
    }
}