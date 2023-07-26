using System.Windows.Forms;

namespace Libra {
    /// <summary>
    /// メッセージボックスをラップします。
    /// </summary>
    public class MessageBoxService : IMessageBoxService {
        /// <summary>
        /// メッセージボックスを表示します。
        /// </summary>
        /// <param name="vMessage"></param>
        /// <returns>DialogResult</returns>
        public DialogResult Show(string vMessage) {
            return MessageBox.Show(vMessage);
        }

        /// <summary>
        /// キャプション付きメッセージボックスを表示します。
        /// </summary>
        /// <param name="vMessage"></param>
        /// <param name="vCaption"></param>
        /// <returns>DialogResult</returns>
        public DialogResult Show(string vMessage, string vCaption) {
            return MessageBox.Show(vMessage, vCaption);
        }

        /// <summary>
        /// キャプション付きメッセージボックスを表示します。
        /// 指定したボタンを表示します。
        /// </summary>
        /// <param name="vMessage"></param>
        /// <param name="vCaption"></param>
        /// <param name="vButton"></param>
        /// <returns>DialogResult</returns>
        public DialogResult Show(string vMessage, string vCaption, MessageBoxButtons vButton) {
            return MessageBox.Show(vMessage, vCaption, vButton);
        }

        /// <summary>
        /// キャプション付きメッセージボックスを表示します。
        /// 指定したボタンとアイコンを表示します。
        /// </summary>
        /// <param name="vMessage"></param>
        /// <param name="vCaption"></param>
        /// <param name="vButton"></param>
        /// <param name="vIcon"></param>
        /// <returns></returns>
        public DialogResult Show(string vMessage, string vCaption, MessageBoxButtons vButton, MessageBoxIcon vIcon) {
            return MessageBox.Show(vMessage, vCaption, vButton, vIcon);
        }

        /// <summary>
        /// キャプション付きメッセージボックスを表示します。
        /// 指定したボタンとアイコンを表示します。
        /// デフォルトの選択状態を指定します。
        /// </summary>
        /// <param name="vMessage"></param>
        /// <param name="vCaption"></param>
        /// <param name="vButton"></param>
        /// <param name="vIcon"></param>
        /// <param name="vDefaultButton"></param>
        /// <returns></returns>
        public DialogResult Show(string vMessage, string vCaption, MessageBoxButtons vButton, MessageBoxIcon vIcon, MessageBoxDefaultButton vDefaultButton) {
            return MessageBox.Show(vMessage, vCaption, vButton, vIcon, vDefaultButton);
        }
    }
}