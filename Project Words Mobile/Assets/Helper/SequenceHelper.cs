using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Helper
{
    public static class SequenceHelper
    {
        private static int successiveCompletedWordCountForBoost = 0;

        private static int successiveCompletedWordCountForBonus = 0;

        public static int CurrentSuccessiveCompletedWordCountForBoost()
        {
            return successiveCompletedWordCountForBoost;
        }

        public static int CurrentSuccessiveCompletedWordCountForBonus()
        {
            return successiveCompletedWordCountForBonus;
        }

        public static void IncreaseSuccessiveCompletedWordCount()
        {
            successiveCompletedWordCountForBoost++; successiveCompletedWordCountForBonus++;
        }

        public static void ResetSuccessiveCompletedWordCountAll()
        {
            successiveCompletedWordCountForBoost = 0;
            successiveCompletedWordCountForBonus = 0;
        }

        public static void ResetSuccessiveCompletedWordCountForBoost()
        {
            successiveCompletedWordCountForBoost = 0;
        }

        public static void ResetSuccessiveCompletedWordCountForBonus()
        {
            successiveCompletedWordCountForBonus = 0;
        }

    }
}
