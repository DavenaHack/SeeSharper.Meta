using Mimp.SeeSharper.Meta.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Mimp.SeeSharper.Meta
{
    public static class MetaProviderExtensions
    {


        public static IMetaProvider Union(this IMetaProvider provider, IEnumerable<IMetaProvider> providers)
        {
            if (provider is null)
                throw new ArgumentNullException(nameof(provider));
            if (providers is null)
                throw new ArgumentNullException(nameof(providers));

            return new UnionMetaProvider(new[] { provider }.Concat(providers));
        }

        public static IMetaProvider Union(this IMetaProvider provider, params IMetaProvider[] providers) =>
            provider.Union((IEnumerable<IMetaProvider>)providers);



    }
}
