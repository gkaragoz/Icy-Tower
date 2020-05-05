﻿using Library.PushNotification;
using UnityEngine;

public class NotificationExample : MonoBehaviour {
    // Notification Title
    private const string Notification_Title = "Race Tower";

    // Notification Text
    private const string Notification_Text = "Hey! We missed you!";

    // Notification Icon
    private const string Notification_LargeIconID = "icon_01";

    private AndroidNotifications _notifications;

    private void Start() {
        _notifications = new AndroidNotifications();
    }

    private void Setup() {
        if (_notifications == null)
        {
            return;
        }
        _notifications.ResetAll();

        _notifications.CreateNotificationChannel();

        // 1 HOUR
        _notifications.AddNotification(Notification_Title, Notification_Text, 1, Notification_LargeIconID);
        // 3 HOUR
        _notifications.AddNotification(Notification_Title, Notification_Text, 3, Notification_LargeIconID);
        // 7 HOUR
        _notifications.AddNotification(Notification_Title, Notification_Text, 7, Notification_LargeIconID);
        // 1 DAY
        _notifications.AddNotification(Notification_Title, Notification_Text, 24, Notification_LargeIconID);
    }

    private void OnApplicationQuit() {

        Setup();
    }

    private void OnApplicationPause(bool pause) {
        if (_notifications==null)
        {
            return;
        }
        if (pause) {
            Setup();
        } else {
            _notifications.ResetAll();
        }
    }

}