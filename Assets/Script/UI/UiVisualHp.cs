using TMPro;
using UnityEngine;

[RequireComponent(typeof(TMP_Text))]
public class UiVisualHp : MonoBehaviour
{
    private TMP_Text _textMeshPro;

    private void Awake() {
        _textMeshPro = GetComponent<TMP_Text>();
    }

    public void UpdateHp(int currentHp, int maxHp){
        _textMeshPro.text = $"{currentHp}/{maxHp}";
    }
}
