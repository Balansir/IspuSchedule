using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace DAL
{
	public static class SessionFactory
	{
		private static SqlConnection _connection;

		public static string ConnectionString { get; set; }

		public static SqlConnection Connection { get { return _connection; } }
		
		public static void SetConnection(string connectionString)
		{
			ConnectionString = connectionString;

			_connection = new SqlConnection(connectionString);
			_connection.Open();
		}
	}
}
