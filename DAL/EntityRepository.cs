using System.Collections.Generic;
using System.Linq;
using Dapper;
using Data.BuisnessObject;

namespace DAL
{
	public class EntityRepository<T> where T : IEntity
	{
		public virtual string GetQuery(params object[] obj)
		{
			return null;
		}

		protected List<T> Execute(params object[] obj)
		{
			return SessionFactory.OpenConnection(connection => connection.Query<T>(GetQuery(obj)).Cast<IEntity>()).Cast<T>().ToList();
		}
	}
}