using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public bool isGameRunning = false; // 게임 실행 상태

    public GameObject startPanel; // 게임 시작 패널
    public GameObject gameOverPanel; // 게임 종료 패널
    public TextMeshProUGUI gasText; // 화면에 표시될 gas 텍스트

    public BackgroundMovement backgroundMovement; // 배경 이동 스크립트
    public ItemSpawner itemSpawner; // 아이템 생성 스크립트
    public PlayerController playerController; // 플레이어 조작 스크립트

    private float gas = 100f; // 초기 gas 값

    private void Awake()
    {
        // 싱글턴 설정
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        // 초기 패널 설정
        startPanel.SetActive(true);
        gameOverPanel.SetActive(false);

        // 게임 시작 전에는 모든 게임 요소 비활성화
        backgroundMovement.enabled = false;
        itemSpawner.enabled = false;
        playerController.enabled = false;
    }

    private void Update()
    {
        if (isGameRunning)
        {
            UpdateGas();
        }
    }

    public void StartGame()
    {
        // 게임 시작 로직
        startPanel.SetActive(false);
        gasText.gameObject.SetActive(true);
        isGameRunning = true;
        gas = 100f;
        UpdateGasText();

        // 게임이 시작되면 배경, 아이템 생성, 플레이어 조작 활성화
        backgroundMovement.enabled = true;
        itemSpawner.enabled = true;
        playerController.enabled = true;
    }

    public void EndGame()
    {
        // 게임 종료 로직
        isGameRunning = false;
        gasText.gameObject.SetActive(false);
        gameOverPanel.SetActive(true);

        // 게임이 종료되면 배경, 아이템 생성, 플레이어 조작 비활성화
        backgroundMovement.enabled = false;
        itemSpawner.enabled = false;
        playerController.enabled = false;
    }

    public void RestartGame()
    {
        // 게임 재시작 로직 (씬 리로드)
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void UpdateGas()
    {
        // 1초당 10 gas 감소
        gas -= 10f * Time.deltaTime;
        UpdateGasText();

        // gas가 0 이하가 되면 게임 종료
        if (gas <= 0)
        {
            gas = 0;
            EndGame();
        }
    }

    public void AddGas(float amount)
    {
        // gas 증가 로직
        gas += amount;
        if (gas > 100f)
        {
            gas = 100f; // 최대값 제한
        }
        UpdateGasText();
    }

    private void UpdateGasText()
    {
        // gas 텍스트 업데이트
        gasText.text = $"Gas: {Mathf.CeilToInt(gas)}";
    }
}
