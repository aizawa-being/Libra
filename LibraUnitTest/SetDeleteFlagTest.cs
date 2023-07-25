﻿using System;
using System.Text.RegularExpressions;
using System.Windows.Forms;

using Moq;
using NUnit.Framework;
using System.Data.Common;
using System.Data.Entity.Infrastructure;

using Libra;

namespace LibraUnitTest {
    [TestFixture]
    public class SetDeleteFlagTest {

        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]
        public void BooksServiceで削除フラグを立てるテスト(int vBookId) {
            var wCreateBookDb = new CreateBooksDb();
            using (var wDbContext = wCreateBookDb.CreateInMemoryDb(true)) {
                // InMemoryDatabaseを利用
                IBookRepository wBookRepository = new BookRepository(wDbContext);
                var wBookService = new BookService(wBookRepository);

                var wBeforeDeleteFlag = wDbContext.Books.Find(vBookId).IsDeleted;

                wBookService.SetDeleteFlag(vBookId);

                var wAfterDeleteFlag = wDbContext.Books.Find(vBookId).IsDeleted;

                Assert.AreEqual(0, wBeforeDeleteFlag);
                Assert.AreEqual(1, wAfterDeleteFlag);
            }
        }

        [Test]
        public void BooksServiceで削除済みエラー発生テスト() {
            // ブックリポジトリのモックを作成
            var wMockRepository = new Mock<IBookRepository>();

            wMockRepository.Setup(m => m.BeginTransaction());
            wMockRepository.Setup(m => m.GetBookById(It.IsAny<int>())).Returns(new Book{ IsDeleted = 1 });
            wMockRepository.Setup(m => m.UpdateBook(It.IsAny<Book>()));
            wMockRepository.Setup(m => m.Save());
            wMockRepository.Setup(m => m.CommitTransaction());
            wMockRepository.Setup(m => m.RollbackTransaction());

            // BookServiceのモックを作成
            var wBookService = new BookService(wMockRepository.Object);

            var wException = Assert.Throws<BookOperationException>(() => wBookService.SetDeleteFlag(It.IsAny<int>()));

            Assert.AreEqual(ErrorTypeEnum.AlreadyDeleted, wException.ErrorType);
            wMockRepository.Verify(m => m.BeginTransaction(), Times.Once);
            wMockRepository.Verify(m => m.GetBookById(It.IsAny<int>()), Times.Once);
            wMockRepository.Verify(m => m.UpdateBook(It.IsAny<Book>()), Times.Never);
            wMockRepository.Verify(m => m.Save(), Times.Never);
            wMockRepository.Verify(m => m.CommitTransaction(), Times.Never);
            wMockRepository.Verify(m => m.RollbackTransaction(), Times.Once);
        }

        [Test]
        public void BooksServiceで貸出中エラー発生テスト() {
            // ブックリポジトリのモックを作成
            var wMockRepository = new Mock<IBookRepository>();

            wMockRepository.Setup(m => m.BeginTransaction());
            wMockRepository.Setup(m => m.GetBookById(It.IsAny<int>()))
                           .Returns(new Book { IsDeleted = 0,
                                               UserName = "貸出中テスト" });

            wMockRepository.Setup(m => m.UpdateBook(It.IsAny<Book>()));
            wMockRepository.Setup(m => m.Save());
            wMockRepository.Setup(m => m.CommitTransaction());
            wMockRepository.Setup(m => m.RollbackTransaction());

            // BookServiceのモックを作成
            var wBookService = new BookService(wMockRepository.Object);

            var wException = Assert.Throws<BookOperationException>(() => wBookService.SetDeleteFlag(It.IsAny<int>()));

            Assert.AreEqual(ErrorTypeEnum.IsBorrowed, wException.ErrorType);
            wMockRepository.Verify(m => m.BeginTransaction(), Times.Once);
            wMockRepository.Verify(m => m.GetBookById(It.IsAny<int>()), Times.Once);
            wMockRepository.Verify(m => m.UpdateBook(It.IsAny<Book>()), Times.Never);
            wMockRepository.Verify(m => m.Save(), Times.Never);
            wMockRepository.Verify(m => m.CommitTransaction(), Times.Never);
            wMockRepository.Verify(m => m.RollbackTransaction(), Times.Once);
        }

        [Test]
        public void BooksServiceで予期せぬエラー発生テスト() {
            // ブックリポジトリのモックを作成
            var wMockRepository = new Mock<IBookRepository>();

            wMockRepository.Setup(m => m.BeginTransaction());
            wMockRepository.Setup(m => m.GetBookById(It.IsAny<int>()))
                           .Returns(new Book {
                               IsDeleted = 0,
                           });
            wMockRepository.Setup(m => m.UpdateBook(It.IsAny<Book>()));
            wMockRepository.Setup(m => m.Save()).Throws<Exception>();
            wMockRepository.Setup(m => m.CommitTransaction());
            wMockRepository.Setup(m => m.RollbackTransaction());

            // BookServiceのモックを作成
            var wBookService = new BookService(wMockRepository.Object);

            var wException = Assert.Throws<Exception>(() => wBookService.SetDeleteFlag(It.IsAny<int>()));
            
            wMockRepository.Verify(m => m.BeginTransaction(), Times.Once);
            wMockRepository.Verify(m => m.GetBookById(It.IsAny<int>()), Times.Once);
            wMockRepository.Verify(m => m.UpdateBook(It.IsAny<Book>()), Times.Once);
            wMockRepository.Verify(m => m.Save(), Times.Once);
            wMockRepository.Verify(m => m.CommitTransaction(), Times.Never);
            wMockRepository.Verify(m => m.RollbackTransaction(), Times.Once);
        }

        [Test]
        public void LibraContolで削除フラグを立てるテスト() {
            // ブックリポジトリのモックを作成
            var wMockRepository = new Mock<IBookRepository>();

            wMockRepository.Setup(m => m.BeginTransaction());
            wMockRepository.Setup(m => m.GetBookById(It.IsAny<int>()))
                           .Returns(new Book {
                               IsDeleted = 0,
                           });
            wMockRepository.Setup(m => m.UpdateBook(It.IsAny<Book>()));
            wMockRepository.Setup(m => m.Save());
            wMockRepository.Setup(m => m.CommitTransaction());
            wMockRepository.Setup(m => m.RollbackTransaction());

            // コントローラのモック作成
            var wMockBookTable = new Mock<BooksTable>();
            var wMockMessageBoxService = new Mock<IMessageBoxService>();
            ILibraControl wLibraControl = new LibraControl(wMockBookTable.Object, wMockRepository.Object, wMockMessageBoxService.Object);

            var wResult = wLibraControl.SetDeleteFlag(1);

            Assert.IsTrue(wResult);
        }

        [Test]
        public void LibraContolで削除済みエラー発生テスト() {
            // ブックリポジトリのモックを作成
            var wMockRepository = new Mock<IBookRepository>();

            wMockRepository.Setup(m => m.BeginTransaction());
            wMockRepository.Setup(m => m.GetBookById(It.IsAny<int>()))
                           .Returns(new Book {
                               IsDeleted = 1,
                           });
            wMockRepository.Setup(m => m.UpdateBook(It.IsAny<Book>()));
            wMockRepository.Setup(m => m.Save());
            wMockRepository.Setup(m => m.CommitTransaction());
            wMockRepository.Setup(m => m.RollbackTransaction());

            // コントローラのモック作成
            var wMockBookTable = new Mock<BooksTable>();
            var wMockMessageBoxService = new Mock<IMessageBoxService>();
            ILibraControl wLibraControl = new LibraControl(wMockBookTable.Object, wMockRepository.Object, wMockMessageBoxService.Object);

            var wResult = wLibraControl.SetDeleteFlag(1);

            Assert.IsFalse(wResult);

            wMockMessageBoxService.Verify(m => m.Show(It.IsRegex($"{Regex.Escape("は\r\n既に削除されています。")}$"),
                                               ErrorMessageConst.C_AlreadyDeletedCaption,
                                               MessageBoxButtons.OK,
                                               MessageBoxIcon.Information),
                                          Times.Once);
        }

        [Test]
        public void LibraContolで貸出中エラー発生テスト() {
            // ブックリポジトリのモックを作成
            var wMockRepository = new Mock<IBookRepository>();

            wMockRepository.Setup(m => m.BeginTransaction());
            wMockRepository.Setup(m => m.GetBookById(It.IsAny<int>()))
                           .Returns(new Book {
                               IsDeleted = 0,
                               UserName = "貸出中テスト"
                           });
            wMockRepository.Setup(m => m.UpdateBook(It.IsAny<Book>()));
            wMockRepository.Setup(m => m.Save());
            wMockRepository.Setup(m => m.CommitTransaction());
            wMockRepository.Setup(m => m.RollbackTransaction());

            // コントローラのモック作成
            var wMockBookTable = new Mock<BooksTable>();
            var wMockMessageBoxService = new Mock<IMessageBoxService>();
            ILibraControl wLibraControl = new LibraControl(wMockBookTable.Object, wMockRepository.Object, wMockMessageBoxService.Object);

            var wResult = wLibraControl.SetDeleteFlag(1);

            Assert.IsFalse(wResult);

            wMockMessageBoxService.Verify(m => m.Show(It.IsRegex($"{Regex.Escape("は\r\n貸出中の為、削除できません。")}$"),
                                               ErrorMessageConst.C_IsBorrowedCaption,
                                               MessageBoxButtons.OK,
                                               MessageBoxIcon.Information),
                                          Times.Once);
        }

        [Test]
        public void LibraContolでDbException発生テスト() {
            // ブックリポジトリのモックを作成
            var wMockRepository = new Mock<IBookRepository>();

            wMockRepository.Setup(m => m.BeginTransaction());
            wMockRepository.Setup(m => m.GetBookById(It.IsAny<int>()))
                           .Returns(new Book {
                               IsDeleted = 0,
                           });
            wMockRepository.Setup(m => m.UpdateBook(It.IsAny<Book>()));
            wMockRepository.Setup(m => m.Save()).Throws<CustomDbException>();
            wMockRepository.Setup(m => m.CommitTransaction());
            wMockRepository.Setup(m => m.RollbackTransaction());

            // コントローラのモック作成
            var wMockBookTable = new Mock<BooksTable>();
            var wMockMessageBoxService = new Mock<IMessageBoxService>();
            ILibraControl wLibraControl = new LibraControl(wMockBookTable.Object, wMockRepository.Object, wMockMessageBoxService.Object);

            var wResult = wLibraControl.SetDeleteFlag(1);

            Assert.IsFalse(wResult);

            wMockMessageBoxService.Verify(m => m.Show(ErrorMessageConst.C_DbError,
                                               ErrorMessageConst.C_DbErrorCaption,
                                               MessageBoxButtons.OK,
                                               MessageBoxIcon.Error),
                                          Times.Once);
        }

        /// <summary>
        /// DbExceptionの派生クラス
        /// </summary>
        public class CustomDbException : DbException {
            public CustomDbException() : base() {
                // コンストラクタの実装
            }
        }

        [Test]
        public void LibraContolでDbUpdateException発生テスト() {
            // ブックリポジトリのモックを作成
            var wMockRepository = new Mock<IBookRepository>();

            wMockRepository.Setup(m => m.BeginTransaction());
            wMockRepository.Setup(m => m.GetBookById(It.IsAny<int>()))
                           .Returns(new Book {
                               IsDeleted = 0,
                           });
            wMockRepository.Setup(m => m.UpdateBook(It.IsAny<Book>()));
            wMockRepository.Setup(m => m.Save()).Throws<DbUpdateException>();
            wMockRepository.Setup(m => m.CommitTransaction());
            wMockRepository.Setup(m => m.RollbackTransaction());

            // コントローラのモック作成
            var wMockBookTable = new Mock<BooksTable>();
            var wMockMessageBoxService = new Mock<IMessageBoxService>();
            ILibraControl wLibraControl = new LibraControl(wMockBookTable.Object, wMockRepository.Object, wMockMessageBoxService.Object);

            var wResult = wLibraControl.SetDeleteFlag(1);

            Assert.IsFalse(wResult);

            wMockMessageBoxService.Verify(m => m.Show(ErrorMessageConst.C_DbError,
                                               ErrorMessageConst.C_DbErrorCaption,
                                               MessageBoxButtons.OK,
                                               MessageBoxIcon.Error),
                                          Times.Once);
        }

        [Test]
        public void LibraContolで予期せぬエラー発生テスト() {
            // ブックリポジトリのモックを作成
            var wMockRepository = new Mock<IBookRepository>();

            wMockRepository.Setup(m => m.BeginTransaction());
            wMockRepository.Setup(m => m.GetBookById(It.IsAny<int>()))
                           .Returns(new Book {
                               IsDeleted = 0,
                           });
            wMockRepository.Setup(m => m.UpdateBook(It.IsAny<Book>()));
            wMockRepository.Setup(m => m.Save()).Throws<Exception>();
            wMockRepository.Setup(m => m.CommitTransaction());
            wMockRepository.Setup(m => m.RollbackTransaction());

            // コントローラのモック作成
            var wMockBookTable = new Mock<BooksTable>();
            var wMockMessageBoxService = new Mock<IMessageBoxService>();
            ILibraControl wLibraControl = new LibraControl(wMockBookTable.Object, wMockRepository.Object, wMockMessageBoxService.Object);

            var wResult = wLibraControl.SetDeleteFlag(1);

            Assert.IsFalse(wResult);

            wMockMessageBoxService.Verify(m => m.Show(It.Is<string>(s => s.StartsWith("予期せぬエラーが発生しました。\r\nエラー：")),
                                               ErrorMessageConst.C_UnexpectedErrorCaption,
                                               MessageBoxButtons.OK,
                                               MessageBoxIcon.Error),
                                          Times.Once);
        }
    }
}