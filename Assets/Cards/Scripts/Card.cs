
using TMPro;
using UnityEngine;

namespace Cards
{
    public class Card : MonoBehaviour
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
        }
    }
}