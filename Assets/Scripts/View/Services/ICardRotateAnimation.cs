using UnityEngine;

namespace View.Services
{
    public interface ICardRotateAnimation
    {
        void RotateUp(Transform card, ICardViewConfig config);
    }
}