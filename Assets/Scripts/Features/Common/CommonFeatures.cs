using Features.BlackJack.Systems;
using Features.Common.Systems;
using Scellecs.Morpeh.Addons.Feature;

namespace Features.Common
{
    public class CommonFeatures : CombinedFeature
    {
        protected override void Initialize()
        {
            AddInitializer(new TurnHolderInitializeSystem());

            AddSystem(new AddTurnHolderSystem());
            AddSystem(new ChangePositionSystem());

            AddSystem(new DisposeViewSystem());
            AddSystem(new DisposeSystem());
        }
    }
}