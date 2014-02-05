using System.Runtime.Serialization;

namespace Data.BuisnessObject
{
	[DataContract]
	public class Group : IEntity
	{
		[DataMember(Name = "group_id", Order = 1)]
		public int? Id { get; set; }

		[DataMember(Name = "group_name", Order = 0)]
		public string Name { get; set; }
	}
}