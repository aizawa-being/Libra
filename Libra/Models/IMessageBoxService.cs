using System.Windows.Forms;

namespace Libra.Models {
    /// <summary>
    /// メッセージボックスのカスタム用インターフェースです。
    /// </summary>
    public interface IMessageBoxService {
        DialogResult Show(string message);
        DialogResult Show(string message, string caption);
        DialogResult Show(string message, string caption, MessageBoxButtons button);
    }
}
