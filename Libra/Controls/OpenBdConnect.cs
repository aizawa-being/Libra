using Libra.Models;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace Libra.Controls {
    public class OpenBdConnect : IOpenBdConnect {
        /// <summary>
        /// ISBNコードで書籍情報を非同期に取得します
        /// </summary>
        /// <param name="vIsbn"></param>
        /// <returns></returns>
        public async Task<string> GetBookByIsbn(string vIsbn) {
            var baseUrl = "https://api.openbd.jp/v1/get?isbn=";
            var url = baseUrl + vIsbn;

            using (HttpClient client = new HttpClient()) {
                HttpResponseMessage response = await client.GetAsync(url);
                if (response.IsSuccessStatusCode) {
                    // レスポンスの取得に成功
                    return await response.Content.ReadAsStringAsync();
                } else if (response.StatusCode >= HttpStatusCode.BadRequest && response.StatusCode < HttpStatusCode.InternalServerError) {
                    // 400番台エラー発生
                    return ErrorMessageConst.NetworkError + "ステータスコード: " + response.StatusCode;
                } else if (response.StatusCode >= HttpStatusCode.InternalServerError) {
                    // 500番台エラー発生
                    return ErrorMessageConst.ServerError + "ステータスコード: " + response.StatusCode;
                } else {
                    // 予期せぬエラー
                    return ErrorMessageConst.UnexpectedError + "ステータスコード: " + response.StatusCode;
                }
            }
        }

        /// <summary>
        /// 受け取ったJson形式の文字列から書籍情報を抽出します。
        /// </summary>
        /// <param name="vBookInfo"></param>
        public Book PerseBookInfo(string vBookInfo) {
            var jsonDocument = JsonDocument.Parse(vBookInfo);
            var rootElement = jsonDocument.RootElement[0];

            // 指定したISBNコードの書籍が存在しない場合
            if (rootElement.ValueKind.ToString() == "Null") {
                return null;
            }

            var book = new Book();

            // 書籍名を設定
            book.Title = rootElement
                .GetProperty("summary")
                .GetProperty("title")
                .GetString();

            // 著者名を設定
            book.Author = rootElement
                .GetProperty("summary")
                .GetProperty("author")
                .GetString();

            // 出版社を設定
            book.Publisher = rootElement
                .GetProperty("summary")
                .GetProperty("publisher")
                .GetString();

            // 概要を設定
            try {
                book.Description = rootElement
                .GetProperty("onix")
                .GetProperty("CollateralDetail")
                .GetProperty("TextContent")[0]
                .GetProperty("Text")
                .GetString();
            } catch (KeyNotFoundException) {
                // Jsonファイル内にKeyが存在しない場合
                book.Description = "";
            }

            // ISBNコードを設定
            book.Barcode = rootElement
                .GetProperty("summary")
                .GetProperty("isbn")
                .GetString();

            return book;
        }
    }
}
