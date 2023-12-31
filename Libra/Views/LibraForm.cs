using System;
using System.Windows.Forms;

namespace Libra {
    /// <summary>
    /// 書籍一覧画面：ホーム画面です。
    /// </summary>
    public partial class LibraForm : Form {
        private readonly ILibraControl FLibraControl;

        /// <summary>
        /// 現在のスクロール位置
        /// </summary>
        private int FScrollPosition;

        /// <summary>
        /// 最後に検索したときの検索ワード
        /// </summary>
        private string FLastSearchWord = "";

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public LibraForm() {
            this.FLibraControl = new LibraControl();
            InitializeComponent();
        }

        /// <summary>
        /// DataGridViewが描画される際に呼び出されるイベントハンドラ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BooksGridCellPainting(object sender, DataGridViewCellPaintingEventArgs e) {
            if (e.ColumnIndex < 0 && 0 <= e.RowIndex) {
                // セルを描画
                e.Paint(e.ClipBounds, DataGridViewPaintParts.All);

                // 行番号を描画
                TextRenderer.DrawText(
                    e.Graphics,
                    (e.RowIndex + 1).ToString(),
                    e.CellStyle.Font,
                    e.CellBounds,
                    e.CellStyle.ForeColor,
                    TextFormatFlags.Right | TextFormatFlags.VerticalCenter);

                // 描画完了
                e.Handled = true;
            }
        }

        /// <summary>
        /// 貸出ボタン押下イベントハンドラ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Borrow_Click(object sender, EventArgs e) {
            if (booksDataGridView.SelectedCells.Count == 0) {
                return;
            }
            // 選択されている行を取得
            DataGridViewCell wSelectedCell = this.booksDataGridView.SelectedCells[0];
            DataGridViewRow wSelectedRow = wSelectedCell.OwningRow;

            IMessageBoxUtil wMessageBox = new MessageBoxUtil();
            
            // 既に貸出中
            if (wSelectedRow.Cells["userNameColumn"].Value != DBNull.Value) {
                wMessageBox.Show(MessageTypeEnum.AlreadyBorrowed, (string)wSelectedRow.Cells["titleColumn"].Value);
                return;
            }

            // 利用者名は入力が必須
            if (string.IsNullOrWhiteSpace(this.userNameTextBox.Text)) {
                wMessageBox.Show(MessageTypeEnum.UserNameNotInput);
                return;
            }
            
            // 貸出する書籍のIDを取得
            int wBookId = (int)wSelectedRow.Cells["bookIdColumn"].Value;

            // 貸出処理
            this.FLibraControl.BorrowBook(wBookId, this.userNameTextBox.Text);

            // 書籍一覧グリッドの検索条件を引き継ぐ
            var wBooks = this.FLibraControl.SearchBooks(this.FLastSearchWord);
            this.booksDataGridView.DataSource = this.FLibraControl.ConvertBooksDataTable(wBooks);

            // 貸出した書籍にフォーカスします。
            int wColumnIndex = this.booksDataGridView.Columns[0].Index;
            foreach (DataGridViewRow wRow in this.booksDataGridView.Rows) {
                if (wRow.Cells[wColumnIndex].Value != null && (int)wRow.Cells[wColumnIndex].Value == wBookId) {
                    wRow.Selected = true;
                    break;
                }
            }

            // スクロール位置を指定
            if (this.booksDataGridView.RowCount > 0) {
                this.booksDataGridView.FirstDisplayedScrollingRowIndex = this.FScrollPosition;
            }
        }

        /// <summary>
        /// 返却ボタン押下イベントハンドラ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Return_Click(object sender, EventArgs e) {
            if (booksDataGridView.SelectedCells.Count == 0) {
                return;
            }
            // 選択されている行を取得
            DataGridViewCell wSelectedCell = this.booksDataGridView.SelectedCells[0];
            DataGridViewRow wSelectedRow = wSelectedCell.OwningRow;

            // 返却する書籍のIDを取得
            int wBookId = (int)wSelectedRow.Cells["bookIdColumn"].Value;

            // 返却処理
            this.FLibraControl.ReturnBook(wBookId);

            // 書籍一覧グリッドの検索条件を引き継ぐ
            var wBooks = this.FLibraControl.SearchBooks(this.FLastSearchWord);
            this.booksDataGridView.DataSource = this.FLibraControl.ConvertBooksDataTable(wBooks);

            // 返却した書籍にフォーカスします。
            int wColumnIndex = this.booksDataGridView.Columns[0].Index;
            foreach (DataGridViewRow wRow in this.booksDataGridView.Rows) {
                if (wRow.Cells[wColumnIndex].Value != null && (int)wRow.Cells[wColumnIndex].Value == wBookId) {
                    wRow.Selected = true;
                    break;
                }
            }

            // スクロール位置を指定
            if (this.booksDataGridView.RowCount > 0) {
                this.booksDataGridView.FirstDisplayedScrollingRowIndex = this.FScrollPosition;
            }
        }

        /// <summary>
        /// 検索ボタン押下イベントハンドラ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Search_Click(object sender, EventArgs e) {
            // 検索ワードを保持する。
            this.FLastSearchWord = this.searchWordTextBox.Text;

            var wBooks = this.FLibraControl.SearchBooks(this.FLastSearchWord);
            this.booksDataGridView.DataSource = this.FLibraControl.ConvertBooksDataTable(wBooks);

            // スクロール位置の初期化
            this.FScrollPosition = 0;
        }

        /// <summary>
        /// クリアボタン押下イベントハンドラ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Clear_Click(object sender, EventArgs e) {
            // 検索ワード入力欄を初期化します。
            this.searchWordTextBox.Text = "";

            // 保持していた検索ワードを初期化します。
            this.FLastSearchWord = "";

            // 書籍一覧グリッドを初期化します。
            var wBooks = this.FLibraControl.GetAllBooks();
            this.booksDataGridView.DataSource = this.FLibraControl.ConvertBooksDataTable(wBooks);

            // スクロール位置の初期化
            this.FScrollPosition = 0;
        }

        /// <summary>
        /// 追加ボタン押下イベントハンドラ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddBook_Click(object sender, EventArgs e) {
            var wAddBookId = this.FLibraControl.OpenAddForm();
            if (wAddBookId == -1) {
                // 書籍を追加していない場合は何もしない
                return;
            }
            // 書籍を追加した場合
            // 書籍一覧グリッドを更新します。
            // 検索条件は引き継ぎません。
            var wBooks = this.FLibraControl.GetAllBooks();
            this.booksDataGridView.DataSource = this.FLibraControl.ConvertBooksDataTable(wBooks);

            // 保持していた検索ワードを初期化します。
            this.FLastSearchWord = "";

            // 追加した書籍にフォーカスします。
            int wColumnIndex = this.booksDataGridView.Columns[0].Index;
            foreach (DataGridViewRow wRow in this.booksDataGridView.Rows) {
                if (wRow.Cells[wColumnIndex].Value != null && (int)wRow.Cells[wColumnIndex].Value == wAddBookId) {
                    wRow.Selected = true;

                    // スクロール位置を指定
                    this.booksDataGridView.FirstDisplayedScrollingRowIndex = wRow.Index;
                    break;
                }
            }
        }

        /// <summary>
        /// 削除ボタン押下イベントハンドラ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DeleteBook_Click(object sender, EventArgs e) {
            if (booksDataGridView.SelectedCells.Count == 0) {
                return;
            }

            // 選択されている行を取得
            DataGridViewCell wSelectedCell = this.booksDataGridView.SelectedCells[0];
            DataGridViewRow wSelectedRow = wSelectedCell.OwningRow;
            int wSelectedCellIndex = this.booksDataGridView.SelectedRows[0].Index;

            // 削除する書籍のIDと書籍名を取得
            int wBookId = (int)wSelectedRow.Cells["bookIdColumn"].Value;
            string wTitle = (string)wSelectedRow.Cells["titleColumn"].Value;
            
            // 削除実施し、結果を取得する
            var wResult = this.FLibraControl.SetDeleteFlag(wTitle, wBookId);

            // 書籍一覧グリッドの検索条件を引き継ぐ
            var wBooks = this.FLibraControl.SearchBooks(this.FLastSearchWord);
            this.booksDataGridView.DataSource = this.FLibraControl.ConvertBooksDataTable(wBooks);

            // スクロール位置を指定
            if (this.booksDataGridView.RowCount > 0) {
                this.booksDataGridView.FirstDisplayedScrollingRowIndex = this.FScrollPosition;
            }

            // フォーカスする行を指定
            if (wSelectedCellIndex == 0 || wSelectedCellIndex > this.booksDataGridView.Rows.Count || this.booksDataGridView.Rows.Count == 0) {
                return;
            }
            if (wResult) {
                // 削除成功時は1行前をフォーカスする
                this.booksDataGridView.CurrentCell = this.booksDataGridView.Rows[wSelectedCellIndex - 1].Cells[1];
                return;
            }
        }

        /// <summary>
        /// フォームの押下イベントハンドラ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LibraFormKeyDown(object sender, KeyEventArgs e) {
            if (e.KeyData == (Keys.Control | Keys.F)) {
                this.searchWordTextBox.Focus();
                this.searchWordTextBox.SelectAll();
            }
        }

        /// <summary>
        /// 検索ワード入力欄のボタン押下イベントハンドラ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SearchWordTextBoxKeyDown(object sender, KeyEventArgs e) {
            if (e.KeyData == Keys.Enter) {
                this.searchButton.PerformClick();
            }
        }

        /// <summary>
        /// フォーム読み込みイベントハンドラ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LibraForm_Load(object sender, EventArgs e) {
            // 書籍一覧グリッドを初期化します。
            var wBooks = this.FLibraControl.GetAllBooks();
            this.booksDataGridView.DataSource = this.FLibraControl.ConvertBooksDataTable(wBooks);

            if (this.booksDataGridView.Rows.Count > 0) {
                this.booksDataGridView.CurrentCell = this.booksDataGridView.FirstDisplayedCell;
            }
        }

        /// <summary>
        /// 書籍一覧グリッドのスクロールイベントハンドラ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BooksDataGridView_Scroll(object sender, ScrollEventArgs e) {
            if (e.ScrollOrientation == ScrollOrientation.VerticalScroll) {
                this.FScrollPosition = e.NewValue;
            }
        }
    }
}