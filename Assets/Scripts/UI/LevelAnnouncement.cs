using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelAnnouncement : MonoBehaviour
{
    private bool showNextAnnouncement = true;
    private int currentAnnouncementIndex = -1;
    private float currentTime;

    public CallMenu callMenuObject;
    public Text textField;
    public bool deactivateInTheEnd;
    public AnnouncementParameters[] announcements;

    [Tooltip("Event that will be called after all announcements.")]
    public string eventName;

    void Update()
    {
        if (Input.GetKeyDown("escape"))
        {
            StopAllCoroutines();

            CloseAnnouncement();
        }

        if (showNextAnnouncement)
        {
            currentAnnouncementIndex++;
            showNextAnnouncement = false;
            currentTime = 0.0f;
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
        while (true)
        {
            if (Math.Abs(textField.color.a - 1.0f) > Mathf.Epsilon)
            {
                UpdateAlpha(announcements[currentAnnouncementIndex].fadeInCurve);
            }
            else
            {
                currentTime = 0.0f;
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
        while (true)
        {
            if (textField.color.a > Mathf.Epsilon)
            {
                UpdateAlpha(announcements[currentAnnouncementIndex].fadeOutCurve);
            }
            else
            {
                StopCoroutine("FadeOut");
                showNextAnnouncement = true;
            }
            yield return null;
        }
    }

    private void UpdateAlpha(AnimationCurve curve)
    {
        var fieldColor = textField.color;
        fieldColor.a = curve.Evaluate(currentTime);
        textField.color = fieldColor;
        currentTime += Time.deltaTime;
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
