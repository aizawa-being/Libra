using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Libra {

    /// <summary>
    /// 書籍関連のサービスを提供します。
    /// 書籍関連の個別の処理はサービスで実装してください。
    /// </summary>
    public class BookService : IDisposable {
        private readonly IBookRepository FBookRepository;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public BookService () {
            this.FBookRepository = new BookRepository(new BooksDbContext());
        }
        
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="vBookRepository"></param>
        public BookService(IBookRepository vBookRepository) {
            this.FBookRepository = vBookRepository;
        }

        /// <summary>
        /// 削除されていない書籍情報を全て取得します。
        /// </summary>
        /// <returns>books</returns>
        public IEnumerable<Book> GetExistBooks() {
            var wBooks = from book in FBookRepository.GetBooks()
                         where book.IsDeleted is 0
                         orderby book.Title
                         select book;
            return wBooks;
        }

        /// <summary>
        /// リソースを破棄します。
        /// </summary>
        public void Dispose() {
            this.FBookRepository.Dispose();
        }
    }
}
