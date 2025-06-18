using System.Collections.Generic;

public class AttendanceDTO
{
    public string ID;
    public string Title;
    public string StartDate;
    public string EndDate;
    public List<AttendanceReward> Rewards;

    public AttendanceDTO(string title, string startDate, string endDate, List<AttendanceReward> rewards)
    {
        Title = title;
        StartDate = startDate;
        EndDate = endDate;
        Rewards = new List<AttendanceReward>(rewards);
    }

    public AttendanceDTO(Attendance attendance)
    {
        Title = attendance.Title;
        StartDate = attendance.StartDate;
        EndDate = attendance.EndDate;
        Rewards = new List<AttendanceReward>(attendance.Rewards);
    }


}
