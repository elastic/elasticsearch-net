using System;
using Nest.Resolvers.Converters;
using Newtonsoft.Json;
using System.Linq.Expressions;

namespace Nest
{
	[JsonConverter(typeof(ReadAsTypeConverter<AnalyzerField>))]
	public interface IAnalyzerField : ISpecialField
	{
		[JsonProperty("index"), JsonConverter(typeof(YesNoBoolConverter))]
		bool? Index { get; set; }

		[JsonProperty("path")]
		FieldName Path { get; set; }
	}

	public class AnalyzerField : IAnalyzerField
	{
		public bool? Index { get; set; }

		public FieldName Path { get; set; }
	}

	public class AnalyzerFieldDescriptor<T> : IAnalyzerField
	{
		private IAnalyzerField Self => this;

		bool? IAnalyzerField.Index { get; set; }

		FieldName IAnalyzerField.Path { get; set; }

		public AnalyzerFieldDescriptor<T> Index(bool indexed = true)
		{
			Self.Index = indexed;
			return this;
		}
		public AnalyzerFieldDescriptor<T> Path(string path)
		{
			Self.Path = path;
			return this;
		}
		public AnalyzerFieldDescriptor<T> Path(Expression<Func<T, object>> objectPath)
		{
			Self.Path = objectPath;
			return this;
		}
	}
}