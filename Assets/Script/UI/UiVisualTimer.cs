using TMPro;
using UnityEngine;

[RequireComponent(typeof(TMP_Text))]
public class UiVisualTimer : MonoBehaviour
{
    private TMP_Text _textMeshPro;

    private void Awake()
    {
        _textMeshPro = GetComponent<TMP_Text>();
    }

    public void UpdateTimer(int minutes, int seconds)
    {
        _textMeshPro.text = string.Format("{00:00}:{01:00}", minutes, seconds); ;
    }
}