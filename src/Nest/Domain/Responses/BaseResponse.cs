using System.Collections.Generic;
using System.Globalization;
using System.Linq;
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
		public BaseResponse()
		{
			this.IsValid = true;
		}
		public virtual bool IsValid { get; internal set; }
		public ConnectionStatus ConnectionStatus { get; internal set; }
	}
}
