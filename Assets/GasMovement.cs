using UnityEngine;

public class GasMovement : MonoBehaviour
{
    public float initialSpeed = 5f; // 초기 속도
    public float acceleration = 1f; // 속도 증가율
    public float maxDistance = 20f; // 최대 이동 거리

    private float currentSpeed; // 현재 속도
    private Vector3 startPosition;

    private void Start()
    {
        // 시작 위치 기록
        startPosition = transform.position;
        currentSpeed = initialSpeed; // 초기 속도로 설정
    }

    private void Update()
    {
        // -Z축 방향으로 점점 이동
        transform.Translate(Vector3.back * currentSpeed * Time.deltaTime);

        // 속도 증가
        currentSpeed += acceleration * Time.deltaTime;

        // 일정 거리만큼 이동한 후 가스를 삭제
        if (Vector3.Distance(startPosition, transform.position) >= maxDistance)
        {
            Destroy(gameObject); // 가스 삭제
        }
    }
}