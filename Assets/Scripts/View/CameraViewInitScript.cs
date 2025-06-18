using DG.Tweening;
using UnityEngine;

public class CameraViewInitScript : MonoBehaviour
{
    [SerializeField] private Vector3 _moveDestination = new Vector3(0, 1.43f, -2.21f);
    [SerializeField] private Vector3 _rotateDestination = new Vector3(35.879f, 0, 0);
    [SerializeField] private float _delay = .5f;
    [SerializeField] private float _timeToMove = 2;
    [SerializeField] private float _timeToRotate = 1;

    private void Awake()
    {
        transform.DOMove(_moveDestination, _timeToMove).SetDelay(_delay);
        transform.DORotate(_rotateDestination, _timeToRotate).SetDelay(_delay);
    }
}
