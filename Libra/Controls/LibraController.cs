using Libra.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Libra.Models.BooksDataSet;

namespace Libra.Controls {
    public class LibraController {

        public DataTable ShowBookList() {
            var bookService = new BookService();

            var books = bookService.GetExistBooks();
            return this.CovertToDataTable(books);
        }

        private DataTable CovertToDataTable(IEnumerable<Book> vBooks) {
            var dataTable = new BooksDataTableDataTable();
            foreach (var book in vBooks) {
                dataTable.Rows.Add(book.BookId,
                                   book.Title,
                                   book.Author,
                                   book.Publisher,
                                   book.Description,
                                   book.UserName);
            }
            return dataTable;
        }
    }
}
