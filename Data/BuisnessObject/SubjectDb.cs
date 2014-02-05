using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DAL;

namespace Data.BuisnessObject
{
	public class SubjectDb:IQueryObject
	{
		public string GroupName { get; set; }
		public int WeekDay { get; set; }
		public string SubjectName { get; set; }
		public int TypeSubject { get; set; }
		public DateTime StartTime { get; set; }
		public DateTime EndTime { get; set; }
		public int Parity { get; set; }
		public DateTime StartTerm { get; set; }
		public DateTime EndTerm { get; set; }
		public string Dates { get; set; }
		public string NameTutor { get; set; }
		public string Auditory { get; set; }
		public string AuditoryAddress { get; set; }

		public string GetQuery(params object[] obj)
		{
			if (obj == null || !obj.Any())
				return null;
			int idGroup = (int) obj[0];

			return string.Format(@"
			SELECT		RTRIM(LTRIM(CAST(subgr.Course as VARCHAR) + '-' + CAST(gr.Name as VARCHAR)+ ' ' + CAST(subgr.Name as VARCHAR))) as GroupName,
						CASE wd.Name 
							WHEN 'понедельник'	THEN 1
							WHEN 'вторник'		THEN 2
							WHEN 'среда'		THEN 3
							WHEN 'четверг'		THEN 4
							WHEN 'пятница'		THEN 5
							WHEN 'суббота'		THEN 6
							WHEN 'воскресение'	THEN 7
						END as [WeekDay],
						dis.Name + isnull(' (' + tt.Date + ')', '') as [SubjectName],
						CASE edKind.Name
							WHEN 'лек.' THEN 2
							WHEN 'сем.' THEN 3
							WHEN 'лаб.' THEN 1
							WHEN 'пр.'	THEN 0
							WHEN 'зач.' THEN 6
							WHEN 'экз.' THEN 7
							ELSE 0
						END as TypeSubject,
						CONVERT(VARCHAR(5), CAST(ETime.BegTime AS TIME), 108) as StartTime,
						CONVERT(VARCHAR(5), CAST(ETime.EndTime AS TIME), 108) as EndTime,
						tt.WeekNumber as Parity,
						CONVERT(VARCHAR, CAST(shedule.BegDate as DATE), 104) as StartTerm,
						CONVERT(VARCHAR, CAST(shedule.EndDate as DATE), 104) as EndTerm,
						tt.Date as Dates,
						t.Name as NameTutor,
						build.Title + audience.Number as Auditory,
						isnull(build.Address, '') as AuditoryAddress
			FROM		TimeTable tt
			LEFT JOIN	SubGroup subgr on tt.StreamId = subgr.Id
			LEFT JOIN	Groups gr on gr.Id = subgr.GroupId
			JOIN		[WeekDay] wd on wd.Id = tt.WeekDayId
			LEFT JOIN	Discipline dis on dis.Id = tt.DisciplineId
			LEFT JOIN	EducationKind edKind on edKind.Id = tt.EducationKindId
			JOIN		ETime eTime on eTime.Id = tt.TemeId
			JOIN		Shedule shedule on tt.SheduleId = shedule.Id
			LEFT JOIN	Tutor t on tt.TutorId = t.Id
			LEFT JOIN	Audience audience on tt.AudienceId = audience.Id
			LEFT JOIN	Building build on build.Id = audience.BuildingId
			WHERE		tt.StreamId = {0}
			ORDER BY	tt.WeekDayId
			", idGroup);
		}
	}
}
