using Features.Common.Systems;
using Scellecs.Morpeh.Addons.Feature;

namespace Features.Common
{
    public class CommonFeatures : CombinedFeature
    {
        protected override void Initialize()
        {
            AddSystem(new ChangePositionSystem());
        }
    }
}