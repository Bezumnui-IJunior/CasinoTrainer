using Features.BlackJack.Components;
using Features.View.Components;
using Scellecs.Morpeh;

namespace View
{
    public class TakeCardButton : RequestButton<ShouldTakeCardTag>
    {
        protected override void OnButtonClick() =>
            CreateTagEntity();
    }
}