using Libra.Views;

namespace Libra.Controls {
    public class AddBookFormController {

        public AddBookFormController() {
        }

        /// <summary>
        /// 書籍追加画面を開きます。
        /// </summary>
        public void ShowAddBookForm() {
            // 書籍追加画面はひとつしか作成できない。
            if (AddBookForm.F_Instance == null) {
                AddBookForm.F_Instance = new AddBookForm();
                AddBookForm.F_Instance.FormClosed += (sender, args) => AddBookForm.F_Instance = null;
                AddBookForm.F_Instance.Show();
            }
        }
    }
}
