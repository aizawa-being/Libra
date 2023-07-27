namespace Libra {
    /// <summary>
    /// 書籍エラー定義クラス
    /// </summary>
    public class BookErrorDefine {
        public MessageTypeEnum FMessageType { get; set; }

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
                    this.FMessageType = MessageTypeEnum.AlreadyDeleted;
                    return;

                case ErrorTypeEnum.IsBorrowed:
                    // 貸出中エラー
                    this.FMessageType = MessageTypeEnum.IsBorrowed;
                    return;

                default:
                    // 予期せぬエラー
                    this.FMessageType = MessageTypeEnum.UnexpectedError;
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
