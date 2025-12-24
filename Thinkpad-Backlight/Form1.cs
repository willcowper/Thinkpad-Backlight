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

using System.Configuration;
using Gma.System.MouseKeyHook;
using Microsoft.Win32;
using Settings = Thinkpad_Backlight.Properties.Settings;

namespace Thinkpad_Backlight;

internal partial class Form1 : Form
{
    private IKeyboardMouseEvents? _globalHook;
    private readonly ToolStripMenuItem _timerMenuItem;
    private readonly ToolStripMenuItem _keypressMenuItem;
    private readonly KeyboardController _keyboardController;

    public Form1(
        ToolStripMenuItem brightMenuItem,
        ToolStripMenuItem dimMenuItem,
        ToolStripMenuItem timerMenuItem,
        ToolStripMenuItem keypressMenuItem,
        KeyboardController keyboardController)
    {
        ArgumentNullException.ThrowIfNull(brightMenuItem);
        ArgumentNullException.ThrowIfNull(dimMenuItem);
        ArgumentNullException.ThrowIfNull(timerMenuItem);
        ArgumentNullException.ThrowIfNull(keypressMenuItem);
        ArgumentNullException.ThrowIfNull(keyboardController);

        _timerMenuItem = timerMenuItem;
        _keypressMenuItem = keypressMenuItem;
        _keyboardController = keyboardController;

        InitializeComponent();
        Icon = Properties.Resources.TrayIcon;

        brightMenuItem.Click += (sender, args) =>
        {
            timer1.Reset();
            _keyboardController.ToggleBacklight(KeyboardBrightness.Bright, allowInTerminalServerSession: true);
        };

        dimMenuItem.Click += (sender, args) =>
        {
            timer1.Reset();
            _keyboardController.ToggleBacklight(KeyboardBrightness.Dim, allowInTerminalServerSession: true);
        };

        timerMenuItem.Click += TimerMenuItemOnClick;
        keypressMenuItem.Click += KeypressMenuItemOnClick;

        if (Settings.Default.Seconds < 1)
            throw new ConfigurationErrorsException("The seconds setting must be 1 or more");

        SystemEvents.PowerModeChanged += SystemEventsOnPowerModeChanged;

        if (Settings.Default.Timer)
        {
            timer1.Interval = Settings.Default.Seconds * 1000;
            timer1.Start();
        }

        if (Settings.Default.MonitorKeys)
            SubscribeToKeyDownEvents();

        BrightnessComboBox.SelectedIndex = Settings.Default.Bright ? 1 : 0;
        BrightnessComboBox.SelectedIndexChanged += (sender, args) =>
        {
            Settings.Default.Bright = BrightnessComboBox.SelectedIndex == 1;
            Settings.Default.Save();
        };

        OnStartupCheckBox.Checked = Settings.Default.EnableAtStartup;
        OnStartupCheckBox.CheckedChanged += (sender, args) =>
        {
            Settings.Default.EnableAtStartup = OnStartupCheckBox.Checked;
            Settings.Default.Save();
        };

        OnKeyPressCheckBox.Checked = Settings.Default.MonitorKeys;
        OnKeyPressCheckBox.CheckedChanged += KeypressMenuItemOnClick;

        TimerCheckBox.Checked = Settings.Default.Timer;
        TimerCheckBox.CheckedChanged += TimerMenuItemOnClick;

        SecondsNumeric.Maximum = ushort.MaxValue;
        SecondsNumeric.Value = Settings.Default.Seconds;
        SecondsDisplayLabel.Text = $"({SecondsNumeric.Value})";
        SecondsNumeric.ValueChanged += (sender, args) =>
        {
            ushort seconds = (ushort)SecondsNumeric.Value;
            SecondsDisplayLabel.Text = $"({seconds})";
            timer1.Interval = seconds * 1000;
            Settings.Default.Seconds = seconds;
            Settings.Default.Save();
        };
    }

    private void TimerMenuItemOnClick(object? sender, EventArgs args)
    {
        if (_timerMenuItem.Checked)
        {
            _timerMenuItem.Checked = false;
            Settings.Default.Timer = false;
            timer1.Stop();
        }
        else
        {
            _timerMenuItem.Checked = true;
            Settings.Default.Timer = true;
            timer1.Stop();
            timer1.Start();
        }

        Settings.Default.Save();
    }

    private void KeypressMenuItemOnClick(object? sender, EventArgs args)
    {
        if (_keypressMenuItem.Checked)
        {
            _keypressMenuItem.Checked = false;
            Settings.Default.MonitorKeys = false;
            UnsubscribeFromKeyDownEvents();
        }
        else
        {
            _keypressMenuItem.Checked = true;
            Settings.Default.MonitorKeys = true;
            SubscribeToKeyDownEvents();
        }

        Settings.Default.Save();
    }

    private void SystemEventsOnPowerModeChanged(object sender, PowerModeChangedEventArgs e)
    {
        switch (e.Mode)
        {
            case PowerModes.Suspend:
                timer1.Stop();
                break;

            case PowerModes.Resume:
                if (Settings.Default.EnableAtStartup)
                    _keyboardController.ToggleBacklight(allowInTerminalServerSession: false);
                timer1.Reset();
                break;
        }
    }

    private void SubscribeToKeyDownEvents()
    {
        if (_globalHook == null)
        {
            // Note: for the application hook, use the Hook.AppEvents() instead
            _globalHook = Hook.GlobalEvents();
            _globalHook.KeyDown += GlobalHookOnKeyDown;
        }
    }

    private void GlobalHookOnKeyDown(object? sender, KeyEventArgs e)
    {
        if (e.KeyCode != Keys.None /* issue 1 */)
            _keyboardController.ToggleBacklight(allowInTerminalServerSession: false);

        timer1.Reset();
    }

    private void UnsubscribeFromKeyDownEvents()
    {
        if (_globalHook != null)
        {
            _globalHook.KeyDown -= GlobalHookOnKeyDown;
            _globalHook.Dispose();
            _globalHook = null;
        }
    }

    private void Timer1Tick(object? sender, EventArgs e) => _keyboardController.ToggleBacklight(KeyboardBrightness.Off, allowInTerminalServerSession: true);

    private void Form1FormClosed(object? sender, FormClosedEventArgs e)
    {
        _keypressMenuItem.Enabled = true;
        _timerMenuItem.Enabled = true;
    }

    internal void ShowConfig(object? sender, EventArgs e)
    {
        _keypressMenuItem.Enabled = false;
        _timerMenuItem.Enabled = false;

        // If we are already showing the window, merely focus it.
        if (Visible)
        {
            Activate();
        }
        else
        {
            ShowDialog();
        }
    }

    /// <summary>
    /// Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
        SystemEvents.PowerModeChanged -= SystemEventsOnPowerModeChanged;
        UnsubscribeFromKeyDownEvents();

        if (disposing && components != null)
        {
            components.Dispose();
        }

        base.Dispose(disposing);
    }
}