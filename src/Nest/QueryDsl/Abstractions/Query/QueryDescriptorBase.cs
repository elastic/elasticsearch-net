namespace Nest
{
	public abstract class QueryDescriptorBase<TDescriptor, TInterface>
		: DescriptorBase<TDescriptor, TInterface>, IQuery
		where TDescriptor : QueryDescriptorBase<TDescriptor, TInterface>, TInterface
		where TInterface : class, IQuery
	{
		protected abstract bool Conditionless { get; }

		double? IQuery.Boost { get; set; }

		bool IQuery.Conditionless => Conditionless;

		bool IQuery.IsStrict { get; set; }

		bool IQuery.IsVerbatim { get; set; }

		bool IQuery.IsWritable => Self.IsVerbatim || !Self.Conditionless;
		string IQuery.Name { get; set; }

		public TDescriptor Name(string name) => Assign(name, (a, v) => a.Name = v);

		public TDescriptor Boost(double? boost) => Assign(boost, (a, v) => a.Boost = v);

		public TDescriptor Verbatim(bool verbatim = true) => Assign(verbatim, (a, v) => a.IsVerbatim = v);

		public TDescriptor Strict(bool strict = true) => Assign(strict, (a, v) => a.IsStrict = v);
	}
}
