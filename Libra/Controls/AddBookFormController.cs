using Libra.Views;

namespace Libra.Controls {
    public class AddBookFormController {

        public AddBookFormController() {
        }

        public void ShowAddBookForm() {
            var addBookForm = new AddBookForm();
            addBookForm.Show();
        }
    }
}
