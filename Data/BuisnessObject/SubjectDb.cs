using System;

namespace Data.BuisnessObject
{
	public class SubjectDb : IEntity
	{
		public int? Id { get; set; }
		public string GroupName { get; set; }
		public int WeekDay { get; set; }
		public string SubjectName { get; set; }
		public int TypeSubject { get; set; }
		public DateTime StartTime { get; set; }
		public DateTime EndTime { get; set; }
		public int WeekNumber { get; set; }
		public int BegWeekNumber { get; set; }
		public DateTime StartTerm { get; set; }
		public DateTime EndTerm { get; set; }
		public string Dates { get; set; }
		public string NameTutor { get; set; }
		public string Auditory { get; set; }
		public string AuditoryAddress { get; set; }
	}
}