﻿using System;
using System.Collections.Generic;
using AmplifyShaderEditor;
using Runtime.Managers;
using TMPro;
using Toolbox.MethodExtensions;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;
using Image = UnityEngine.UI.Image;

namespace Runtime.UserInterfaces
{
    public class LevelEndScreenManager : MonoBehaviour
    {
        [SerializeField] private List<Image> stars;
        [SerializeField] private TMP_Text scoreText;
        [SerializeField] private TMP_Text successText;
        
        private void OnEnable()
        {
            var percentage = LevelManager.Instance.GetScorePercentage();
            var rating = LevelManager.Instance.GetStarRating();
            
            scoreText.text = $"{percentage}%";
            successText.text = IsWin() ? "Success!" : "Failed!";
            for (int i = 0; i < rating; i++)
            {
                if (!stars.ContainsSlot(i)) return;
                stars[i].color = Color.green;
            }
        }

        public void GotoMainScreen()
        {
            SceneManager.LoadScene("MainMenu");
        }

        private bool IsWin()
        {
            return LevelManager.Instance.GetStarRating() >= 1;
        }
    }
}