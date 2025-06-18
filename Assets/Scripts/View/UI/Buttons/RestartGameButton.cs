using Features.BlackJack.Components;

namespace View
{
    public class RestartGameButton : RequestButton<RestartGameRequestTag>
    {
        protected override void OnButtonClick() =>
            CreateTagEntity();
    }
}