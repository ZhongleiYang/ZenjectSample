using System;
using System.Collections.Generic;
using ModestTree.Zenject;
using NUnit.Framework;
using TestAssert=NUnit.Framework.Assert;

namespace ModestTree.Zenject.Test
{
    [TestFixture]
    public class TestTestOptional : TestWithContainer
    {
        class Test1
        {
        }

        class Test2
        {
            [Inject] public Test1 val1 = null;
        }

        class Test3
        {
            [InjectOptional] public Test1 val1 = null;
        }

        [Test]
        [ExpectedException(typeof(ZenjectResolveException))]
        public void TestFieldRequired()
        {
            _container.Bind<Test2>().ToSingle();

            var test1 = _container.Resolve<Test2>();
        }

        [Test]
        public void TestFieldOptional()
        {
            _container.Bind<Test3>().ToSingle();

            var test = _container.Resolve<Test3>();
            Assert.That(test.val1 == null);
        }

        [Test]
        public void TestFieldOptional2()
        {
            _container.Bind<Test3>().ToSingle();

            var test1 = new Test1();
            _container.Bind<Test1>().ToSingle(test1);

            TestAssert.AreEqual(_container.Resolve<Test3>().val1, test1);
        }

        class Test4
        {
            public Test4(Test1 val1)
            {
            }
        }

        class Test5
        {
            public Test1 Val1;

            public Test5(
                [InjectOptional]
                Test1 val1)
            {
                Val1 = val1;
            }
        }

        [Test]
        [ExpectedException(typeof(ZenjectResolveException))]
        public void TestParameterRequired()
        {
            _container.Bind<Test4>().ToSingle();
            var test1 = _container.Resolve<Test4>();
        }

        [Test]
        public void TestParameterOptional()
        {
            _container.Bind<Test5>().ToSingle();

            var test = _container.Resolve<Test5>();
            Assert.That(test.Val1 == null);
        }

        class Test6
        {
            public Test6(Test2 test2)
            {
            }
        }

        [Test]
        [ExpectedException(typeof(ZenjectResolveException))]
        public void TestChildDependencyOptional()
        {
            _container.Bind<Test6>().ToSingle();
            _container.Bind<Test2>().ToSingle();

            _container.Resolve<Test6>();
        }
    }
}



