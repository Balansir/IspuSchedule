using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Data.BuisnessObject
{
	[DataContract]
	public class Day
	{
		[DataMember(Name = "weekday", Order = 0)]
		public int WeekDay { get; set; }

		[DataMember(Name = "lessons", Order = 1)]
		public List<Lessons> lessons { get; set; }
	}
}