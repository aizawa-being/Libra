using System;
using System.Collections.Generic;

namespace Libra {
    /// <summary>
    /// 書籍データアクセス用のインターフェースです。
    /// </summary>
    public interface IBookRepository : IDisposable {

        /// <summary>
        /// 書籍情報を取得します。
        /// </summary>
        /// <returns></returns>
        IEnumerable<Book> GetBooks();

        /// <summary>
        /// 指定したIDの書籍情報を取得する。
        /// </summary>
        /// <param name="vBookId"></param>
        /// <returns></returns>
        Book GetBookById(int vBookId);

        /// <summary>
        /// 書籍を追加する。
        /// </summary>
        /// <param name="vBook"></param>
        void AddBook(Book vBook);

        /// <summary>
        /// 書籍情報を更新する。
        /// </summary>
        /// <param name="vBook"></param>
        void UpdateBook(Book vBook);

        /// <summary>
        /// 指定したIDの書籍を削除する。
        /// </summary>
        /// <param name="vBookId"></param>
        void DeleteBook(int vBookId);

        /// <summary>
        /// データベースを保存する。
        /// </summary>
        void Save();

        /// <summary>
        /// トランザクションを開始する。
        /// </summary>
        /// <returns></returns>
        void BeginTransaction();

        /// <summary>
        /// トランザクションをコミットする。
        /// </summary>
        void CommitTransaction();

        /// <summary>
        /// トランザクションをロールバックする。
        /// </summary>
        void RollbackTransaction();

        /// <summary>
        /// データベースが存在するか確認します。
        /// </summary>
        /// <returns></returns>
        bool DatabaseExists();

        /// <summary>
        /// スキーマ情報のキャッシュをリフレッシュします。
        /// </summary>
        void InitializeDatabase();
    }
}
