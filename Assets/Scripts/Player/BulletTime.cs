using UnityEngine;
using UnityEngine.UI;

public class BulletTime : MonoBehaviour
{
    public float bulletTimeScale;
    public float duration;
    public Slider bulletTimeSlider;
    private float bulletTimeDelay;

    // Cached currentTime to avoid calling Time.time multiple times
    private void Update()
    {
        if (Time.timeScale != 0)
        {
            float currentTime = Time.time;

            if (Input.GetButton("BulletTime") && duration > 0 && bulletTimeDelay < currentTime)
            {
                StartBulletTime();
            }

            if (Input.GetButtonUp("BulletTime") && bulletTimeDelay < currentTime)
            {
                ResetTimeScale();
                bulletTimeDelay = currentTime + 1f;
                InvokeRepeating(nameof(ResetBulletTime), 3, Time.unscaledDeltaTime);
            }

            bulletTimeSlider.value = duration;
        }
    }

    private void FixedUpdate()
    {
        if (duration <= 0)
        {
            ResetTimeScale();
            InvokeRepeating(nameof(ResetBulletTime), 3, 0.033f);
        }
    }

    // Replaced deltaTime with unscaledDeltaTime
    private void StartBulletTime()
    {
        CancelInvoke(nameof(ResetBulletTime));
        Time.timeScale = bulletTimeScale;
        duration -= Time.unscaledDeltaTime * 10;
    }

    private void ResetBulletTime()
    {
        duration = Mathf.Lerp(duration, bulletTimeSlider.maxValue, Time.deltaTime);

        if (duration >= bulletTimeSlider.maxValue * 0.99f)
        {
            duration = bulletTimeSlider.maxValue;
            CancelInvoke(nameof(ResetBulletTime));
        }
    }

    // Seperate method to reset time scale to avoid clutter
    private void ResetTimeScale()
    {
        Time.timeScale = 1;
    }
}
