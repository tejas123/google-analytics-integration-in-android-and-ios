using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SocialPlatforms.GameCenter;

public class SlotMachine : MonoBehaviour
{
    public GoogleAnalyticsV3 googleAnalytics;
    private string[] eventNames = { "0 - 99,999", "1,00,000 - 9,99,999", "10,00,000 - 99,99,999", "1,00,00,000 - 9,99,99,999", "10,00,00,000 - 49,99,99,999", "50,00,00,000 - More" };
    public TextMesh sampleTextLogScreen, sampleTextLogEvent;
    // calculate which theme is currently selected and that can be logged real time
    void Start()
    {
        googleAnalytics.StartSession();
        Invoke("registerEventLog", 10);
    }

    private void registerEventLog()
    {
        Debug.Log("111");
        // 
        int selectedTheme = getSelectedTheme();
        googleAnalytics.LogScreen("Theme : " + selectedTheme);
        sampleTextLogScreen.text = "Theme Log : Theme - " + selectedTheme;

        int eventNumber = calculateGoogleAnalyticsEvent();
        googleAnalytics.LogEvent("Event_Number : " + eventNumber, "Event_Action : " + eventNumber, eventNames[eventNumber - 1], eventNumber);
        sampleTextLogEvent.text = "Event_Number : " + eventNumber + " \n Event_Action : " + eventNumber + "\n Event Name : " + eventNames[eventNumber - 1];
    }

    private int getSelectedTheme()
    {
        // calculate and return them has selected
        return Random.Range(1, 3);
    }
    private int calculateGoogleAnalyticsEvent()
    {
        //  Debug.Log("total  credits " + GLOBAL.totalCredit);
        int totalCredit = Random.Range(0, 500000000);
        int EventNumber = 0;
        if (totalCredit >= 0 && totalCredit < 100000)
            EventNumber = 1;
        else if (totalCredit >= 100000 && totalCredit < 1000000)
            EventNumber = 2;
        else if (totalCredit >= 1000000 && totalCredit < 10000000)
            EventNumber = 3;
        else if (totalCredit >= 10000000 && totalCredit < 100000000)
            EventNumber = 4;
        else if (totalCredit >= 100000000 && totalCredit < 500000000)
            EventNumber = 5;
        return EventNumber;
    }

    void OnApplicationQuit()
    {
        googleAnalytics.StopSession();
    }
}