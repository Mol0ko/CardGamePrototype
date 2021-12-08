namespace Cards
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class PlayerHand : MonoBehaviour
    {
        [SerializeField]
        private Transform[] _cardParents;

        private Card[] _cards = new Card[8];

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

            var startRotation = card.transform.rotation;
            var eulerAngles = card.transform.eulerAngles;
            var endRotation = Quaternion.Euler(eulerAngles.x, eulerAngles.y, eulerAngles.z + 180);

            while (time < 1f)
            {
                card.transform.position = Vector3.Lerp(startPos, endPos, time);
                // card.transform.rotation = Quaternion.Slerp(startRotation, endRotation, time);
                time += Time.deltaTime;
                yield return null;
            }
            card.transform.Rotate(0, 0, 180);
        }
    }
}