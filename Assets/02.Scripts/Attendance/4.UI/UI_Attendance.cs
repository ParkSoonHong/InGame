using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UI_Attendance : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _titleText;
    [SerializeField] private TextMeshProUGUI _startDayText;
    [SerializeField] private TextMeshProUGUI _endDayText;

    [SerializeField] private GameObject _slot;
    [SerializeField] private Transform _slotParent;
    private List<UI_AttendanceRewoardSlot> _slots;

    private void Start()
    {
        Init();
        Refresh();
    }

    private void Init()
    {
        AttendanceDTO attendanceDTO = AttendanceManager.Instance.Attendances;
        _titleText.text = attendanceDTO.Title;
        _startDayText.text = attendanceDTO.StartDate;
        _endDayText.text = attendanceDTO.EndDate;


        _slots = new List<UI_AttendanceRewoardSlot>();
        // 만큼 팝업 생성 
        for (int i = 0; i < attendanceDTO.Rewards.Count; i++)
        {
            GameObject slot = Instantiate(_slot, _slotParent);
            _slots.Add(slot.GetComponent<UI_AttendanceRewoardSlot>());
        }

    }

    private void Refresh()
    {
        AttendanceDTO attendanceDTO = AttendanceManager.Instance.Attendances;
        for (int i = 0; i < attendanceDTO.Rewards.Count; i++)
        {
            _slots[i].Refresh(attendanceDTO.Rewards[i]);
        }
    }

}
