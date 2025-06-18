using Windows;
using Scellecs.Morpeh;
using Unity.IL2CPP.CompilerServices;
using VContainer;

namespace View.Windows
{
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
    public class ScoreWindow : Window
    {
        private World _world;

        [Inject]
        private void Constructor(World world)
        {
            _world = world;
        }

        protected override void Initialize()
        {
            foreach (IScoreView score in GetComponents<IScoreView>())
                score.Initialize(_world);
        }
    }
}