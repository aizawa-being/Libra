namespace Libra {
    /// <summary>
    /// メッセージ種別の列挙クラス
    /// </summary>
    public enum MessageTypeEnum {

        /// <summary>
        /// 書籍情報未取得エラー
        /// </summary>
        BookInfoUnacquiredError,

        /// <summary>
        /// 書籍情報が見つからないエラー
        /// </summary>
        BookNotFound,

        /// <summary>
        /// 削除確認メッセージ
        /// </summary>
        DeleteConfirmation,

        /// <summary>
        /// 貸出中に削除不可エラー
        /// </summary>
        DeleteWhileBorrowed,

        /// <summary>
        /// 削除済みエラー
        /// </summary>
        AlreadyDeleted,

        /// <summary>
        /// DBエラー
        /// </summary>
        DbError,

        /// <summary>
        /// クライアントエラー
        /// </summary>
        ClientError,

        /// <summary>
        /// ネットワークエラー
        /// </summary>
        NetworkError,

        /// <summary>
        /// サーバーエラー
        /// </summary>
        ServerError,

        /// <summary>
        /// 既に貸出中エラー
        /// </summary>
        AlreadyBorrowed,

        /// <summary>
        /// 利用者名未入力エラー
        /// </summary>
        UserNameNotInput,

        /// <summary>
        /// 貸出中ではないエラー
        /// </summary>
        NotBorrowed,

        /// <summary>
        /// 予期せぬエラー
        /// </summary>
        UnexpectedError,
    }
}
