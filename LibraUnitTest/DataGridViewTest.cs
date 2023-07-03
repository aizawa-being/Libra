using Libra.Controls;
using Libra.Models;
using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraUnitTest {
    [TestFixture]
    class DataGridViewTest {
        [Test]
        public void 書籍一覧グリッド表示テスト() {

            var libraController = new LibraController();

            var books = new List<Book> {
                new Book { BookId = 1,
                           Title = "テストタイトル1",
                           Author = "テスト著者1",
                           Barcode = "0000000000001" },
                new Book { BookId = 2,
                           Title = "テストタイトル2",
                           Author = "テスト著者2",
                           Barcode = "0000000000002" },
                new Book { BookId = 3,
                           Title = "テストタイトル3",
                           Author = "テスト著者3",
                           Barcode = "0000000000003" }
            };

            libraController.SetBooksDataTable(books);
            var booksDataTable = libraController.GetBooksDateTable();

            Assert.AreEqual(3, booksDataTable.Count);
        }
    }
}
