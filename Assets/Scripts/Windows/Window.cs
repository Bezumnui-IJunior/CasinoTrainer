using UnityEngine;

namespace Windows
{
    public class Window : MonoBehaviour
    {
        private void Awake()
        {
            OnAwake();
        }

        private void Start()
        {
            Initialize();
        }

        private void Update()
        {
            OnUpdate();
        }

        private void OnDestroy()
        {
            Deinitialize();
        }

        protected virtual void OnAwake() { }

        protected virtual void Initialize() { }
        protected virtual void OnUpdate() { }
        protected virtual void Deinitialize() { }

        public void Destroy() =>
            Destroy(gameObject);
    }
}