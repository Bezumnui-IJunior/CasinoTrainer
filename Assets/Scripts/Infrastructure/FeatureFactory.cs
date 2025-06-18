using System;
using Scellecs.Morpeh.Addons.Feature;
using VContainer;

namespace Infrastructure
{
    public class FeatureFactory : IFeatureFactory
    {
        private readonly IObjectResolver _resolver;

        public FeatureFactory(IObjectResolver resolver)
        {
            _resolver = resolver;
        }

        public T Create<T>() =>
            _resolver.Resolve<T>();

        public BaseFeature Create(Type type) =>
            (BaseFeature)_resolver.Resolve(type);
    }
}