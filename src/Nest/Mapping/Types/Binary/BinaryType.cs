using System.Collections.Generic;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace Nest
{
	public interface IBinaryType : IElasticType
	{

	}

	[JsonObject(MemberSerialization.OptIn)]
	public class BinaryType : ElasticType, IBinaryType
	{
		public BinaryType() : base("binary") { }
	}

	public class BinaryTypeDescriptor<T> where T : class
	{
		internal BinaryType _Mapping = new BinaryType();

		public BinaryTypeDescriptor<T> Name(string name)
		{
			this._Mapping.Name = name;
			return this;
		}
		public BinaryTypeDescriptor<T> Name(Expression<Func<T, object>> objectPath)
		{
			this._Mapping.Name = objectPath;
			return this;
		}

		public BinaryTypeDescriptor<T> IndexName(string indexName)
		{
			this._Mapping.IndexName = indexName;
			return this;
		}

		public BinaryTypeDescriptor<T> DocValues(bool docValues = true)
		{
			this._Mapping.DocValues = docValues;
			return this;
		}

		public BinaryTypeDescriptor<T> Store(bool store = true)
		{
			this._Mapping.Store = store;
			return this;
		}

		public BinaryTypeDescriptor<T> CopyTo(params string[] fields)
		{
			this._Mapping.CopyTo = fields.Select(f => (FieldName)f);
			return this;
		}

		public BinaryTypeDescriptor<T> CopyTo(params Expression<Func<T, object>>[] objectPaths)
		{
			this._Mapping.CopyTo = objectPaths.Select(e => (FieldName)e);
			return this;
		}

		public BinaryTypeDescriptor<T> Fields(Func<PropertiesDescriptor<T>, PropertiesDescriptor<T>> fieldSelector)
		{
			fieldSelector.ThrowIfNull(nameof(fieldSelector));
			var properties = fieldSelector(new PropertiesDescriptor<T>());
			foreach (var p in properties.Properties)
			{
				var value = p.Value as IElasticType;
				if (value == null)
					continue;

				_Mapping.Fields[p.Key] = value;
			}
			return this;
		}
	}
}