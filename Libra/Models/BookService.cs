using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Libra.Models {

    /// <summary>
    /// 書籍関連のサービスを提供します。
    /// 書籍ごとの個別の処理はサービスを利用してください。
    /// </summary>
    public class BookService {

        private readonly IBookRepository F_BookRepository;

        public BookService () {
            this.F_BookRepository = new BooksRepository(new BooksDbContext());
        }
        
        public BookService(IBookRepository vBookRepository) {
            this.F_BookRepository = vBookRepository;
        }

        /// <summary>
        /// 削除されていない書籍情報を全て取得します。
        /// </summary>
        /// <returns>books</returns>
        public IEnumerable<Book> GetExistBooks() {
            var books = from book in F_BookRepository.GetBooks()
                        where book.IsDeleted is 0
                        orderby book.Title
                        select book;
            return books;
        }

    }
}
