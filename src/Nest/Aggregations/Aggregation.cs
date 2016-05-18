using Newtonsoft.Json;
using System.Collections.Generic;
using System;

namespace Nest
{
	/// <summary>
	/// Represents an aggregation on the request
	/// </summary>
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public interface IAggregation
	{
		string Name { get; set; }
		string TypeName { get; }
		IDictionary<string, object> Meta { get; set; }
	}

	public abstract class AggregationBase : IAggregation
	{
		string IAggregation.Name { get; set; }
		public abstract string TypeName { get; }

		private IDictionary<string, object> _meta;
		public IDictionary<string, object> Meta
		{
			get { return _meta; }
			set { _meta = NewMeta(this.TypeName, value); }
		}

		internal static IDictionary<string, object> NewMeta(string typeName, IDictionary<string, object> value = null)
		{
			var meta = new Dictionary<string, object> { { "_type", typeName } };
			if (value != null)
			{
				if (value.ContainsKey("_type"))
					throw new ArgumentException("_type is a reserved metadata key.");
				foreach (var kv in value)
					meta.Add(kv.Key, kv.Value);
			}
			return meta;
		}

		internal AggregationBase()
		{
			_meta = NewMeta(this.TypeName);
		}

		protected AggregationBase(string name) : this()
		{
			((IAggregation)this).Name = name;
		}

		internal abstract void WrapInContainer(AggregationContainer container);

		//always evaluate to false so that each side of && equation is evaluated
		public static bool operator false(AggregationBase a) => false;

		//always evaluate to false so that each side of && equation is evaluated
		public static bool operator true(AggregationBase a) => false;

		public static AggregationBase operator &(AggregationBase left, AggregationBase right)
		{
			return new AggregationCombinator(null, left, right);
		}
	}

	public abstract class AggregationDescriptorBase<TAggregationDescriptor, TAggregationInterface, T>
		: DescriptorBase<TAggregationDescriptor, TAggregationInterface>, IAggregation
		where TAggregationDescriptor : AggregationDescriptorBase<TAggregationDescriptor, TAggregationInterface, T>
			, TAggregationInterface, IAggregation
		where TAggregationInterface : class, IAggregation
		where T : class

	{
		string IAggregation.Name { get; set; }
		private IDictionary<string, object> _meta { get; set; }
		IDictionary<string, object> IAggregation.Meta
		{
			get { return _meta; }
			set { _meta = AggregationBase.NewMeta(Self.TypeName, value); }
		}

		public abstract string TypeName { get; }

		public AggregationDescriptorBase()
		{
			_meta = AggregationBase.NewMeta(Self.TypeName);
		}

		public TAggregationDescriptor Meta(Func<FluentDictionary<string, object>, FluentDictionary<string, object>> selector)
		{
			var value = selector?.Invoke(new FluentDictionary<string, object>());
			_meta = AggregationBase.NewMeta(Self.TypeName, value);
			return (TAggregationDescriptor)this;
		}
	}

	internal class AggregationCombinator : AggregationBase, IAggregation
	{
		internal List<AggregationBase> Aggregations { get; } = new List<AggregationBase>();

		public override string TypeName { get; }

		internal override void WrapInContainer(AggregationContainer container) { }

		public AggregationCombinator(string name, AggregationBase left, AggregationBase right) : base(name)
		{
			this.AddAggregation(left);
			this.AddAggregation(right);
		}

		private void AddAggregation(AggregationBase agg)
		{
			if (agg == null) return;
			var combinator = agg as AggregationCombinator;
			if ((combinator?.Aggregations.HasAny()).GetValueOrDefault(false))
			{
				this.Aggregations.AddRange(combinator.Aggregations);
			}
			else this.Aggregations.Add(agg);
		}
	}
}
