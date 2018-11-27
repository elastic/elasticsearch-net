using System;
using System.Runtime.Serialization;

namespace Nest
{
	[DataContract]
	[JsonConverter(typeof(ReserializeJsonConverter<TransformContainer, ITransformContainer>))]
	public interface ITransformContainer
	{
		[DataMember(Name ="chain")]
		IChainTransform Chain { get; set; }

		[DataMember(Name ="script")]
		IScriptTransform Script { get; set; }

		[DataMember(Name ="search")]
		ISearchTransform Search { get; set; }
	}

	[DataContract]
	public class TransformContainer : ITransformContainer, IDescriptor
	{
		internal TransformContainer() { }

		public TransformContainer(TransformBase transform)
		{
			transform.ThrowIfNull(nameof(transform));
			transform.WrapInContainer(this);
		}

		IChainTransform ITransformContainer.Chain { get; set; }
		IScriptTransform ITransformContainer.Script { get; set; }

		ISearchTransform ITransformContainer.Search { get; set; }
	}

	public class TransformDescriptor : TransformContainer
	{
		private TransformDescriptor Assign(Action<ITransformContainer> assigner) => Fluent.Assign(this, assigner);

		public TransformDescriptor Search(Func<SearchTransformDescriptor, ISearchTransform> selector) =>
			Assign(a => a.Search = selector?.InvokeOrDefault(new SearchTransformDescriptor()));

		public TransformDescriptor Script(Func<ScriptTransformDescriptor, IScriptTransform> selector) =>
			Assign(a => a.Script = selector?.Invoke(new ScriptTransformDescriptor()));

		public TransformDescriptor Chain(Func<ChainTransformDescriptor, IChainTransform> selector) =>
			Assign(a => a.Chain = selector.Invoke(new ChainTransformDescriptor()));
	}
}
