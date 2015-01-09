using System;

namespace Nest
{
	// THESE CLASSES HAD BEEN GENERATED ALREADY BUT NOT EXPOSED THROUGH THE CLIENT
	// WHILE ADDING THERE RESPECTIVE API's TO THE CLIENT WE RENAMED THEM TO BETTER SUITED NAMES
	// WE PUT THEM BACK HERE AS PLACEHOLDERS SO THAT WE CAN CLAIM SEMVER BACKWARDS COMPAT


	[Obsolete("Scheduled to be removed in 2.0, renamed to AliasExistsDescriptor")]
	public class IndicesExistsAliasDescriptor : AliasExistsDescriptor {}

	[Obsolete("Scheduled to be removed in 2.0, renamed to AliasExistsRequest")]
	public class IndicesExistsAliasRequest : AliasExistsRequest 
	{
		public IndicesExistsAliasRequest() : base("") {}
		public IndicesExistsAliasRequest(string name) : base(name) { }
		public IndicesExistsAliasRequest(IndexNameMarker index, string name) : base(index, name) { }
	}


	[Obsolete("Scheduled to be removed in 2.0, renamed to TypeExistsDescriptor")]
	public class IndicesExistsTypeDescriptor : TypeExistsDescriptor {}

	[Obsolete("Scheduled to be removed in 2.0, renamed to TypeExistsRequest")]
	public class IndicesExistsTypeRequest : TypeExistsRequest 
	{
		public IndicesExistsTypeRequest() : base("", ""){}
		public IndicesExistsTypeRequest(IndexNameMarker index, TypeNameMarker typeNameMarker) : base(index, typeNameMarker) { }
	}

	[Obsolete("Scheduled to be removed in 2.0, renamed to TemplateExistsRequest")]
	public class IndicesExistsTemplateRequest : TemplateExistsRequest
	{
		public IndicesExistsTemplateRequest(string name) : base(name) { }
	}

	[Obsolete("Scheduled to be removed in 2.0, renamed to TemplateExistsDescriptor")]
	public class IndicesExistsTemplateDescriptor : TemplateExistsDescriptor { }

	[Obsolete("Scheduled to be removed in 2.0, renamed to GetRepositoryDescriptor")]
	public class SnapshotGetRepositoryDescriptor : GetRepositoryDescriptor { }
	
	[Obsolete("Scheduled to be removed in 2.0, renamed to GetRepositoryRequest")]
	public class SnapshotGetRepositoryRequest : GetRepositoryRequest {}

	[Obsolete("Scheduled to be removed in 2.0, use the generic variant of this class instead")]
	public class ExplainDescriptor : ExplainDescriptor<object> { }

	[Obsolete("Scheduled to be removed in 2.0, use the generic variant of this class instead")]
	public class SearchShardsDescriptor : SearchShardsDescriptor<object> {}

	[Obsolete("Scheduled to be removed in 2.0, use MultiPercolateDescriptor instead")]
	public class MpercolateDescriptor : MultiPercolateDescriptor { }

	[Obsolete("Scheduled to be removed in 2.0, use MultiPercolateRequest instead")]
	public class MpercolateRequest : MultiPercolateRequest {}

	[Obsolete("Scheduled to be removed in 2.0, use RecoveryStatusDescriptor instead")]
	public class IndicesRecoveryDescriptor : RecoveryStatusDescriptor { }

	[Obsolete("Scheduled to be removed in 2.0, use RecoveryStatusRequest instead")]
	public class IndicesRecoveryRequest : RecoveryStatusRequest {}
}
