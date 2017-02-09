using System.Threading.Tasks;

namespace Tests.Framework.Profiling
{
	internal interface IProfileFactory
	{
		void Run(ProfileConfiguration configuration);

		Task RunAsync(ProfileConfiguration configuration);
	}
}
