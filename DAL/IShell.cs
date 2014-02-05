using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DAL
{
	public interface IShell<T>
	{
		void Initialization(List<T> items);
	}

	public interface IShell<T1, T2>: IShell<T1>
	{
		void Initialization(List<T2> items);
	}
}
