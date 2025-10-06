using UnityEngine;
using System.Collections;

public class SpriteBlinkUtil 
{
    public static IEnumerator Blinking(SpriteRenderer spriteRenderer, float totalTime = 3f, float interval = 0.2f)
    {
        bool isVisible = true;
        Color oldColor = spriteRenderer.color;
        float t = 0;
        while (t<totalTime)
        {
            // 每隔 `interval` 秒切换 sprite 的可见状态
            oldColor.a = isVisible? 1f : 0.4f;
            spriteRenderer.color = oldColor;
            isVisible = !isVisible;
            yield return new WaitForSeconds(interval);
            t += interval;
        }
    }
    public static IEnumerator AcceleratedBlinking(SpriteRenderer spriteRenderer, float totalTime = 3f, float interval = 0.4f)
    {
        bool isVisible = true;
        Color oldColor = spriteRenderer.color;
        float t = 0;
        while (t<totalTime)
        {
            // 每隔 `interval` 秒切换 sprite 的可见状态
            oldColor.a = isVisible? 1f : 0.4f;
            spriteRenderer.color = oldColor;
            isVisible = !isVisible;
            float currentInterval = Mathf.Lerp(interval, 0.05f, t/totalTime);
            yield return new WaitForSeconds(currentInterval);
            t += currentInterval;
        }
    }
}
