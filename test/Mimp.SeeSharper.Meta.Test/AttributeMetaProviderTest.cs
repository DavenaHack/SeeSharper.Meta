using Microsoft.VisualStudio.TestTools.UnitTesting;
using Mimp.SeeSharper.Meta.Abstraction;
using Mimp.SeeSharper.Meta.Test.Mock;
using System.Linq;

namespace Mimp.SeeSharper.Meta.Test
{
    [TestClass]
    public class AttributeMetaProviderTest
    {

        [TestMethod]
        public void TestProvide()
        {

            var provider = new AttributeMetaProvider();

            var metas = provider.GetMetas<MockClass>();
            Assert.IsTrue(metas.Count() == 2 && metas.All(m => m is MockAttribute));

            metas = provider.GetMetas(typeof(MockClass).GetProperty(nameof(MockClass.Prop)));
            Assert.IsTrue(metas.Count() == 1 && metas.All(m => m is MockAttribute));

            var ametas = provider.GetMetas<MockMetaType, MockAttribute>();
            Assert.IsTrue(metas.Count() == 1 && metas.All(m => m is MockAttribute));
        }

    }
}
