using System.Windows.Forms;

namespace Libra {
    /// <summary>
    /// メッセージボックスのカスタム用インターフェースです。
    /// </summary>
    public interface IMessageBoxService {

        /// <summary>
        /// メッセージボックスを表示します。
        /// </summary>
        /// <param name="vMessage"></param>
        /// <returns>DialogResult</returns>
        DialogResult Show(string vMessage);

        /// <summary>
        /// キャプション付きメッセージボックスを表示します。
        /// </summary>
        /// <param name="vMessage"></param>
        /// <param name="vCaption"></param>
        /// <returns>DialogResult</returns>
        DialogResult Show(string vMessage, string vCaption);

        /// <summary>
        /// キャプション付きメッセージボックスを表示します。
        /// 指定したボタンを表示します。
        /// </summary>
        /// <param name="vMessage"></param>
        /// <param name="vCaption"></param>
        /// <param name="vButton"></param>
        /// <returns>DialogResult</returns>
        DialogResult Show(string vMessage, string vCaption, MessageBoxButtons vButton);

        /// <summary>
        /// キャプション付きメッセージボックスを表示します。
        /// 指定したボタンとアイコンを表示します。
        /// </summary>
        /// <param name="vMessage"></param>
        /// <param name="vCaption"></param>
        /// <param name="vButton"></param>
        /// <param name="vIcon"></param>
        /// <returns></returns>
        DialogResult Show(string vMessage, string vCaption, MessageBoxButtons vButton, MessageBoxIcon vIcon);
    }
}