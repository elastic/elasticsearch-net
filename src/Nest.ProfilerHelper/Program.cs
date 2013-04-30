using Nest.ProfilerHelper.Actions;
using PowerArgs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nest.ProfilerHelper
{
	class Program
	{
		[ArgExample("asdasd", "")]
		public class NestProfilerHelperArgs
		{
			[ArgRequired]
			[ArgPosition(0)]
			public string Action { get; set; }

			public SearchActionArgs SearchArgs { get; set; }

			public IndexActionArgs IndexArgs { get; set; }

			public SearchResultActionArgs SearchResultArgs { get; set; }

			public static void Search(SearchActionArgs args)
			{
				SearchAction.Search(args);
			}

			public static void SearchResult(SearchResultActionArgs args)
			{
				SearchResultAction.SearchResult(args);
			}

			public static void Index(IndexActionArgs args)
			{
				IndexAction.Index(args);
			}
		}

		


		/// <summary>
		/// Nest.ProfilerHelper is a simple console app I can use to profile different sections of NEST
		/// </summary>
		/// <param name="args"></param>
		static void Main(string[] args)
		{
			try
			{
				Args.InvokeAction<NestProfilerHelperArgs>(args);
			}
			catch (ArgException ex)
			{
				Console.WriteLine(ex.Message);
				ArgUsage.GetStyledUsage<NestProfilerHelperArgs>().Write();
			}
		}
	}
}
