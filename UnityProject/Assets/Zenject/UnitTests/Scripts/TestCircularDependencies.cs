using System;
using System.Collections.Generic;
using ModestTree.Zenject;
using NUnit.Framework;
using TestAssert=NUnit.Framework.Assert;

namespace ModestTree.Zenject.Test
{
    [TestFixture]
    public class TestCircularDependencies : TestWithContainer
    {
        class Test1
        {
            [Inject]
            public Test2 test = null;
        }

        class Test2
        {
            [Inject]
            public Test2 test = null;
        }

        [Test]
        [ExpectedException]
        public void Test()
        {
            _container.Bind<Test1>().ToSingle();
            _container.Bind<Test2>().ToSingle();

            _container.Resolve<Test2>();
        }
    }
}


