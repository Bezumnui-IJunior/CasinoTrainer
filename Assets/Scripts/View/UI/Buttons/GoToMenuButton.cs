using GameStates;
using Infrastructure;
using Unity.IL2CPP.CompilerServices;
using UnityEngine.UI;

namespace View.UI.Buttons
{
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
    public class GoToMenuButton
    {
        private readonly Button _exitButton;
        private readonly IStateMachine _stateMachine;

        public GoToMenuButton(IStateMachine stateMachine, Button exitButton)
        {
            _stateMachine = stateMachine;
            _exitButton = exitButton;
        }

        public void Enable()
        {
            _exitButton.onClick.AddListener(OnClicked);
        }

        public void Disable()
        {
            _exitButton.onClick.AddListener(OnClicked);
        }

        private void OnClicked()
        {
            _stateMachine.ChangeState<MainMenuState>();
            _exitButton.interactable = false;
        }
    }
}