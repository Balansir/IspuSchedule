using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Data.BuisnessObject
{
	[DataContract]
	public class SubjectsByGroup
	{
		[DataMember(Name = "group_name", Order = 0)]
		public string GroupName { get; set; }

		[DataMember(Name = "days", Order = 1)]
		public List<Day> Days { get; set; }
	}
}