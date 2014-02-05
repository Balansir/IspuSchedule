using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using DAL;

namespace Data.BuisnessObject
{
	[DataContract]
	public class Day : IShell<Lessons>
	{
		[DataMember(Name = "weekday", Order = 0)]
		public int WeekDay { get; set; }

		[DataMember(Name = "lessons", Order = 1)]
		public List<Lessons> lessons { get; set; }

		public void Initialization(List<Lessons> items)
		{
			lessons = items;
		}
	}
}
