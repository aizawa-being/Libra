namespace Libra {
    using SQLite.CodeFirst;
    using System.Data.Common;
    using System.Data.Entity;

    public class BooksDbContext : DbContext {
        public virtual DbSet<Book> Books { get; set; }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public BooksDbContext()
            : base("name=BooksDbContext") {
        }

        /// <summary>
        /// コンストラクタ
        /// 既存の接続を利用してコンテキストを作成します。
        /// 第2引数にtrueを指定すると、コンテキストが破棄されるときに接続も破棄されます。
        /// </summary>
        /// <param name="vConnection"></param>
        /// <param name="vContextOwnsConnection"></param>
        public BooksDbContext(DbConnection vConnection, bool vContextOwnsConnection)
            : base(vConnection, vContextOwnsConnection) {
        }

        /// <summary>
        /// CodeFirstでデータベースを作成します。
        /// </summary>
        /// <param name="vModelBuilder"></param>
        protected override void OnModelCreating(DbModelBuilder vModelBuilder) {
            var wSqliteConnectionInitializer = new SqliteCreateDatabaseIfNotExists<BooksDbContext>(vModelBuilder);
            Database.SetInitializer(wSqliteConnectionInitializer);
        }
    }
}