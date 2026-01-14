using System.Collections;
using UnityEngine;

public class SpriteFlash : MonoBehaviour
{
    private SpriteRenderer _spriteRenderer;

    private void Awake()
    {
        _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }

    public IEnumerator FlashCoroutine(float flashDuration, Color flashColor, int numberOfFlashes)
    {
        Color startColor = _spriteRenderer.color;
        float elaspedTime = 0;
        float elapsedPercentage = 0;

        while (elapsedPercentage < flashDuration)
        {
            elaspedTime += Time.deltaTime;
            elapsedPercentage = elaspedTime / flashDuration;

            if (elapsedPercentage > 1)
            {
                elapsedPercentage = 1;
            }
            
            float pingpongPercentage = Mathf.PingPong(elapsedPercentage * 2 *numberOfFlashes, 1);
            _spriteRenderer.color = Color.Lerp(startColor, flashColor, pingpongPercentage);
            
            yield return null;
        }
    }
}
