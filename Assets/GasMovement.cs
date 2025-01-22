using UnityEngine;

public class GasMovement : MonoBehaviour
{
    public float currentSpeed; // 속도를 외부에서 설정

    public float maxDistance = 20f; // 최대 이동 거리
    private Vector3 startPosition;

    void Start()
    {
        // 시작 위치 기록
        startPosition = transform.position;
    }

    void Update()
    {
        // -Z축 방향으로 점점 이동
        transform.Translate(Vector3.back * currentSpeed * Time.deltaTime);

        // 일정 거리만큼 이동한 후 가스를 삭제
        if (Vector3.Distance(startPosition, transform.position) >= maxDistance)
        {
            Destroy(gameObject); // 가스 삭제
        }
    }
}