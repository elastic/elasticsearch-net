using System.Threading.Tasks;

namespace Tests.Profiling.Framework
{
	internal interface IProfileFactory
	{
		void Run(ProfileConfiguration configuration);

		Task RunAsync(ProfileConfiguration configuration);
	}
}
