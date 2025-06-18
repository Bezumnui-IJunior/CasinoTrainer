using Features.Dealer.Components;
using Unity.IL2CPP.CompilerServices;
using View.UI.Display;

namespace View.Windows
{
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
    public class DealerScoreView : ScoreView<DealerTag> { }
}