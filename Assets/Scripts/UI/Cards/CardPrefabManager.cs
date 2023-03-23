using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace SupremacyKingdom
{
    public class CardPrefabManager : MonoBehaviour
    {
        [Header("Card Manager")]
        [SerializeField] private CardSelectionMenu _cardSelectionMenu;
        [Header("Card Info")]
        [SerializeField] private TMP_Text _titleCard;
        [SerializeField] private Image _birdImage;
        [SerializeField] private Image _colorCard;
        [SerializeField] private Button _selectButton;
        [SerializeField] private CanvasGroup _canvasGroup;
        [Header("Discard Card")]
        [SerializeField] private Button _discardButton;
        [SerializeField] private GameObject _discardScreen;

        private bool _selected;

        private void OnEnable() => CardSelectionMenu.OnCardsSelected += SetCardOff;

        private void OnDisable() => CardSelectionMenu.OnCardsSelected -= SetCardOff;

        private void Start()
        {
            _discardButton.onClick.AddListener(() => CardStatus(false));
            _selectButton.onClick.AddListener(() => CardStatus(true));
        }

        public void SetData(CardSelectionMenu cardSelectionMenu, string titleCard, Sprite birdSprite, Color colorCard)
        {
            _cardSelectionMenu = cardSelectionMenu;
            _birdImage.sprite = birdSprite;
            _titleCard.text = titleCard;
            _colorCard.color = colorCard;
        }

        private void CardStatus(bool status)
        {
            _selected = status;
            _discardScreen.SetActive(status);

            if (status)
                _cardSelectionMenu.UpdateCardsSelected(-1);
            else
                _cardSelectionMenu.UpdateCardsSelected(1);
        }

        private void SetCardOff(bool status)
        {
            if (!status && !_selected)
            {
                _canvasGroup.alpha = 0.2f;
                _canvasGroup.interactable = status;
                _canvasGroup.blocksRaycasts = status;
            }
            else if (!status && _selected)
            {
                _canvasGroup.alpha = 1f;
                _canvasGroup.interactable = !status;
                _canvasGroup.blocksRaycasts = !status;
            }
            else
            {
                _canvasGroup.alpha = 1f;
                _canvasGroup.interactable = status;
                _canvasGroup.blocksRaycasts = status;
            }
        }
    }
}
