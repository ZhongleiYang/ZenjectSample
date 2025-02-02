using System;
using System.Collections.Generic;
using System.Linq;
using Fasterflect;

namespace ModestTree.Zenject
{
    public class MethodProvider<T> : ProviderBase
    {
        readonly DiContainer _container;
        readonly Func<DiContainer, T> _method;

        public MethodProvider(Func<DiContainer, T> method, DiContainer container)
        {
            _method = method;
            _container = container;
        }

        public override Type GetInstanceType()
        {
            return typeof(T);
        }

        public override bool HasInstance(Type contractType)
        {
            Assert.That(typeof(T).DerivesFromOrEqual(contractType));
            return false;
        }

        public override object GetInstance(Type contractType)
        {
            Assert.That(typeof(T).DerivesFromOrEqual(contractType));
            var obj = _method(_container);

            Assert.That(obj != null, () =>
                "Method provider returned null when looking up type '{0}'. \nObject graph:\n{1}".With(typeof(T).Name(), _container.GetCurrentObjectGraph()));

            return obj;
        }

        public override IEnumerable<ZenjectResolveException> ValidateBinding()
        {
            return Enumerable.Empty<ZenjectResolveException>();
        }
    }
}
