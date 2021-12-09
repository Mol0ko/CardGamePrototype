namespace Cards
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class PlayerHand : MonoBehaviour
    {
        [SerializeField]
        private Transform[] _cardParents;
        [SerializeField]
        private PlayerBattleField _battleField;
        [SerializeField]
        private GameManager _gameManager;

        private Card[] _cards = new Card[8];

        private bool _activePlayer = false;

        public void SetActivePlayer(bool value) => _activePlayer = value;

        public void AddCardsFromDeck(IEnumerable<Card> cards)
            => StartCoroutine(AddCardsFromDeckRoutine(cards));

        private IEnumerator AddCardsFromDeckRoutine(IEnumerable<Card> cards)
        {
            var newCards = new Queue<Card>(cards);
            for (int i = 0; i < _cards.Length; i++)
            {
                if (_cards[i] == null)
                {
                    var card = newCards.Dequeue();
                    _cards[i] = card;
                    card.OnClick += OnCardClick;
                    card.PassedToHand();
                    yield return new WaitForSeconds(0.1f);
                    StartCoroutine(MoveCardToHand(card, _cardParents[i]));
                }
            }
        }

        private IEnumerator MoveCardToHand(Card card, Transform target)
        {
            var time = 0f;
            var startPos = card.transform.position;
            var endPos = target.position;

            while (time < 1f)
            {
                card.transform.position = Vector3.Lerp(startPos, endPos, time);
                time += Time.deltaTime;
                yield return null;
            }
            card.transform.Rotate(0, 0, 180);
            card.SetInteractable(true);
        }

        private void OnCardClick(Card card) {
            _battleField.AddCardFromHand(card);
            card.OnClick -= OnCardClick;
            _gameManager.EndStep();
        }
    }
}