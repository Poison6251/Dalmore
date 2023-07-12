using UnityEngine;

namespace Item
{
    [CreateAssetMenu(menuName ="ItemData/DrinkItemData")]
    public class DrinkItemData : CountableItemData
    {
        ///<summary>�� �� ���� ��(ml), ������ 0ml ~ 10,000ml(ml)</summary>
        [Header("�� �� ���� ��(ml), ������ 0ml ~ 10,000ml")]
        [Range(0f, 10000f), SerializeField] private float max;
        public float ml;

        private void Awake()
        {
            ml = max;
        }
    }

}
