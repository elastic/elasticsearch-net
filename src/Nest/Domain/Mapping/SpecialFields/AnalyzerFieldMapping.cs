using Elasticsearch.Net;
using Nest.Resolvers;
using Nest.Resolvers.Converters;
using Newtonsoft.Json;
using Shared.Extensions;
using System;
using System.Linq.Expressions;

namespace Nest
{
	public class AnalyzerFieldMapping
	{
		public AnalyzerFieldMapping()
		{
			this.Index = true;
		}

		[JsonProperty("index"), JsonConverter(typeof(YesNoBoolConverter))]
		public bool? Index { get; internal set; }

		[JsonProperty("path")]
		public PropertyPathMarker Path { get; internal set; }
	}


	public class AnalyzerFieldMapping<T> : AnalyzerFieldMapping
	{
		public AnalyzerFieldMapping<T> SetIndexed(bool indexed = true)
		{
			this.Index = indexed;
			return this;
		}
		public AnalyzerFieldMapping<T> SetPath(string path)
		{
			this.Path = path;
			return this;
		}
		public AnalyzerFieldMapping<T> SetPath(Expression<Func<T, object>> objectPath)
		{
			objectPath.ThrowIfNull("objectPath");
			this.Path = objectPath;
			return this;
		}
	}
}