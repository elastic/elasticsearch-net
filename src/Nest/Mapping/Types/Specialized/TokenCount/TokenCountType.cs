using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nest
{
	[JsonObject(MemberSerialization.OptIn)]
	public interface ITokenCountProperty : IElasticsearchProperty
	{
		string Analyzer { get; set; }
	}

	public class TokenCountProperty : NumberProperty, ITokenCountProperty
	{
		public TokenCountProperty() : base("token_count") { }

		internal TokenCountProperty(TokenCountAttribute attribute)
			: base("token_count", attribute)
		{
			Analyzer = attribute.Analyzer;
		}

		public string Analyzer { get; set; }
	}

	public class TokenCountPropertyDescriptor<T> : NumberPropertyDescriptor<T>, ITokenCountProperty
		where T : class
	{
		string ITokenCountProperty.Analyzer { get; set; }

		public TokenCountPropertyDescriptor<T> Analyzer(string analyzer)
		{
			((ITokenCountProperty)this).Analyzer = analyzer;
			return this;
		}
	}
}
