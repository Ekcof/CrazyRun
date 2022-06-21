using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinTriggerScript : MonoBehaviour
{
    [SerializeField] private GameObject UISystem;
    [SerializeField] private bool isLastLevel;
    private AudioSource audioSource;
    private AudioSource audioSourceCamera;
    private LevelManager levelManager;
    private TouchControl playerInput;
    private IEnumerator coroutine;

    private void Awake()
    {
        levelManager = UISystem.GetComponent<LevelManager>();
        audioSource = GetComponent<AudioSource>();
        audioSourceCamera = Camera.main.GetComponent<AudioSource>();
        if (GlobalControl.Instance.level == 0) ++GlobalControl.Instance.level;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {

            if (audioSource != null) audioSource.Play();
            if (audioSourceCamera != null) audioSourceCamera.Stop();
            playerInput = Camera.main.GetComponent<TouchControl>();
            playerInput.enabled = false;

            if (!isLastLevel)
            {
                coroutine = NextLevelDelay(2.0f);
                StartCoroutine(coroutine);
                levelManager.WinMenu();
            }
            else
            {
                levelManager.EndPanel();
            }

            int buildIndex = SceneManager.GetActiveScene().buildIndex - 2;
            int level = GlobalControl.Instance.level;
            int maxTime = GlobalControl.Instance.levelTime[buildIndex];
            int currentTime = levelManager.GetMaxTime() - levelManager.GetMinTime();

            if (currentTime < maxTime || maxTime==0)
            {
                RatingSum(buildIndex, currentTime);
            }

            if (level <= (buildIndex + 1))
            {
                ++GlobalControl.Instance.level;
            }
        }
    }

    IEnumerator NextLevelDelay(float waitFor)
    {
        yield return new WaitForSeconds(waitFor);
        levelManager.NextLevel();
    }

    public void RatingSum(int buildIndex, int rating)
    {
        GlobalControl.Instance.levelTime[buildIndex] = rating;
    }

}
