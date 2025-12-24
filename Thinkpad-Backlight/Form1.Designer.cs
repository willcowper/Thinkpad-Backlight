#nullable enable

namespace Thinkpad_Backlight;

partial class Form1
{
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer? components = null;

    #region Windows Form Designer generated code

    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
        components = new System.ComponentModel.Container();
        timer1 = new System.Windows.Forms.Timer(components);
        BrightnessLabel = new Label();
        BrightnessComboBox = new ComboBox();
        OnStartupCheckBox = new CheckBox();
        OnKeyPressCheckBox = new CheckBox();
        TimerCheckBox = new CheckBox();
        SecondsLabel = new Label();
        SecondsNumeric = new NumericUpDown();
        SecondsDisplayLabel = new Label();
        ((System.ComponentModel.ISupportInitialize)SecondsNumeric).BeginInit();
        SuspendLayout();
        // 
        // timer1
        // 
        timer1.Tick += Timer1Tick;
        // 
        // BrightnessLabel
        // 
        BrightnessLabel.AutoSize = true;
        BrightnessLabel.Location = new System.Drawing.Point(12, 15);
        BrightnessLabel.Name = "BrightnessLabel";
        BrightnessLabel.Size = new System.Drawing.Size(65, 15);
        BrightnessLabel.TabIndex = 1;
        BrightnessLabel.Text = "Brightness";
        // 
        // BrightnessComboBox
        // 
        BrightnessComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
        BrightnessComboBox.FormattingEnabled = true;
        BrightnessComboBox.Items.AddRange(new object[] { "Low", "High" });
        BrightnessComboBox.Location = new System.Drawing.Point(100, 12);
        BrightnessComboBox.Name = "BrightnessComboBox";
        BrightnessComboBox.Size = new System.Drawing.Size(120, 23);
        BrightnessComboBox.TabIndex = 2;
        // 
        // OnStartupCheckBox
        // 
        OnStartupCheckBox.AutoSize = true;
        OnStartupCheckBox.Location = new System.Drawing.Point(12, 50);
        OnStartupCheckBox.Name = "OnStartupCheckBox";
        OnStartupCheckBox.Size = new System.Drawing.Size(228, 19);
        OnStartupCheckBox.TabIndex = 4;
        OnStartupCheckBox.Text = "Turn backlight on when program starts";
        OnStartupCheckBox.UseVisualStyleBackColor = true;
        // 
        // OnKeyPressCheckBox
        // 
        OnKeyPressCheckBox.AutoSize = true;
        OnKeyPressCheckBox.Location = new System.Drawing.Point(12, 80);
        OnKeyPressCheckBox.Name = "OnKeyPressCheckBox";
        OnKeyPressCheckBox.Size = new System.Drawing.Size(234, 19);
        OnKeyPressCheckBox.TabIndex = 5;
        OnKeyPressCheckBox.Text = "Turn backlight on when a key is pressed";
        OnKeyPressCheckBox.UseVisualStyleBackColor = true;
        // 
        // TimerCheckBox
        // 
        TimerCheckBox.AutoSize = true;
        TimerCheckBox.Location = new System.Drawing.Point(12, 110);
        TimerCheckBox.Name = "TimerCheckBox";
        TimerCheckBox.Size = new System.Drawing.Size(260, 19);
        TimerCheckBox.TabIndex = 6;
        TimerCheckBox.Text = "Turn backlight off when keyboard is inactive:";
        TimerCheckBox.UseVisualStyleBackColor = true;
        // 
        // SecondsLabel
        // 
        SecondsLabel.AutoSize = true;
        SecondsLabel.Location = new System.Drawing.Point(32, 145);
        SecondsLabel.Name = "SecondsLabel";
        SecondsLabel.Size = new System.Drawing.Size(51, 15);
        SecondsLabel.TabIndex = 7;
        SecondsLabel.Text = "Seconds";
        // 
        // SecondsNumeric
        // 
        SecondsNumeric.Location = new System.Drawing.Point(100, 143);
        SecondsNumeric.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
        SecondsNumeric.Name = "SecondsNumeric";
        SecondsNumeric.Size = new System.Drawing.Size(100, 23);
        SecondsNumeric.TabIndex = 8;
        SecondsNumeric.Value = new decimal(new int[] { 1, 0, 0, 0 });
        // 
        // SecondsDisplayLabel
        // 
        SecondsDisplayLabel.AutoSize = true;
        SecondsDisplayLabel.Location = new System.Drawing.Point(210, 145);
        SecondsDisplayLabel.Name = "SecondsDisplayLabel";
        SecondsDisplayLabel.Size = new System.Drawing.Size(0, 15);
        SecondsDisplayLabel.TabIndex = 9;
        // 
        // Form1
        // 
        AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new System.Drawing.Size(350, 180);
        Controls.Add(SecondsDisplayLabel);
        Controls.Add(SecondsNumeric);
        Controls.Add(SecondsLabel);
        Controls.Add(TimerCheckBox);
        Controls.Add(OnKeyPressCheckBox);
        Controls.Add(OnStartupCheckBox);
        Controls.Add(BrightnessComboBox);
        Controls.Add(BrightnessLabel);
        MaximizeBox = false;
        MinimizeBox = false;
        Name = "Form1";
        Text = "Thinkpad Backlight Settings";
        FormClosed += Form1FormClosed;
        ((System.ComponentModel.ISupportInitialize)SecondsNumeric).EndInit();
        ResumeLayout(false);
        PerformLayout();
    }

    #endregion

    private System.Windows.Forms.Timer timer1 = null!;
    private Label BrightnessLabel = null!;
    private ComboBox BrightnessComboBox = null!;
    private CheckBox OnStartupCheckBox = null!;
    private CheckBox OnKeyPressCheckBox = null!;
    private CheckBox TimerCheckBox = null!;
    private Label SecondsLabel = null!;
    private NumericUpDown SecondsNumeric = null!;
    private Label SecondsDisplayLabel = null!;
}