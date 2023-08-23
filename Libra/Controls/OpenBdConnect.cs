using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

using System.Text.Json;

namespace Libra {
    /// <summary>
    /// OpenBD関連クラス
    /// </summary>
    public class OpenBdConnect : IOpenBdConnect {
        private readonly IHttpClient FHttpClient;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public OpenBdConnect() {
            this.FHttpClient = new HttpClientWrapper();
        }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="vHttpClient"></param>
        public OpenBdConnect(IHttpClient vHttpClient) {
            this.FHttpClient = vHttpClient;
        }

        /// <summary>
        /// ISBNコードを使用して非同期で書籍情報を取得します。
        /// </summary>
        /// <param name="vIsbn"></param>
        /// <returns>Task<HttpResponseMessage></returns>
        public async Task<HttpResponseMessage> SendRequest(string vIsbn) {
            var wBaseUrl = "https://api.openbd.jp/v1/get?isbn=";
            var wUrl = wBaseUrl + vIsbn;
            try {
                return await this.FHttpClient.GetAsync(wUrl);
            } catch (HttpRequestException) {
                return null;
            }
        }

        /// <summary>
        /// 受け取ったJson形式の文字列から書籍情報を抽出します。
        /// </summary>
        /// <param name="vBookInfo"></param>
        public Book PerseBookInfo(string vBookInfo) {
            var wJsonDocument = JsonDocument.Parse(vBookInfo);
            var wRootElement = wJsonDocument.RootElement[0];

            // 指定したISBNコードの書籍が存在しない場合
            if (wRootElement.ValueKind.ToString() == "Null") {
                return null;
            }

            var wBook = new Book();

            // 書籍名を設定
            wBook.Title = wRootElement
                .GetProperty("summary")
                .GetProperty("title")
                .GetString();

            // 著者名を設定
            wBook.Author = wRootElement
                .GetProperty("summary")
                .GetProperty("author")
                .GetString();

            // 出版社を設定
            wBook.Publisher = wRootElement
                .GetProperty("summary")
                .GetProperty("publisher")
                .GetString();

            // 概要を設定
            try {
                wBook.Description = wRootElement
                .GetProperty("onix")
                .GetProperty("CollateralDetail")
                .GetProperty("TextContent")[0]
                .GetProperty("Text")
                .GetString();
            } catch (KeyNotFoundException) {
                // Jsonファイル内にKeyが存在しない場合
                wBook.Description = "";
            }

            // ISBNコードを設定
            wBook.Barcode = wRootElement
                .GetProperty("summary")
                .GetProperty("isbn")
                .GetString();

            return wBook;
        }
    }
}