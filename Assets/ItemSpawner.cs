using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    public GameObject gasItemPrefab; // 가스 아이템 프리팹
    public float spawnInterval = 2f; // 스폰 간격 (초)
    public float spawnZPosition = 20f; // 스폰될 Z 위치
    public float laneOffset = 2.5f; // 차선 간 간격 (왼쪽, 중앙, 오른쪽)

    private float timer = 0f;

    void Update()
    {
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
        Instantiate(gasItemPrefab, spawnPosition, Quaternion.identity);
    }
}