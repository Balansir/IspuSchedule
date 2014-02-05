using System.ServiceModel;
using System.ServiceModel.Web;
using Data.BuisnessObject;

namespace RestService
{
	[ServiceContract]
	public interface IApiScheduleService
	{
		[OperationContract]
		[WebInvoke(Method = "GET",
			ResponseFormat = WebMessageFormat.Json,
			BodyStyle = WebMessageBodyStyle.Bare,
			UriTemplate = "get_faculties/")]
		Faculties GetFaculties();

		[OperationContract]
		[WebInvoke(Method = "GET",
			ResponseFormat = WebMessageFormat.Json,
			BodyStyle = WebMessageBodyStyle.Bare,
			UriTemplate = "get_groups?faculty_id={idFaculty}")]
		Groups GetGroups(int idFaculty);

		[OperationContract]
		[WebInvoke(Method = "GET",
			ResponseFormat = WebMessageFormat.Json,
			BodyStyle = WebMessageBodyStyle.Bare,
			UriTemplate = "get_schedule?group_id={idGroup}")]
		SubjectsByGroup GetSchedule(int idGroup);
	}
}