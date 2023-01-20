using UnityEngine;

namespace Assets.Script.InteractableItems.Firewood
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class FirewoodVisual : MonoBehaviour
    {
        [SerializeField] private Sprite[] _woodSprites;
        [SerializeField] private string _pickedUpSortingLayer;
        private string _defaultSortingLayer;
        private Sprite RandomSprite => _woodSprites[Random.Range(0, _woodSprites.Length)];
        private SpriteRenderer _spriteRender;

        private void Awake()
        {
            _spriteRender = GetComponent<SpriteRenderer>();
            _defaultSortingLayer = _spriteRender.sortingLayerName;
        }

        private void Start()
        {
            _spriteRender.sprite = RandomSprite;
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