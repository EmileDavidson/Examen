using System;
using Runtime.Enums;
using Runtime.Managers;
using Runtime.Player;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Utilities.MethodExtensions;

namespace Runtime.UserInterfaces
{
    public class LevelEndScreenManager : MonoBehaviour
    {
        [SerializeField] private GameObject[] stars;
        [SerializeField] private TMP_Text moneySpentText;
        [SerializeField] private TMP_Text moneyEarnedText;
        [SerializeField] private TMP_Text totalPercentageText;
        [SerializeField] private TMP_Text customerHappinessText;

        [SerializeField] private Button restartButton;
        [SerializeField] private Button returnToMenuButton;

        private void OnEnable()
        {
            PlayerManager.Instance.PlayerInputs.ForEach(input =>
            {
                if (!input.TryGetComponent<PlayerInputEvents>(out var eventComp)) return;
                eventComp.onRightShoulder.AddListener(() => { restartButton.onClick.Invoke(); });

                eventComp.onLeftShoulder.AddListener(() => { returnToMenuButton.onClick.Invoke(); });
            });

            var percentage = LevelManager.Instance.GetTotalScorePercentage();
            var rating = LevelManager.Instance.GetStarRating();
            var happinessMaxScore = LevelManager.Instance.GetMaxScore(ScoreType.CustomerHappiness) +
                                    Math.Abs(LevelManager.Instance.GetMinScore(ScoreType.CustomerHappiness));
            var happinessScore = LevelManager.Instance.GetScore(ScoreType.CustomerHappiness) +
                                 Math.Abs(LevelManager.Instance.GetMinScore(ScoreType.CustomerHappiness));

            moneyEarnedText.text = $"${LevelManager.Instance.MoneyEarned},-";
            moneySpentText.text = $"${LevelManager.Instance.MoneySpent},-";
            totalPercentageText.text = $"{percentage}%";
            customerHappinessText.text = $"{happinessScore}/{happinessMaxScore}";

            foreach (var star in stars)
            {
                star.SetActive(false);
            }

            for (int i = 0; i < rating; i++)
            {
                if (!stars.ContainsSlot(i)) return;
                stars[i].SetActive(true);
            }
        }

        public void GotoMainScreen()
        {
            SceneManager.LoadScene("MainMenu");
        }

        public void RestartLevel()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}