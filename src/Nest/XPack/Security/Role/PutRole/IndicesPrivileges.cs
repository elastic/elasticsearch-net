// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using Elasticsearch.Net.Utf8Json;

namespace Nest
{
	[ReadAs(typeof(IndicesPrivileges))]
	public interface IIndicesPrivileges
	{
		[DataMember(Name = "field_security")]
		IFieldSecurity FieldSecurity { get; set; }

		[DataMember(Name = "names")]
		[JsonFormatter(typeof(IndicesFormatter))]
		Indices Names { get; set; }

		[DataMember(Name = "privileges")]
		IEnumerable<string> Privileges { get; set; }

		[DataMember(Name = "query")]
		QueryContainer Query { get; set; }
	}

	public class IndicesPrivileges : IIndicesPrivileges
	{
		public IFieldSecurity FieldSecurity { get; set; }

		[JsonFormatter(typeof(IndicesFormatter))]
		public Indices Names { get; set; }

		public IEnumerable<string> Privileges { get; set; }
		public QueryContainer Query { get; set; }
	}

	public class IndicesPrivilegesDescriptor : DescriptorPromiseBase<IndicesPrivilegesDescriptor, IList<IIndicesPrivileges>>
	{
		public IndicesPrivilegesDescriptor() : base(new List<IIndicesPrivileges>()) { }

		public IndicesPrivilegesDescriptor Add<T>(Func<IndicesPrivilegesDescriptor<T>, IIndicesPrivileges> selector) where T : class =>
			Assign(selector, (a, v) => a.AddIfNotNull(v?.Invoke(new IndicesPrivilegesDescriptor<T>())));
	}

	public class IndicesPrivilegesDescriptor<T> : DescriptorBase<IndicesPrivilegesDescriptor<T>, IIndicesPrivileges>, IIndicesPrivileges
		where T : class
	{
		IFieldSecurity IIndicesPrivileges.FieldSecurity { get; set; }
		Indices IIndicesPrivileges.Names { get; set; }
		IEnumerable<string> IIndicesPrivileges.Privileges { get; set; }
		QueryContainer IIndicesPrivileges.Query { get; set; }

		public IndicesPrivilegesDescriptor<T> Names(Indices indices) => Assign(indices, (a, v) => a.Names = v);

		public IndicesPrivilegesDescriptor<T> Names(params IndexName[] indices) => Assign(indices, (a, v) => a.Names = v);

		public IndicesPrivilegesDescriptor<T> Names(IEnumerable<IndexName> indices) => Assign(indices.ToArray(), (a, v) => a.Names = v);

		public IndicesPrivilegesDescriptor<T> Privileges(params string[] privileges) => Assign(privileges, (a, v) => a.Privileges = v);

		public IndicesPrivilegesDescriptor<T> Privileges(IEnumerable<string> privileges) => Assign(privileges, (a, v) => a.Privileges = v);

		public IndicesPrivilegesDescriptor<T> FieldSecurity(Func<FieldSecurityDescriptor<T>, IFieldSecurity> fields) =>
			Assign(fields, (a, v) => a.FieldSecurity = v?.Invoke(new FieldSecurityDescriptor<T>()));

		public IndicesPrivilegesDescriptor<T> Query(Func<QueryContainerDescriptor<T>, QueryContainer> query) =>
			Assign(query, (a, v) => a.Query = v?.Invoke(new QueryContainerDescriptor<T>()));
	}
}
