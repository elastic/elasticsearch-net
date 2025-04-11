// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System;
using System.Linq.Expressions;
using System.Text.Json;
using System.Text.Json.Serialization;

using Elastic.Clients.Elasticsearch.Serialization;
using Elastic.Transport;

namespace Elastic.Clients.Elasticsearch;

public readonly partial struct SortOptionsDescriptor<TDocument>
{
	public SortOptionsDescriptor<TDocument> Field(Field field)
	{
		Instance.Field = new FieldSort(field);
		return this;
	}

	public SortOptionsDescriptor<TDocument> Field(Expression<Func<TDocument, object?>> field)
	{
		Instance.Field = new FieldSort(field);
		return this;
	}

	public SortOptionsDescriptor<TDocument> Field(Field field, Action<FieldSortDescriptor<TDocument>>? action)
	{
		Instance.Field = new FieldSort(field);

		if (action is null)
		{
			return this;
		}

		var descriptor = new FieldSortDescriptor<TDocument>(Instance.Field);
		action.Invoke(descriptor);

		return this;
	}

	public SortOptionsDescriptor<TDocument> Field(Expression<Func<TDocument, object?>> field, Action<FieldSortDescriptor<TDocument>>? action)
	{
		Instance.Field = new FieldSort(field);

		if (action is null)
		{
			return this;
		}

		var descriptor = new FieldSortDescriptor<TDocument>(Instance.Field);
		action.Invoke(descriptor);

		return this;
	}

	public SortOptionsDescriptor<TDocument> Field(Field field, SortOrder order)
	{
		Instance.Field = new FieldSort(field)
		{
			Order = order
		};

		return this;
	}

	public SortOptionsDescriptor<TDocument> Field(Expression<Func<TDocument, object?>> field, SortOrder order)
	{
		Instance.Field = new FieldSort(field)
		{
			Order = order
		};

		return this;
	}

	public SortOptionsDescriptor<TDocument> Field(Field field, SortOrder order, Action<FieldSortDescriptor<TDocument>>? action)
	{
		Instance.Field = new FieldSort(field)
		{
			Order = order
		};

		if (action is null)
		{
			return this;
		}

		var descriptor = new FieldSortDescriptor<TDocument>(Instance.Field);
		action.Invoke(descriptor);

		return this;
	}

	public SortOptionsDescriptor<TDocument> Field(Expression<Func<TDocument, object?>> field, SortOrder order, Action<FieldSortDescriptor<TDocument>>? action)
	{
		Instance.Field = new FieldSort(field)
		{
			Order = order
		};

		if (action is null)
		{
			return this;
		}

		var descriptor = new FieldSortDescriptor<TDocument>(Instance.Field);
		action.Invoke(descriptor);

		return this;
	}
}

public readonly partial struct SortOptionsDescriptor
{
	public SortOptionsDescriptor Field(Field field)
	{
		Instance.Field = new FieldSort(field);
		return this;
	}

	public SortOptionsDescriptor Field<T>(Expression<Func<T, object?>> field)
	{
		Instance.Field = new FieldSort(field);
		return this;
	}

	public SortOptionsDescriptor Field(Field field, Action<FieldSortDescriptor>? action)
	{
		Instance.Field = new FieldSort(field);

		if (action is null)
		{
			return this;
		}

		var descriptor = new FieldSortDescriptor(Instance.Field);
		action.Invoke(descriptor);

		return this;
	}

	public SortOptionsDescriptor Field<T>(Field field, Action<FieldSortDescriptor<T>>? action)
	{
		Instance.Field = new FieldSort(field);

		if (action is null)
		{
			return this;
		}

		var descriptor = new FieldSortDescriptor<T>(Instance.Field);
		action.Invoke(descriptor);

		return this;
	}

	public SortOptionsDescriptor Field<T>(Expression<Func<T, object?>> field, Action<FieldSortDescriptor<T>>? action)
	{
		Instance.Field = new FieldSort(field);

		if (action is null)
		{
			return this;
		}

		var descriptor = new FieldSortDescriptor<T>(Instance.Field);
		action.Invoke(descriptor);

		return this;
	}

	public SortOptionsDescriptor Field(Field field, SortOrder order)
	{
		Instance.Field = new FieldSort(field)
		{
			Order = order
		};

		return this;
	}

	public SortOptionsDescriptor Field<T>(Expression<Func<T, object?>> field, SortOrder order)
	{
		Instance.Field = new FieldSort(field)
		{
			Order = order
		};

		return this;
	}

	public SortOptionsDescriptor Field(Field field, SortOrder order, Action<FieldSortDescriptor>? action)
	{
		Instance.Field = new FieldSort(field)
		{
			Order = order
		};

		if (action is null)
		{
			return this;
		}

		var descriptor = new FieldSortDescriptor(Instance.Field);
		action.Invoke(descriptor);

		return this;
	}

	public SortOptionsDescriptor Field<T>(Field field, SortOrder order, Action<FieldSortDescriptor<T>>? action)
	{
		Instance.Field = new FieldSort(field)
		{
			Order = order
		};

		if (action is null)
		{
			return this;
		}

		var descriptor = new FieldSortDescriptor<T>(Instance.Field);
		action.Invoke(descriptor);

		return this;
	}

	public SortOptionsDescriptor Field<T>(Expression<Func<T, object?>> field, SortOrder order, Action<FieldSortDescriptor<T>>? action)
	{
		Instance.Field = new FieldSort(field)
		{
			Order = order
		};

		if (action is null)
		{
			return this;
		}

		var descriptor = new FieldSortDescriptor<T>(Instance.Field);
		action.Invoke(descriptor);

		return this;
	}
}
