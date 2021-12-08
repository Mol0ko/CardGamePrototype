
using TMPro;
using UnityEngine;

namespace Cards
{
    public class CardController : MonoBehaviour
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

        public void PseudoConstructor(CardPropertiesData data, Material avatarBaseMaterial)
        {
            avatarBaseMaterial.SetTexture(data.Name, data.Texture);
            _avatar.material = avatarBaseMaterial;
            _name.text = data.Name;
            _cost.text = data.Cost.ToString();
            _attack.text = data.Attack.ToString();
            _hp.text = data.Health.ToString();
            _type.text = data.Type.ToString();
            _description.text = CardUtility.GetDescriptionById(data.Id);
        }
    }
}