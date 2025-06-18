using Features.BlackJack.Components;

namespace View
{
    public class TakeCardButton : RequestButton<PlayerConsumeRequestTag>
    {
        protected override void OnButtonClick() =>
            CreateTagEntity();
    }
}