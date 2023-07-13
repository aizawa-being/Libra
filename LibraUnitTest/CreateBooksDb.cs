using System.Data.SQLite;
using System.Linq;

using Libra;

namespace LibraUnitTest {
    public class CreateBooksDb {

        /// <summary>
        /// インメモリデータベースを構築します。
        /// </summary>
        /// <returns></returns>
        public BooksDbContext CreateInMemoryDb() {
            // メモリ上にDBを構築する
            string connectionString = "Data Source=:memory:;Version=3;New=True;";

            var connection = new SQLiteConnection(connectionString);
            connection.Open();

            var dbContext = new BooksDbContext(connection, true);

            bool tableExists = dbContext.Database.SqlQuery<int>(
                "SELECT 1 " +
                "FROM sqlite_master " +
                "WHERE type='table' AND name='Book'"
                ).Any();

            if (!tableExists) {
                dbContext.Database.ExecuteSqlCommand(@"
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
            SetDefaultBooks(dbContext);
            dbContext.SaveChanges();

            return dbContext;
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
