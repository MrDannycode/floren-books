using WinFormsAppV3FlorenBooksV3.Models;

namespace WinFormsAppV3FlorenBooksV3;

public static class AccountSettingsButton
{
    public static void AddTo(Form dashboard, User currentUser, Action accountUpdated)
    {
        var settingsButton = new Button
        {
            Text = "Setari cont",
            Anchor = AnchorStyles.Top | AnchorStyles.Right,
            UseVisualStyleBackColor = true
        };
        settingsButton.Click += (_, _) =>
        {
            using var settings = new AccountSettingsForm(currentUser, accountUpdated);
            settings.ShowDialog(dashboard);
        };

        var logoutButton = new Button
        {
            Text = "Logout",
            Anchor = AnchorStyles.Top | AnchorStyles.Right,
            UseVisualStyleBackColor = true
        };
        logoutButton.Click += (_, _) => dashboard.Close();

        dashboard.Controls.Add(settingsButton);
        dashboard.Controls.Add(logoutButton);

        dashboard.Load += (_, _) => PlaceButtons(dashboard, settingsButton, logoutButton);
    }

    private static void PlaceButtons(Form dashboard, Button settingsButton, Button logoutButton)
    {
        // Use an existing designer button as size reference so DPI scaling is respected
        // Exclude the buttons we just added (they have Top=0 before placement)
        var reference = dashboard.Controls.OfType<Button>()
            .FirstOrDefault(b => b != settingsButton && b != logoutButton && b.Top >= 5);

        int y = reference?.Top ?? 12;
        int h = reference?.Height ?? 23;
        int baseW = reference?.Width ?? 90;

        // "Setari cont" needs a bit more width than a standard button
        int settingsW = baseW + 20;
        int logoutW = baseW;

        settingsButton.Size = new Size(settingsW, h);
        logoutButton.Size = new Size(logoutW, h);

        settingsButton.Location = new Point(dashboard.ClientSize.Width - settingsW - logoutW - 5, y);
        logoutButton.Location = new Point(dashboard.ClientSize.Width - logoutW - 2, y);

        settingsButton.BringToFront();
        logoutButton.BringToFront();
    }
}
