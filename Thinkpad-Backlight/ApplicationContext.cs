/*
Copyright © Stephen Kennedy 2019  

This file is part of Thinkpad-Backlight.

Thinkpad-Backlight is free software: you can redistribute it and/or modify
it under the terms of the GNU General Public License as published by
the Free Software Foundation, either version 3 of the License, or
(at your option) any later version.

Thinkpad-Backlight is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
GNU General Public License for more details.

You should have received a copy of the GNU General Public License
along with Thinkpad-Backlight.  If not, see <https://www.gnu.org/licenses/>.
*/

using Settings = Thinkpad_Backlight.Properties.Settings;

namespace Thinkpad_Backlight;

public sealed class ApplicationContext : System.Windows.Forms.ApplicationContext
{
    private NotifyIcon? _trayIcon;

    public ApplicationContext()
    {
        var keyboardController = new KeyboardController();

        if (Settings.Default.EnableAtStartup)
            keyboardController.ToggleBacklight(allowInTerminalServerSession: false);

        var brightMenuItem = new ToolStripMenuItem("On: Bright");
        var dimMenuItem = new ToolStripMenuItem("On: Dim");
        var offMenuItem = new ToolStripMenuItem("Off");
        var timerMenuItem = new ToolStripMenuItem("Timer") { Checked = Settings.Default.Timer };
        var keypressMenuItem = new ToolStripMenuItem("Monitor key presses") { Checked = Settings.Default.MonitorKeys };
        var settingsMenuItem = new ToolStripMenuItem("Settings");
        var exitMenuItem = new ToolStripMenuItem("Exit");

        offMenuItem.Click += (_, _) => keyboardController.ToggleBacklight(KeyboardBrightness.Off, allowInTerminalServerSession: true);
        exitMenuItem.Click += (_, _) => Application.Exit();

        _trayIcon = new NotifyIcon
        {
            Icon = Properties.Resources.TrayIcon,
            ContextMenuStrip = new ContextMenuStrip
            {
                Items =
                {
                    brightMenuItem,
                    dimMenuItem,
                    offMenuItem,
                    new ToolStripSeparator(),
                    timerMenuItem,
                    keypressMenuItem,
                    new ToolStripSeparator(),
                    settingsMenuItem,
                    exitMenuItem
                }
            },
            Visible = false,
            Text = "Thinkpad Backlight"
        };

        var configWindow = new Form1(brightMenuItem, dimMenuItem, timerMenuItem, keypressMenuItem, keyboardController);

        _trayIcon.DoubleClick += configWindow.ShowConfig;
        settingsMenuItem.Click += configWindow.ShowConfig;
        _trayIcon.Visible = true;
    }

    /// <summary>Releases the unmanaged resources used by the <see cref="T:System.Windows.Forms.ApplicationContext" /> and optionally releases the managed resources.</summary>
    /// <param name="disposing">
    /// <see langword="true" /> to release both managed and unmanaged resources; <see langword="false" /> to release only unmanaged resources.</param>
    protected override void Dispose(bool disposing)
    {
        if (_trayIcon != null)
        {
            _trayIcon.Dispose();
            _trayIcon = null;
        }

        base.Dispose(disposing);
    }
}