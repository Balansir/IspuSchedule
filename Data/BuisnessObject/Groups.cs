using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DAL;

namespace Data.BuisnessObject
{
	public class Groups:IShell<Group>
	{
		public IList<Group> groups;

		public void Initialization(List<Group> items)
		{
			groups = items;
		}
	}
}
