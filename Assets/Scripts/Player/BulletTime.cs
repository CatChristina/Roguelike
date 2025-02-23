using UnityEngine;
using UnityEngine.UI;

public class BulletTime : MonoBehaviour
{
    public float bulletTimeScale;
    public float duration;
    public Slider bulletTimeSlider;
    private float bulletTimeDelay;
    private AudioSource music;
    private GameController gameController;

    private void Start()
    {
        music = GameObject.FindGameObjectWithTag("GameController").GetComponent<AudioSource>();
        gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
    }

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
        if (duration <= 0 && !gameController.isPaused)
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
        music.pitch = Mathf.Clamp(Time.timeScale * 1.7f, 0.5f, 1);
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
        music.pitch = Time.timeScale;
    }
}
