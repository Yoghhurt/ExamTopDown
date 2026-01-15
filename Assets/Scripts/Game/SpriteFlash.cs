using System.Collections;
using UnityEngine;

public class SpriteFlash : MonoBehaviour
{
    private SpriteRenderer _spriteRenderer;

    private void Awake()
    {
        _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }

    public void StartFlash(float flashDuration, Color flashColor, int numberOfFlashes)
    {
        StartCoroutine(FlashCoroutine(flashDuration, flashColor, numberOfFlashes));
    }
    public IEnumerator FlashCoroutine(float flashDuration, Color flashColor, int numberOfFlashes)
    {
        Color startColor = _spriteRenderer.color;
        float elapsedTime = 0;
        float elapsedPercentage = 0;

        while (elapsedTime < flashDuration)
        {
            elapsedTime += Time.deltaTime;
            elapsedPercentage = elapsedTime / flashDuration;

            if (elapsedPercentage > 1)
            {
                elapsedPercentage = 1;
            }
            
            float pingpongPercentage = Mathf.PingPong(elapsedPercentage * 2 *numberOfFlashes, 1);
            _spriteRenderer.color = Color.Lerp(startColor, flashColor, pingpongPercentage);
            
            yield return null;
        }
        _spriteRenderer.color = startColor;
    }
}
