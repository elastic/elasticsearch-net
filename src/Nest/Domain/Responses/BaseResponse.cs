using Nest.Resolvers;

namespace Nest
{
    public interface IResponse
    {
        bool IsValid { get; }
        ConnectionStatus ConnectionStatus { get; }
    }

    public class BaseResponse : IResponse
    {
		public bool IsValid { get; internal set; }
		public ConnectionStatus ConnectionStatus { get; internal set; }

		internal PropertyNameResolver PropertyNameResolver { get; set; }
	}
}
