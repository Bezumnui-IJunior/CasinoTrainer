using System;
using Scellecs.Morpeh.Addons.Feature;

namespace Infrastructure
{
    public interface IFeatureFactory
    {
        T Create<T>();
        BaseFeature Create(Type type);
    }
}