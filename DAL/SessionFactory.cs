using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Data.BuisnessObject;

namespace DAL
{
	public static class SessionFactory
	{
		public static string ConnectionString { get; set; }

		public static void Load(string connectionString)
		{
			ConnectionString = connectionString;
		}

		internal static IEnumerable<IEntity> OpenConnection(Func<SqlConnection, IEnumerable<IEntity>> sqlAction)
		{
			IEnumerable<IEntity> result;
			using (var connection = new SqlConnection(ConnectionString))
			{
				connection.Open();
				result = sqlAction(connection);
				connection.Close();
			}
			return result;
		}
	}
}