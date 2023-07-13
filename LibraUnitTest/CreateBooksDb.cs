using System.Data.SQLite;
using System.Linq;

using Libra;

namespace LibraUnitTest {
    /// <summary>
    /// テスト用書籍DBの作成用クラスです。
    /// </summary>
    public class CreateBooksDb {

        /// <summary>
        /// インメモリデータベースを構築します。
        /// </summary>
        /// <returns></returns>
        public BooksDbContext CreateInMemoryDb() {
            // メモリ上にDBを構築する
            string wConnectionString = "Data Source=:memory:;Version=3;New=True;";

            var wConnection = new SQLiteConnection(wConnectionString);
            wConnection.Open();

            var wDbContext = new BooksDbContext(wConnection, true);

            bool wTableExists = wDbContext.Database.SqlQuery<int>(
                "SELECT 1 " +
                "FROM sqlite_master " +
                "WHERE type='table' AND name='Book'"
                ).Any();

            if (!wTableExists) {
                wDbContext.Database.ExecuteSqlCommand(@"
                    CREATE TABLE Book (
                        BookId INTEGER PRIMARY KEY,
                        Title TEXT,
                        Author TEXT,
                        Publisher TEXT,
                        Description TEXT,
                        Barcode TEXT,
                        IsDeleted INTEGER,
                        UserName TEXT,
                        BorrowingDate TEXT
                    )");
            }
            SetDefaultBooks(wDbContext);
            wDbContext.SaveChanges();

            return wDbContext;
        }

        /// <summary>
        /// DBに初期データを格納します。
        /// </summary>
        /// <param name="vDbContext"></param>
        private void SetDefaultBooks(BooksDbContext vDbContext) {
            vDbContext.Books.Add(new Book {
                BookId = 1,
                Title = "テストタイトル1",
                Author = "テスト著者1",
                Barcode = "0000000000001",
                IsDeleted = 0
            });
            vDbContext.Books.Add(new Book {
                BookId = 2,
                Title = "テストタイトル2",
                Author = "テスト著者2",
                Barcode = "0000000000002",
                IsDeleted = 0
            });
            vDbContext.Books.Add(new Book {
                BookId = 3,
                Title = "テストタイトル3",
                Author = "テスト著者3",
                Barcode = "0000000000003",
                IsDeleted = 0
            });
        }
    }
}
