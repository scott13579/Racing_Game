using UnityEngine;

public class GasMovement : MonoBehaviour
{
    private float currentSpeed; // 현재 속도
    private Vector3 startPosition;

    public float maxDistance = 20f; // 최대 이동 거리 (삭제될 거리)

    private void Start()
    {
        // 시작 위치 기록
        startPosition = transform.position;
        currentSpeed = 5f; // 초기 속도 설정
    }

    private void Update()
    {
        // -Z축 방향으로 점점 이동
        transform.Translate(Vector3.back * currentSpeed * Time.deltaTime);

        // 일정 거리만큼 이동한 후 가스를 삭제
        if (Vector3.Distance(startPosition, transform.position) >= maxDistance)
        {
            Destroy(gameObject); // 가스 삭제
        }
    }

    // 외부에서 속도를 설정하는 메서드
    public void SetSpeed(float speed)
    {
        currentSpeed = speed;
    }
}