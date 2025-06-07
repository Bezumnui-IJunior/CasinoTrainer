using Features.BlackJack.Components;

namespace View
{
    public class DoneButton : RequestButton<DelegateTurnToDealerRequestTag>
    {
        protected override void OnButtonClick() =>
            CreateTagEntity();
    }
}