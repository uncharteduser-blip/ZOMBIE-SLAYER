using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;



public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public GameObject gameOverPanel;
    public TextMeshProUGUI killText;
    int killCount = 0;
    //public TextMeshProUGUI killText;
    public TextMeshProUGUI currentKillsText;
    public TextMeshProUGUI bestKillsText;
    //int killCount = 0;
    int bestKills = 0;
    
    void Start()
{
    bestKills = PlayerPrefs.GetInt("BestKills", 0);
}
    
    void Awake()
    {
        instance = this;
       // bestKills = PlayerPrefs.GetInt("BestKills", 0);
    }

    public void GameOver()
{
    gameOverPanel.SetActive(true);
    Time.timeScale = 0f;

    // Show current kills
    currentKillsText.text = "Kills: " + killCount;

    // Update best score
    if (killCount > bestKills)
    {
        bestKills = killCount;
    }

    bestKillsText.text = "Best: " + bestKills;
    if (killCount > bestKills)
{
    bestKills = killCount;
    PlayerPrefs.SetInt("BestKills", bestKills);
    PlayerPrefs.Save();
}
}

    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void AddKill()
{
    killCount++;
    killText.text = "Kills: " + killCount;
}
}