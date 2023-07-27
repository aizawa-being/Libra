namespace Libra {
    /// <summary>
    /// エラーメッセージの定数クラスです。
    /// </summary>
    public static class MessageConst {
        /// <summary>
        /// 書籍情報エラーメッセージ
        /// </summary>
        public const string C_BookNotFound = "該当のバーコード番号の\r\n書籍情報が見つかりませんでした。\r\n入力内容を確認し再度実行してください。";

        /// <summary>
        /// 書籍情報エラーキャプション
        /// </summary>
        public const string C_BookNotFoundCaption = "書籍情報が見つかりません";

        /// <summary>
        /// ネットワーク接続エラーメッセージ
        /// </summary>
        public const string C_NetworkError = "ネットワーク接続エラーが発生しました。\r\n通信状況を確認し、再度実行してください。";

        /// <summary>
        /// ネットワーク接続エラーキャプション
        /// </summary>
        public const string C_NetworkErrorCaption = "ネットワーク接続エラー";

        /// <summary>
        /// クライアントエラーメッセージ
        /// </summary>
        public const string C_ClientError = "リクエストの処理中にエラーが発生しました。\r\nしばらく経ってから再度お試しください。";

        /// <summary>
        /// クライアントエラーキャプション
        /// </summary>
        public const string C_ClientErrorCaption = "クライアントエラー";

        /// <summary>
        /// サーバー通信エラーメッセージ
        /// </summary>
        public const string C_ServerError = "サーバーエラーが発生しました。\r\nしばらく経ってから再度お試しください。";

        /// <summary>
        /// サーバー通信エラーキャプション
        /// </summary>
        public const string C_ServerErrorCaption = "サーバー通信エラー";

        /// <summary>
        /// 予期せぬエラーメッセージ
        /// エラー内容を指定してください
        /// </summary>
        public const string C_UnexpectedError = "予期せぬエラーが発生しました。\r\nエラー：{0}";

        /// <summary>
        /// 予期せぬエラーキャプション
        /// </summary>
        public const string C_UnexpectedErrorCaption = "予期せぬエラー";

        /// <summary>
        /// 書籍情報未取得エラーメッセージ
        /// </summary>
        public const string C_BookInfoUnacquiredError = "書籍情報がありません。\r\n書籍情報取得後、再度実行してください。";

        /// <summary>
        /// 書籍情報未取得エラーキャプション
        /// </summary>
        public const string C_BookInfoUnacquiredErrorCaption = "書籍の追加に失敗しました";

        /// <summary>
        /// データベースエラーメッセージ
        /// </summary>
        public const string C_DbError = "データベースエラーが発生しました。\r\n恐れ入りますが、再度お試しください。";

        /// <summary>
        /// データベースエラーキャプション
        /// </summary>
        public const string C_DbErrorCaption = "データベースエラー";

        /// <summary>
        /// 削除済みエラーメッセージ
        /// </summary>
        public const string C_AlreadyDeleted = "{0} は\r\n既に削除されています。";

        /// <summary>
        /// 削除済みエラーキャプション
        /// </summary>
        public const string C_AlreadyDeletedCaption = "削除済エラー";

        /// <summary>
        /// 貸出中エラーメッセージ
        /// </summary>
        public const string C_IsBorrowed = "{0} は\r\n貸出中の為、削除できません。";

        /// <summary>
        /// 貸出中エラーキャプション
        /// </summary>
        public const string C_IsBorrowedCaption = "貸出中エラー";

        /// <summary>
        /// 削除確認メッセージ
        /// </summary>
        public const string C_DeleteConfirmation = "{0}を\r\n本当に削除しますか？";

        /// <summary>
        /// 削除確認メッセージキャプション
        /// </summary>
        public const string C_DeleteConfirmationCaption = "削除確認メッセージ";
    }
}