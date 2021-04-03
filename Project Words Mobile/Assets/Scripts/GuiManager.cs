using System;
using UnityEngine;
using UnityEngine.UI;


namespace Assets.Scripts
{
    public class GuiManager : MonoBehaviour
    {
        //ScoreValueTextGui
        public static GameObject scoreValue;
        public static GameObject wordText;
        public static GameObject timeText;
        public static GameObject wordHistoryText;
        public static GameObject commentAnimationText;
        public static GameObject boostText;
        //public static GameObject wordCategory;
        public static Animation commentAnimation;
        public static Animator boostAnimator; //It is easy to use animator than animation clip to control speed.

        void Awake()
        {
            //Every awake: look for the objects
            scoreValue = GameObject.Find("ScoreValue");
            wordText = GameObject.Find("WordZone");
            timeText = GameObject.Find("TimeZone");
            wordHistoryText = GameObject.Find("WordHistoryZone");
            commentAnimationText = GameObject.Find("CommentAnimationText");
            commentAnimationText.SetActive(false);
            commentAnimation = commentAnimationText.GetComponent<Animation>();
            boostText = GameObject.Find("BoostText");
            boostAnimator = boostText.GetComponent<Animator>();
            boostText.SetActive(false);
        }

        public static int Score
        {
            get
            {
                return int.Parse(scoreValue.GetComponent<Text>().text);
            }
            set
            {
                scoreValue.GetComponent<Text>().text = value.ToString();
            }
        }

        public static string WordingText
        {
            get
            {
                return wordText.GetComponent<Text>().text;
            }
            set
            {
                wordText.GetComponent<Text>().text = value;
            }
        }

        //public static int WordCategory
        //{
        //    get
        //    {
        //        return int.Parse(wordCategory.GetComponent<Text>().text);
        //    }
        //    set
        //    {
        //        wordCategory.GetComponent<Text>().text = value.ToString();
        //    }
        //}

        public static string WordingHistoryText
        {
            get
            {
                return wordHistoryText.GetComponent<Text>().text;
            }
            set
            {
                wordHistoryText.GetComponent<Text>().text = value;
            }
        }

        public static string TimeText
        {
            get
            {
                return timeText.GetComponent<Text>().text;
            }
            set
            {
                timeText.GetComponent<Text>().text = value;
            }

        }

        public static void PlayMalusAnimation(string textOfMalus)
        {
            var textComponent = commentAnimationText.GetComponent<Text>();
            textComponent.color = Color.red;
            textComponent.text = textOfMalus;
            commentAnimationText.SetActive(true);
            commentAnimation.Play("MalusAnimation"); // The animation must be marked as legacy (inspector window after clicking on the animation asset => Debug mode)
        }
        public static void PlayBonusAnimation(string textOfMalus)
        {
            var textComponent = commentAnimationText.GetComponent<Text>();
            textComponent.color = Color.green;
            textComponent.text = textOfMalus;
            commentAnimationText.SetActive(true);
            commentAnimation.Play("BonusAnimation"); // The animation must be marked as legacy (inspector window after clicking on the animation asset => Debug mode)
        }

        public static void UpdateBoostText(string text, TimeSpan animationDuration)
        {
            boostText.GetComponent<Text>().text = text;
            boostText.SetActive(true);
            var speed = 1 / ((float)animationDuration.TotalSeconds);
            boostAnimator.SetFloat("runMultiplier", speed);
            //boostAnimator.speed = 1 / ((float)animationDuration.TotalSeconds);
            boostAnimator.Play("SimpleBoostAnimation", -1, 0);
        }

    }
}
