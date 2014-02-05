using System;
using System.Configuration;
using DAL;
using Data.BuisnessObject;

namespace RestService
{
	public class ApiScheduleService : IApiScheduleService, IDisposable
	{
		private readonly FacultyRepository _facultyRepository;
		private readonly GroupRepository _groupRepository;
		private readonly SubjectRepository _subjRepository;

		public ApiScheduleService()
		{
			SessionFactory.Load(ConfigurationManager.ConnectionStrings["Audience"].ConnectionString);

			_facultyRepository = new FacultyRepository();
			_groupRepository = new GroupRepository();
			_subjRepository = new SubjectRepository();
		}

		public Faculties GetFaculties()
		{
			var faculties = _facultyRepository.Load();
			return faculties;
		}

		public Groups GetGroups(int idFaculty)
		{
			var groups = _groupRepository.Load(idFaculty);
			return groups;
		}

		public SubjectsByGroup GetSchedule(int idGroup)
		{
			var subj = _subjRepository.Load(idGroup);
			return subj;
		}

		public void Dispose()
		{
		}
	}
}