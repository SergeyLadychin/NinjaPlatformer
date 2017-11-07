using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreditController : MonoBehaviour
{
    public CallMenu callMenuObject;
    public LevelAnnouncement creditAnnoucements;

    public float showCreditDelay;

    void OnEnable()
    {
        EventManager.StartListen(Constants.ShowCreditsEvent, ShowCredits);
    }

    void OnDisable()
    {
        EventManager.StopListen(Constants.ShowCreditsEvent, ShowCredits);
    }

    private void ShowCredits()
    {
        callMenuObject.enabled = false;
        StartCoroutine(MakeDelay());
    }

    private IEnumerator MakeDelay()
    {
        yield return new WaitForSeconds(showCreditDelay);
        creditAnnoucements.gameObject.SetActive(true);
    }
}
