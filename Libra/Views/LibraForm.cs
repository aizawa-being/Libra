using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Libra {
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

        private void BorrowButtonClick(object sender, EventArgs e) {

        }

        private void ReturnButtonClick(object sender, EventArgs e) {

        }

        private void SearchButtonClick(object sender, EventArgs e) {

        }

        private void ClearButtonClick(object sender, EventArgs e) {

        }

        private void AddBookButtonClick(object sender, EventArgs e) {

        }

        private void DeleteBookButtonClick(object sender, EventArgs e) {

        }
    }
}
