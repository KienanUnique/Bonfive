using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class RunesVisual : MonoBehaviour
{
    [SerializeField] private List<SpriteRenderer> _runes;
    [SerializeField] private float _glowEffectLerpSpeed;
    [SerializeField] private Color _nonGlowRuneColor = new Color(1, 1, 1, 0);
    [SerializeField] private Color _glowRuneColor = new Color(1, 1, 1, 1);
    private bool _isAnimationRunning = false;

    public void StartRuneGlowAnimation()
    {
        if (!_isAnimationRunning)
        {
            StartCoroutine(RuneGlowAnimation());
        }
    }

    private IEnumerator RuneGlowAnimation()
    {
        _isAnimationRunning = true;
        int currentAnimationPhase = 0;
        Color currentColor = _nonGlowRuneColor;
        Color targetColor = _glowRuneColor;
        while (currentAnimationPhase != 2)
        {
            currentColor = Color.Lerp(currentColor, targetColor, _glowEffectLerpSpeed * Time.deltaTime);
            ApplyColorToAllRunes(currentColor);
            if (currentColor == _glowRuneColor && currentAnimationPhase == 0)
            {
                targetColor = _nonGlowRuneColor;
                currentAnimationPhase = 1;
            }
            else if (currentColor == _nonGlowRuneColor && currentAnimationPhase == 1)
            {
                targetColor = _glowRuneColor;
                currentAnimationPhase = 2;
            }
            yield return null;
        }
        _isAnimationRunning = false;
    }

    private void ApplyColorToAllRunes(Color newColor)
    {
        foreach (var rune in _runes)
        {
            rune.color = newColor;
        }
    }

}
