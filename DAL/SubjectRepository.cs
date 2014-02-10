using System;
using System.Collections.Generic;
using System.Linq;
using Data.BuisnessObject;

namespace DAL
{
	public class SubjectRepository : EntityRepository<SubjectDb>, IConvertable<SubjectDb, SubjectsByGroup>
	{
		public SubjectsByGroup Convert(List<SubjectDb> subDb)
		{
			var result = new SubjectsByGroup();
			if (!subDb.Any())
				return null;
			int lastWeekDay = 0;
			result.GroupName = subDb.First().GroupName;
			result.Days = new List<Day>();
			var day = new Day();
			foreach (SubjectDb s in subDb)
			{
				int weekDay = s.WeekDay;
				if (weekDay != lastWeekDay)
				{
					day = new Day();
					day.WeekDay = weekDay;
					day.lessons = new List<Lessons>();
					result.Days.Add(day);
				}

				var lesson = new Lessons();
				lesson.Subject = s.SubjectName;
				lesson.TypeSubject = s.TypeSubject;
				lesson.StartTime = s.StartTime.ToShortTimeString();
				lesson.EndTime = s.EndTime.ToShortTimeString();
				lesson.Parity = GetParity(s.WeekNumber, s.BegWeekNumber, s.StartTerm);
				lesson.StartTerm = s.StartTerm.ToShortDateString();
				lesson.EndTerm = s.EndTerm.ToShortDateString();
				lesson.Dates = null; // здесь нужно бы записывать даты для некоторых предметов, но нужно проводить парсинг. Парсинг проводить из s.Dates
				lesson.Teachers = s.NameTutor == null ? null : new List<Teacher> {new Teacher {TeacherName = s.NameTutor}};
				lesson.Auditories = s.Auditory == null ? null : new List<Auditory> {new Auditory {AuditoryName = s.Auditory, AuditoryAddress = s.AuditoryAddress}};
				day.lessons.Add(lesson);
				lastWeekDay = weekDay;
			}

			return result;
		}

		public SubjectsByGroup Load(params object[] obj)
		{
			return Convert(Execute(obj));
		}

		public override string GetQuery(params object[] obj)
		{
			if (obj == null || !obj.Any())
				return null;
			int idGroup = (int) obj[0];

			return string.Format(@"
			SELECT		tt.Id,
						RTRIM(LTRIM(CAST(subgr.Course as VARCHAR) + '-' + gr.Name + ' ' + subgr.Name)) as GroupName,
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
						tt.WeekNumber as WeekNumber,
						shedule.BegWeekNumber,
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

		// TODO: нужно определить чётность недели, т.к. наши недели не совпадают с неделями сервиса рассписания
		/// <summary>
		/// Определение чётности/нечётности недели
		/// </summary>
		/// <param name="weekNumber">Номер недели в нашем рассписании</param>
		/// <param name="weekNumberStart">Неделя, с которой начинается семестр</param>
		/// <param name="start">Начало занятий</param>
		/// <returns>Чётность недели</returns>
		private int GetParity(int weekNumber, int weekNumberStart, DateTime start)
		{
			var countDay = (int) start.ToOADate();
			var parity = (countDay/7)%2;
			if ((parity == 0 && weekNumberStart == 2) || (parity == weekNumberStart))
				return weekNumber;
			return weekNumber == 1 ? 2 : 1;
		}
	}
}