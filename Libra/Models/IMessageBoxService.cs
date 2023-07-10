using System.Windows.Forms;

namespace Libra.Models {
    public interface IMessageBoxService {
        DialogResult Show(string message);
        DialogResult Show(string message, string caption);
        DialogResult Show(string message, string caption, MessageBoxButtons button);
    }
}
