using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Libra.Models.BooksDataSet;

namespace Libra.Models {
    public class BooksTable {
        private BooksDataTable F_Books;

        public BooksTable() {
            this.F_Books = new BooksDataTable();
        }

        public BooksDataTable GetBooksTable() {
            return this.F_Books;
        }

        public void SetBooksTable (BooksDataTable vBooksTable) {
            this.F_Books = vBooksTable;
        }
    }
}
