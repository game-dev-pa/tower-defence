using System;
using System.Collections;
using Application.Services;
using UnityEngine;
using UnityEngine.UI;
using VContainer;

namespace UI.Views
{
    public sealed class UIManagerView : MonoBehaviour
    {
        [SerializeField] private Text _coinText;
        [SerializeField] private GameObject _winPopup;
        [SerializeField] private GameObject _losePopup;

        private IGameManagerService _gameManagerService;
        private Coroutine _flashRoutine;

        [Inject]
        public void Construct(IGameManagerService gameManagerService)
        {
            _gameManagerService = gameManagerService;

            _gameManagerService.OnCoinChanged += HandleCoinChanged;
            _gameManagerService.OnGameWin += ShowWinPopup;
            _gameManagerService.OnGameLose += ShowLosePopup;

            HandleCoinChanged(_gameManagerService.CurrentCoins);
            _winPopup.SetActive(false);
            _losePopup.SetActive(false);
        }

        public void FlashInsufficientFunds()
        {
            if (_flashRoutine != null)
                StopCoroutine(_flashRoutine);

            _flashRoutine = StartCoroutine(FlashTextRedCoroutine());
        }

        private void OnDestroy()
        {
            if (_gameManagerService == null) return;

            _gameManagerService.OnCoinChanged -= HandleCoinChanged;
            _gameManagerService.OnGameWin -= ShowWinPopup;
            _gameManagerService.OnGameLose -= ShowLosePopup;
        }

        private void HandleCoinChanged(int coins)
        {
            if (_coinText == null)
                throw new NullReferenceException("Coin text NOT assigned!!");

            _coinText.text = $"Coins: {coins}";
        }

        private void ShowWinPopup()
        {
            if (_winPopup == null)
                throw new NullReferenceException("WinPopup NOT assigned!!");

            _winPopup.SetActive(true);
        }

        private void ShowLosePopup()
        {
            if (_losePopup == null)
                throw new NullReferenceException("LosePopup NOT assigned!!");

            _losePopup.SetActive(true);
        }

        private IEnumerator FlashTextRedCoroutine()
        {
            _coinText.color = Color.red;
            yield return new WaitForSeconds(0.5f);
            _coinText.color = Color.black;
            _flashRoutine = null;
        }
    }
}