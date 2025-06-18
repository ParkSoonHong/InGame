using System.Collections.Generic;
using UnityEngine;

public class UI_Notification : MonoBehaviour
{
    [SerializeField]
    private GameObject _notification;
    private List<UI_AchievementNotification> _notifications;
    void Start()
    {
        _notifications = new List<UI_AchievementNotification>();
        RefreshNotifications();
    }

    private void RefreshNotifications()
    {
        List<AchievementDTO> achievements = AchievementManager.Instance.Achievements;
        while (_notifications.Count != achievements.Count)
        {
            var newfication = Instantiate(_notification, transform).GetComponent<UI_AchievementNotification>();
            _notifications.Add(newfication);
        }

        for (int i = 0; i < achievements.Count; i++)
        {
            _notifications[i].Refresh(achievements[i]);
        }
    }

    private void Done()
    {
   
    }
}
