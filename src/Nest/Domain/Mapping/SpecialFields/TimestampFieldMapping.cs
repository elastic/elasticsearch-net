using System;
using Newtonsoft.Json;
using System.Linq.Expressions;
using Nest.Resolvers;

namespace Nest
{
	public class	TimestampFieldMapping
	{
		public TimestampFieldMapping()
		{

		}

		[JsonProperty("enabled")]
		public bool Enabled { get; internal set; }

		[JsonProperty("path")]
		public string Path { get; internal set; }

		[JsonProperty("format")]
		public string Format { get; internal set; }
	}


	public class TimestampFieldMapping<T> : TimestampFieldMapping
    {
		public TimestampFieldMapping<T> SetDisabled(bool disabled = true)
		{
			this.Enabled = !disabled;
			return this;
		}
		public TimestampFieldMapping<T> SetPath(string path)
		{
			this.Path = path;
			return this;
		}
		public TimestampFieldMapping<T> SetPath(Expression<Func<T, object>> objectPath)
		{
			objectPath.ThrowIfNull("objectPath");
			this.Path = new PropertyNameResolver().Resolve(objectPath);
			return this;	
		}
		public TimestampFieldMapping<T> SetFormat(string format)
		{
			this.Format = format;
			return this;
		}
    }
}