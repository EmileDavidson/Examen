using Runtime.Enums;
using Runtime.Managers;
using UnityEngine;

public class HappinessMeterRenderer : MonoBehaviour
{
    [SerializeField] private RectTransform slider;
    [SerializeField] private float maxHeight;
    [SerializeField] private float minHeight;
    private LevelManager _levelManager;

    private void Awake()
    {
        _levelManager = LevelManager.Instance;
        _levelManager.onScoreChange.AddListener(UpdateScoreSlider);
    }

    private void UpdateScoreSlider()
    {
        float scorePercentage = (float)_levelManager.GetScorePercentage(ScoreType.CustomerHappiness) / 100;
        float sliderHeight = (maxHeight + Mathf.Abs(minHeight)) * scorePercentage - Mathf.Abs(minHeight);

        slider.anchoredPosition = new Vector2(slider.anchoredPosition.x, sliderHeight);
    }
}
