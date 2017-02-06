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
	[JsonConverter(typeof(ProcessorJsonConverter<UppercaseProcessor>))]
	public interface IUppercaseProcessor : IProcessor
	{
		[JsonProperty("field")]
		Field Field { get; set; }
	}

	public class UppercaseProcessor : ProcessorBase, IUppercaseProcessor
	{
		protected override string Name => "uppercase";
		[JsonProperty("field")]
		public Field Field { get; set; }
	}

	//TODO RENAME TO PROCESSOR AND WRITE A CODE STANDARDS TEST FOR THIS
	public class UppercaseProcessDescriptor<T>
		: ProcessorDescriptorBase<UppercaseProcessDescriptor<T>, IUppercaseProcessor>, IUppercaseProcessor
		where T : class
	{
		protected override string Name => "uppercase";

		Field IUppercaseProcessor.Field { get; set; }

		public UppercaseProcessDescriptor<T> Field(Field field) => Assign(a => a.Field = field);

		public UppercaseProcessDescriptor<T> Field(Expression<Func<T, object>> objectPath) =>
			Assign(a => a.Field = objectPath);
	}
}
