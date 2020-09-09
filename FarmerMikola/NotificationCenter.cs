using System;
using Unity.Notifications.Android;
using UnityEngine;

public class NotificationCenter : MonoBehaviour
{

    private AndroidNotificationChannel channel;
    public FirstRun firstRun;

    private void Start()
    {
        channel = new AndroidNotificationChannel()
        {
            Id = "TOLChannel",
            Name = "Default Channel",
            Importance = Importance.High,
            Description = "Generic notifications",
        };

        AndroidNotificationCenter.RegisterNotificationChannel(channel);

        ChangeScene changeScene = firstRun.changeScene;

        
        if (!changeScene.firstRun)
        {
            AndroidNotificationCenter.CancelAllNotifications();
            SendNotification("First Notifiication", "Ready to game?", DateTime.Now.AddSeconds(10));
            SendNotification("First Notifiication", "We are wait you", DateTime.Now.AddSeconds(30));
            SendNotification("First Notifiication", "Maybe play the game?", DateTime.Today.AddHours(10));
            SendNotification("First Notifiication", "Begin play, bastard", DateTime.Today.AddHours(20));
        }

    }


    private void SendNotification(string tiitle, string text, DateTime time)
    {
        var notification = new AndroidNotification();
        notification.Title = tiitle;
        notification.Text = text;
        notification.FireTime = time;

        AndroidNotificationCenter.SendNotification(notification, "TOLChannel");
    }
}
