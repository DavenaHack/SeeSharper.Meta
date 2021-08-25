using Mimp.SeeSharper.Meta.Abstraction;
using Mimp.SeeSharper.Reflection;
using Mimp.SeeSharper.TypeProvider.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Mimp.SeeSharper.Meta.TypeProvider
{
    public class TypeConventionMetaProvider : MetaTypeMetaProvider
    {


        public ITypeProvider TypeProvider { get; }

        protected TypeConvention? Convention { get; set; }


        public TypeConventionMetaProvider(ITypeProvider typeProvider, IMetaProvider metaProvider)
            : base(metaProvider)
        {
            TypeProvider = typeProvider ?? throw new ArgumentNullException(nameof(typeProvider));
        }


        public void AddConvention(TypeConvention convention)
        {
            if (convention is null)
                throw new ArgumentNullException(nameof(convention));

            var old = Convention;
            Convention = old is null ? convention
                : (f, t) => old(f, t) || convention(f, t);
        }


        protected override IEnumerable<Type> GetMetaTypes(Type type)
        {
            try
            {
                return Convention is null ? Array.Empty<Type>()
                    : TypeProvider.GetTypes()
                        .Where(t => Convention(t, type));
            }
            catch (Exception ex)
            {
                throw new MetaProvideException($"Can't provide metas: {ex.Message}", ex);
            }
        }


        #region Conventions


        public static bool MetaTypeAttributeConvention(Type type, Type searchType) =>
            type.TryGetSingleCustomAttribute<MetaTypeAttribute>(true, out var attr)
                && attr!.Type.Inherit(searchType);

        public static bool EndsWithTypeNameConvention(Type type, Type searchType) =>
            type != searchType && type.Name.EndsWith(searchType.Name);

        public static bool StartsWithTypeNameAndEndsWithMeta(Type type, Type searchType) =>
            type.Name.StartsWith(searchType.Name) && type.Name.EndsWith("Meta");


        #endregion


    }
}
