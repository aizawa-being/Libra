using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Libra.Models {
    /// <summary>
    /// メッセージボックスをラップします。
    /// </summary>
    public class MessageBoxService : IMessageBoxService {
        /// <summary>
        /// メッセージボックスを表示します。
        /// </summary>
        /// <param name="message"></param>
        /// <returns>DialogResult</returns>
        public DialogResult Show(string message) {
            return MessageBox.Show(message);
        }

        /// <summary>
        /// キャプション付きメッセージボックスを表示します。
        /// </summary>
        /// <param name="message"></param>
        /// <param name="caption"></param>
        /// <returns>DialogResult</returns>
        public DialogResult Show(string message, string caption) {
            return MessageBox.Show(message, caption);
        }

        /// <summary>
        /// キャプション付きメッセージボックスを表示します。
        /// 指定したボタンを表示します。
        /// </summary>
        /// <param name="message"></param>
        /// <param name="caption"></param>
        /// <param name="button"></param>
        /// <returns>DialogResult</returns>
        public DialogResult Show(string message, string caption, MessageBoxButtons button) {
            return MessageBox.Show(message, caption, button);
        }

        /// <summary>
        /// キャプション付きメッセージボックスを表示します。
        /// 指定したボタンとアイコンを表示します。
        /// </summary>
        /// <param name="message"></param>
        /// <param name="caption"></param>
        /// <param name="button"></param>
        /// <param name="icon"></param>
        /// <returns></returns>
        public DialogResult Show(string message, string caption, MessageBoxButtons button, MessageBoxIcon icon) {
            return MessageBox.Show(message, caption, button, icon);
        }
    }
}
