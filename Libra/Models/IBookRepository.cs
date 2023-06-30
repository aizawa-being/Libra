using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Libra.Models {
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
    }
}
