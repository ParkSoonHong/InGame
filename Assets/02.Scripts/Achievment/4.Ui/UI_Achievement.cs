using System.Collections.Generic;
using UnityEngine;

public class UI_Achievement : MonoBehaviour
{
    [SerializeField]
    // private UI_AchievementSlot _slot;
    private GameObject _slot;
    [SerializeField]
    private GameObject _scrollVeiwContent;

    private List<UI_AchievementSlot> _slots;

    //private void OnEnable()
    //{
    //    Refresh();
    //}

    private void Start()
    {
        _slots = new List<UI_AchievementSlot>();
        Refresh();
      
        AchievementManager.Instance.OnDataChanged += Refresh;

    }

    private void Refresh()
    {
        // ³²Àº ¼ö´Â ²¨ÁÖ±â.
        List<AchievementDTO> achievements = AchievementManager.Instance.Achievements;

        // ½½·Ô °¹¼ö¿¡ µû¶ó »ý¼º È¤Àº ²ô±â
        if (_slots.Count < achievements.Count)
        {
            while (_slots.Count != achievements.Count)
            {
                var newslot = Instantiate(_slot, _scrollVeiwContent.transform).GetComponent<UI_AchievementSlot>();
                _slots.Add(newslot);
            }
        }
        else if (_slots.Count > achievements.Count)
        {
            for (int i = achievements.Count; i < _slots.Count; i++)
            {
                _slots[i].gameObject.SetActive(false);
            }
        }

        for (int i = 0; i < achievements.Count; i++)
        {
            _slots[i].Refresh(achievements[i]);
        }
  
    }

}
