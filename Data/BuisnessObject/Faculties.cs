using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using DAL;

namespace Data.BuisnessObject
{
	[DataContract]
	public class Faculties:IShell<Faculty>
	{
		[DataMember(Name = "faculties", Order = 1)]
		public IList<Faculty> faculties;

		public void Initialization(List<Faculty> items)
		{
			faculties = items;
		}
	}
}
