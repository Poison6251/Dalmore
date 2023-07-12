using UnityEngine;

namespace Item
{
    [CreateAssetMenu(menuName ="ItemData/DrinkItemData")]
    public class DrinkItemData : CountableItemData
    {
        ///<summary>술 한 병의 양(ml), 범위는 0ml ~ 10,000ml(ml)</summary>
        [Header("술 한 병의 양(ml), 범위는 0ml ~ 10,000ml")]
        [Range(0f, 10000f), SerializeField] private float max;
        public float ml;

        private void Awake()
        {
            ml = max;
        }
    }

}
