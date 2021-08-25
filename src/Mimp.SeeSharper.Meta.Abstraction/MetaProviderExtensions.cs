using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Mimp.SeeSharper.Meta.Abstraction
{
    public static class MetaProviderExtensions
    {


        public static IEnumerable<IMeta> Provide<T>(this IMetaProvider provider)
        {
            if (provider is null)
                throw new ArgumentNullException(nameof(provider));

            return provider.Provide(typeof(T));
        }


        public static IEnumerable<IMeta<TMeta>> Provide<TMeta>(this IMetaProvider provider, MemberInfo member) where TMeta : notnull
        {
            if (provider is null)
                throw new ArgumentNullException(nameof(provider));
            if (member is null)
                throw new ArgumentNullException(nameof(member));

            return provider.Provide(member, typeof(TMeta)).Cast<IMeta<TMeta>>();
        }

        public static IEnumerable<IMeta<TMeta>> Provide<T, TMeta>(this IMetaProvider provider) where TMeta : notnull =>
            provider.Provide<TMeta>(typeof(T));


        public static IEnumerable<object> GetMetas(this IMetaProvider provider, MemberInfo member) =>
            provider.Provide(member).Select(x => x.Meta);

        public static IEnumerable<object> GetMetas<T>(this IMetaProvider provider) =>
            provider.GetMetas(typeof(T));


        public static IEnumerable<TMeta> GetMetas<TMeta>(this IMetaProvider provider, MemberInfo member) where TMeta : notnull =>
            provider.Provide(member, typeof(TMeta)).Select(x => (TMeta)x.Meta);

        public static IEnumerable<TMeta> GetMetas<T, TMeta>(this IMetaProvider provider) where TMeta : notnull =>
            provider.GetMetas<TMeta>(typeof(T));


        public static object GetSingleMeta<TMeta>(this IMetaProvider provider, MemberInfo member) where TMeta : notnull
        {
            var metas = provider.GetMetas<TMeta>(member).GetEnumerator();
            if (!metas.MoveNext())
                throw new InvalidOperationException($"No {typeof(TMeta)} found.");

            var result = metas.Current;
            if (metas.MoveNext())
                throw new InvalidOperationException($"More than one {typeof(TMeta)} found.");

            return result;
        }

        public static object GetSingleMeta<T, TMeta>(this IMetaProvider provider) where TMeta : notnull =>
            provider.GetSingleMeta<TMeta>(typeof(T));


    }
}
