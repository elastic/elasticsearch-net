using Nest.Tests.MockData.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nest.ProfilerHelper.Actions
{
	public class IndexActionArgs
	{

	}
	public static class IndexAction
	{
		public static void Index(IndexActionArgs args)
		{
			try
			{
				for (var i = 0; i < 100; i++)
				{
					BaseAction.Setup();
					BaseAction.TearDown();
				}
			}
			catch (Exception)
			{
				BaseAction.TearDown();
				throw;
			}
		}
	}
}
