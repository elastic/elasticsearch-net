using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Nest
{
	/// <summary>
	/// URL-decodes a string
	/// </summary>
	[JsonObject(MemberSerialization.OptIn)]
	[JsonConverter(typeof(ProcessorJsonConverter<UrlDecodeProcessor>))]
	public interface IUrlDecodeProcessor : IProcessor
	{
		/// <summary>
		/// The field to decode
		/// </summary>
		[JsonProperty("field")]
		Field Field { get; set; }

		/// <summary>
		/// The field to assign the converted value to, by default <see cref="Field"/> is updated in-place
		/// </summary>
		[JsonProperty("target_field")]
		Field TargetField { get; set; }

		/// <summary>
		/// If <c>true</c> and <see cref="Field"/> does not exist or is null,
		/// the processor quietly exits without modifying the document. Default is <c>false</c>
		/// </summary>
		[JsonProperty("ignore_missing")]
		bool? IgnoreMissing { get; set; }
	}

	/// <inheritdoc cref="IUrlDecodeProcessor"/>
	public class UrlDecodeProcessor : ProcessorBase, IUrlDecodeProcessor
	{
		protected override string Name => "urldecode";

		/// <inheritdoc />
		[JsonProperty("field")]
		public Field Field { get; set; }

		/// <inheritdoc />
		[JsonProperty("target_field")]
		public Field TargetField { get; set; }

		/// <inheritdoc />
		[JsonProperty("ignore_missing")]
		public bool? IgnoreMissing { get; set; }
	}

	/// <inheritdoc cref="IUrlDecodeProcessor"/>
	public class UrlDecodeProcessorDescriptor<T>
		: ProcessorDescriptorBase<UrlDecodeProcessorDescriptor<T>, IUrlDecodeProcessor>, IUrlDecodeProcessor
		where T : class
	{
		protected override string Name => "urldecode";

		Field IUrlDecodeProcessor.Field { get; set; }
		Field IUrlDecodeProcessor.TargetField { get; set; }
		bool? IUrlDecodeProcessor.IgnoreMissing { get; set; }

		/// <summary>
		/// The field to decode
		/// </summary>
		public UrlDecodeProcessorDescriptor<T> Field(Field field) => Assign(a => a.Field = field);

		/// <summary>
		/// The field to decode
		/// </summary>
		public UrlDecodeProcessorDescriptor<T> Field(Expression<Func<T, object>> objectPath) =>
			Assign(a => a.Field = objectPath);

		/// <summary>
		/// The field to assign the converted value to, by default <see cref="IUrlDecodeProcessor.Field"/> is updated in-place
		/// </summary>
		public UrlDecodeProcessorDescriptor<T> TargetField(Field field) => Assign(a => a.TargetField = field);

		/// <summary>
		/// The field to assign the converted value to, by default <see cref="IUrlDecodeProcessor.Field"/> is updated in-place
		/// </summary>
		public UrlDecodeProcessorDescriptor<T> TargetField(Expression<Func<T, object>> objectPath) =>
			Assign(a => a.TargetField = objectPath);

		/// <summary>
		/// If <c>true</c> and <see cref="IUrlDecodeProcessor.Field"/> does not exist or is null,
		/// the processor quietly exits without modifying the document. Default is <c>false</c>
		/// </summary>
		public UrlDecodeProcessorDescriptor<T> IgnoreMissing(bool? ignoreMissing = true) =>
			Assign(a => a.IgnoreMissing = ignoreMissing);
	}
}
