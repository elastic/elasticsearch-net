using System.Collections.Generic;
using CodeGeneration.LowLevelClient.Domain;

namespace CodeGeneration.LowLevelClient.Overrides.Descriptors
{
	/// <summary>
	/// Tweaks the generated descriptors
	/// </summary>
	public interface IDescriptorOverrides
	{
		/// <summary>
		/// Sometimes params can be defined on the body as well as on the querystring
		/// We favor specifying params on the body so here we can specify params we don't want on the querystring.
		/// </summary>
		IEnumerable<string> SkipQueryStringParams { get; }

		/// <summary>
		/// Override how the query param name is exposed to the client.
		/// </summary>
		IDictionary<string, string> RenameQueryStringParams { get; }

        /// <summary>
        /// Patches the CSharp method.
        /// </summary>
        /// <param name="method">The method.</param>
        /// <returns>patched method</returns>
        CsharpMethod PatchMethod(CsharpMethod method);
	}

    public abstract class DescriptorOverridesBase : IDescriptorOverrides
    {
        public virtual IEnumerable<string> SkipQueryStringParams
        {
            get { return null; }
        }

        public virtual IDictionary<string, string> RenameQueryStringParams
        {
            get { return null; }
        }

        public virtual CsharpMethod PatchMethod(CsharpMethod method)
        {
            return method;         
        }
    }
}