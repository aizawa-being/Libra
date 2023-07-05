using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Libra.Models.BooksDataSet;

namespace Libra.Models {
    public class BooksTable {
        public BooksDataTable Books { get; set; }

        public BooksTable() {
            this.Books = new BooksDataTable();
        }
    }
}
