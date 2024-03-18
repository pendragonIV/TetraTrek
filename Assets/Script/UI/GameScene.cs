using DG.Tweening;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameScene : MonoBehaviour
{
    [SerializeField]
    private Transform overlayPanel;
    [SerializeField]
    private Transform winPanel;
    [SerializeField]
    private Transform losePanel;    
    [SerializeField]
    private Button replayButton;
    [SerializeField]
    private Button homeButton;
    [SerializeField]
    private TMP_Text _promotionText;

    private string[] _text = new string[]
    {
        "Good Job!",
        "Well Done!",
        "Great!",
        "Awesome!",
        "Amazing!",
        "Excellent!",
        "Fantastic!",
        "Incredible!",
        "Unbelievable!",
        "Outstanding!",
        "Impressive!",
        "Extraordinary!",
        "Marvelous!",
        "Magnificent!",
        "Brilliant!",
        "Fabulous!",
        "Mind-blowing!",
        "You got this!",
        "You're a star!",
    };

    public void ShowWinPanel()
    {
        overlayPanel.gameObject.SetActive(true);
        winPanel.gameObject.SetActive(true);
        FadeIn(overlayPanel.GetComponent<CanvasGroup>(), winPanel.GetComponent<RectTransform>());
        homeButton.interactable = false;
        replayButton.interactable = false;
        _promotionText.text = _text[Random.Range(0, _text.Length)];
    }

    public void ShowLosePanel()
    {
        overlayPanel.gameObject.SetActive(true);
        losePanel.gameObject.SetActive(true);
        FadeIn(overlayPanel.GetComponent<CanvasGroup>(), losePanel.GetComponent<RectTransform>());
        homeButton.interactable = false;
        replayButton.interactable = false;
    }

    private void FadeIn(CanvasGroup canvasGroup, RectTransform rectTransform)
    {
        canvasGroup.alpha = 0f;
        canvasGroup.DOFade(1, .3f).SetUpdate(true);

        rectTransform.anchoredPosition = new Vector3(0, 500, 0);
        rectTransform.DOAnchorPos(new Vector2(0, 0), .3f, false).SetEase(Ease.OutQuint).SetUpdate(true);
    }
}
