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

using System;
using System.Linq.Expressions;

namespace Nest
{
	public abstract class ClrPropertyMappingBase<TDocument> : IClrPropertyMapping<TDocument>
		where TDocument : class
	{
		protected ClrPropertyMappingBase(Expression<Func<TDocument, object>> property) => Self.Property = property;

		protected IClrPropertyMapping<TDocument> Self => this;
		bool IClrPropertyMapping<TDocument>.Ignore { get; set; }
		string IClrPropertyMapping<TDocument>.NewName { get; set; }
		Expression<Func<TDocument, object>> IClrPropertyMapping<TDocument>.Property { get; set; }

		IPropertyMapping IClrPropertyMapping<TDocument>.ToPropertyMapping() => Self.Ignore
			? PropertyMapping.Ignored
			: new PropertyMapping { Name = Self.NewName };
	}

	public interface IClrPropertyMapping<TDocument> where TDocument : class
	{
		bool Ignore { get; set; }
		string NewName { get; set; }
		Expression<Func<TDocument, object>> Property { get; set; }

		IPropertyMapping ToPropertyMapping();
	}

	public class IgnoreClrPropertyMapping<TDocument> : ClrPropertyMappingBase<TDocument> where TDocument : class
	{
		public IgnoreClrPropertyMapping(Expression<Func<TDocument, object>> property) : base(property) => Self.Ignore = true;
	}

	public class RenameClrPropertyMapping<TDocument> : ClrPropertyMappingBase<TDocument> where TDocument : class
	{
		public RenameClrPropertyMapping(Expression<Func<TDocument, object>> property, string newName) : base(property)
		{
			newName.ThrowIfNull(nameof(newName));
			Self.NewName = newName;
		}
	}
}
