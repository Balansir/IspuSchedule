using System.Collections.Generic;
using Data.BuisnessObject;

namespace DAL
{
	public class FacultyRepository : EntityRepository<Faculty>, IConvertable<Faculty, Faculties>
	{
		public override string GetQuery(params object[] obj)
		{
			return "SELECT Id, Title as Name, null as DateStart, null as DateEnd FROM Subdivision  WHERE id = FacultyId";
		}

		public Faculties Convert(List<Faculty> items)
		{
			return new Faculties {faculties = items};
		}

		public Faculties Load(params object[] obj)
		{
			return Convert(Execute(obj));
		}
	}
}