using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Serialization.Json;
using System.Windows.Forms;
using Data.BuisnessObject;

namespace TestProject
{
	public partial class MainForm : Form
	{
		//private string path = "http://schedule.ispu.ru/API/ApiScheduleService.svc/";
		private string path = "http://localhost:11111/ApiScheduleService.svc/";

		private DataTable _dataFaculties;
		private DataTable _dataSubjects;

		public MainForm()
		{
			InitializeComponent();

			_dataFaculties = new DataTable();
			_dataFaculties.Columns.Add("Id", typeof (int));
			_dataFaculties.Columns.Add("Name");
			_dataFaculties.Columns.Add("Count", typeof (int));
			_dataFaculties.Columns.Add("Time", typeof (TimeSpan));
			grdGroups.DataSource = _dataFaculties;

			_dataSubjects = new DataTable();
			_dataSubjects.Columns.Add("Id", typeof (int));
			_dataSubjects.Columns.Add("Name");
			_dataSubjects.Columns.Add("Count", typeof (int));
			_dataSubjects.Columns.Add("Time", typeof (TimeSpan));
			grdSubjects.DataSource = _dataSubjects;
		}

		public Stream GetStream(string url)
		{
			if (string.IsNullOrEmpty(url))
				throw new ArgumentNullException("url", "Parameter is null or empty");

			HttpWebRequest request = GenerateHttpWebRequest(url);
			HttpWebResponse response = (HttpWebResponse) request.GetResponse();
			if (VerifyResponse(response) != ResponseCategories.Success)
				return null;
			Stream responseStream = response.GetResponseStream();
			return responseStream;
		}

		public HttpWebRequest GenerateHttpWebRequest(string UriString)
		{
			Uri Uri = new Uri(UriString);
			HttpWebRequest httpRequest = (HttpWebRequest) WebRequest.Create(Uri);
			return httpRequest;
		}

		public ResponseCategories VerifyResponse(HttpWebResponse httpResponse)
		{
			int statusCode = (int) httpResponse.StatusCode;
			if ((statusCode >= 100) && (statusCode <= 199))
			{
				return ResponseCategories.Informational;
			}
			if ((statusCode >= 200) && (statusCode <= 299))
			{
				return ResponseCategories.Success;
			}
			if ((statusCode >= 300) && (statusCode <= 399))
			{
				return ResponseCategories.Redirected;
			}
			if ((statusCode >= 400) && (statusCode <= 499))
			{
				return ResponseCategories.ClientError;
			}
			if ((statusCode >= 500) && (statusCode <= 599))
			{
				return ResponseCategories.ServerError;
			}
			return ResponseCategories.Unknown;
		}

		private void ButBeginTestClick(object sender, EventArgs e)
		{
			_dataFaculties.Rows.Clear();
			_dataSubjects.Rows.Clear();
			Extentions.BeginAsyncOperation(() =>
			{
				butBeginTest.CallFromUIThead(but => but.Enabled = false);

				var serF = new DataContractJsonSerializer(typeof (Faculties));
				var serG = new DataContractJsonSerializer(typeof (Groups));
				var serSch = new DataContractJsonSerializer(typeof (SubjectsByGroup));

				string get_faculties = string.Format("{0}get_faculties/", path);
				string get_groups = string.Format("{0}get_groups?faculty_id=", path) + "{0}";
				string get_schedule = string.Format("{0}get_schedule?group_id=", path) + "{0}";

				DateTime start;
				DateTime end;

				List<Group> allGroups = new List<Group>();

				var faculties = serF.ReadObject(GetStream(get_faculties)) as Faculties;
				// заполняем таблицу с факультетами
				foreach (var f in faculties.faculties)
					_dataFaculties.Rows.Add(f.Id, f.Name);
				grdGroups.CallFromUIThead(grid => grid.Invalidate());

				this.CallFromUIThead(t =>
				{
					t.Width += 1;
					t.Width -= 1;
				});
				// получаем группы, отображаем статистику
				foreach (var faculty in faculties.faculties)
				{
					start = DateTime.Now;
					var groups = serG.ReadObject(GetStream(string.Format(get_groups, faculty.Id))) as Groups;
					end = DateTime.Now;
					// пишем данные о группе
					var row = _dataFaculties.Select(string.Format("Id = {0}", faculty.Id)).FirstOrDefault();
					if (row == null)
						throw new Exception("error");

					row["Count"] = groups.groups.Count;
					row["Time"] = end - start;

					grdGroups.CallFromUIThead(grid => grid.Invalidate());

					// отображаем в таблице предметов
					foreach (var g in groups.groups)
						_dataSubjects.Rows.Add(g.Id, g.Name);
					grdSubjects.CallFromUIThead(grid => grid.Invalidate());

					allGroups.AddRange(groups.groups);
				}

				this.CallFromUIThead(t =>
				{
					t.Width += 1;
					t.Width -= 1;
				});
				// получаем предметы
				foreach (var group in allGroups)
				{
					start = DateTime.Now;
					var schedule = serSch.ReadObject(GetStream(string.Format(get_schedule, group.Id))) as SubjectsByGroup;
					end = DateTime.Now;

					// отображаем
					var row = _dataSubjects.Select(string.Format("Id = {0}", group.Id)).FirstOrDefault();
					if (row == null)
						throw new Exception("error");

					row["Count"] = schedule.Days.Sum(day => day.lessons.Count);
					row["Time"] = end - start;

					grdSubjects.CallFromUIThead(grid => grid.Invalidate());
				}

				butBeginTest.CallFromUIThead(but => but.Enabled = true);
			});
		}
	}

	public enum ResponseCategories
	{
		Unknown = 0, // Unknown code ( < 100 or > 599)
		Informational = 1, // Informational codes (100 >= 199)
		Success = 2, // Success codes (200 >= 299)
		Redirected = 3, // Redirection code (300 >= 399)
		ClientError = 4, // Client error code (400 >= 499)
		ServerError = 5 // Server error code (500 >= 599)
	}
}