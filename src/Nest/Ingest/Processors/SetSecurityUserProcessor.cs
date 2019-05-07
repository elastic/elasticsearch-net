using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Newtonsoft.Json;

namespace Nest
{
	/// <summary>
	/// Sets user-related details (such as username, roles, email, full_name and metadata ) from the
	/// current authenticated user to the current document by pre-processing the ingest.
	/// </summary>
	[JsonObject(MemberSerialization.OptIn)]
	[JsonConverter(typeof(ProcessorJsonConverter<SetSecurityUserProcessor>))]
	public interface ISetSecurityUserProcessor : IProcessor
	{
		/// <summary>The field to store the user information into. </summary>
		[JsonProperty("field")]
		Field Field { get; set; }

		/// <summary>
		/// Controls what user related properties are added to the field.
		/// Defaults to username, roles, email, full_name, metadata
		/// </summary>
		[JsonProperty("properties")]
		IEnumerable<string> Properties { get; set; }
	}

	/// <inheritdoc cref="ISetSecurityUserProcessor" />
	public class SetSecurityUserProcessor : ProcessorBase, ISetSecurityUserProcessor
	{
		/// <inheritdoc />
		public Field Field { get; set; }
		/// <inheritdoc />
		public IEnumerable<string> Properties { get; set; }
		protected override string Name => "set_security_user";
	}

	/// <inheritdoc cref="ISetSecurityUserProcessor" />
	public class SetSecurityUserProcessorDescriptor<T>
		: ProcessorDescriptorBase<SetSecurityUserProcessorDescriptor<T>, ISetSecurityUserProcessor>, ISetSecurityUserProcessor
		where T : class
	{
		protected override string Name => "set_security_user";
		Field ISetSecurityUserProcessor.Field { get; set; }
		IEnumerable<string> ISetSecurityUserProcessor.Properties { get; set; }

		/// <inheritdoc cref="ISetSecurityUserProcessor.Field"/>
		public SetSecurityUserProcessorDescriptor<T> Field(Field field) => Assign(field, (a, v) => a.Field = v);

		/// <inheritdoc cref="ISetSecurityUserProcessor.Field"/>
		public SetSecurityUserProcessorDescriptor<T> Field(Expression<Func<T, object>> objectPath) =>
			Assign(objectPath, (a, v) => a.Field = v);

		/// <inheritdoc cref="ISetSecurityUserProcessor.Properties"/>
		public SetSecurityUserProcessorDescriptor<T> Properties(IEnumerable<string> properties) =>
			Assign(properties, (a, v) => a.Properties = v);

		/// <inheritdoc cref="ISetSecurityUserProcessor.Properties"/>
		public SetSecurityUserProcessorDescriptor<T> Properties(params string[] properties) =>
			Assign(properties, (a, v) => a.Properties = v);
	}
}
