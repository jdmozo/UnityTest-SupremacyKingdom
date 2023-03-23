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
        [SerializeField] private CardInfo _cardInfo;
        [Header("Discard Card")]
        [SerializeField] private Button _discardButton;
        [SerializeField] private GameObject _discardScreen;

        private bool _selected;

        private void OnEnable()
        {
            CardSelectionMenu.OnCardsSelected += SetCardOff;
        }

        private void OnDisable()
        {
            CardSelectionMenu.OnCardsSelected -= SetCardOff;
        }

        private void Start()
        {
            _discardButton.onClick.AddListener(() => CardStatus(false));
            _selectButton.onClick.AddListener(() => CardStatus(true));
        }

        public void SetData(CardSelectionMenu cardSelectionMenu, string titleCard, Sprite birdSprite, Color colorCard, CardInfo cardInfo)
        {
            _cardSelectionMenu = cardSelectionMenu;
            _birdImage.sprite = birdSprite;
            _titleCard.text = titleCard;
            _colorCard.color = colorCard;
            _cardInfo = cardInfo;
        }

        private void CardStatus(bool status)
        {
            _selected = status;
            _discardScreen.SetActive(status);
            _cardSelectionMenu.ChangeCard(status, _cardInfo);

            if (status)
                _cardSelectionMenu.UpdateCardsSelected(-1, _cardInfo);
            else
                _cardSelectionMenu.UpdateCardsSelected(1, _cardInfo);
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
