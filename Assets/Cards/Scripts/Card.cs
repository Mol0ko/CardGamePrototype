
using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Cards
{
    public class Card : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler
    {
        [SerializeField]
        private MeshRenderer _avatar;
        [SerializeField]
        private TextMeshPro _name;
        [SerializeField]
        private TextMeshPro _cost;
        [SerializeField]
        private TextMeshPro _attack;
        [SerializeField]
        private TextMeshPro _hp;
        [SerializeField]
        private TextMeshPro _type;
        [SerializeField]
        private TextMeshPro _description;

        private ushort _hpValue;
        private CardState _state = CardState.Deck;
        private bool _interactable = false;

        public Action<Card> OnClick;

        public void PseudoConstructor(CardPropertiesData data)
        {
            var shader = Shader.Find("TextMeshPro/Sprite");
            var material = new Material(shader);
            material.renderQueue = 2995;
            material.mainTexture = data.Texture;
            _avatar.material = material;
            _name.text = data.Name;
            _cost.text = data.Cost.ToString();
            _attack.text = data.Attack.ToString();
            _hp.text = data.Health.ToString();
            _type.text = data.Type.ToString();
            _description.text = CardUtility.GetDescriptionById(data.Id);
            _hpValue = data.Health;
        }

        public void SetInteractable(bool value) => _interactable = value;

        public void PassedToHand() => _state = CardState.Hand;

        public void PassedToBattle() => _state = CardState.Battle;
        public void AddDamage(ushort damage)
        {
            if (_state == CardState.Battle)
            {
                _hpValue -= damage;
                StartCoroutine(AnimateDamage());
                if (_hpValue <= 0)
                {
                    _state = CardState.Beaten;
                }
            }
        }

        private IEnumerator AnimateDamage()
        {
            var time = 1f;
            var scaleDown = false;
            var scaleDiff = new Vector3(0.3f, 0.3f, 0.3f);

            while (time > 0)
            {
                transform.eulerAngles = scaleDown ? transform.eulerAngles - scaleDiff : transform.eulerAngles + scaleDiff;
                scaleDown = !scaleDown;
                time -= Time.deltaTime;
                yield return null;
            }
            if (_hpValue <= 0)
                Destroy(gameObject);
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            if (_interactable && _state == CardState.Hand)
                transform.localScale *= 1.15f;
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            if (_interactable && _state == CardState.Hand)
                transform.localScale /= 1.15f;
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            if (_interactable && _state == CardState.Hand)
            {
                transform.localScale /= 1.15f;
                OnClick?.Invoke(this);
            }
        }
    }
}