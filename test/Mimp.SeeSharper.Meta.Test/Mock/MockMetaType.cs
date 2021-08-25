using Mimp.SeeSharper.Meta.TypeProvider;

namespace Mimp.SeeSharper.Meta.Test.Mock
{
    [Mock]
    [MetaType(typeof(MockClass))]
    public class MockMetaType
    {

        [Mock]
        [Mock]
        public int Prop { get; set; }


    }
}
