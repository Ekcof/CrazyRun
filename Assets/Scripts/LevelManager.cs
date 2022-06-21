using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{

    [SerializeField] private int index;
    [SerializeField] private GameObject fadePanel;
    [SerializeField] private GameObject deadPanel;
    [SerializeField] private GameObject winPanel;
    [SerializeField] private GameObject endPanel;
    [SerializeField] private Button pauseButton;
    [SerializeField] private Text ratingText;
    [SerializeField] private Text titleText;
    [SerializeField] private float maxTime;
    [SerializeField] private string levelName;
    private string minuteString;
    private int minutes;
    private int seconds;

    private float minTime;
    private int buildIndex;

    public int GetMinTime()
    {
        int getTime = (int)minTime;
        return getTime;
    }

    public void AddTime()
    {
        if (ratingText != null)
        {
            seconds = (int)Mathf.Round(minTime);
            minutes = seconds / 60;
            if (minutes > 0)
            {
                seconds -= (minutes * 60);
                if (minutes >= 10)
                {
                    minuteString = minutes.ToString() + ":";
                    ratingText.text = minutes.ToString() + ":" + seconds.ToString();
                }
                else
                {
                    minuteString = "0" + minutes.ToString() + ":";
                }
            }
            else
            {
                minuteString = "00:";
            }
            if (seconds >= 10)
            {
                ratingText.text = minuteString + seconds.ToString();
            }
            else
            {
                ratingText.text = minuteString + "0" + seconds.ToString();
            }
        }
    }

    public int GetMaxTime()
    {
        return (int)maxTime;
    }

    private void Awake()
    {
        minTime = maxTime;
        if(titleText!=null) titleText.text = levelName;
    }

    private void Update()
    {
        if (deadPanel != null)
        {
            if (!deadPanel.activeSelf && !winPanel.activeSelf) minTime -= Time.deltaTime;
            AddTime();
            if (minTime <= 0 && !deadPanel.activeSelf) DeadMenu();
        }
    }

    private void ActivateScene()
    {
        Time.timeScale = 1f;
        fadePanel.SetActive(false);
    }

    public void OnLevelButtonClick()
    {
        SceneManager.LoadScene(index);

        Time.timeScale = 1f;
    }

    public void PauseGame()
    {
        if (Time.timeScale > 0f)
        {
            Time.timeScale = 0f;
            fadePanel.SetActive(true);
        }
        else
        {
            ActivateScene();
        }
    }

    public void WinMenu()
    {
        fadePanel.SetActive(false);
        winPanel.SetActive(true);
        pauseButton.enabled = false;
        Time.timeScale = 0.3f;
    }

    public void DeadMenu()
    {
        if (minTime <= 0f)
        {
            Transform deadText = deadPanel.transform.Find("Text");
            if (deadText != null) deadText.GetComponent<Text>().text = "Time's up! ";
        }
        fadePanel.SetActive(false);
        deadPanel.SetActive(true);
        pauseButton.enabled = false;
        Time.timeScale = 0.3f;
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        ActivateScene();
    }
    public void ReturnToMenu()
    {
        SceneManager.LoadScene(1);
        Time.timeScale = 1f;
    }

    public void NextLevel()
    {
        buildIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(buildIndex + 1);
        ActivateScene();
    }

    public void EndPanel()
    {
        endPanel.SetActive(true);
        pauseButton.enabled = false;
        Time.timeScale = 0.3f;
    }
}
