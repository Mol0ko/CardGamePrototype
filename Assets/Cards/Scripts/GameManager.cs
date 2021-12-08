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
        private Transform _player1Deck;
        [SerializeField]
        private Transform _player2Deck;
        [SerializeField]
        private Transform[] _player1Hand;
        [SerializeField]
        private Transform[] _player2Hand;

        private Material _cardAvatarBaseMaterial;

        private void Awake()
        {
            CreateCardAvatarBaseMaterial();
            GeneratePlayerDecks();
        }

        private void CreateCardAvatarBaseMaterial()
        {
            var shader = Shader.Find("TextMeshPro/Sprite");
            _cardAvatarBaseMaterial = new Material(shader);
            _cardAvatarBaseMaterial.renderQueue = 2995;
        }

        private void GeneratePlayerDecks()
        {

        }
    }
}