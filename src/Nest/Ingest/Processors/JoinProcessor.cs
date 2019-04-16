using System;
using System.Linq.Expressions;
using System.Runtime.Serialization;
using Elasticsearch.Net;

namespace Nest
{
	[InterfaceDataContract]
	public interface IJoinProcessor : IProcessor
	{
		[DataMember(Name ="field")]
		Field Field { get; set; }

		[DataMember(Name ="separator")]
		string Separator { get; set; }
	}

	public class JoinProcessor : ProcessorBase, IJoinProcessor
	{
		public Field Field { get; set; }

		public string Separator { get; set; }
		protected override string Name => "join";
	}

	public class JoinProcessorDescriptor<T>
		: ProcessorDescriptorBase<JoinProcessorDescriptor<T>, IJoinProcessor>, IJoinProcessor
		where T : class
	{
		protected override string Name => "join";

		Field IJoinProcessor.Field { get; set; }
		string IJoinProcessor.Separator { get; set; }

		public JoinProcessorDescriptor<T> Field(Field field) => Assign(a => a.Field = field);

		public JoinProcessorDescriptor<T> Field(Expression<Func<T, object>> objectPath) =>
			Assign(a => a.Field = objectPath);

		public JoinProcessorDescriptor<T> Separator(string separator) => Assign(a => a.Separator = separator);
	}
}
