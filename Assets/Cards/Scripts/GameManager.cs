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
        private Transform[] _player1HandParents;
        [SerializeField]
        private Transform[] _player2HandParents;
        [SerializeField]
        private GameObject _cardPrefab;

        private CardController[] _player1Cards;
        private CardController[] _player2Cards;

        private Material _cardAvatarBaseMaterial;

        private void Awake()
        {
            CreateCardAvatarBaseMaterial();
            CreatePlayerDecks();
        }

        private void CreateCardAvatarBaseMaterial()
        {
            var shader = Shader.Find("TextMeshPro/Sprite");
            _cardAvatarBaseMaterial = new Material(shader);
            _cardAvatarBaseMaterial.renderQueue = 2995;
        }

        private void CreatePlayerDecks()
        {
            var random = new System.Random();

            CardController[] CreatePlayerDeck(CardPackConfiguration[] packs, Transform deckParent)
            {
                IEnumerable<CardPropertiesData> cardData = new List<CardPropertiesData>();
                foreach (var pack in packs) cardData = pack.UnionProperties(cardData);

                var deckSize = Mathf.Min(_maxDeckSize, cardData.Count());
                var cards = new CardController[deckSize];
                var position = Vector3.zero;
                var index = 0;

                foreach (var data in cardData)
                {
                    position += new Vector3(0, 0.5f, 0);
                    var cardGameObject = Instantiate(_cardPrefab, deckParent);
                    cardGameObject.transform.localPosition = position;
                    cardGameObject.transform.localScale = Vector3.one;
                    var cardController = cardGameObject.GetComponent<CardController>();
                    cardController.PseudoConstructor(data, _cardAvatarBaseMaterial);
                    cards[index] = cardController;
                    index++;
                    if (index >= deckSize)
                        break;
                }
                random.Shuffle(cards);
                return cards;
            }
        
            _player1Cards = CreatePlayerDeck(_player1Packs, _player1DeckParent);
            _player2Cards = CreatePlayerDeck(_player2Packs, _player2DeckParent);
        }
    }
}