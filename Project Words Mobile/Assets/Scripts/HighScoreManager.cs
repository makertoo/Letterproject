using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts
{
    /// <summary>
    /// High score manager.
    /// Local highScore manager for LeaderboardLength number of entries
    /// 
    /// this is a singleton class.  to access these functions, use HighScoreManager._instance object.
    /// eg: HighScoreManager._instance.SaveHighScore("meh",1232);
    /// No need to attach this to any game object, thought it would create errors attaching.
    /// </summary>

    public class HighScoreManager : Singleton<HighScoreManager>
    {

        private const int LeaderboardLength = 10;
        public void Awake()
        {
            destroyOnLoad = false;
            base.Awake();
        }

        public void SaveHighScore(string category, int score)
        {
            List<HighScore> HighScores = new List<HighScore>();

            int i = 1;
            //LoadingExistingHighScore from prefs
            while (i <= LeaderboardLength && PlayerPrefs.HasKey(HighScore.HighScore_Class_Name + i + HighScore.SCORE_PROPERTY))
            {
                HighScore temp = HighScore.LoadHighScoreFromPref(i);
                HighScores.Add(temp);
                i++;
            }
            //Inserting HighScore into the existing list.
            if (HighScores.Count == 0)
            {
                HighScore _temp = new HighScore(score, category);
                HighScores.Add(_temp);
            }
            else //Insert and order the highscore list
            {
                for (i = 1; i <= HighScores.Count && i <= LeaderboardLength; i++)
                {
                    if (score > HighScores[i - 1].Score)
                    {
                        HighScore _temp = new HighScore(score, category);
                        HighScores.Insert(i - 1, _temp);
                        break;
                    }
                    if (i == HighScores.Count && i < LeaderboardLength)
                    {
                        HighScore _temp = new HighScore(score, category);
                        HighScores.Add(_temp);
                        break;
                    }
                }
            }

            i = 1;
            while (i <= LeaderboardLength && i <= HighScores.Count)
            {
                PlayerPrefs.SetString(HighScore.HighScore_Class_Name + i + HighScore.CATEGORY_PROPERTY, HighScores[i - 1].Category);
                PlayerPrefs.SetInt(HighScore.HighScore_Class_Name + i + HighScore.SCORE_PROPERTY, HighScores[i - 1].Score);
                PlayerPrefs.SetString(HighScore.HighScore_Class_Name + i + HighScore.HIGHSCORE_DATE_PROPERTY, HighScores[i - 1].HighScoreDate.ToString(HighScore.DATEPREFFORMAT));
                i++;
            }

        }

        public int GetScoreRankInHighScore(int score)
        {
            var scoreList = GetHighScore();
            var candidateHighscore = new HighScore(score, "temp");
            scoreList.Add(candidateHighscore);
            return scoreList.OrderByDescending(j => j.Score).ToList().IndexOf(candidateHighscore)+1;
        }

        public List<HighScore> GetHighScore()
        {
            List<HighScore> HighScores = new List<HighScore>();
            int i = 1;
            while (i <= LeaderboardLength && PlayerPrefs.HasKey(HighScore.HighScore_Class_Name + i + HighScore.SCORE_PROPERTY))
            {
                HighScore temp = HighScore.LoadHighScoreFromPref(i);
                HighScores.Add(temp);
                i++;
            }
            return HighScores;
        }

        public HighScore GetFirstRankHighScore()
        {
            return GetHighScore().FirstOrDefault();
        }

        public void ClearLeaderBoard()
        {
            //for(int i=0;i<HighScores.
            List<HighScore> HighScores = GetHighScore();

            for (int i = 1; i <= HighScores.Count; i++)
            {
                PlayerPrefs.DeleteKey("HighScore" + i + "name");
                PlayerPrefs.DeleteKey("HighScore" + i + "score");
            }
        }

        void OnApplicationQuit()
        {
            PlayerPrefs.Save();
        }


    }

    public class HighScore
    {
        public const string SCORE_PROPERTY = "Score";
        public const string CATEGORY_PROPERTY = "Category";
        public const string HIGHSCORE_DATE_PROPERTY = "HighScoreDate";
        public const string DATEPREFFORMAT = "dd/MM/yy";
        public const string HighScore_Class_Name = "HighScore";

        public int Score;
        public string Category;
        public DateTime HighScoreDate;


        private HighScore()
        {

        }

        public HighScore(int score, string category, DateTime? dateTime = null)
        {
            Score = score;
            Category = category;
            HighScoreDate = dateTime.HasValue ? dateTime.Value : DateTime.Now;
        }

        public static HighScore LoadHighScoreFromPref(int index)
        {
            HighScore temp = new HighScore();
            temp.Score = GetPropertyFromPrefList<int>(SCORE_PROPERTY, index);
            temp.Category = GetPropertyFromPrefList<string>(CATEGORY_PROPERTY, index);
            temp.HighScoreDate = GetPropertyFromPrefList<DateTime>(HIGHSCORE_DATE_PROPERTY, index);
            return temp;
        }

        public static T GetPropertyFromPrefList<T>(string attributeFromObjectClasse, int index) where T : IConvertible
        {
            var type = typeof(T);
            if (type == typeof(int))
            {
                return (T)(object)PlayerPrefs.GetInt(HighScore_Class_Name + index + attributeFromObjectClasse);
            }
            if (type == typeof(string))
            {
                return (T)(object)PlayerPrefs.GetString(HighScore_Class_Name + index + attributeFromObjectClasse);
            }
            if (type == typeof(DateTime))
            {
                DateTime dateTime;
                DateTime.TryParse(PlayerPrefs.GetString(HighScore_Class_Name + index + attributeFromObjectClasse), out dateTime);
                return (T)(object)dateTime;
            }

            throw new NotImplementedException();
        }
    }
}
