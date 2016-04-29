using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace Nest
{
	[JsonConverter(typeof(ReadAsTypeJsonConverter<IndicesPrivileges>))]
	public interface IIndicesPrivileges
	{
		[JsonProperty("names")]
		[JsonConverter(typeof(IndicesJsonConverter))]
		Indices Names { get; set; }

		[JsonProperty("privileges")]
		IEnumerable<string> Privileges { get; set; }

		[JsonProperty("fields")]
		Fields Fields { get; set; }

		[JsonProperty("query")]
		QueryContainer Query { get; set; }

	}
	public class IndicesPrivileges : IIndicesPrivileges
	{
		[JsonConverter(typeof(IndicesJsonConverter))]
		public Indices Names { get; set; }
		public IEnumerable<string> Privileges { get; set; }
		public Fields Fields { get; set; }
		public QueryContainer Query { get; set; }
	}


	public class IndicesPrivilegesDescriptor : DescriptorPromiseBase<IndicesPrivilegesDescriptor, IList<IIndicesPrivileges>>
	{
		public IndicesPrivilegesDescriptor() : base(new List<IIndicesPrivileges>()) { }

		public IndicesPrivilegesDescriptor Add<T>(Func<IndicesPrivilegesDescriptor<T>, IIndicesPrivileges> selector) where T : class  =>
			Assign(a => a.AddIfNotNull(selector?.Invoke(new IndicesPrivilegesDescriptor<T>())));

	}
	public class IndicesPrivilegesDescriptor<T>: DescriptorBase<IndicesPrivilegesDescriptor<T>, IIndicesPrivileges>, IIndicesPrivileges
		where T :class
	{
		Indices IIndicesPrivileges.Names { get; set; }
		IEnumerable<string> IIndicesPrivileges.Privileges { get; set; }
		Fields IIndicesPrivileges.Fields { get; set; }
		QueryContainer IIndicesPrivileges.Query { get; set; }

		public IndicesPrivilegesDescriptor<T> Names(Indices indices) => Assign(a => a.Names = indices);
		public IndicesPrivilegesDescriptor<T> Names(params IndexName[] indices) => Assign(a => a.Names = indices);
		public IndicesPrivilegesDescriptor<T> Names(IEnumerable<IndexName> indices) => Assign(a => a.Names = indices.ToArray());

		public IndicesPrivilegesDescriptor<T> Privileges(params string[] privileges) => Assign(a => a.Privileges = privileges);
		public IndicesPrivilegesDescriptor<T> Privileges(IEnumerable<string> privileges) => Assign(a => a.Privileges = privileges);

		public IndicesPrivilegesDescriptor<T> Fields(Func<FieldsDescriptor<T>, IPromise<Fields>> fields) =>
			Assign(a => a.Fields = fields?.Invoke(new FieldsDescriptor<T>())?.Value);

		public IndicesPrivilegesDescriptor<T> Fields(Fields fields) => Assign(a => a.Fields = fields);

		public IndicesPrivilegesDescriptor<T> Query(Func<QueryContainerDescriptor<T>, QueryContainer> query) =>
			Assign(a => a.Query = query?.InvokeQuery(new QueryContainerDescriptor<T>()));

	}
}
