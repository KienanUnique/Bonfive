using TMPro;
using UnityEngine;

[RequireComponent(typeof(TMP_Text))]
public class UiVisualSouls : MonoBehaviour
{
    private TMP_Text _textMeshPro;

    private void Awake()
    {
        _textMeshPro = GetComponent<TMP_Text>();
    }

    public void UpdateSouls(int currentSouls, int maxSouls)
    {
        _textMeshPro.text = $"{currentSouls}/{maxSouls}";
    }
}
