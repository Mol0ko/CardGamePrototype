namespace Cards
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class PlayerBattleField : MonoBehaviour
    {
        [SerializeField]
        private Transform _parent;
        [SerializeField]
        private GameManager _gameManager;

        private List<Card> _cards = new List<Card>();

        public void AddCardFromHand(Card card)
        {
            _cards.Add(card);
            card.PassedToBattle();
            card.OnAttack += OnAttackByCard;
            card.OnDestroyCard += OnDestroyCard;
            UpdatePositions();
        }

        private void UpdatePositions()
        {
            var startX = _parent.position.x;
            var index = 0;
            foreach (var card in _cards)
            {
                var xPosition = startX - 4f * index + 2 * (_cards.Count - 1);
                card.transform.position = new Vector3(xPosition, card.transform.position.y, _parent.position.z);
                index++;
            }
        }

        private void OnAttackByCard(Card card) => StartCoroutine(EndTurnAfterDelay());

        private IEnumerator EndTurnAfterDelay()
        {
            yield return new WaitForSeconds(0.5f);
            _gameManager.EndTurn();
        }

        private void OnDestroyCard(Card card)
        {
            _cards.Remove(card);
            UpdatePositions();
        }
    }
}