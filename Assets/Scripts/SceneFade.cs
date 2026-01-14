using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SceneFade : MonoBehaviour
{
    private Image _sceneFade;

    private void Awake()
    {
        _sceneFade = GetComponent<Image>();
    }

    public IEnumerator FadeInCoroutine(float duration)
    {
        Color startColor = new Color(_sceneFade.color.r, _sceneFade.color.g, _sceneFade.color.b, 1);
        Color targetColor = new Color(_sceneFade.color.r, _sceneFade.color.g, _sceneFade.color.b, 0);
        
        yield return FadeCoroutine(startColor, targetColor, duration);
        
        gameObject.SetActive(false);
    }

    public IEnumerator FadeOutCoroutine(float duration)
    {
        Color startColor = new Color(_sceneFade.color.r, _sceneFade.color.g, _sceneFade.color.b, 0);
        Color targetColor = new Color(_sceneFade.color.r, _sceneFade.color.g, _sceneFade.color.b, 1);
        
        gameObject.SetActive(true);
        yield return FadeCoroutine(startColor, targetColor, duration);
    }
    private IEnumerator FadeCoroutine(Color startColor, Color targetColor, float duration)
    {
        float elapsedTime = 0;
        float elapsedPercent = 0;

        while (elapsedPercent < 1)
        {
            elapsedPercent = elapsedTime / duration;
            _sceneFade.color = Color.Lerp(startColor, targetColor, elapsedPercent);
            
            yield return null;
            elapsedTime += Time.deltaTime;
        }
    }
}
