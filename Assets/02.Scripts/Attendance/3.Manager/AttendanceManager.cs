using UnityEngine;
using System.Collections.Generic;

public class AttendanceManager : MonoBehaviour
{
    public static AttendanceManager Instance;
    private Attendance _attendances;
  //  public AttendanceDTO Attendances => _attendances.ConvertAll(a => a.ToDTO());
    public AttendanceDTO Attendances => _attendances.ToDTO();

    [SerializeField]
    private List<AttendanceSO> _metaDatas;

    private AttendanceRepository _attendanceRepository;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        Init();
    }

    public void Init()
    {
        _attendanceRepository = new AttendanceRepository();
        _attendances = new Attendance(_metaDatas[0].Title, _metaDatas[0].StartDate, _metaDatas[0].EndDate,_metaDatas[0].Rewards);
    }
}
