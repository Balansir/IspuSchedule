using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using Dapper;

namespace DAL
{
	public class EntityRepository<T> where T:IQueryObject
	{
		private SqlConnection _connection;

		public List<T> Load()
		{
			T defaultObj = default(T);
			return _connection.Query<T>(defaultObj.GetQuery()).ToList();
		}
	}
}
