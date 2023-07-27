using System.Windows.Forms;

namespace Libra {
    /// <summary>
    /// メッセージボックスのカスタム用インターフェースです。
    /// </summary>
    public interface IMessageBoxService {
        
        /// <summary>
        /// メッセージタイプ毎に既定のメッセージボックスを表示します。
        /// </summary>
        /// <param name="vMessageType"></param>
        /// <returns></returns>
        DialogResult Show(MessageTypeEnum vMessageType);

        /// <summary>
        /// メッセージタイプ毎に既定のメッセージボックスを表示します。
        /// </summary>
        /// <param name="vMessageType"></param>
        /// <param name="vAppendMessage"></param>
        /// <returns></returns>
        DialogResult Show(MessageTypeEnum vMessageType, object vAppendMessage);
    }
}