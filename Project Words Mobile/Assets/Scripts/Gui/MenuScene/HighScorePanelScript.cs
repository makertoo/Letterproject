using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static Assets.Helper.CommonUtils;

namespace Assets.Scripts.Gui.MenuScene
{
    public class HighScorePanelScript : MonoBehaviour
    {
        private Transform entryContainer;
        private Transform entryTemplate;
        private List<Transform> highscoreEntryTransformList;

        private void Awake()
        {
            entryContainer = transform.Find("highscoreEntryContainer");
            entryTemplate = entryContainer.Find("highscoreEntryTemplate");

            entryTemplate.gameObject.SetActive(false);
        }

        private void OnEnable()
        {
            LoadHighScore();
        }

        public void LoadHighScore()
        {
            List<HighScore> highScoreList = HighScoreManager.Instance.GetHighScore();
            highscoreEntryTransformList = new List<Transform>();
            foreach (var score in highScoreList)
            {
                CreateHighscoreEntryTransform(score, entryContainer, highscoreEntryTransformList);
            }
        }

        private void CreateHighscoreEntryTransform(HighScore highscoreEntry, Transform container, List<Transform> transformList)
        {
            float templateHeight = 31f;
            Transform entryTransform = Instantiate(entryTemplate, container);
            RectTransform entryRectTransform = entryTransform.GetComponent<RectTransform>();
            RectTransform backgroundPerLine = entryRectTransform.Find("background").GetComponent<RectTransform>();
            entryRectTransform.anchoredPosition = new Vector2(0, -templateHeight * transformList.Count);
            entryTransform.gameObject.SetActive(true);

            int rank = transformList.Count + 1;
            string rankString;
            switch (rank)
            {
                default:
                    rankString = rank + "TH"; break;

                case 1: rankString = "1ST"; break;
                case 2: rankString = "2ND"; break;
                case 3: rankString = "3RD"; break;
            }

            entryTransform.Find("posText").GetComponent<Text>().text = rankString;

            int score = highscoreEntry.Score;

            entryTransform.Find("scoreText").GetComponent<Text>().text = score.ToString();

            string category = highscoreEntry.Category;
            entryTransform.Find("categoryText").GetComponent<Text>().text = category;

            string date = highscoreEntry.HighScoreDate.ToString(HighScore.DATEPREFFORMAT);
            entryTransform.Find("dateText").GetComponent<Text>().text = date;

            // Set background visible odds and evens, easier to read
            entryTransform.Find("background").gameObject.SetActive(rank % 2 == 1);

            // Highlight First
            if (rank == 1)
            {
                entryTransform.Find("posText").GetComponent<Text>().color = Color.green;
                entryTransform.Find("scoreText").GetComponent<Text>().color = Color.green;
                entryTransform.Find("categoryText").GetComponent<Text>().color = Color.green;
                entryTransform.Find("dateText").GetComponent<Text>().color = Color.green;
            }

            // Set tropy
            switch (rank)
            {
                default:
                    entryTransform.Find("trophy").gameObject.SetActive(false);
                    break;
                case 1:
                    entryTransform.Find("trophy").GetComponent<Image>().color = GetColorFromString("FFD200");
                    break;
                case 2:
                    entryTransform.Find("trophy").GetComponent<Image>().color = GetColorFromString("C6C6C6");
                    break;
                case 3:
                    entryTransform.Find("trophy").GetComponent<Image>().color = GetColorFromString("B76F56");
                    break;
            }

            transformList.Add(entryTransform);
        }
    }
}
