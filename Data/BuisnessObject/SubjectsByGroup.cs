using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using DAL;

namespace Data.BuisnessObject
{
	[DataContract]
	public class SubjectsByGroup : IShell<SubjectDb>
	{
		[DataMember(Name = "group_name", Order = 0)]
		public string GroupName { get; set; }

		[DataMember(Name = "days", Order = 1)]
		public List<Day> Days { get; set; }

		public void Initialization(List<SubjectDb> subDb)
		{
			if (!subDb.Any())
				return;
			int lastWeekDay = 0;
			var item = new SubjectsByGroup();
			item.GroupName = subDb.First().GroupName;
			item.Days = new List<Day>();
			var day = new Day();
			foreach (SubjectDb s in subDb)
			{
				int weekDay = s.WeekDay;
				if (weekDay != lastWeekDay)
				{
					day = new Day();
					day.WeekDay = weekDay;
					day.lessons = new List<Lessons>();
					item.Days.Add(day);
				}
				else
				{
					var lesson = new Lessons();
					lesson.Subject = s.SubjectName;
					lesson.TypeSubject = s.TypeSubject;
					lesson.StartTime = s.StartTime.ToShortTimeString();
					lesson.EndTime = s.EndTime.ToShortTimeString();
					lesson.Parity = s.Parity;
					lesson.StartTerm = s.StartTerm.ToShortDateString();
					lesson.EndTerm = s.EndTerm.ToShortDateString();
					lesson.Dates = null; // здесь нужно бы записывать даты для некоторых предметов, но нужно проводить парсинг. Парсинг проводить из s.Dates
					lesson.Teachers = s.NameTutor == null ? null : new List<Teacher> { new Teacher { TeacherName = s.NameTutor } };
					lesson.Auditories = s.Auditory == null ? null : new List<Auditory> { new Auditory { AuditoryName = s.Auditory, AuditoryAddress = s.AuditoryAddress } };
					day.lessons.Add(lesson);
				}
				lastWeekDay = weekDay;
			}
		}
	}
}
