using System.Linq;
using TMPro;
using UnityEngine;

namespace Cards
{
    public class HeroController : MonoBehaviour
    {
        [SerializeField]
        private MeshRenderer _avatar;
        [SerializeField]
        private TextMeshPro _name;
        [SerializeField]
        private TextMeshPro _hp;
        [SerializeField]
        private TextMeshPro _manaValue;
        [SerializeField]
        private TextAsset _heroesJsonFile;
        [SerializeField]
        private HeroClass _class;
        [SerializeField]
        private Material[] _avatarMaterials;

        private int _mana = 0;

        private void Awake()
        {
            SetupHero();
        }

        private void SetupHero()
        {
            var classesData = JsonUtility.FromJson<HeroClasses>(_heroesJsonFile.text);
            HeroData heroData;
            switch (_class)
            {
                case HeroClass.Hunter:
                    heroData = classesData.Hunter;
                    break;
                case HeroClass.Mage:
                    heroData = classesData.Mage;
                    break;
                case HeroClass.Priest:
                    heroData = classesData.Priest;
                    break;
                case HeroClass.Warrior:
                default:
                    heroData = classesData.Warrior;
                    break;
            }

            _avatar.material = _avatarMaterials.FirstOrDefault(material => material.name == heroData.AvatarMaterial);
            _name.text = heroData.Name;
            _hp.text = heroData.Hp.ToString();
            _manaValue.text = _mana + "/10";
        }

        public void IncrementMana() {
            _mana = Mathf.Min(10, _mana + 1);
            _manaValue.text = _mana + "/10";
        }

        public void DecrementMana() {
            _mana = Mathf.Max(0, _mana - 1);
            _manaValue.text = _mana + "/10";
        }
    }
}
