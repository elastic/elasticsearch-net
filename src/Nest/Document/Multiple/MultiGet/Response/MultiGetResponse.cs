using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Newtonsoft.Json;

namespace Nest
{
	public interface IMultiGetResponse : IResponse
	{
		IReadOnlyCollection<IMultiGetHit<object>> Documents { get; }
		MultiGetHit<T> Get<T>(string id) where T : class;
		MultiGetHit<T> Get<T>(long id) where T : class;
		T Source<T>(string id) where T : class;
		T Source<T>(long id) where T : class;
		FieldValues GetFieldValues<T>(string id) where T : class;
		FieldValues GetFieldSelection<T>(long id) where T : class;
		IEnumerable<T> SourceMany<T>(IEnumerable<string> ids) where T : class;
		IEnumerable<T> SourceMany<T>(IEnumerable<long> ids) where T : class;
		IEnumerable<IMultiGetHit<T>> GetMany<T>(IEnumerable<string> ids) where T : class;
		IEnumerable<IMultiGetHit<T>> GetMany<T>(IEnumerable<long> ids) where T : class;
	}

	[JsonObject]
	//TODO validate this, ported over from ElasticContractResolver but it seems out of place
	[ContractJsonConverter(typeof(MultiGetHitJsonConverter))]
	public class MultiGetResponse : ResponseBase, IMultiGetResponse
	{
		public override bool IsValid => base.IsValid && !this._Documents.HasAny(d => d.Error != null);

		internal ICollection<IMultiGetHit<object>> _Documents { get; set; } = new List<IMultiGetHit<object>>();

		public IReadOnlyCollection<IMultiGetHit<object>> Documents => this._Documents.ToList().AsReadOnly();

		public MultiGetHit<T> Get<T>(string id) where T : class
		{
			return this.Documents.OfType<MultiGetHit<T>>().FirstOrDefault(m => m.Id == id);
		}

		public MultiGetHit<T> Get<T>(long id) where T : class
		{
			return this.Get<T>(id.ToString(CultureInfo.InvariantCulture));
		}

		public T Source<T>(string id) where T : class
		{
			var multiHit = this.Get<T>(id);
			return multiHit?.Source;
		}

		public T Source<T>(long id) where T : class
		{
			return this.Source<T>(id.ToString(CultureInfo.InvariantCulture));
		}

		public IEnumerable<T> SourceMany<T>(IEnumerable<string> ids) where T : class
		{
			var docs = this.Documents.OfType<IMultiGetHit<T>>();
			return from d in docs
				   join id in ids on d.Id equals id
				   where d.Found
				   select d.Source;
		}

		public IEnumerable<T> SourceMany<T>(IEnumerable<long> ids) where T : class
		{
			return this.SourceMany<T>(ids.Select(i => i.ToString(CultureInfo.InvariantCulture)));
		}

		public IEnumerable<IMultiGetHit<T>> GetMany<T>(IEnumerable<string> ids) where T : class
		{
			var docs = this.Documents.OfType<IMultiGetHit<T>>();
			return from d in docs
				   join id in ids on d.Id equals id
				   select d;
		}

		public IEnumerable<IMultiGetHit<T>> GetMany<T>(IEnumerable<long> ids) where T : class
		{
			return this.GetMany<T>(ids.Select(i => i.ToString(CultureInfo.InvariantCulture)));
		}

		public FieldValues GetFieldValues<T>(string id) where T : class
		{
			var multiHit = this.Get<T>(id);
			return multiHit?.Fields ?? FieldValues.Empty;
		}

		public FieldValues GetFieldSelection<T>(long id) where T : class
		{
			return this.GetFieldValues<T>(id.ToString(CultureInfo.InvariantCulture));
		}
	}
}
