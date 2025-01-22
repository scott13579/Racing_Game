using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance; // 싱글턴 패턴으로 GameManager 접근

    public GameObject startPanel; // 게임 시작 패널
    public GameObject gameOverPanel; // 게임 종료 패널
    public TextMeshProUGUI gasText; // 화면에 표시될 gas 텍스트

    private float gas = 100f; // 초기 gas 값
    private bool isGameRunning = false; // 게임 실행 상태

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
        gasText.gameObject.SetActive(false); // 게임 시작 시 gasText 비활성화
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
        isGameRunning = true;
        gas = 100f;
        gasText.gameObject.SetActive(true); // 게임 시작 시 gasText 활성화
        UpdateGasText();
    }

    public void EndGame()
    {
        // 게임 종료 로직
        isGameRunning = false;
        gameOverPanel.SetActive(true);
        gasText.gameObject.SetActive(false); // 게임 종료 시 gasText 비활성화
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
