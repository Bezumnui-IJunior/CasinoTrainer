using System.Collections.Generic;

namespace Windows.Configs
{
    public interface IWindowsConfig
    {
        IReadOnlyList<WindowConfig> Windows { get; }
    }
}