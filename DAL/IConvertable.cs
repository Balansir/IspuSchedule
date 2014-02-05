using System.Collections.Generic;

namespace DAL
{
	public interface IConvertable<T, out ReturnType>
	{
		ReturnType Convert(List<T> items);

		ReturnType Load(params object[] obj);
	}

	public interface IConvertable<T1, T2, out ReturnType> : IConvertable<T1, ReturnType>
	{
		ReturnType Convert(List<T2> items);
	}
}