using System;
using System.Collections.Generic;

public class Attendance
{
    public readonly string ID;
    public readonly string Title;
    public readonly string StartDate;
    public readonly string EndDate;
    public readonly List<AttendanceReward> Rewards;

    public readonly bool HasBonusReward;
    public readonly List<AttendanceReward> BonusReward;

    public Attendance(string title, string startDate, string endDate , List<AttendanceReward> rewards)
    {
        Title = title;
        StartDate = startDate;
        EndDate = endDate;
        Rewards = new List<AttendanceReward>(rewards);
    }

    public AttendanceDTO ToDTO()
    {
        return new AttendanceDTO(this);
    }
}
