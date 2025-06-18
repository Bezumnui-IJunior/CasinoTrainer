using UnityEngine;

namespace Windows
{
    public interface IWindowsManager
    {
        void Open(WindowsId id);
        void Close(WindowsId id);
        void CloseAll();
        void SetRootUI(RectTransform newRoot);
        RectTransform RootUi { get; }
        void OpenOrLeaveOnly(params WindowsId[] ids);
    }
}