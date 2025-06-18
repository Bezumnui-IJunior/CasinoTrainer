using UnityEngine;

namespace Windows
{
    public interface IWindowsManager
    {
        RectTransform RootUi { get; }
        void Open(WindowsId id);
        void Close(WindowsId id);
        void CloseAll();
        void SetRootUI(RectTransform newRoot);
        void OpenOrLeaveOnly(params WindowsId[] ids);
    }
}