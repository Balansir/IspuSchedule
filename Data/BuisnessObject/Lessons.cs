using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Data.BuisnessObject
{
	[DataContract]
	public class Lessons
	{
		[DataMember(Name = "subject", Order = 0)]
		public string Subject { get; set; }

		[DataMember(Name = "type", Order = 1)]
		public int TypeSubject { get; set; }

		[DataMember(Name = "time_start", Order = 2)]
		public string StartTime { get; set; }

		[DataMember(Name = "time_end", Order = 3)]
		public string EndTime { get; set; }

		[DataMember(Name = "parity", Order = 4)]
		public int Parity { get; set; }

		[DataMember(Name = "date_start", Order = 5)]
		public string StartTerm { get; set; }

		[DataMember(Name = "date_end", Order = 6)]
		public string EndTerm { get; set; }

		[DataMember(Name = "dates", Order = 7)]
		public List<DateTime> Dates { get; set; }

		[DataMember(Name = "teachers", Order = 8)]
		public List<Teacher> Teachers { get; set; }

		[DataMember(Name = "auditories", Order = 9)]
		public List<Auditory> Auditories { get; set; }

		public void Initialization(List<Teacher> items)
		{
			Teachers = items;
		}

		public void Initialization(List<Auditory> items)
		{
			Auditories = items;
		}
	}

	[DataContract]
	public class Auditory
	{
		[DataMember(Name = "auditory_name", Order = 0)]
		public string AuditoryName { get; set; }

		[DataMember(Name = "auditory_address", Order = 1)]
		public string AuditoryAddress { get; set; }
	}

	[DataContract]
	public class Teacher
	{
		[DataMember(Name = "teacher_name", Order = 0)]
		public string TeacherName { get; set; }
	}
}