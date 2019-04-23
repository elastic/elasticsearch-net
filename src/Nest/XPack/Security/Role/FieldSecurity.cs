using System;
using Newtonsoft.Json;

namespace Nest
{
	[ContractJsonConverter(typeof(ReadAsTypeJsonConverter<FieldSecurity>))]
	public interface IFieldSecurity
	{
		[JsonProperty("except")]
		Fields Except { get; set; }

		[JsonProperty("grant")]
		Fields Grant { get; set; }
	}

	public class FieldSecurity : IFieldSecurity
	{
		public Fields Except { get; set; }
		public Fields Grant { get; set; }
	}

	public class FieldSecurityDescriptor<T> : DescriptorBase<FieldSecurityDescriptor<T>, IFieldSecurity>, IFieldSecurity
		where T : class
	{
		Fields IFieldSecurity.Except { get; set; }
		Fields IFieldSecurity.Grant { get; set; }

		public FieldSecurityDescriptor<T> Grant(Func<FieldsDescriptor<T>, IPromise<Fields>> fields) =>
			Assign(fields, (a, v) => a.Grant = v?.Invoke(new FieldsDescriptor<T>())?.Value);

		public FieldSecurityDescriptor<T> Grant(Fields fields) => Assign(fields, (a, v) => a.Grant = v);

		public FieldSecurityDescriptor<T> Except(Func<FieldsDescriptor<T>, IPromise<Fields>> fields) =>
			Assign(fields, (a, v) => a.Except = v?.Invoke(new FieldsDescriptor<T>())?.Value);

		public FieldSecurityDescriptor<T> Except(Fields fields) => Assign(fields, (a, v) => a.Except = v);
	}
}
