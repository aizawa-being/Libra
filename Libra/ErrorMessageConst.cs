namespace Libra {
    public static class ErrorMessageConst {
        /// <summary>
        /// 書籍情報エラーメッセージ
        /// </summary>
        public const string BookNotFound = "該当のバーコード番号の\r\n書籍情報が見つかりませんでした。\r\n入力内容を確認し再度実行してください。";
        
        /// <summary>
        /// 書籍情報エラーキャプション
        /// </summary>
        public const string BookNotFoundCaption = "書籍情報が見つかりません";

        /// <summary>
        /// ネットワーク接続エラーメッセージ
        /// </summary>
        public const string NetworkError = "ネットワーク接続エラーが発生しました。\r\n通信状況を確認し、再度実行してください。";

        /// <summary>
        /// ネットワーク接続エラーキャプション
        /// </summary>
        public const string NetworkErrorCaption = "ネットワーク接続エラー";

        /// <summary>
        /// サーバー通信エラーメッセージ
        /// </summary>
        public const string ServerError = "サーバーエラーが発生しました。\r\nしばらく経ってから再度お試しください。";

        /// <summary>
        /// サーバー通信エラーキャプション
        /// </summary>
        public const string ServerErrorCaption = "サーバー通信エラー";

        /// <summary>
        /// 予期せぬエラーメッセージ
        /// </summary>
        public const string UnexpectedError = "予期せぬエラーが発生しました。";

        /// <summary>
        /// 予期せぬエラーキャプション
        /// </summary>
        public const string UnexpectedErrorCaprion = "予期せぬエラー";
    }
}
