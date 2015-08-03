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

	public class AnalyzerFieldDescriptor<T> 
		: DescriptorBase<AnalyzerFieldDescriptor<T>, IAnalyzerField>, IAnalyzerField
	{
		bool? IAnalyzerField.Index { get; set; }
		FieldName IAnalyzerField.Path { get; set; }

		public AnalyzerFieldDescriptor<T> Index(bool index = true) => Assign(a => a.Index = index);
		public AnalyzerFieldDescriptor<T> Path(string path) => Assign(a => a.Path = path);
		public AnalyzerFieldDescriptor<T> Path(Expression<Func<T, object>> objectPath) => Assign(a => a.Path = objectPath);
	}
}