using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Nest
{
	[JsonObject(MemberSerialization.OptIn)]
	[JsonConverter(typeof(ProcessorJsonConverter<JoinProcessor>))]
	public interface IJoinProcessor : IProcessor
	{
		[JsonProperty("field")]
		Field Field { get; set; }

		[JsonProperty("separator")]
		string Separator { get; set; }
	}

	public class JoinProcessor : ProcessorBase, IJoinProcessor
	{
		protected override string Name => "join";

		public Field Field { get; set; }

		public string Separator { get; set; }
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
