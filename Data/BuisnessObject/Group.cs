using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using DAL;

namespace Data.BuisnessObject
{
	[DataContract]
	public class Group:IQueryObject
	{
		[DataMember(Name = "group_id", Order = 1)]
		public int Id { get; set; }

		[DataMember(Name = "group_name", Order = 0)]
		public string Name { get; set; }

		public string GetQuery(params object[] obj)
		{
			if (obj == null || !obj.Any())
				return null;

			int idFaculty = (int) obj[0];
			return string.Format(@"
				SELECT sub.Id as Id, 
				RTRIM(LTRIM(CAST(sub.Course as VARCHAR) + '-' + CAST(g.Name as VARCHAR)+ ' ' + CAST(sub.Name as VARCHAR))) as Name 
				FROM SubGroup sub, 
					 Groups g
				WHERE g.Id = sub.GroupId AND g.SubDivisionId = {0} AND
					  EXISTS(SELECT TOP 1 1 FROM TimeTable tt WHERE tt.StreamId = sub.Id)
				ORDER BY sub.Course, g.Name
			", idFaculty);
		}
	}
}