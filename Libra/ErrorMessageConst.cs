namespace Libra {
    /// <summary>
    /// エラーメッセージの定数クラスです。
    /// </summary>
    public static class ErrorMessageConst {
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
        /// API接続時の予期せぬエラーメッセージ
        /// エラー内容を指定してください
        /// </summary>
        public const string C_UnexpectedError = "予期せぬエラーが発生しました。\r\nエラー：{0}";

        /// <summary>
        /// 予期せぬエラーキャプション
        /// </summary>
        public const string C_UnexpectedErrorCaprion = "予期せぬエラー";

        /// <summary>
        /// 書籍情報未取得エラーメッセージ
        /// </summary>
        public const string C_BookInfoUnacquiredError = "書籍情報がありません。\r\n書籍情報取得後、再度実行してください。";

        /// <summary>
        /// 書籍情報未取得エラーキャプション
        /// </summary>
        public const string C_BookInfoUnacquiredErrorCaprion = "書籍の追加に失敗しました";

        /// <summary>
        /// データベースエラーメッセージ
        /// </summary>
        public const string C_DbError = "データベースエラーが発生しました。\r\n恐れ入りますが、再度お試しください。";

        /// <summary>
        /// データベースエラーキャプション
        /// </summary>
        public const string C_DbErrorCaprion = "データベースエラー";
    }
}