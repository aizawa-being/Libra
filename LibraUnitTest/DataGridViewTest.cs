using System.Collections.Generic;
using System.Linq;

using NUnit.Framework;
using Moq;

using Libra;

namespace LibraUnitTest {
    [TestFixture]
    class DataGridViewTest {
        [TestCase(1234567890, "1１aAａＡあｱ! 　", "1１aAａＡあｱ! 　", "1１aAａＡあｱ! 　", "1１aAａＡあｱ! 　")]
        [TestCase(0, "", "", "", "")]
        public void 書籍一覧グリッド表示テスト(int vBookId, string vTitle, string vAuthor, string vPublisher, string vDescription) {
            var wLibraControl = new LibraControl();

            var wBooks = new List<Book> {
                new Book { BookId = vBookId,
                           Title = vTitle,
                           Author = vAuthor,
                           Publisher = vPublisher,
                           Description = vDescription },
                new Book { BookId = 2,
                           Title = "テストタイトル2",
                           Author = "テスト著者2",
                           Publisher = "テスト出版社2",
                           Description = "テスト概要2" },
                new Book { BookId = 3,
                           Title = "テストタイトル3",
                           Author = "テスト著者3",
                           Publisher = "テスト出版社3",
                           Description = "テスト概要3" }
            };
            
            var wBooksDataTable = wLibraControl.ConvertBooksDataTable(wBooks);
            
            Assert.AreEqual(3, wBooksDataTable.Count);
            Assert.AreEqual(vBookId, wBooksDataTable.First(b => b.BookId == vBookId).BookId);
            Assert.AreEqual(vTitle, wBooksDataTable.First(b => b.Title == vTitle).Title);
            Assert.AreEqual(vAuthor, wBooksDataTable.First(b => b.Author == vAuthor).Author);
            Assert.AreEqual(vPublisher, wBooksDataTable.First(b => b.Publisher == vPublisher).Publisher);
            Assert.AreEqual(vDescription, wBooksDataTable.First(b => b.Description == vDescription).Description);
        }
    }
}
