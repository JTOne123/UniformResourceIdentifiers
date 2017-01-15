﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nito.UniformResourceIdentifiers.Helpers;
using static Nito.UniformResourceIdentifiers.Helpers.Util;

namespace Nito.UniformResourceIdentifiers
{
    /// <summary>
    /// An HTTP URI.
    /// </summary>
    public sealed class HttpUniformResourceIdentifier : HttpUniformResourceIdentifierBase
    {
        /// <summary>
        /// The HTTP scheme.
        /// </summary>
        public static string HttpScheme { get; } = "http";

        /// <summary>
        /// Constructs a new URI instance.
        /// </summary>
        /// <param name="host">The host name portion of the authority, if any. This is converted to lowercase. This may be <c>null</c> to indicate no host name, or the empty string to indicate an empty host name.</param>
        /// <param name="port">The port portion of the authority, if any. This may be <c>null</c> to indicate no port, or the empty string to indicate an empty port. This must be <c>null</c>, the empty string, or a valid port as defined by <see cref="Util.IsValidPort"/>.</param>
        /// <param name="pathSegments">The path segments. Dot segments are normalized. May not be <c>null</c>, neither may any element be <c>null</c>.</param>
        /// <param name="query">The query. This may be <c>null</c> to indicate no query, or the empty string to indicate an empty query.</param>
        /// <param name="fragment">The fragment. This may be <c>null</c> to indicate no fragment, or the empty string to indicate an empty fragment.</param>
        public HttpUniformResourceIdentifier(string host, string port, IEnumerable<string> pathSegments, string query, string fragment)
            : base(HttpScheme, host, port, pathSegments, query, fragment)
        {
        }

        private static readonly DelegateFactory<HttpUniformResourceIdentifier> Factory = CreateFactory(() => new HttpUniformResourceIdentifierBuilder(), x => x.Build());

        static HttpUniformResourceIdentifier()
        {
            Factories.RegisterSchemeFactory(HttpScheme, Factory);
        }

        /// <summary>
        /// Resolves a relative URI against this URI.
        /// </summary>
        /// <param name="relativeUri">The relative URI to resolve.</param>
        public HttpUniformResourceIdentifier Resolve(RelativeReference relativeUri) => Util.Resolve(this, relativeUri, Factory);

        /// <summary>
        /// Resolves a reference URI against this URI.
        /// </summary>
        /// <param name="referenceUri">The reference URI to resolve.</param>
        public UniformResourceIdentifier Resolve(UniformResourceIdentifierReference referenceUri) => Util.Resolve(this, referenceUri, Factory);

        /// <summary>
        /// Parses an HTTP URI.
        /// </summary>
        /// <param name="uri">The HTTP URI to parse.</param>
        public new static HttpUniformResourceIdentifier Parse(string uri)
        {
            var result = UniformResourceIdentifierReference.Parse(uri) as HttpUniformResourceIdentifier;
            if (result == null)
                throw new InvalidOperationException($"URI is not an HTTP URI: {uri}");
            return result;
        }
    }
}
