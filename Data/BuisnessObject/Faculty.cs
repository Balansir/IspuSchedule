using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using DAL;

namespace Data.BuisnessObject
{
	[DataContract]
	public class Faculty:IQueryObject
	{
		[DataMember(Name = "faculty_id", Order = 1)]
		public int? Id { get; set; }

		[DataMember(Name = "faculty_name", Order = 0)]
		public String Name { get; set; }

		[DataMember(Name = "date_start", Order = 2)]
		public DateTime? DateStart { get; set; }

		[DataMember(Name = "date_end", Order = 3)]
		public DateTime? DateEnd { get; set; }

		public string GetQuery(params object[] obj)
		{
			return "SELECT Id, Title as Name, null as DateStart, null as DateEnd FROM Subdivision  WHERE id = FacultyId";
		}
	}

	
}