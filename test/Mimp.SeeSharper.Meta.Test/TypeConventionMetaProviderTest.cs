using Microsoft.VisualStudio.TestTools.UnitTesting;
using Mimp.SeeSharper.Meta.Abstraction;
using Mimp.SeeSharper.Meta.Test.Mock;
using Mimp.SeeSharper.Meta.TypeProvider;
using Mimp.SeeSharper.TypeProvider;
using System.Linq;

namespace Mimp.SeeSharper.Meta.Test
{
    [TestClass]
    public class TypeConventionMetaProviderTest
    {

        [TestMethod]
        public void TestProvideStartsWithTypeNameAndEndsWithMeta()
        {

            var provider = new TypeConventionMetaProvider(new AssemblyTypeProvider(typeof(TypeConventionMetaProviderTest).Assembly), new AttributeMetaProvider());
            provider.AddConvention(TypeConventionMetaProvider.StartsWithTypeNameAndEndsWithMeta);

            var metas = provider.GetMetas<MockClass>();
            Assert.IsTrue(metas.Count() == 1 && metas.All(m => m is MockAttribute));

            metas = provider.GetMetas(typeof(MockClass).GetProperty(nameof(MockClass.Prop)));
            Assert.IsTrue(metas.Count() == 2 && metas.All(m => m is MockAttribute));

        }

        [TestMethod]
        public void TestProvideEndsWithTypeNameConvention()
        {

            var provider = new TypeConventionMetaProvider(new AssemblyTypeProvider(typeof(TypeConventionMetaProviderTest).Assembly), new AttributeMetaProvider());
            provider.AddConvention(TypeConventionMetaProvider.EndsWithTypeNameConvention);

            var metas = provider.GetMetas<MockClass>();
            Assert.IsTrue(metas.Count() == 1 && metas.All(m => m is MockAttribute));

            metas = provider.GetMetas(typeof(MockClass).GetProperty(nameof(MockClass.Prop)));
            Assert.IsTrue(metas.Count() == 2 && metas.All(m => m is MockAttribute));

        }

        [TestMethod]
        public void TestProvideMetaTypeAttributeConvention()
        {

            var provider = new TypeConventionMetaProvider(new AssemblyTypeProvider(typeof(TypeConventionMetaProviderTest).Assembly), new AttributeMetaProvider());
            provider.AddConvention(TypeConventionMetaProvider.MetaTypeAttributeConvention);

            var metas = provider.GetMetas<MockClass>();
            Assert.IsTrue(metas.Count() == 2 && metas.All(m => m is MockAttribute || m is MetaTypeAttribute));

            metas = provider.GetMetas(typeof(MockClass).GetProperty(nameof(MockClass.Prop)));
            Assert.IsTrue(metas.Count() == 2 && metas.All(m => m is MockAttribute));

        }

    }
}
