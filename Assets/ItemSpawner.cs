using UnityEngine;
using TMPro; // TextMeshProUGUI 사용을 위한 네임스페이스

public class ItemSpawner : MonoBehaviour
{
    public GameObject gasItemPrefab; // 가스 아이템 프리팹
    public float spawnInterval = 2f; // 스폰 간격 (초)
    public float spawnZPosition = 20f; // 스폰될 Z 위치
    public float laneOffset = 2.5f; // 차선 간 간격 (왼쪽, 중앙, 오른쪽)

    public float initialSpeed = 5f; // 초기 속도
    public float maxSpeed = 10f; // 최대 속도
    public float speedIncreaseRate = 0.1f; // 속도 증가 비율
    
    private float currentSpeed; // 현재 속도
    private float timer = 0f;

    void Start()
    {
        currentSpeed = initialSpeed; // 초기 속도 설정
    }

    void Update()
    {
        // 속도 증가
        currentSpeed += speedIncreaseRate * Time.deltaTime;

        // 최대 속도 제한
        currentSpeed = Mathf.Min(currentSpeed, maxSpeed);

        // 스폰 타이머 업데이트
        timer += Time.deltaTime;

        if (timer >= spawnInterval)
        {
            SpawnGasItem();
            timer = 0f; // 타이머 초기화
        }
    }

    private void SpawnGasItem()
    {
        // 랜덤으로 차선 선택 (0: 왼쪽, 1: 중앙, 2: 오른쪽)
        int lane = Random.Range(0, 3);
        float xPosition = (lane - 1) * laneOffset;

        // 가스 아이템 생성
        Vector3 spawnPosition = new Vector3(xPosition, 1f, spawnZPosition);
        GameObject gasItem = Instantiate(gasItemPrefab, spawnPosition, Quaternion.identity);

        // 생성된 가스 아이템에 속도 적용
        gasItem.GetComponent<GasMovement>().currentSpeed = currentSpeed;
    }
}