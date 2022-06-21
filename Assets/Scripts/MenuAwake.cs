using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuAwake : MonoBehaviour
{
    [SerializeField] private GameObject playButton;
    [SerializeField] private GameObject authorButton;
    [SerializeField] private GameObject mainPanel;
    [SerializeField] private GameObject levelPanel;
    [SerializeField] private GameObject authorPanel;
    [SerializeField] private GameObject hintPanel;
    [SerializeField] private GameObject levelScrollBox;
    [SerializeField] private Transform content;
    [SerializeField] private GameObject scrollBoxItemPrefab;
    [SerializeField] private Transform scrollBar;
    private int levelNum;
    private int[] levelTime;
    private float contentParentRectSize;
    private RectTransform contentParentRectTransform;
    private RectTransform contentRectTransform;
    private float contentDeltaSize;
    private float currentScrollBoxDeltaSize;
    private float scrollBoxItemDeltaSize;
    private RectTransform scrollBoxItemRectTransform;

    private void Awake()
    {
        levelNum = GlobalControl.Instance.level;
        levelTime = GlobalControl.Instance.levelTime;
        //levelNum = levelTime.Length;
        contentRectTransform = content.GetComponent<RectTransform>();
        scrollBoxItemRectTransform = scrollBoxItemPrefab.GetComponent<RectTransform>();
        contentDeltaSize = contentRectTransform.sizeDelta.y;
        scrollBoxItemDeltaSize = scrollBoxItemRectTransform.sizeDelta.y;
        contentParentRectTransform = content.parent.GetComponent<RectTransform>();
        contentParentRectSize = contentParentRectTransform.rect.height;

        FillTheBox();
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void PlayButton()
    {
        if (levelNum == 0)
        {
            SceneManager.LoadScene(2);
        }
        else
        {
            mainPanel.SetActive(false);
            levelPanel.SetActive(true);
            hintPanel.SetActive(false);
        }
    }

    public void AuthorButton()
    {
        authorPanel.SetActive(true);
        mainPanel.SetActive(false);
        hintPanel.SetActive(false);
    }

    public void HintButton()
    {
        authorPanel.SetActive(false);
        mainPanel.SetActive(false);
        hintPanel.SetActive(true);
    }

    public void BackToMenuButton()
    {
        authorPanel.SetActive(false);
        mainPanel.SetActive(true);
        levelPanel.SetActive(false);
        hintPanel.SetActive(false);
    }

    private void FillTheBox()
    {
        if (levelNum > 0)
        {
            if (levelNum > (SceneManager.sceneCountInBuildSettings - 2)) levelNum = (SceneManager.sceneCountInBuildSettings - 2);
            for (int i = 0; i < levelNum; ++i)
            {
                AddNewLevel(i);
            }
        }
        currentScrollBoxDeltaSize = scrollBoxItemDeltaSize*(levelNum + 1);

        if (currentScrollBoxDeltaSize > contentParentRectSize)
        {
            scrollBar.GetChild(0).gameObject.SetActive(true);
            contentRectTransform.sizeDelta = new Vector2(contentRectTransform.sizeDelta.x, currentScrollBoxDeltaSize);
        }
        else
        {
            scrollBar.GetChild(0).gameObject.SetActive(false);
        }
    }

    private void AddNewLevel(int i)
    {
        GameObject newScrollBoxItem = Instantiate(scrollBoxItemPrefab, new Vector3(0, 0, 0), Quaternion.identity);
        newScrollBoxItem.transform.SetParent(content, false);
        GameObject textObject = newScrollBoxItem.transform.Find("LevelText").gameObject;
        GameObject timeTextObject = newScrollBoxItem.transform.Find("TimeText").gameObject;
        Text textGO = textObject.transform.GetComponent<Text>();
        Text timeTextGO = timeTextObject.transform.GetComponent<Text>();
        textGO.text = "Level #" + (i + 1);

        LevelButtonScript levelButtonScript = newScrollBoxItem.GetComponent<LevelButtonScript>();
        levelButtonScript.SceneNum = (i+2);

        if (levelTime[i] > 0) { timeTextGO.text = "Best time: " + levelTime[i] + " s"; } else { timeTextGO.text = ""; }

        RectTransform rectTransform = newScrollBoxItem.GetComponent<RectTransform>();
        int childrenCount = content.childCount;
        if (childrenCount > 1)
        {
            newScrollBoxItem.transform.localPosition = new Vector3(newScrollBoxItem.transform.localPosition.x, newScrollBoxItem.transform.localPosition.y - rectTransform.sizeDelta.y * (childrenCount - 1), newScrollBoxItem.transform.localPosition.z);
        }
    }
}
