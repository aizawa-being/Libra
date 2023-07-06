using Libra.Controls;
using Libra.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Libra.Models.BooksDataSet;

namespace Libra {
    /// <summary>
    /// 書籍一覧画面：ホーム画面です。
    /// </summary>
    public partial class LibraForm : Form {

        public LibraForm() {
            InitializeComponent();
        }

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

        private void Borrow_Click(object sender, EventArgs e) {

        }

        private void Return_Click(object sender, EventArgs e) {

        }

        private void Search_Click(object sender, EventArgs e) {
            
        }

        private void Clear_Click(object sender, EventArgs e) {

        }

        private void AddBook_Click(object sender, EventArgs e) {
            this.addBookButton.Focus();

        }

        private void DeleteBook_Click(object sender, EventArgs e) {
            this.deleteBookButton.Focus();
        }

        private void LibraFormKeyDown(object sender, KeyEventArgs e) {
            if (e.KeyData == (Keys.Control | Keys.F)) {
                this.searchWordTextBox.Focus();
                this.searchWordTextBox.SelectAll();
            }
        }

        private void SearchWordTextBoxKeyDown(object sender, KeyEventArgs e) {
            if (e.KeyData == Keys.Enter) {
                this.searchButton.Focus();
                this.searchButton.PerformClick();
            }
        }

        private void LibraForm_Load(object sender, EventArgs e) {
            var libraController = new LibraController();

            // 書籍一覧グリッドを初期化します。
            libraController.InitializeBookList();
            this.booksDataGridView.DataSource = libraController.GetBooksDataTable();

            if (this.booksDataGridView.RowCount > 0) {
                this.booksDataGridView.CurrentCell = this.booksDataGridView.FirstDisplayedCell;
            }
        }
    }
}
