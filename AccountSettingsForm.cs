using System.Text.RegularExpressions;
using WinFormsAppV3FlorenBooksV3.Models;

namespace WinFormsAppV3FlorenBooksV3;

public sealed class AccountSettingsForm : Form
{
    private readonly User _currentUser;
    private readonly Action _accountUpdated;
    private readonly TextBox _email = new() { Width = 250 };
    private readonly TextBox _currentPassword = new() { Width = 250, UseSystemPasswordChar = true };
    private readonly TextBox _newPassword = new() { Width = 250, UseSystemPasswordChar = true };
    private readonly TextBox _confirmPassword = new() { Width = 250, UseSystemPasswordChar = true };

    public AccountSettingsForm(User currentUser, Action accountUpdated)
    {
        _currentUser = currentUser;
        _accountUpdated = accountUpdated;
        Text = "Setari cont";
        StartPosition = FormStartPosition.CenterParent;
        FormBorderStyle = FormBorderStyle.FixedDialog;
        MaximizeBox = false;
        MinimizeBox = false;
        ClientSize = new Size(430, 360);
        _email.Text = currentUser.Email;

        var layout = new TableLayoutPanel { Dock = DockStyle.Fill, Padding = new Padding(18), ColumnCount = 1 };
        layout.Controls.Add(new Label { Text = "Email", AutoSize = true });
        layout.Controls.Add(_email);
        layout.Controls.Add(new Label { Text = "Parola curenta (obligatoriu pentru salvare)", AutoSize = true, Margin = new Padding(3, 12, 3, 3) });
        layout.Controls.Add(_currentPassword);
        layout.Controls.Add(new Label { Text = "Parola noua (lasa gol pentru a o pastra)", AutoSize = true, Margin = new Padding(3, 12, 3, 3) });
        layout.Controls.Add(_newPassword);
        layout.Controls.Add(new Label { Text = "Confirma parola noua", AutoSize = true, Margin = new Padding(3, 12, 3, 3) });
        layout.Controls.Add(_confirmPassword);
        var save = new Button { Text = "Salveaza", Size = new Size(92, 30) };
        save.Click += Save_Click;
        var cancel = new Button { Text = "Cancel", Size = new Size(92, 30), DialogResult = DialogResult.Cancel };
        var buttons = new FlowLayoutPanel { Dock = DockStyle.Bottom, Height = 52, FlowDirection = FlowDirection.RightToLeft, Padding = new Padding(12, 10, 18, 10) };
        buttons.Controls.Add(cancel);
        buttons.Controls.Add(save);
        Controls.Add(layout);
        Controls.Add(buttons);
        AcceptButton = save;
        CancelButton = cancel;
    }

    private void Save_Click(object? sender, EventArgs e)
    {
        string email = _email.Text.Trim();
        string newPassword = _newPassword.Text;
        if (!Regex.IsMatch(email, @"^[^\s@]+@[^\s@]+\.[^\s@]+$")) { Validation("Introdu o adresa de email valida."); return; }
        if (string.IsNullOrEmpty(_currentPassword.Text)) { Validation("Introdu parola curenta pentru a confirma modificarile."); return; }
        if (!string.IsNullOrEmpty(newPassword) && newPassword.Length < 6) { Validation("Parola noua trebuie sa aiba cel putin 6 caractere."); return; }
        if (newPassword != _confirmPassword.Text) { Validation("Confirmarea parolei noi nu corespunde."); return; }
        try
        {
            var result = UserRepository.UpdateOwnAccount(_currentUser.Id, _currentPassword.Text, email, newPassword);
            if (result == UserRepository.AccountUpdateResult.Success)
            {
                _currentUser.Email = email;
                _accountUpdated();
                MessageBox.Show("Setarile contului au fost salvate.", "Succes", MessageBoxButtons.OK, MessageBoxIcon.Information);
                DialogResult = DialogResult.OK;
                Close();
                return;
            }
            Validation(result == UserRepository.AccountUpdateResult.InvalidCurrentPassword ? "Parola curenta este incorecta." : result == UserRepository.AccountUpdateResult.EmailAlreadyInUse ? "Exista deja un cont cu acest email." : "Contul nu mai exista.");
        }
        catch (Exception ex) { MessageBox.Show($"Nu s-au putut salva setarile contului:\n{ex.Message}", "Eroare", MessageBoxButtons.OK, MessageBoxIcon.Error); }
    }

    private static void Validation(string message) => MessageBox.Show(message, "Validare", MessageBoxButtons.OK, MessageBoxIcon.Warning);
}
