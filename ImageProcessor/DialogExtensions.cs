using System.IO;
using System.Windows.Forms;

namespace ImageProcessor {
    public static class DialogExtensions {

        public static bool IsEmpty(this TextBox tb, string description) {
            if (tb == null || tb.Text == null || tb.Text == "") {
                MessageBox.Show(description + " can't be empty!",
                                    "Critical Warning",
                                    MessageBoxButtons.OKCancel,
                                    MessageBoxIcon.Warning,
                                    MessageBoxDefaultButton.Button1,
                                    MessageBoxOptions.RightAlign,
                                    true);
                return true; // empty
            } else
                return false; // not empty
        }

        public static bool IsEmpty(this string s, string description) {
            if (s == null || s == "") {
                MessageBox.Show(description + " can't be empty!",
                                    "Critical Warning",
                                    MessageBoxButtons.OKCancel,
                                    MessageBoxIcon.Warning,
                                    MessageBoxDefaultButton.Button1,
                                    MessageBoxOptions.RightAlign,
                                    true);
                return true; // empty
            } else
                return false; // not empty
        }

        public static bool DirExists(this string dir) {
            if (!Directory.Exists(dir)) {
                var result = MessageBox.Show(dir + " doesn't exist. \r\n" +
                                    "Would you like to create it?",
                                    "Critical Warning",
                                    MessageBoxButtons.YesNo,
                                    MessageBoxIcon.Question,
                                    MessageBoxDefaultButton.Button2,
                                    MessageBoxOptions.RightAlign,
                                    true);
                if (result == DialogResult.Yes) {
                    Directory.CreateDirectory(dir);
                    return true; // now it exists
                } else
                    return false; // no
            } else
                return true; // yes
        }
    }
}
