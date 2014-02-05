using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Serialization.Json;
using Data.BuisnessObject;

namespace TestService
{
	internal class Program
	{
		//private static string path = "http://schedule.ispu.ru/API/ApiScheduleService.svc/";
		private static string path = "http://localhost:11111/ApiScheduleService.svc/";

		private static string LastQuery;

		private static void Main(string[] args)
		{
			Console.ReadKey();

			//try
			//{
			//    while (true)
			//    {
			//        var start = DateTime.Now;
			//        //var text = GetHtmlFromUrl("http://schedule.ispu.ru");
			//        //var stream = GetStream("http://schedule.ispu.ru/");
			//        //var stream = GetStream("http://schedule.ispu.ru/API/ApiScheduleService.svc/get_faculties/");
			//        var stream = GetStream("http://localhost:11111/ApiScheduleService.svc/get_faculties/");
			//        stream.ReadByte();
			//        stream.Close();
			//        Console.WriteLine("{0}", DateTime.Now - start);
			//    }
			//}
			//catch (Exception ex)
			//{
			//    Console.WriteLine(ex.Message);
			//}
			//Console.ReadKey();
			//return;

			int countLessons = 0;
			try
			{
				var serF = new DataContractJsonSerializer(typeof (Faculties));
				var serG = new DataContractJsonSerializer(typeof (Groups));
				var serSch = new DataContractJsonSerializer(typeof (SubjectsByGroup));

				string get_faculties = string.Format("{0}get_faculties/", path);
				string get_groups = string.Format("{0}get_groups?faculty_id=", path) + "{0}";
				string get_schedule = string.Format("{0}get_schedule?group_id=", path) + "{0}";

				var faculties = serF.ReadObject(GetStream(get_faculties)) as Faculties;

				foreach (var f in faculties.faculties)
				{
					Console.WriteLine("--------------- {0}", f.Name);
					var groups = serG.ReadObject(GetStream(string.Format(get_groups, f.Id))) as Groups;
					foreach (var group in groups.groups)
					{
						//Thread.Sleep(50);
						Console.WriteLine("-------------------- {0}", group.Name);
						var schedule = serSch.ReadObject(GetStream(string.Format(get_schedule, group.Id))) as SubjectsByGroup;
						if (schedule == null)
							Console.WriteLine("Для группы {0} не удаётся получить рассписание", group.Id);
						else
							countLessons += schedule.Days.Sum(d => d.lessons.Count());

						//var task = new Task(() =>
						//{
						//    for (int i = 0; i < 1000; i++)
						//    {
						//        var schedule = serSch.ReadObject(GetStream(string.Format(get_schedule, group.Id))) as SubjectsByGroup;
						//        if (schedule == null)
						//            Console.WriteLine("Для группы {0} не удаётся получить рассписание", group.Id);
						//    }
						//}, TaskCreationOptions.LongRunning);
						//task.Start();
					}
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
				Console.WriteLine("Last query: {0}", LastQuery);
			}
			Console.WriteLine("All good. Count: {0}", countLessons);
			Console.ReadKey();
		}

		public static Stream GetStream(string url)
		{
			if (string.IsNullOrEmpty(url))
				throw new ArgumentNullException("url", "Parameter is null or empty");

			LastQuery = url;
			HttpWebRequest request = GenerateHttpWebRequest(url);
			HttpWebResponse response = (HttpWebResponse) request.GetResponse();
			if (VerifyResponse(response) != ResponseCategories.Success)
				return null;
			Stream responseStream = response.GetResponseStream();
			return responseStream;
		}

		public static HttpWebRequest GenerateHttpWebRequest(string UriString)
		{
			Uri Uri = new Uri(UriString);
			HttpWebRequest httpRequest = (HttpWebRequest) WebRequest.Create(Uri);
			return httpRequest;
		}

		public static ResponseCategories VerifyResponse(HttpWebResponse httpResponse)
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