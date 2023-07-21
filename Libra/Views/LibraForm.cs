using System;
using System.Windows.Forms;

namespace Libra {
    /// <summary>
    /// 書籍一覧画面：ホーム画面です。
    /// </summary>
    public partial class LibraForm : Form {
        private ILibraControl FLibraControl;

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

        }

        /// <summary>
        /// 返却ボタン押下イベントハンドラ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Return_Click(object sender, EventArgs e) {

        }

        /// <summary>
        /// 検索ボタン押下イベントハンドラ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Search_Click(object sender, EventArgs e) {

        }

        /// <summary>
        /// クリアボタン押下イベントハンドラ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Clear_Click(object sender, EventArgs e) {

        }

        /// <summary>
        /// 追加ボタン押下イベントハンドラ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddBook_Click(object sender, EventArgs e) {
            this.addBookButton.Focus();
            this.FLibraControl.OpenAddForm();
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

            // 削除する書籍のIDと書籍名を取得
            int wBookId = (int)wSelectedRow.Cells["bookIdDataGridViewTextBoxColumn"].Value;
            string wTitle = (string)wSelectedRow.Cells["titleDataGridViewTextBoxColumn"].Value;

            // TODO: MessageBoxServiceを利用するように修正する。
            if (MessageBox.Show(string.Format("{0}を\r\n本当に削除しますか？", wTitle), "削除確認メッセージ", MessageBoxButtons.OKCancel) == DialogResult.OK) {
                this.FLibraControl.SetDeleteFlag(wBookId);
                this.FLibraControl.InitializeBookList();
                this.booksDataGridView.DataSource = this.FLibraControl.GetBooksDataTable();
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
                this.searchButton.Focus();
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
            this.FLibraControl.InitializeBookList();
            this.booksDataGridView.DataSource = this.FLibraControl.GetBooksDataTable();

            if (this.booksDataGridView.Rows.Count > 0) {
                this.booksDataGridView.CurrentCell = this.booksDataGridView.FirstDisplayedCell;
            }
        }
    }
}