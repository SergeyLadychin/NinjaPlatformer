using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class LevelAnnouncement : MonoBehaviour
{
    private bool showNextAnnouncement = true;
    private int currentAnnouncementIndex = -1;

    public CallMenu callMenuObject;
    public Text textField;
    [Tooltip("Allow interupt with Escape button.")]
    public bool allowInteruption;
    public bool deactivateInTheEnd;
    public AnnouncementParameters[] announcements;

    [Tooltip("Event that will be called after all announcements.")]
    public string eventName;

    void Update()
    {
        if (allowInteruption && Input.GetKeyDown("escape"))
        {
            StopAllCoroutines();

            CloseAnnouncement();
        }

        if (showNextAnnouncement)
        {
            currentAnnouncementIndex++;
            showNextAnnouncement = false;
            if (currentAnnouncementIndex < announcements.Length)
            {
                textField.text = announcements[currentAnnouncementIndex].text;
                StartCoroutine("FadeIn");
            }
            else
            {
                CloseAnnouncement();
            }
        }
    }

    private void CloseAnnouncement()
    {
        EventManager.TriggerEvent(eventName);
        if (deactivateInTheEnd)
            this.gameObject.SetActive(false);
        callMenuObject.enabled = true;
    }

    private IEnumerator FadeIn()
    {
        var time = 0.0f;
        while (true)
        {
            if (Math.Abs(textField.color.a - 1.0f) > Mathf.Epsilon)
            {
                UpdateAlpha(announcements[currentAnnouncementIndex].fadeInCurve, time);
                time += Time.deltaTime;
            }
            else
            {
                StartCoroutine(Show());
                StopCoroutine("FadeIn");
            }
            yield return null;
        }
    }

    private IEnumerator Show()
    {
        yield return new WaitForSeconds(announcements[currentAnnouncementIndex].showTime);
        StartCoroutine("FadeOut");
    }

    private IEnumerator FadeOut()
    {
        var time = 0.0f;
        while (true)
        {
            if (textField.color.a > Mathf.Epsilon)
            {
                UpdateAlpha(announcements[currentAnnouncementIndex].fadeOutCurve, time);
                time += Time.deltaTime;
            }
            else
            {
                StopCoroutine("FadeOut");
                showNextAnnouncement = true;
            }
            yield return null;
        }
    }

    private void UpdateAlpha(AnimationCurve curve, float time)
    {
        var fieldColor = textField.color;
        fieldColor.a = curve.Evaluate(time);
        textField.color = fieldColor;
    }
}

[Serializable]
public class AnnouncementParameters
{
    [TextArea]
    public string text;
    public AnimationCurve fadeInCurve;
    public float showTime;
    public AnimationCurve fadeOutCurve;
}
