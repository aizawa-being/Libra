using System.Windows.Forms;

namespace Libra {
    /// <summary>
    /// メッセージボックスのカスタム用インターフェースです。
    /// </summary>
    public interface IMessageBoxService {
        DialogResult Show(string vMessage);
        DialogResult Show(string vMessage, string vCaption);
        DialogResult Show(string vMessage, string vCaption, MessageBoxButtons vButton);
        DialogResult Show(string vMessage, string vCaption, MessageBoxButtons vButton, MessageBoxIcon vIcon);
    }
}