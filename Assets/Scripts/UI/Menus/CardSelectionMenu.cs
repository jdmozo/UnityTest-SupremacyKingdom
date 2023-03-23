using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace SupremacyKingdom
{
    public class CardSelectionMenu : MonoBehaviour
    {
        public static event Action<bool> OnCardsSelected;

        [Header("Components")]
        [SerializeField] private TMP_Text _selectableText;
        [SerializeField] private Button _nextStageButton;

        [Header("Card Info")]
        [SerializeField] private GameObject _cardPrefab;
        [SerializeField] private int _cardQuantity;
        [SerializeField] private int _cardSelectablesQuantity = 3;
        [SerializeField] private Transform _cardContainer;
        [SerializeField] private CardInfo[] _cardsInfo;


        private void OnEnable()
        {
            _selectableText.text = $"You have <b><color=\"red\">{_cardSelectablesQuantity}</color></b> left";
            _nextStageButton.interactable = false;
        }

        private void Start()
        {
            StartCoroutine(GenerateCards());
            _nextStageButton.onClick.AddListener(delegate {

                CloseMenu();
                });
        }

        private IEnumerator GenerateCards()
        {
            for (int i = 0; i < _cardContainer.childCount; i++)
                Destroy( _cardContainer.GetChild(i).gameObject);

            yield return null;

            for (int i = 0; i < _cardQuantity; i++)
            {
                CardInfo currentInfo = _cardsInfo[i];
                GameObject newCard = Instantiate(_cardPrefab, _cardContainer);
                newCard.GetComponent<CardPrefabManager>().SetData(this, currentInfo.birdName, currentInfo.birdImage, currentInfo.cardColor);
            }
        }

        public void UpdateCardsSelected(int quantity)
        {
            _cardSelectablesQuantity += quantity;
            _selectableText.text = $"You have <b><color=\"red\">{_cardSelectablesQuantity}</color></b> left";

            if (_cardSelectablesQuantity == 0)
            {
                OnCardsSelected?.Invoke(false);
                _nextStageButton.interactable= true;
            }
            else
            {
                OnCardsSelected?.Invoke(true);
                _nextStageButton.interactable = false;
            }
        }

        private void CloseMenu() => gameObject.SetActive(false);
    }
}
