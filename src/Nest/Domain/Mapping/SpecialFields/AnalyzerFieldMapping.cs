using System;
using Newtonsoft.Json;
using System.Linq.Expressions;
using Nest.Resolvers;

namespace Nest
{
	public class AnalyzerFieldMapping
	{
		public AnalyzerFieldMapping()
		{
			this.Index = true;
		}

		[JsonProperty("index")]
		public bool Index { get; internal set; }

		[JsonProperty("path")]
		public string Path { get; internal set; }
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
			this.Path = new PropertyNameResolver().Resolve(objectPath);
			return this;	
		}
    }
}