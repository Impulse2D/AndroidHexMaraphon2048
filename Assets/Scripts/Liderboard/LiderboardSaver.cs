using UnityEngine;
using YG;

namespace Liderboard
{
    public class LiderboardSaver : MonoBehaviour
    {
        private const string NameLiderBoard = "Score";

        public void AddNewLeaderboardScores(int recordValue)
        {
            if (YandexGame.auth == true)
            {
                YandexGame.NewLeaderboardScores(NameLiderBoard, recordValue);
            }
        }

        public void ResetResultLiderboard()
        {
            int minValueRecord = 0;

            AddNewLeaderboardScores(minValueRecord);
        }
    }
}
