using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TestProject
{
	public static class Extentions
	{
		public static void BeginAsyncOperation(Action action, Action cancelAction = null)
		{
			Task task;
			if (cancelAction == null)
				task = new Task(action);
			else
			{
				var tokenSource = new CancellationTokenSource();
				tokenSource.Token.Register(cancelAction);

				task = new Task(action, tokenSource.Token);
			}

			task.Start();
		}

		public static void CallFromUIThead<T>(this T obj, Action<T> action) where T : Control
		{
			if (action == null)
				return;
			obj.Invoke(new Action(() => action(obj)));
		}
	}
}