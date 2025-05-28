using Services;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using YG;

namespace UI
{
    public class AwardsView : MonoBehaviour
    {
        private PauseService _pauseService;
        private Button _openAwardPanelButton;
        private Button _closeAwardPanelButton;
        private GameObject _awardsPanel;

        private TextMeshProUGUI _award2048;
        private TextMeshProUGUI _award4096;
        private TextMeshProUGUI _award8192;
        private TextMeshProUGUI _award16384;
        private TextMeshProUGUI _award32768;
        private TextMeshProUGUI _award65536;

        private Image _imageComplete2048;
        private Image _imageComplete4096;
        private Image _imageComplete8192;
        private Image _imageComplete16384;
        private Image _imageComplete32768;
        private Image _imageComplete65536;

        private void OnDisable()
        {
            _openAwardPanelButton.onClick.RemoveListener(Show);
            _closeAwardPanelButton.onClick.RemoveListener(Hide);
        }

        public void Construct(PauseService pauseService,
            Button openAwardPanelButton,
            Button closeAwardPanelButton,
            GameObject awardsPanel,
            TextMeshProUGUI award2048,
            TextMeshProUGUI award4096,
            TextMeshProUGUI award8192,
            TextMeshProUGUI award16384,
            TextMeshProUGUI award32768,
            TextMeshProUGUI award65536,
            Image imageComplete2048,
            Image imageComplete4096,
            Image imageComplete8192,
            Image imageComplete16384,
            Image imageComplete32768,
            Image imageComplete65536)
        {
            _pauseService = pauseService;
            _openAwardPanelButton = openAwardPanelButton;
            _closeAwardPanelButton = closeAwardPanelButton;
            _awardsPanel = awardsPanel;
            _award2048 = award2048;
            _award4096 = award4096;
            _award8192 = award8192;
            _award16384 = award16384;
            _award32768 = award32768;
            _award65536 = award65536;
            _imageComplete2048 = imageComplete2048;
            _imageComplete4096 = imageComplete4096;
            _imageComplete8192 = imageComplete8192;
            _imageComplete16384 = imageComplete16384;
            _imageComplete32768 = imageComplete32768;
            _imageComplete65536 = imageComplete65536;

            _openAwardPanelButton.onClick.AddListener(Show);
            _closeAwardPanelButton.onClick.AddListener(Hide);
        }

        private void Show()
        {
            _pauseService.EnablePause();
            _awardsPanel.gameObject.SetActive(true);

            SetTextAward(_award2048, YandexGame.savesData.award2048, _imageComplete2048);
            SetTextAward(_award4096, YandexGame.savesData.award4096, _imageComplete4096);
            SetTextAward(_award8192, YandexGame.savesData.award8192, _imageComplete8192);
            SetTextAward(_award16384, YandexGame.savesData.award16384, _imageComplete16384);
            SetTextAward(_award32768, YandexGame.savesData.award32768, _imageComplete32768);
            SetTextAward(_award65536, YandexGame.savesData.award65536, _imageComplete65536);
        }

        private void Hide()
        {
            _pauseService.DisablePause();
            _awardsPanel.gameObject.SetActive(false);
        }

        private void SetTextAward(TextMeshProUGUI textAward, int quantyAwardsScore, Image imageAwardComlete)
        {
            int maxQuantyAwardsScore = 1000000;

            if (quantyAwardsScore >= maxQuantyAwardsScore)
            {
                textAward.text = 123456.ToString();
                imageAwardComlete.gameObject.SetActive(true);
            }
            else
            {
                textAward.text = quantyAwardsScore.ToString();
            }
        }
    }
}
