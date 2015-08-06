using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nest
{
	[JsonObject(MemberSerialization.OptIn)]
	public interface ITokenCountType : IElasticType
	{
		string Analyzer { get; set; }
	}

	public class TokenCountType : NumberType, ITokenCountType
	{
		public TokenCountType() : base("token_count") { }
		public string Analyzer { get; set; }
	}

	public class TokenCountTypeDescriptor<T> : NumberTypeDescriptor<T>, ITokenCountType
		where T : class
	{
		string ITokenCountType.Analyzer { get; set; }

		public TokenCountTypeDescriptor<T> Analyzer(string analyzer)
		{
			((ITokenCountType)this).Analyzer = analyzer;
			return this;
		}
	}
}
