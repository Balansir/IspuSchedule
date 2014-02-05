namespace DAL
{
	public interface IQuery
	{
		string GetQuery(params object[] obj);
	}
}