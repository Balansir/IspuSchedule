using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using Data.BuisnessObject;
using RestService.Repositories;

namespace RestService
{
	public class ApiScheduleService : IApiScheduleService, IDisposable
	{
		//private static SqlConnection _connection;
		//private static string _connectionString;
		private EntityRepository<Faculty> _facultyRepository;
		private EntityRepository<Group> _groupRepository;
		private EntityRepository<SubjectDb> _subjRepository;
 
		public ApiScheduleService()
		{
			//if (_connectionString == null)
			//_connectionString = ConfigurationManager.ConnectionStrings["Audience"].ConnectionString;

			//if (_connection == null)
			//{
			//    _connection = new SqlConnection(_connectionString);
			//    _connection.Open();
			//}
			SessionFactory.SetConnection(ConfigurationManager.ConnectionStrings["Audience"].ConnectionString);
			
			_facultyRepository = new EntityRepository<Faculty>();
			_groupRepository = new EntityRepository<Group>();
			_subjRepository = new EntityRepository<SubjectDb>();
		}

		public Faculties GetFaculties()
		{
			//return new Faculties {faculties = _connection.Query<Faculty>(Faculties.GetQuery()).ToList()};
			var faculties = new Faculties();
			faculties.Initialization(_facultyRepository.Load());
			return faculties;
		}

		public Groups GetGroups(int idFaculty)
		{
			//return new Groups {groups = _connection.Query<Group>(Groups.GetQuery(idFaculty)).ToList()};
			var groups = new Groups();
			groups.Initialization(_groupRepository.Load());
			return groups;
		}

		public SubjectsByGroup GetSchedule(int idGroup)
		{
			var subj = new SubjectsByGroup();
			subj.Initialization(_subjRepository.Load());
			return subj;
		}

		public string Test()
		{
			var result = new StringBuilder();
			try
			{

				IList<Faculty> faculties = GetFaculties().faculties;
				foreach (Faculty f in faculties)
				{
					IList<Group> groups = GetGroups((int) f.Id).groups;
					foreach (Group group in groups)
					{
						SubjectsByGroup schedule = GetSchedule(group.Id);
						if (schedule == null)
							result.AppendFormat("Error in group: {0} {1}", group.Id, Environment.NewLine);
					}
				}
			}
			catch (Exception ex)
			{
				result.AppendFormat("Error: {0} {1}", ex.Message, Environment.NewLine);
			}
			return result.ToString();
		}

		public void Dispose()
		{
			//_connection.Close();
		}
	}
}