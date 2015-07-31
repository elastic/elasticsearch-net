using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nest
{
	public interface ITokenCountType : IElasticType
	{

	}

	public class TokenCountType : ElasticType, ITokenCountType
	{
		public TokenCountType() : base("token_count") { }
	}
}
