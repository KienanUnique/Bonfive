using UnityEngine;

namespace Assets.Script.InteractableItems.Firewood
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class FirewoodVisual : MonoBehaviour
    {
        [SerializeField] private string _pickedUpSortingLayer;
        private string _defaultSortingLayer;

        private SpriteRenderer _spriteRender;

        private void Awake()
        {
            _spriteRender = GetComponent<SpriteRenderer>();
            _defaultSortingLayer = _spriteRender.sortingLayerName;
        }

        public void ProcessPickUp()
        {
            ChangeSortingLayer(_pickedUpSortingLayer);
        }

        public void ProcessDropDown()
        {
            ChangeSortingLayer(_defaultSortingLayer);
        }

        private void ChangeSortingLayer(string newSortingLayerName)
        {
            _spriteRender.sortingLayerID = SortingLayer.NameToID(newSortingLayerName);
        }
    }
}