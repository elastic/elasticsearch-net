/*
 * Licensed to Elasticsearch B.V. under one or more contributor
 * license agreements. See the NOTICE file distributed with
 * this work for additional information regarding copyright
 * ownership. Elasticsearch B.V. licenses this file to you under
 * the Apache License, Version 2.0 (the "License"); you may
 * not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *    http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing,
 * software distributed under the License is distributed on an
 * "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY
 * KIND, either express or implied.  See the License for the
 * specific language governing permissions and limitations
 * under the License.
 */

using System.Runtime.Serialization;
using Elasticsearch.Net.Utf8Json;

namespace Nest
{
	[InterfaceDataContract]
	[JsonFormatter(typeof(FieldNameQueryFormatter<TermQuery, ITermQuery>))]
	public interface ITermQuery : IFieldNameQuery
	{
		[DataMember(Name = "value")]
		[JsonFormatter(typeof(SourceWriteFormatter<object>))]
		object Value { get; set; }

		[DataMember(Name = "case_insensitive")]
		bool? CaseInsensitive { get; set; }
	}

	[DataContract]
	public class TermQuery : FieldNameQueryBase, ITermQuery
	{
		public object Value { get; set; }
		
		public bool? CaseInsensitive { get; set; }

		protected override bool Conditionless => IsConditionless(this);

		internal override void InternalWrapInContainer(IQueryContainer c) => c.Term = this;

		internal static bool IsConditionless(ITermQuery q) => q.Value == null || q.Value.ToString().IsNullOrEmpty() || q.Field.IsConditionless();
	}

	public abstract class TermQueryDescriptorBase<TDescriptor, TInterface, T>
		: FieldNameQueryDescriptorBase<TDescriptor, TInterface, T>
			, ITermQuery
		where TDescriptor : TermQueryDescriptorBase<TDescriptor, TInterface, T>, TInterface
		where TInterface : class, ITermQuery
		where T : class
	{
		protected override bool Conditionless => TermQuery.IsConditionless(this);
		object ITermQuery.Value { get; set; }
		bool? ITermQuery.CaseInsensitive { get; set; }

		public TDescriptor Value(object value)
		{
			Self.Value = value;
			return (TDescriptor)this;
		}

		public TDescriptor CaseInsensitive(bool? caseInsensitive = true)
		{
			Self.CaseInsensitive = caseInsensitive;
			return (TDescriptor)this;
		}
	}

	public class TermQueryDescriptor<T> : TermQueryDescriptorBase<TermQueryDescriptor<T>, ITermQuery, T>
		where T : class { }
}
