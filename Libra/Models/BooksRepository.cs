using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Libra.Models {
    /// <summary>
    /// 書籍情報テーブルのCRUD操作用クラス。
    /// CRUD操作時は、Contextではなくリポジトリを呼び出してください。
    /// </summary>
    public class BooksRepository : IBookRepository, IDisposable {
        private BooksDbContext F_Context;

        /// <summary>
        /// 新しいコンテキストインスタンスを作成します。
        /// </summary>
        /// <param name="vContext"></param>
        public BooksRepository(BooksDbContext vContext) {
            this.F_Context = vContext;
        }

        /// <summary>
        /// 書籍をIEnumerable形式で取得します。
        /// </summary>
        /// <returns>IEnumerable<Book></returns>
        public IEnumerable<Book> GetBooks() {
            return this.F_Context.Books.ToList();
        }

        /// <summary>
        /// 指定したIDの書籍情報を取得します。
        /// </summary>
        /// <param name="vBookId"></param>
        /// <returns>Book</returns>
        public Book GetBookById(int vBookId) {
            return this.F_Context.Books.Find(vBookId);
        }

        /// <summary>
        /// 書籍情報を追加します。
        /// </summary>
        /// <param name="vBook"></param>
        public void AddBook(Book vBook) {
            this.F_Context.Books.Add(vBook);
        }

        /// <summary>
        /// 書籍情報を更新します。
        /// </summary>
        /// <param name="vBook"></param>
        public void UpdateBook(Book vBook) {
            this.F_Context.Entry(vBook).State = EntityState.Modified;
        }

        /// <summary>
        /// 指定したIDの書籍情報を削除します。
        /// </summary>
        /// <param name="vBookId"></param>
        public void DeleteBook(int vBookId) {
            Book book = F_Context.Books.Find(vBookId);
            this.F_Context.Books.Remove(book);
        }

        /// <summary>
        /// DBの変更を保存します。
        /// </summary>
        public void Save() {
            this.F_Context.SaveChanges();
        }

        private bool F_disposed = false;
        /// <summary>
        /// リソースを破棄します。
        /// </summary>
        /// <param name="vDisposing"></param>
        protected virtual void Dispose(bool vDisposing) {
            if (!this.F_disposed) {
                if (vDisposing) {
                    this.F_Context.Dispose();
                }
            }
            this.F_disposed = true;
        }

        /// <summary>
        /// リソースを破棄します。
        /// </summary>
        public void Dispose() {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
