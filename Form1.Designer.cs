namespace FocusTV
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            ListViewItem listViewItem1 = new ListViewItem("test");
            ListViewItem listViewItem2 = new ListViewItem("test2");
            notifyIcon1 = new NotifyIcon(components);
            contextMenuStrip1 = new ContextMenuStrip(components);
            settingsToolStripMenuItem = new ToolStripMenuItem();
            toolStripSeparator1 = new ToolStripSeparator();
            toolStripMenuItem_Quit = new ToolStripMenuItem();
            closeButton = new Button();
            tableLayoutPanel1 = new TableLayoutPanel();
            label11 = new Label();
            label4 = new Label();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            updateTimeTextBox = new TextBox();
            processBlacklistListView = new ListView();
            columnHeader1 = new ColumnHeader();
            label6 = new Label();
            removeSelectedProcessButton = new Button();
            addProcessTextBox = new TextBox();
            label5 = new Label();
            runOnStartupCheckBox = new CheckBox();
            label7 = new Label();
            keepMouseInScreenBoundsCheckBox = new CheckBox();
            label8 = new Label();
            keybindModifierTextBox = new TextBox();
            clearKeybindModifierButton = new Button();
            clearKeybindButton = new Button();
            keybindKeyTextBox = new TextBox();
            audioDeviceComboBox = new ComboBox();
            focusScreenComboBox = new ComboBox();
            contextMenuStrip1.SuspendLayout();
            tableLayoutPanel1.SuspendLayout();
            SuspendLayout();
            // 
            // notifyIcon1
            // 
            notifyIcon1.ContextMenuStrip = contextMenuStrip1;
            notifyIcon1.Icon = (Icon)resources.GetObject("notifyIcon1.Icon");
            notifyIcon1.Text = "FocusTV";
            notifyIcon1.Visible = true;
            notifyIcon1.MouseDoubleClick += notifyIcon1_MouseDoubleClick;
            // 
            // contextMenuStrip1
            // 
            contextMenuStrip1.Items.AddRange(new ToolStripItem[] { settingsToolStripMenuItem, toolStripSeparator1, toolStripMenuItem_Quit });
            contextMenuStrip1.Name = "contextMenuStrip1";
            contextMenuStrip1.Size = new Size(117, 54);
            // 
            // settingsToolStripMenuItem
            // 
            settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
            settingsToolStripMenuItem.Size = new Size(116, 22);
            settingsToolStripMenuItem.Text = "Settings";
            settingsToolStripMenuItem.Click += settingsToolStripMenuItem_Click;
            // 
            // toolStripSeparator1
            // 
            toolStripSeparator1.Name = "toolStripSeparator1";
            toolStripSeparator1.Size = new Size(113, 6);
            // 
            // toolStripMenuItem_Quit
            // 
            toolStripMenuItem_Quit.Name = "toolStripMenuItem_Quit";
            toolStripMenuItem_Quit.Size = new Size(116, 22);
            toolStripMenuItem_Quit.Text = "Quit";
            toolStripMenuItem_Quit.Click += ToolStripMenuItem_Quit_Click;
            // 
            // closeButton
            // 
            closeButton.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            closeButton.BackColor = Color.Red;
            closeButton.ForeColor = Color.White;
            closeButton.Location = new Point(596, 12);
            closeButton.Name = "closeButton";
            closeButton.Size = new Size(40, 40);
            closeButton.TabIndex = 1;
            closeButton.Text = "X";
            closeButton.UseVisualStyleBackColor = false;
            closeButton.Click += closeButton_Click;
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 2;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 39.17323F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 60.82677F));
            tableLayoutPanel1.Controls.Add(label11, 0, 10);
            tableLayoutPanel1.Controls.Add(label4, 0, 3);
            tableLayoutPanel1.Controls.Add(label1, 0, 1);
            tableLayoutPanel1.Controls.Add(label2, 0, 0);
            tableLayoutPanel1.Controls.Add(label3, 0, 2);
            tableLayoutPanel1.Controls.Add(updateTimeTextBox, 1, 2);
            tableLayoutPanel1.Controls.Add(processBlacklistListView, 1, 3);
            tableLayoutPanel1.Controls.Add(label6, 0, 5);
            tableLayoutPanel1.Controls.Add(removeSelectedProcessButton, 1, 4);
            tableLayoutPanel1.Controls.Add(addProcessTextBox, 1, 5);
            tableLayoutPanel1.Controls.Add(label5, 0, 6);
            tableLayoutPanel1.Controls.Add(runOnStartupCheckBox, 1, 6);
            tableLayoutPanel1.Controls.Add(label7, 0, 7);
            tableLayoutPanel1.Controls.Add(keepMouseInScreenBoundsCheckBox, 1, 7);
            tableLayoutPanel1.Controls.Add(label8, 0, 8);
            tableLayoutPanel1.Controls.Add(keybindModifierTextBox, 1, 8);
            tableLayoutPanel1.Controls.Add(clearKeybindModifierButton, 1, 9);
            tableLayoutPanel1.Controls.Add(clearKeybindButton, 1, 11);
            tableLayoutPanel1.Controls.Add(keybindKeyTextBox, 1, 10);
            tableLayoutPanel1.Controls.Add(audioDeviceComboBox, 1, 1);
            tableLayoutPanel1.Controls.Add(focusScreenComboBox, 0, 0);
            tableLayoutPanel1.GrowStyle = TableLayoutPanelGrowStyle.FixedSize;
            tableLayoutPanel1.Location = new Point(12, 9);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 12;
            tableLayoutPanel1.RowStyles.Add(new RowStyle());
            tableLayoutPanel1.RowStyles.Add(new RowStyle());
            tableLayoutPanel1.RowStyles.Add(new RowStyle());
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 146F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle());
            tableLayoutPanel1.RowStyles.Add(new RowStyle());
            tableLayoutPanel1.RowStyles.Add(new RowStyle());
            tableLayoutPanel1.RowStyles.Add(new RowStyle());
            tableLayoutPanel1.RowStyles.Add(new RowStyle());
            tableLayoutPanel1.RowStyles.Add(new RowStyle());
            tableLayoutPanel1.RowStyles.Add(new RowStyle());
            tableLayoutPanel1.RowStyles.Add(new RowStyle());
            tableLayoutPanel1.Size = new Size(556, 454);
            tableLayoutPanel1.TabIndex = 2;
            // 
            // label11
            // 
            label11.Dock = DockStyle.Fill;
            label11.Location = new Point(3, 394);
            label11.Name = "label11";
            label11.Size = new Size(211, 29);
            label11.TabIndex = 22;
            label11.Text = "Toggle Focus Keybind Key:";
            label11.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // label4
            // 
            label4.Dock = DockStyle.Fill;
            label4.Location = new Point(3, 87);
            label4.Name = "label4";
            label4.Size = new Size(211, 146);
            label4.TabIndex = 6;
            label4.Text = "App/Process Blacklist (.exe):";
            // 
            // label1
            // 
            label1.Dock = DockStyle.Fill;
            label1.Location = new Point(3, 29);
            label1.Name = "label1";
            label1.Size = new Size(211, 29);
            label1.TabIndex = 0;
            label1.Text = "Focus Audio Device (Optional):";
            label1.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // label2
            // 
            label2.Dock = DockStyle.Fill;
            label2.Location = new Point(3, 0);
            label2.Name = "label2";
            label2.Size = new Size(211, 29);
            label2.TabIndex = 2;
            label2.Text = "Focus Screen:";
            label2.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // label3
            // 
            label3.Dock = DockStyle.Fill;
            label3.Location = new Point(3, 58);
            label3.Name = "label3";
            label3.Size = new Size(211, 29);
            label3.TabIndex = 4;
            label3.Text = "Update Interval when Focused (ms):";
            label3.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // updateTimeTextBox
            // 
            updateTimeTextBox.Dock = DockStyle.Fill;
            updateTimeTextBox.Location = new Point(220, 61);
            updateTimeTextBox.Name = "updateTimeTextBox";
            updateTimeTextBox.ScrollBars = ScrollBars.Horizontal;
            updateTimeTextBox.Size = new Size(333, 23);
            updateTimeTextBox.TabIndex = 5;
            updateTimeTextBox.KeyPress += updateTimeTextBox_KeyPress;
            updateTimeTextBox.Leave += updateTimeTextBox_Leave;
            // 
            // processBlacklistListView
            // 
            processBlacklistListView.Columns.AddRange(new ColumnHeader[] { columnHeader1 });
            processBlacklistListView.Dock = DockStyle.Fill;
            processBlacklistListView.HeaderStyle = ColumnHeaderStyle.None;
            listViewItem1.StateImageIndex = 0;
            listViewItem2.StateImageIndex = 0;
            processBlacklistListView.Items.AddRange(new ListViewItem[] { listViewItem1, listViewItem2 });
            processBlacklistListView.Location = new Point(220, 90);
            processBlacklistListView.MultiSelect = false;
            processBlacklistListView.Name = "processBlacklistListView";
            processBlacklistListView.ShowGroups = false;
            processBlacklistListView.Size = new Size(333, 140);
            processBlacklistListView.TabIndex = 7;
            processBlacklistListView.UseCompatibleStateImageBehavior = false;
            processBlacklistListView.View = View.Details;
            // 
            // columnHeader1
            // 
            columnHeader1.Text = "";
            // 
            // label6
            // 
            label6.Dock = DockStyle.Fill;
            label6.Location = new Point(3, 264);
            label6.Name = "label6";
            label6.Size = new Size(211, 29);
            label6.TabIndex = 9;
            label6.Text = "Add App to Blacklist:";
            label6.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // removeSelectedProcessButton
            // 
            removeSelectedProcessButton.Dock = DockStyle.Fill;
            removeSelectedProcessButton.Location = new Point(220, 236);
            removeSelectedProcessButton.Name = "removeSelectedProcessButton";
            removeSelectedProcessButton.Size = new Size(333, 25);
            removeSelectedProcessButton.TabIndex = 10;
            removeSelectedProcessButton.Text = "Remove Selected App";
            removeSelectedProcessButton.UseVisualStyleBackColor = true;
            removeSelectedProcessButton.Click += removeSelectedProcessButton_Click;
            // 
            // addProcessTextBox
            // 
            addProcessTextBox.Dock = DockStyle.Fill;
            addProcessTextBox.Location = new Point(220, 267);
            addProcessTextBox.Name = "addProcessTextBox";
            addProcessTextBox.ScrollBars = ScrollBars.Horizontal;
            addProcessTextBox.Size = new Size(333, 23);
            addProcessTextBox.TabIndex = 11;
            addProcessTextBox.KeyPress += addProcessTextBox_KeyPress;
            addProcessTextBox.Leave += addProcessTextBox_Leave;
            // 
            // label5
            // 
            label5.Dock = DockStyle.Fill;
            label5.Location = new Point(3, 293);
            label5.Name = "label5";
            label5.Size = new Size(211, 20);
            label5.TabIndex = 12;
            label5.Text = "Run on Startup:";
            label5.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // runOnStartupCheckBox
            // 
            runOnStartupCheckBox.Location = new Point(220, 296);
            runOnStartupCheckBox.Name = "runOnStartupCheckBox";
            runOnStartupCheckBox.Size = new Size(15, 14);
            runOnStartupCheckBox.TabIndex = 13;
            runOnStartupCheckBox.UseVisualStyleBackColor = true;
            runOnStartupCheckBox.CheckedChanged += runOnStartupCheckBox_CheckedChanged;
            // 
            // label7
            // 
            label7.Dock = DockStyle.Fill;
            label7.Location = new Point(3, 313);
            label7.Name = "label7";
            label7.Size = new Size(211, 20);
            label7.TabIndex = 14;
            label7.Text = "Keep Mouse In Screen Bounds:";
            label7.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // keepMouseInScreenBoundsCheckBox
            // 
            keepMouseInScreenBoundsCheckBox.Location = new Point(220, 316);
            keepMouseInScreenBoundsCheckBox.Name = "keepMouseInScreenBoundsCheckBox";
            keepMouseInScreenBoundsCheckBox.Size = new Size(15, 14);
            keepMouseInScreenBoundsCheckBox.TabIndex = 15;
            keepMouseInScreenBoundsCheckBox.UseVisualStyleBackColor = true;
            keepMouseInScreenBoundsCheckBox.CheckedChanged += keepMouseInScreenBoundsCheckBox_CheckedChanged;
            // 
            // label8
            // 
            label8.Dock = DockStyle.Fill;
            label8.Location = new Point(3, 333);
            label8.Name = "label8";
            label8.Size = new Size(211, 30);
            label8.TabIndex = 16;
            label8.Text = "Toggle Focus Keybind Modifier(s) (Optional: CTRL, ALT, SHIFT):";
            label8.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // keybindModifierTextBox
            // 
            keybindModifierTextBox.Dock = DockStyle.Fill;
            keybindModifierTextBox.Location = new Point(220, 336);
            keybindModifierTextBox.Name = "keybindModifierTextBox";
            keybindModifierTextBox.ScrollBars = ScrollBars.Horizontal;
            keybindModifierTextBox.Size = new Size(333, 23);
            keybindModifierTextBox.TabIndex = 17;
            keybindModifierTextBox.KeyDown += keybindModifierTextBox_KeyDown;
            // 
            // clearKeybindModifierButton
            // 
            clearKeybindModifierButton.Dock = DockStyle.Fill;
            clearKeybindModifierButton.Location = new Point(220, 366);
            clearKeybindModifierButton.Name = "clearKeybindModifierButton";
            clearKeybindModifierButton.Size = new Size(333, 25);
            clearKeybindModifierButton.TabIndex = 20;
            clearKeybindModifierButton.Text = "Clear Keybind Modifier";
            clearKeybindModifierButton.UseVisualStyleBackColor = true;
            clearKeybindModifierButton.Click += clearKeybindModifierButton_Click;
            // 
            // clearKeybindButton
            // 
            clearKeybindButton.Dock = DockStyle.Fill;
            clearKeybindButton.Location = new Point(220, 426);
            clearKeybindButton.Name = "clearKeybindButton";
            clearKeybindButton.Size = new Size(333, 25);
            clearKeybindButton.TabIndex = 23;
            clearKeybindButton.Text = "Clear Keybind";
            clearKeybindButton.UseVisualStyleBackColor = true;
            clearKeybindButton.Click += clearKeybindButton_Click;
            // 
            // keybindKeyTextBox
            // 
            keybindKeyTextBox.Dock = DockStyle.Fill;
            keybindKeyTextBox.Location = new Point(220, 397);
            keybindKeyTextBox.Name = "keybindKeyTextBox";
            keybindKeyTextBox.ScrollBars = ScrollBars.Horizontal;
            keybindKeyTextBox.Size = new Size(333, 23);
            keybindKeyTextBox.TabIndex = 19;
            keybindKeyTextBox.KeyDown += keybindKeyTextBox_KeyDown;
            // 
            // audioDeviceComboBox
            // 
            audioDeviceComboBox.Dock = DockStyle.Fill;
            audioDeviceComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            audioDeviceComboBox.FormattingEnabled = true;
            audioDeviceComboBox.Location = new Point(220, 32);
            audioDeviceComboBox.MaxDropDownItems = 50;
            audioDeviceComboBox.Name = "audioDeviceComboBox";
            audioDeviceComboBox.Size = new Size(333, 23);
            audioDeviceComboBox.TabIndex = 24;
            audioDeviceComboBox.SelectedIndexChanged += audioDeviceComboBox_SelectedIndexChanged;
            // 
            // focusScreenComboBox
            // 
            focusScreenComboBox.Dock = DockStyle.Fill;
            focusScreenComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            focusScreenComboBox.FormattingEnabled = true;
            focusScreenComboBox.Location = new Point(220, 3);
            focusScreenComboBox.MaxDropDownItems = 50;
            focusScreenComboBox.Name = "focusScreenComboBox";
            focusScreenComboBox.Size = new Size(333, 23);
            focusScreenComboBox.TabIndex = 25;
            focusScreenComboBox.SelectedIndexChanged += focusScreenComboBox_SelectedIndexChanged;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(648, 475);
            ControlBox = false;
            Controls.Add(tableLayoutPanel1);
            Controls.Add(closeButton);
            Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Name = "Form1";
            ShowInTaskbar = false;
            Text = "FocusTV - Settings";
            TopMost = true;
            WindowState = FormWindowState.Minimized;
            FormClosing += Form1_FormClosing;
            contextMenuStrip1.ResumeLayout(false);
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private NotifyIcon notifyIcon1;
        private ContextMenuStrip contextMenuStrip1;
        private ToolStripMenuItem toolStripMenuItem_Quit;
        private Button closeButton;
        private ToolStripMenuItem settingsToolStripMenuItem;
        private ToolStripSeparator toolStripSeparator1;
        private TableLayoutPanel tableLayoutPanel1;
        private Label label1;
        private Label label2;
        private Label label3;
        private TextBox updateTimeTextBox;
        private Label label4;
        private ListView processBlacklistListView;
        private ColumnHeader columnHeader1;
        private Label label6;
        private Button removeSelectedProcessButton;
        private TextBox addProcessTextBox;
        private Label label5;
        private CheckBox runOnStartupCheckBox;
        private Label label7;
        private CheckBox keepMouseInScreenBoundsCheckBox;
        private Label label8;
        private TextBox keybindModifierTextBox;
        private TextBox keybindKeyTextBox;
        private Label label11;
        private Button clearKeybindModifierButton;
        private Button clearKeybindButton;
        private ComboBox audioDeviceComboBox;
        private ComboBox focusScreenComboBox;
    }
}