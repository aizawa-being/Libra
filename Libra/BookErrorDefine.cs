using System.Windows.Forms;

namespace Libra {
    /// <summary>
    /// 書籍エラー列挙型クラス
    /// </summary>
    public class BookErrorDefine {
        /// <summary>
        /// エラーメッセージ
        /// </summary>
        public string ErrorMessage { get; private set; }

        /// <summary>
        /// エラーキャプション
        /// </summary>
        public string ErrorCaption { get; private set; }

        /// <summary>
        /// メッセージボックスボタン
        /// </summary>
        public MessageBoxButtons BoxButton { get; private set; }

        /// <summary>
        /// メッセージボックスアイコン
        /// </summary>
        public MessageBoxIcon BoxIcon { get; private set; }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="vErrorType"></param>
        public BookErrorDefine(ErrorTypeEnum vErrorType) {
            this.SetErrorProperty(vErrorType);
        }

        /// <summary>
        /// エラー毎に値を設定します。
        /// </summary>
        /// <param name="vErrorType"></param>
        private void SetErrorProperty(ErrorTypeEnum vErrorType) {
            switch (vErrorType) {
                case ErrorTypeEnum.AlreadyDeleted:
                    // 削除済みエラー
                    this.ErrorMessage = ErrorMessageConst.C_AlreadyDeleted;
                    this.ErrorCaption = ErrorMessageConst.C_AlreadyDeletedCaption;
                    this.BoxButton = MessageBoxButtons.OK;
                    this.BoxIcon = MessageBoxIcon.Information;
                    return;

                case ErrorTypeEnum.IsBorrowed:
                    // 貸出中エラー
                    this.ErrorMessage = ErrorMessageConst.C_IsBorrowed;
                    this.ErrorCaption = ErrorMessageConst.C_IsBorrowedCaption;
                    this.BoxButton = MessageBoxButtons.OK;
                    this.BoxIcon = MessageBoxIcon.Information;
                    return;

                default:
                    // 予期せぬエラー
                    this.ErrorMessage = ErrorMessageConst.C_UnexpectedError;
                    this.ErrorCaption = ErrorMessageConst.C_UnexpectedErrorCaption;
                    this.BoxButton = MessageBoxButtons.OK;
                    this.BoxIcon = MessageBoxIcon.Error;
                    return;
            }
        }
    }

    /// <summary>
    /// エラータイプを列挙します。
    /// </summary>
    public enum ErrorTypeEnum {
        IsBorrowed,
        AlreadyDeleted,
    }
}
