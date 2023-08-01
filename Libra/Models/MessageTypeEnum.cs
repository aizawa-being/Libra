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
        /// 貸出中エラー
        /// </summary>
        IsBorrowed,

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
        /// 予期せぬエラー
        /// </summary>
        UnexpectedError,
    }
}
