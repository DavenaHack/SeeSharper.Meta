using Mimp.SeeSharper.Meta.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Mimp.SeeSharper.Meta
{
    public class UnionMetaProvider : IMetaProvider
    {


        public IEnumerable<IMetaProvider> Providers { get; }


        public UnionMetaProvider(IEnumerable<IMetaProvider> providers)
        {
            Providers = providers?.Select(p => p ?? throw new ArgumentNullException(nameof(providers), "At least one provider is null."))
                .ToArray() ?? throw new ArgumentNullException(nameof(providers));
        }


        public IEnumerable<IMeta> Provide(MemberInfo member) =>
            Providers.SelectMany(p => p.Provide(member));


        public IEnumerable<IMeta> Provide(MemberInfo member, Type metaType) =>
            Providers.SelectMany(p => p.Provide(member, metaType));


    }
}
