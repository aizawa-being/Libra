using System;
using System.Collections.Generic;
using System.Linq;

using System.Data.Entity;
using System.Data.SQLite;
using System.IO;

namespace Libra {
    /// <summary>
    /// 書籍情報テーブルのCRUD操作用クラス。
    /// CRUD操作時は、Contextではなくリポジトリを呼び出してください。
    /// </summary>
    public class BookRepository : IBookRepository, IDisposable {
        private BooksDbContext FContext;
        private DbContextTransaction FTransaction;

        /// <summary>
        /// 新しいコンテキストインスタンスを作成します。
        /// </summary>
        /// <param name="vContext"></param>
        public BookRepository(BooksDbContext vContext) {
            this.FContext = vContext;
        }

        /// <summary>
        /// 書籍をIEnumerable形式で取得します。
        /// </summary>
        /// <returns>IEnumerable<Book></returns>
        public IEnumerable<Book> GetBooks() {
            return this.FContext.Books.ToList();
        }

        /// <summary>
        /// 指定したIDの書籍情報を取得します。
        /// </summary>
        /// <param name="vBookId"></param>
        /// <returns>Book</returns>
        public Book GetBookById(int vBookId) {
            return this.FContext.Books.Find(vBookId);
        }

        /// <summary>
        /// 書籍情報を追加します。
        /// </summary>
        /// <param name="vBook"></param>
        public void AddBook(Book vBook) {
            this.FContext.Books.Add(vBook);
        }

        /// <summary>
        /// 書籍情報を更新します。
        /// </summary>
        /// <param name="vBook"></param>
        public void UpdateBook(Book vBook) {
            this.FContext.Entry(vBook).State = EntityState.Modified;
        }

        /// <summary>
        /// 指定したIDの書籍情報を削除します。
        /// </summary>
        /// <param name="vBookId"></param>
        public void DeleteBook(int vBookId) {
            Book wBook = FContext.Books.Find(vBookId);
            this.FContext.Books.Remove(wBook);
        }

        /// <summary>
        /// DBの変更を保存します。
        /// </summary>
        public void Save() {
            this.FContext.SaveChanges();
        }

        /// <summary>
        /// トランザクションを開始します。
        /// </summary>
        /// <returns></returns>
        public void BeginTransaction() {
            this.FTransaction = this.FContext.Database.BeginTransaction();
        }
        
        /// <summary>
        /// トランザクションをコミットします。
        /// </summary>
        public void CommitTransaction() {
            this.FTransaction.Commit();
            this.FTransaction.Dispose();
            this.FTransaction = null;
        }
        
        /// <summary>
        /// トランザクションをロールバックします。
        /// </summary>
        public void RollbackTransaction() {
            this.FTransaction.Rollback();
            this.FTransaction.Dispose();
            this.FTransaction = null;
        }

        /// <summary>
        /// データベースが存在するか確認します。
        /// </summary>
        /// <returns></returns>
        public bool DatabaseExists() {
            var wConnectionString = this.FContext.Database.Connection.ConnectionString;
            var wDataSource = new SQLiteConnectionStringBuilder(wConnectionString).DataSource;
            return File.Exists(wDataSource);
        }

        /// <summary>
        /// スキーマ情報のキャッシュをリフレッシュします。
        /// </summary>
        public void InitializeDatabase() {
            this.FContext.Database.Initialize(true);
        }

        private bool FDisposed = false;
        /// <summary>
        /// リソースを破棄します。
        /// </summary>
        /// <param name="vDisposing"></param>
        protected virtual void Dispose(bool vDisposing) {
            if (!this.FDisposed) {
                if (vDisposing) {
                    this.FContext.Dispose();
                }
            }
            this.FDisposed = true;
        }

        /// <summary>
        /// リソースを破棄します。
        /// </summary>
        public void Dispose() {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
