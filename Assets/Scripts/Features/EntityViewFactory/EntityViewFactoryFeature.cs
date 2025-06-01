using Features.EntityViewFactory.Systems;
using Scellecs.Morpeh.Addons.Feature;

namespace Features.EntityViewFactory
{
    public class EntityViewFactoryFeature : UpdateFeature
    {
        protected override void Initialize()
        {
            AddSystem(new EntityViewPrefabFactorySystem());
            AddSystem(new EntityViewPathFactorySystem());
        }
    }
}