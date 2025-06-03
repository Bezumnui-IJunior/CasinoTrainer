using Scellecs.Morpeh;
using UnityEngine;
using UnityEngine.UI;

namespace View
{
    public class RequestButton<T> : MonoBehaviour where T : struct, IRequestData
    {
        [SerializeField] private Button _button;
        private Request<T> _takeCardRequest;

        private void Awake()
        {
            _takeCardRequest = World.Default.GetRequest<T>();
        }

        private void OnEnable()
        {
            _button.onClick.AddListener(OnButtonClick);
        }

        private void OnDisable()
        {
            _button.onClick.RemoveListener(OnButtonClick);
        }

        private void OnButtonClick()
        {
            _takeCardRequest.Publish(new T());
        }
    }
}