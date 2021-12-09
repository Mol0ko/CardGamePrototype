using System.Collections.Generic;
using System.Linq;
using Cards.ScriptableObjects;
using UnityEngine;

namespace Cards
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField, Range(10, 30), Space]
        private int _maxDeckSize = 20;

        [SerializeField, Header("Наборы карт, из которых формируются колоды игроков"), Space]
        private CardPackConfiguration[] _player1Packs;
        [SerializeField]
        private CardPackConfiguration[] _player2Packs;

        [SerializeField, Space]
        private Transform _player1DeckParent;
        [SerializeField]
        private Transform _player2DeckParent;
        [SerializeField]
        private PlayerHand _player1Hand;
        [SerializeField]
        private PlayerHand _player2Hand;
        [SerializeField]
        private GameObject _cardPrefab;

        private Card[] _player1DeckCards;
        private Card[] _player2DeckCards;

        private void Awake()
        {
            CreatePlayerDecks();
            PopulateHands();
        }

        private void CreatePlayerDecks()
        {
            var random = new System.Random();

            Card[] CreatePlayerDeck(CardPackConfiguration[] packs, Transform deckParent)
            {
                IEnumerable<CardPropertiesData> cardData = new List<CardPropertiesData>();
                foreach (var pack in packs) cardData = pack.UnionProperties(cardData);
                var cardDataArray = cardData.ToArray();
                random.Shuffle(cardDataArray);

                var deckSize = Mathf.Min(_maxDeckSize, cardDataArray.Length);
                var cards = new Card[deckSize];
                var position = Vector3.zero;
                var index = 0;

                foreach (var data in cardData)
                {
                    position += new Vector3(0, 0.5f, 0);
                    var cardGameObject = Instantiate(_cardPrefab, deckParent);
                    cardGameObject.transform.localPosition = position;
                    cardGameObject.transform.localScale = Vector3.one;
                    cardGameObject.transform.localRotation = Quaternion.Euler(0, 180, 0);
                    var cardController = cardGameObject.GetComponent<Card>();
                    cardController.PseudoConstructor(data);
                    cards[index] = cardController;
                    index++;
                    if (index >= deckSize)
                        break;
                }
                return cards.Reverse().ToArray();
            }

            _player1DeckCards = CreatePlayerDeck(_player1Packs, _player1DeckParent);
            _player2DeckCards = CreatePlayerDeck(_player2Packs, _player2DeckParent);
        }

        private void PopulateHands()
        {
            _player1Hand.AddCardsFromDeck(_player1DeckCards.Take(8));
            _player2Hand.AddCardsFromDeck(_player2DeckCards.Take(8));
        }
    }
}