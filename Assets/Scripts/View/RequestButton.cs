using Scellecs.Morpeh;
using UnityEngine;
using UnityEngine.UI;

namespace View
{
    public abstract class RequestButton<T> : MonoBehaviour where T : struct, IComponent
    {
        [SerializeField] private Button _button;

        protected Stash<T> Stash { get; private set; }

        private void Awake()
        {
            Stash = World.Default.GetStash<T>();
        }

        private void OnEnable()
        {
            _button.onClick.AddListener(OnButtonClick);
        }

        private void OnDisable()
        {
            _button.onClick.RemoveListener(OnButtonClick);
        }

        protected abstract void OnButtonClick();

        protected ref T CreateTagEntity()
        {
            Entity entity = World.Default.CreateEntity();
            ref T component = ref Stash.Add(entity);

            return ref component;
        }
    }
}