using System.Windows.Forms;

namespace Libra {
    /// <summary>
    /// メッセージボックスを表示します。
    /// </summary>
    public class MessageBoxUtil : IMessageBoxUtil {

        /// <summary>
        /// メッセージタイプ毎に既定のメッセージボックスを表示します。
        /// </summary>
        /// <param name="vMessageType"></param>
        /// <param name="vAppendMessage"></param>
        /// <returns></returns>
        public DialogResult Show(MessageTypeEnum vMessageType, object vAppendMessage = null) {
            switch (vMessageType) {
                case MessageTypeEnum.BookInfoUnacquiredError:
                    // 書籍情報未取得
                    return MessageBox.Show(MessageConst.C_BookInfoUnacquiredError,
                                           MessageConst.C_BookInfoUnacquiredErrorCaption,
                                           MessageBoxButtons.OK,
                                           MessageBoxIcon.Information);

                case MessageTypeEnum.BookNotFound:
                    // 書籍情報が見つからない
                    return MessageBox.Show(MessageConst.C_BookNotFound,
                                           MessageConst.C_BookNotFoundCaption,
                                           MessageBoxButtons.OK,
                                           MessageBoxIcon.Information);

                case MessageTypeEnum.DeleteConfirmation:
                    // 書籍確認メッセージ
                    return MessageBox.Show(string.Format(MessageConst.C_DeleteConfirmation, vAppendMessage),
                                           MessageConst.C_DeleteConfirmationCaption,
                                           MessageBoxButtons.OKCancel,
                                           MessageBoxIcon.Information,
                                           MessageBoxDefaultButton.Button2);

                case MessageTypeEnum.DeleteWhileBorrowed:
                    // 貸出中に削除不可エラー
                    return MessageBox.Show(string.Format(MessageConst.C_DeleteWhileBorrowed, vAppendMessage),
                                           MessageConst.C_DeleteWhileBorrowedCaption,
                                           MessageBoxButtons.OK,
                                           MessageBoxIcon.Information);

                case MessageTypeEnum.AlreadyDeleted:
                    // 削除済みエラー
                    return MessageBox.Show(string.Format(MessageConst.C_AlreadyDeleted, vAppendMessage),
                                           MessageConst.C_AlreadyDeletedCaption,
                                           MessageBoxButtons.OK,
                                           MessageBoxIcon.Information);

                case MessageTypeEnum.AlreadyBorrowed:
                    // 既に貸出中エラー
                    return MessageBox.Show(string.Format(MessageConst.C_AlreadyBorrowed, vAppendMessage),
                                           MessageConst.C_AlreadyBorrowedCaption,
                                           MessageBoxButtons.OK,
                                           MessageBoxIcon.Information);

                case MessageTypeEnum.UserNameNotInput:
                    // 利用者名未入力エラー
                    return MessageBox.Show(MessageConst.C_UserNameNotInput,
                                           MessageConst.C_UserNameNotInputCaption,
                                           MessageBoxButtons.OK,
                                           MessageBoxIcon.Information);

                case MessageTypeEnum.NotBorrowed:
                    // 貸出中ではないエラー
                    return MessageBox.Show(string.Format(MessageConst.C_NotBorrowed, vAppendMessage),
                                           MessageConst.C_NotBorrowedCaption,
                                           MessageBoxButtons.OK,
                                           MessageBoxIcon.Information);

                case MessageTypeEnum.DbError:
                    // DBエラー
                    return MessageBox.Show(MessageConst.C_DbError,
                                           MessageConst.C_DbErrorCaption,
                                           MessageBoxButtons.OK,
                                           MessageBoxIcon.Error);

                case MessageTypeEnum.ClientError:
                    // クライアントエラー
                    return MessageBox.Show(MessageConst.C_ClientError,
                                           MessageConst.C_ClientErrorCaption,
                                           MessageBoxButtons.OK,
                                           MessageBoxIcon.Error);

                case MessageTypeEnum.NetworkError:
                    // ネットワークエラー
                    return MessageBox.Show(MessageConst.C_NetworkError,
                                           MessageConst.C_NetworkErrorCaption,
                                           MessageBoxButtons.OK,
                                           MessageBoxIcon.Error);

                case MessageTypeEnum.ServerError:
                    // サーバーエラー
                    return MessageBox.Show(MessageConst.C_ServerError,
                                           MessageConst.C_ServerErrorCaption,
                                           MessageBoxButtons.OK,
                                           MessageBoxIcon.Error);

                case MessageTypeEnum.UnexpectedError:
                    // 予期せぬエラー
                    return MessageBox.Show(string.Format(MessageConst.C_UnexpectedError, vAppendMessage),
                                           MessageConst.C_UnexpectedErrorCaption,
                                           MessageBoxButtons.OK,
                                           MessageBoxIcon.Error);

                default:
                    // 予期せぬエラー
                    return MessageBox.Show(string.Format(MessageConst.C_UnexpectedError, vAppendMessage),
                                           MessageConst.C_UnexpectedErrorCaption,
                                           MessageBoxButtons.OK,
                                           MessageBoxIcon.Error);
            }
        }
    }
}