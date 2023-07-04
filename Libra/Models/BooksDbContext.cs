namespace Libra.Models {
    using SQLite.CodeFirst;
    using System.Data.Entity;

    public class BooksDbContext : DbContext {
        // コンテキストは、アプリケーションの構成ファイル (App.config または Web.config) から 'BooksDbContext' 
        // 接続文字列を使用するように構成されています。既定では、この接続文字列は LocalDb インスタンス上
        // の 'Libra.Models.BooksDbContext' データベースを対象としています。 
        // 
        // 別のデータベースとデータベース プロバイダーまたはそのいずれかを対象とする場合は、
        // アプリケーション構成ファイルで 'BooksDbContext' 接続文字列を変更してください。

        public BooksDbContext()
            : base("name=BooksDbContext") {
        }

        // モデルに含めるエンティティ型ごとに DbSet を追加します。Code First モデルの構成および使用の
        // 詳細については、http://go.microsoft.com/fwlink/?LinkId=390109 を参照してください。

        public virtual DbSet<Book> Books { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder) {
            var sqliteConnectionInitializer = new SqliteCreateDatabaseIfNotExists<BooksDbContext>(modelBuilder);
            Database.SetInitializer(sqliteConnectionInitializer);
        }

    }
}