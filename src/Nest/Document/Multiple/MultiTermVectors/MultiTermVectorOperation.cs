using System;
using Newtonsoft.Json;

namespace Nest
{
	public interface IMultiTermVectorOperation
	{
		[JsonProperty("doc")]
		object Document { get; set; }

		[JsonProperty("field_statistics")]
		bool? FieldStatistics { get; set; }

		[JsonProperty("filter")]
		ITermVectorFilter Filter { get; set; }

		[JsonProperty("_id")]
		Id Id { get; set; }

		[JsonProperty("_index")]
		IndexName Index { get; set; }

		[JsonProperty("offsets")]
		bool? Offsets { get; set; }

		[JsonProperty("payloads")]
		bool? Payloads { get; set; }

		[JsonProperty("positions")]
		bool? Positions { get; set; }

		[JsonProperty("fields")]
		Fields StoredFields { get; set; }

		[JsonProperty("term_statistics")]
		bool? TermStatistics { get; set; }

		[JsonProperty("_type")]
		TypeName Type { get; set; }
	}

	public class MultiTermVectorOperation<T> : IMultiTermVectorOperation
		where T : class
	{
		public MultiTermVectorOperation(Id id)
		{
			Id = id;
			Index = typeof(T);
			Type = typeof(T);
		}

		public object Document { get; set; }
		public bool? FieldStatistics { get; set; }
		public ITermVectorFilter Filter { get; set; }
		public Id Id { get; set; }

		public IndexName Index { get; set; }
		public bool? Offsets { get; set; }
		public bool? Payloads { get; set; }
		public bool? Positions { get; set; }
		public Fields StoredFields { get; set; }
		public bool? TermStatistics { get; set; }
		public TypeName Type { get; set; }
	}

	public class MultiTermVectorOperationDescriptor<T>
		: DescriptorBase<MultiTermVectorOperationDescriptor<T>, IMultiTermVectorOperation>, IMultiTermVectorOperation
		where T : class
	{
		object IMultiTermVectorOperation.Document { get; set; }
		bool? IMultiTermVectorOperation.FieldStatistics { get; set; }
		ITermVectorFilter IMultiTermVectorOperation.Filter { get; set; }
		Id IMultiTermVectorOperation.Id { get; set; }
		IndexName IMultiTermVectorOperation.Index { get; set; } = typeof(T);
		bool? IMultiTermVectorOperation.Offsets { get; set; }
		bool? IMultiTermVectorOperation.Payloads { get; set; }
		bool? IMultiTermVectorOperation.Positions { get; set; }
		Fields IMultiTermVectorOperation.StoredFields { get; set; }
		bool? IMultiTermVectorOperation.TermStatistics { get; set; }
		TypeName IMultiTermVectorOperation.Type { get; set; } = typeof(T);

		public MultiTermVectorOperationDescriptor<T> StoredFields(Func<FieldsDescriptor<T>, IPromise<Fields>> fields) =>
			Assign(a => a.StoredFields = fields?.Invoke(new FieldsDescriptor<T>())?.Value);

		public MultiTermVectorOperationDescriptor<T> StoredFields(Fields fields) => Assign(a => a.StoredFields = fields);

		public MultiTermVectorOperationDescriptor<T> Id(Id id) => Assign(a => a.Id = id);

		public MultiTermVectorOperationDescriptor<T> Offsets(bool offsets = true) => Assign(a => a.Offsets = offsets);

		public MultiTermVectorOperationDescriptor<T> Payloads(bool payloads = true) => Assign(a => a.Payloads = payloads);

		public MultiTermVectorOperationDescriptor<T> Positions(bool positions = true) => Assign(a => a.Positions = positions);

		public MultiTermVectorOperationDescriptor<T> TermStatistics(bool termStatistics = true) => Assign(a => a.TermStatistics = termStatistics);

		public MultiTermVectorOperationDescriptor<T> FieldStatistics(bool fieldStatistics = true) => Assign(a => a.FieldStatistics = fieldStatistics);

		public MultiTermVectorOperationDescriptor<T> Filter(Func<TermVectorFilterDescriptor, ITermVectorFilter> filterSelector) =>
			Assign(a => a.Filter = filterSelector?.Invoke(new TermVectorFilterDescriptor()));
	}
}
