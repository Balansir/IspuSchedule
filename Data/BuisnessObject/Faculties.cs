using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Data.BuisnessObject
{
	[DataContract]
	public class Faculties
	{
		[DataMember(Name = "faculties", Order = 1)] public IList<Faculty> faculties;
	}
}