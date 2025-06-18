using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AttendanceSO", menuName = "Scriptable Objects/AttendanceSO")]
public class AttendanceSO : ScriptableObject
{
    public string Title;
    public string StartDate;
    public string EndDate;
    public bool IsTodayClaimed;
    public List<AttendanceReward> Rewards;
}
