using CoreAudioApi;
using FocusTV.Properties;
using IWshRuntimeLibrary;
using System.ComponentModel;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Timers;
using Application = System.Windows.Forms.Application;
using Timer = System.Timers.Timer;

namespace FocusTV
{
    public partial class Form1 : Form
    {
        private string audioDeviceName = "LG TV SSCR2";
        private int screenID = 2;
        private List<string> processBlacklist = new List<string>() { "devenv", "TextInputHost", "ShellExperienceHost", "NVIDIA Share" };
        private int updateTime = 500; //ms
        private bool keepMouseInScreenBounds = true;
        private int keybindKey = 8;
        private int keybindKeyModifiers = 6;
        private bool runOnStartup = true;

        private bool isFocused = false;
        private Timer timer = new Timer();
        private string previousAudioDeviceID = "";
        private List<WindowData> processWindowDataList = new List<WindowData>();
        private MMDeviceEnumerator audioDeviceEnumerator = new MMDeviceEnumerator();
        private const int HOTKEY_ID = 1;
        private Settings settings;
        private Screen? focusScreen = null;
        private bool updatingComboBoxes = false;

        public struct WindowData
        {
            public IntPtr handle;
            public Rectangle rect;
            public int showCmd;
        }

        public Form1()
        {
            InitializeComponent();

            settings = Settings.Default;
            LoadSettings();
            LoadComboBoxAudioDeviceNames();
            LoadComboBoxScreenNames();

            //make the program run as background app
            ShowInTaskbar = false;
            WindowState = FormWindowState.Minimized;

            //setup update timer
            timer.AutoReset = true;
            timer.Elapsed += CheckTimerElapsed;
        }

        private void LoadSettings()
        {
            //load settings and set their respective UI elements
            audioDeviceName = settings.audioDeviceName;

            //screenID
            SetFocusScreen(settings.screenID);

            //updateTime
            updateTime = settings.updateTime;
            timer.Interval = updateTime;
            updateTimeTextBox.Text = updateTime.ToString();

            //processBlacklist
            processBlacklist.Clear();
            processBlacklistListView.Items.Clear();
            foreach (string? process in settings.processBlacklist)
            {
                if (!string.IsNullOrEmpty(process))
                {
                    processBlacklist.Add(process);
                    processBlacklistListView.Items.Add(new ListViewItem(process));
                }
            }
            processBlacklistListView.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);

            //runOnStartup
            runOnStartup = settings.runOnStartup;
            runOnStartupCheckBox.Checked = runOnStartup;
            bool startupFileExists = System.IO.File.Exists(Environment.GetFolderPath(Environment.SpecialFolder.Startup) + "\\" + Application.ProductName + ".lnk");
            if (runOnStartup && !startupFileExists)
            {
                AddToWindowsStartupFolder();
            }
            else if (!runOnStartup && startupFileExists)
            {
                RemoveFromWindowsStartupFolder();
            }

            //keepMouseInScreenBounds
            keepMouseInScreenBoundsCheckBox.Checked = settings.keepMouseInScreenBounds;

            //keyBindKeyModifiers
            keybindKeyModifiers = settings.keybindModifiers;
            keybindModifierTextBox.Text = KeybindModifiersToString();

            //keybindKey
            keybindKey = settings.keybindKey;
            if (keybindKey != 0)
            {
                keybindKeyTextBox.Text = ((Keys)keybindKey).ToString();
            }
            else
            {
                keybindKeyTextBox.Text = "";
            }

            //register the hotkey
            ResetHotkey();
        }

        private void SetFocusScreen(int newScreenID)
        {
            //make sure the screen we want to set is within bounds
            if (newScreenID >= 0 && newScreenID < Screen.AllScreens.Length)
            {
                screenID = newScreenID;
                focusScreen = Screen.AllScreens[screenID];
            }
            else if (Screen.AllScreens.Length > 0)
            {
                screenID = 0;
                focusScreen = Screen.PrimaryScreen;
            }
            else
            {
                screenID = -1;
                focusScreen = null;
            }
        }

        private void SaveSettings()
        {
            settings.audioDeviceName = audioDeviceName;
            settings.screenID = screenID;
            settings.updateTime = updateTime;

            settings.processBlacklist.Clear();
            foreach (string process in processBlacklist)
            {
                settings.processBlacklist.Add(process);
            }

            settings.runOnStartup = runOnStartup;
            settings.keepMouseInScreenBounds = keepMouseInScreenBounds;
            settings.keybindKey = keybindKey;
            settings.keybindModifiers = keybindKeyModifiers;

            settings.Save();
        }

        private void StartFocusScreen()
        {
            isFocused = true;

            if (focusScreen != null)
            {
                if (!string.IsNullOrEmpty(audioDeviceName) && !audioDeviceName.Equals("None"))
                {
                    //save previous default playback audio device ID
                    MMDevice previousAudioDevice = audioDeviceEnumerator.GetDefaultAudioEndpoint(EDataFlow.eRender, ERole.eMultimedia);
                    previousAudioDeviceID = previousAudioDevice.ID;

                    //find the desired playback audio device ID
                    MMDeviceCollection devices = audioDeviceEnumerator.EnumerateAudioEndPoints(EDataFlow.eRender, EDeviceState.DEVICE_STATE_ACTIVE);
                    string audioDeviceID = "";

                    //loop through all output audio devices
                    for (int i = 0; i < devices.Count; i++)
                    {
                        //see if the device we're looking for exists
                        if (devices[i].FriendlyName.Contains(audioDeviceName))
                        {
                            //get device ID of the device we were looking for
                            audioDeviceID = devices[i].ID;
                            break;
                        }
                    }

                    if (audioDeviceID != "")
                    {
                        //set default playback audio device to desired audio device
                        PolicyConfigClient client = new PolicyConfigClient();
                        client.SetDefaultEndpoint(audioDeviceID, ERole.eMultimedia);
                    }
                    else
                    {
                        Debug.WriteLine("Error Finding Audio Device: " + audioDeviceName);
                    }
                }

                //move cursor to center of focused screen
                int x = focusScreen.Bounds.Left + focusScreen.Bounds.Width / 2;
                int y = focusScreen.Bounds.Top + focusScreen.Bounds.Height / 2;
                Cursor.Position = new Point(x, y);

                UpdateFocusScreen(true);
                timer.Interval = updateTime;
                timer.Start();
            }
            else
            {
                Debug.WriteLine("Error starting focus, screenID is invalid");
            }
        }

        private void UpdateFocusScreen(bool firstUpdate)
        {
            if (isFocused && focusScreen != null)
            {
                Process[] processes = Process.GetProcesses();
                List<IntPtr> windowsStillOpen = new List<IntPtr>();
                WINDOWPLACEMENT windowPlacement;
                int showCmd;
                int processWindowDataIndex;
                Rectangle rect;
                Rectangle newRect;

                //move all processes with windows to the focused screen
                foreach (Process p in processes)
                {
                    if (!string.IsNullOrEmpty(p.MainWindowTitle) && !processBlacklist.Contains(p.ProcessName))
                    {
                        try
                        {
                            //get process window showCmd (normal, minimized, maximized)
                            windowPlacement = new WINDOWPLACEMENT();
                            windowPlacement.length = Marshal.SizeOf(typeof(WINDOWPLACEMENT));
                            GetWindowPlacement(p.MainWindowHandle, out windowPlacement);
                            showCmd = windowPlacement.showCmd;

                            //keep track of which process windows are still open so we can remove any closed process windows from our list easily later
                            windowsStillOpen.Add(p.MainWindowHandle);

                            //ignore minimized windows
                            if (showCmd != 6)
                            {
                                //get process window rect
                                rect = GetWindowRect(p.MainWindowHandle);

                                //save window data if unique to restore window positions after focus is stopped
                                processWindowDataIndex = processWindowDataList.FindIndex(item => item.handle == p.MainWindowHandle);
                                if (processWindowDataIndex == -1)
                                {
                                    WindowData windowData = new WindowData();
                                    windowData.handle = p.MainWindowHandle;
                                    windowData.rect = rect;
                                    windowData.showCmd = showCmd;
                                    processWindowDataList.Add(windowData);
                                }

                                //copy current rect so we can compare them later
                                newRect = rect;

                                //only change width and height if not maximized
                                if (showCmd != 3)
                                {
                                    if (rect.Width > focusScreen.Bounds.Width)
                                    {
                                        newRect.Width = focusScreen.Bounds.Width;
                                    }
                                    if (rect.Height > focusScreen.Bounds.Height)
                                    {
                                        newRect.Height = focusScreen.Bounds.Height;
                                    }
                                }
                                if (firstUpdate)
                                {
                                    //on first update move the process window to the top left of the screen
                                    newRect.X = focusScreen.Bounds.Left;
                                    newRect.Y = focusScreen.Bounds.Top;
                                }
                                else
                                {
                                    //keep the process window in the focused screen relative to its center
                                    if (rect.Right < focusScreen.Bounds.Left || rect.Left > focusScreen.Bounds.Right)
                                    {
                                        newRect.X = focusScreen.Bounds.Left;
                                    }
                                    if (rect.Bottom < focusScreen.Bounds.Top || rect.Top > focusScreen.Bounds.Bottom)
                                    {
                                        newRect.Y = focusScreen.Bounds.Top;
                                    }
                                }

                                //only update process window if something has changed
                                if (rect.X != newRect.X || rect.Y != newRect.Y || rect.Width != newRect.Width || rect.Height != newRect.Height)
                                {
                                    try
                                    {
                                        //if screen is maximized, un-maximize it (can't move it while it's maximized)
                                        if (showCmd == 3)
                                        {
                                            ShowWindow(p.MainWindowHandle, 1);
                                        }

                                        //move any window that is offscreen to the focused screen
                                        MoveWindow(p.MainWindowHandle, newRect.X, newRect.Y, newRect.Width, newRect.Height, true);

                                        //if screen was maximized before, restore its state to maximized
                                        if (showCmd == 3)
                                        {
                                            ShowWindow(p.MainWindowHandle, 3);
                                        }
                                    }
                                    catch (Win32Exception exception)
                                    {
                                        Debug.WriteLine("Error moving process window " + exception.Message);
                                    }
                                }
                            }
                        }
                        catch (Win32Exception exception)
                        {
                            Debug.WriteLine("Error getting window rect " + exception.Message);
                        }
                    }
                }

                //remove all closed process windows from our list
                for (int i = 0; i < processWindowDataList.Count; i++)
                {
                    if (!windowsStillOpen.Contains(processWindowDataList[i].handle))
                    {
                        processWindowDataList.RemoveAt(i);
                        i--;
                    }
                }

                //keep cursor inside the bounds of the focused screen
                if (keepMouseInScreenBounds)
                {
                    int cursorX = Cursor.Position.X;
                    int cursorY = Cursor.Position.Y;
                    if (cursorX < focusScreen.Bounds.X)
                    {
                        cursorX = focusScreen.Bounds.X;
                    }
                    else if (cursorX > focusScreen.Bounds.Right)
                    {
                        cursorX = focusScreen.Bounds.Right;
                    }
                    if (cursorY < focusScreen.Bounds.Y)
                    {
                        cursorY = focusScreen.Bounds.Y;
                    }
                    else if (cursorY > focusScreen.Bounds.Bottom)
                    {
                        cursorY = focusScreen.Bounds.Bottom;
                    }
                    Cursor.Position = new Point(cursorX, cursorY);
                }
            }
            else
            {
                Debug.WriteLine("Error updating focus- screenID is invalid. Stopping focus.");
                StopFocusScreen();
            }
        }

        private void StopFocusScreen()
        {
            isFocused = false;
            timer.Stop();

            //set default playback device to previous audio device
            if (previousAudioDeviceID != "")
            {
                PolicyConfigClient client = new PolicyConfigClient();
                client.SetDefaultEndpoint(previousAudioDeviceID, ERole.eMultimedia);
            }
            previousAudioDeviceID = "";

            //move cursor to center of primary screen
            if (Screen.PrimaryScreen != null)
            {
                Cursor.Position = new Point(Screen.PrimaryScreen.Bounds.Width / 2, Screen.PrimaryScreen.Bounds.Height / 2);
            }

            //move all windows back to their previous position, size & state
            foreach (WindowData data in processWindowDataList)
            {
                Rectangle rect = data.rect;
                try
                {
                    ShowWindow(data.handle, 1);
                    MoveWindow(data.handle, rect.X, rect.Y, rect.Width, rect.Height, true);
                    if (data.showCmd != 1)
                    {
                        ShowWindow(data.handle, data.showCmd);
                    }

                }
                catch (Win32Exception exception)
                {
                    Debug.WriteLine("Error moving process window " + exception.Message);
                }
            }
            processWindowDataList.Clear();
        }

        private void ResetFocus()
        {
            if (isFocused)
            {
                StopFocusScreen();
                StartFocusScreen();
            }
        }

        private void CheckTimerElapsed(object? sender, ElapsedEventArgs e)
        {
            UpdateFocusScreen(false);
        }

        private void LoadComboBoxAudioDeviceNames()
        {
            //this is set in order to not trigger combo box UI events
            updatingComboBoxes = true;

            MMDeviceCollection devices = audioDeviceEnumerator.EnumerateAudioEndPoints(EDataFlow.eRender, EDeviceState.DEVICE_STATE_ACTIVE);
            audioDeviceComboBox.Items.Clear();
            audioDeviceComboBox.Items.Add("None");
            audioDeviceComboBox.SelectedIndex = 0;

            //loop through all audio devices
            for (int i = 0; i < devices.Count; i++)
            {
                audioDeviceComboBox.Items.Add(devices[i].FriendlyName);

                if (audioDeviceName.Equals(devices[i].FriendlyName))
                {
                    audioDeviceComboBox.SelectedIndex = i + 1;
                }
            }

            updatingComboBoxes = false;
        }

        private void LoadComboBoxScreenNames()
        {
            //this is set in order to not trigger combo box UI events
            updatingComboBoxes = true;

            focusScreenComboBox.Items.Clear();

            double scalingFactor;
            Screen screen;
            DEVMODE devMode;
            string comboBoxItem = "";
            bool found = false;

            //loop through all screens
            for (int i = 0; i < Screen.AllScreens.Length; i++)
            {
                screen = Screen.AllScreens[i];
                comboBoxItem = screen.DeviceName;
                //used for getting the scaling factor and refresh rate of the screen
                devMode = new DEVMODE();
                devMode.dmSize = (short)Marshal.SizeOf(typeof(DEVMODE));

                try
                {
                    //get the advanced display settings of the screen
                    if (EnumDisplaySettings(screen.DeviceName, -1, ref devMode))
                    {
                        //used to calculate the actual resolution of the screen (if the scaling factor of the screen has been set in Windows to be something other then 100%)
                        scalingFactor = Math.Round((double)devMode.dmPelsWidth / (double)screen.Bounds.Width, 2);
                        comboBoxItem += " (" + Math.Floor(screen.Bounds.Width * scalingFactor) + "x" + Math.Floor(screen.Bounds.Height * scalingFactor) + "@" + devMode.dmDisplayFrequency + "hz)";
                    }
                }
                catch (Exception e)
                {
                    Debug.WriteLine("Error getting screen scaling factor " + e.Message);
                }

                focusScreenComboBox.Items.Add(comboBoxItem);

                if (i == screenID)
                {
                    found = true;
                    focusScreenComboBox.SelectedIndex = i;
                }
            }

            if (!found)
            {
                focusScreenComboBox.SelectedIndex = -1;
            }

            updatingComboBoxes = false;
        }

        private void AddToWindowsStartupFolder()
        {
            //make the program run automatically when Windows boots up
            try
            {
                string path = Assembly.GetExecutingAssembly().Location;
                if (path != null)
                {
                    path = path.Replace(".dll", ".exe");
                    CreateShortcut(Application.ProductName, Environment.GetFolderPath(Environment.SpecialFolder.Startup), path);
                }
            }
            catch (Exception exception)
            {
                Debug.WriteLine("Error Adding To Startup " + exception.Message);
                runOnStartupCheckBox.Checked = !runOnStartupCheckBox.Checked;
                runOnStartup = runOnStartupCheckBox.Checked;
            }
        }

        private void RemoveFromWindowsStartupFolder()
        {
            //remove the shortcut to this program from the Windows startup folder
            try
            {
                if (System.IO.File.Exists(Environment.GetFolderPath(Environment.SpecialFolder.Startup) + "\\" + Application.ProductName + ".lnk"))
                {
                    System.IO.File.Delete(Environment.GetFolderPath(Environment.SpecialFolder.Startup) + "\\" + Application.ProductName + ".lnk");
                }
            }
            catch (Exception exception)
            {
                Debug.WriteLine("Error Removing From Startup " + exception.Message);
                runOnStartupCheckBox.Checked = !runOnStartupCheckBox.Checked;
                runOnStartup = runOnStartupCheckBox.Checked;
            }
        }

        private void CreateShortcut(string shortcutName, string destinationPath, string sourcePath)
        {
            //creates a shortcut/link to a file
            string shortcutLocation = Path.Combine(destinationPath, shortcutName + ".lnk");
            WshShell shell = new WshShell();
            IWshShortcut shortcut = (IWshShortcut)shell.CreateShortcut(shortcutLocation);
            shortcut.TargetPath = sourcePath;
            shortcut.Save();
        }

        private void QuitApplication()
        {
            SaveSettings();
            UnregisterHotKey(Handle, HOTKEY_ID);
            StopFocusScreen();
        }

        private void ResetHotkey()
        {
            UnregisterHotKey(Handle, HOTKEY_ID);

            //only register the hotkey if the keybind key has been set
            if (keybindKey != 0)
            {
                RegisterHotKey(Handle, HOTKEY_ID, keybindKeyModifiers, keybindKey);
            }
        }

        private string KeybindModifiersToString()
        {
            //convert the number value of the keybind key modifiers to a readable string of what the key(s) represents
            string modifiers = "";
            if (keybindKeyModifiers == 1)
            {
                modifiers = "ALT";
            }
            else if (keybindKeyModifiers == 2)
            {
                modifiers = "CTRL";
            }
            else if (keybindKeyModifiers == 3)
            {
                modifiers = "CTRL + ALT";
            }
            else if (keybindKeyModifiers == 4)
            {
                modifiers = "SHIFT";
            }
            else if (keybindKeyModifiers == 5)
            {
                modifiers = "ALT + SHIFT";
            }
            else if (keybindKeyModifiers == 6)
            {
                modifiers = "CTRL + SHIFT";
            }
            else if (keybindKeyModifiers == 7)
            {
                modifiers = "CTRL + ALT + SHIFT";
            }
            return modifiers;
        }

        protected override void WndProc(ref Message m)
        {
            //detect global keybind being pressed
            if (m.Msg == 0x0312 && m.WParam.ToInt32() == HOTKEY_ID)
            {
                if (isFocused)
                {
                    StopFocusScreen();
                }
                else
                {
                    StartFocusScreen();
                }
            }
            base.WndProc(ref m);
        }

        //
        //External Win32 Interfaces
        //
        [StructLayout(LayoutKind.Sequential)]
        public struct RECT
        {
            public int left;
            public int top;
            public int right;
            public int bottom;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct WINDOWPLACEMENT
        {
            public int length;
            public int flags;
            public int showCmd;
            public POINT ptMinPosition;
            public POINT ptMaxPosition;
            public RECT rcNormalPosition;
            public RECT rcDevice;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct POINT
        {
            public int x;
            public int y;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct DEVMODE
        {
            private const int CCHDEVICENAME = 0x20;
            private const int CCHFORMNAME = 0x20;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 0x20)]
            public string dmDeviceName;
            public short dmSpecVersion;
            public short dmDriverVersion;
            public short dmSize;
            public short dmDriverExtra;
            public int dmFields;
            public int dmPositionX;
            public int dmPositionY;
            public ScreenOrientation dmDisplayOrientation;
            public int dmDisplayFixedOutput;
            public short dmColor;
            public short dmDuplex;
            public short dmYResolution;
            public short dmTTOption;
            public short dmCollate;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 0x20)]
            public string dmFormName;
            public short dmLogPixels;
            public int dmBitsPerPel;
            public int dmPelsWidth;
            public int dmPelsHeight;
            public int dmDisplayFlags;
            public int dmDisplayFrequency;
            public int dmICMMethod;
            public int dmICMIntent;
            public int dmMediaType;
            public int dmDitherType;
            public int dmReserved1;
            public int dmReserved2;
            public int dmPanningWidth;
            public int dmPanningHeight;
        }

        [DllImport("user32.dll", SetLastError = true, EntryPoint = "GetWindowRect")]
        private static extern bool GetWindowRectInternal(IntPtr hWnd, out RECT lpRect);

        private static Rectangle GetWindowRect(IntPtr hwnd)
        {
            RECT rect;
            if (!GetWindowRectInternal(hwnd, out rect))
            {
                throw new Win32Exception(Marshal.GetLastWin32Error());
            }
            return Rectangle.FromLTRB(rect.left, rect.top, rect.right, rect.bottom);
        }

        [DllImport("user32.dll", SetLastError = true)]
        private static extern bool MoveWindow(IntPtr hWnd, int X, int Y, int nWidth, int nHeight, bool bRepaint);

        [DllImport("user32.dll")]
        private static extern bool GetWindowPlacement(IntPtr hWnd, out WINDOWPLACEMENT lpwndpl);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        [DllImport("user32.dll")]
        private static extern bool RegisterHotKey(IntPtr hWnd, int id, int fsModifiers, int vlc);

        [DllImport("user32.dll")]
        private static extern bool UnregisterHotKey(IntPtr hWnd, int id);

        [DllImport("user32.dll")]
        public static extern bool EnumDisplaySettings(string lpszDeviceName, int iModeNum, ref DEVMODE lpDevMode);

        //
        //WinForms UI Events
        //
        private void ToolStripMenuItem_Quit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void settingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Normal;
            Show();
        }

        private void closeButton_Click(object sender, EventArgs e)
        {
            Hide();
        }

        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            LoadComboBoxScreenNames();
            LoadComboBoxAudioDeviceNames();

            WindowState = FormWindowState.Normal;
            Show();
        }

        private void removeSelectedProcessButton_Click(object sender, EventArgs e)
        {
            bool change = false;

            foreach (int index in processBlacklistListView.SelectedIndices)
            {
                change = processBlacklist.Remove(processBlacklistListView.Items[index].Text);
                processBlacklistListView.Items.RemoveAt(index);
            }
            processBlacklistListView.SelectedItems.Clear();
            processBlacklistListView.SelectedIndices.Clear();
            processBlacklistListView.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);

            if (change)
            {
                ResetFocus();
            }
        }

        private void updateTimeTextBox_Leave(object sender, EventArgs e)
        {
            updateTimeTextBox_Confirm();
        }

        private void updateTimeTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Return)
            {
                updateTimeTextBox_Confirm();
            }
        }

        private void updateTimeTextBox_Confirm()
        {
            int prevTime = updateTime;

            int outVal;
            //make sure its a number
            if (int.TryParse(updateTimeTextBox.Text, out outVal))
            {
                if (outVal < 100)
                {
                    outVal = 100;
                }
                updateTime = outVal;

                //only reset focus if the state has changed
                if (prevTime != updateTime)
                {
                    //reset focus to reflect the new change
                    ResetFocus();
                }
            }
            else
            {
                updateTimeTextBox.Text = updateTime.ToString();
            }
        }

        private void addProcessTextBox_Leave(object sender, EventArgs e)
        {
            addProcessTextBox_Confirm();
        }

        private void addProcessTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Return)
            {
                addProcessTextBox_Confirm();
            }
        }

        private void addProcessTextBox_Confirm()
        {
            string processName = addProcessTextBox.Text;
            //make sure its not empty and that it hasn't already been added
            if (!string.IsNullOrEmpty(processName) && !processBlacklist.Contains(processName))
            {
                if (processName.EndsWith(".exe"))
                {
                    processName = processName.Replace(".exe", "");
                }
                processBlacklist.Add(processName);
                processBlacklistListView.Items.Add(processName);
                processBlacklistListView.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);

                //reset focus to reflect the new change
                ResetFocus();
            }
            addProcessTextBox.Text = "";
        }

        private void runOnStartupCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            runOnStartup = runOnStartupCheckBox.Checked;
            if (runOnStartupCheckBox.Checked)
            {
                AddToWindowsStartupFolder();
            }
            else
            {
                RemoveFromWindowsStartupFolder();
            }
        }

        private void keepMouseInScreenBoundsCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            keepMouseInScreenBounds = keepMouseInScreenBoundsCheckBox.Checked;
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            QuitApplication();
        }

        private void keybindModifierTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            int prevKeybindKeyModifiers = keybindKeyModifiers;

            //convert the key combinations into the format the Win32 function RegisterHotkey() wants
            //alt = 1, ctrl = 2, shift = 4
            if (e.Control && e.Alt && e.Shift)
            {
                keybindKeyModifiers = 7;
            }
            else if (e.Shift && e.Control)
            {
                keybindKeyModifiers = 6;
            }
            else if (e.Shift && e.Control)
            {
                keybindKeyModifiers = 5;
            }
            else if (e.Shift)
            {
                keybindKeyModifiers = 4;
            }
            else if (e.Control && e.Alt)
            {
                keybindKeyModifiers = 3;
            }
            else if (e.Control)
            {
                keybindKeyModifiers = 2;
            }
            else if (e.Alt)
            {
                keybindKeyModifiers = 1;
            }
            keybindModifierTextBox.Text = KeybindModifiersToString();

            //if the keybind has changed, reset it
            if (prevKeybindKeyModifiers != keybindKeyModifiers)
            {
                ResetHotkey();
            }

            e.SuppressKeyPress = true;
        }

        private void keybindKeyTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            int prevKeybindKey = keybindKey;

            //check that no keybind modifiers are pressed as we're handling them seperately
            if (e.KeyCode != Keys.ControlKey && e.KeyCode != Keys.Menu && e.KeyCode != Keys.ShiftKey && e.KeyCode != Keys.LWin && e.KeyCode != Keys.RWin)
            {
                keybindKey = (int)e.KeyCode;
            }
            keybindKeyTextBox.Text = ((Keys)keybindKey).ToString();

            //if the keybind has changed, reset it
            if (prevKeybindKey != keybindKey)
            {
                ResetHotkey();
            }

            e.SuppressKeyPress = true;
        }

        private void clearKeybindModifierButton_Click(object sender, EventArgs e)
        {
            //only clear the keybind key modifiers if isn't already cleared
            if (keybindKeyModifiers != 0)
            {
                keybindKeyModifiers = 0;
                keybindModifierTextBox.Text = "";
                ResetHotkey();
            }
        }

        private void clearKeybindButton_Click(object sender, EventArgs e)
        {
            //only clear the keybind key if isn't already cleared
            if (keybindKey != 0)
            {
                keybindKey = 0;
                keybindKeyTextBox.Text = "";
                ResetHotkey();
            }
        }

        private void audioDeviceComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!updatingComboBoxes && audioDeviceComboBox.SelectedIndex >= 0)
            {
                string prevAudioDevice = audioDeviceName;
                audioDeviceName = (string)audioDeviceComboBox.Items[audioDeviceComboBox.SelectedIndex];

                //only reset focus if the state has changed
                if (prevAudioDevice != audioDeviceName)
                {
                    //reset focus to reflect the new change
                    ResetFocus();
                }
            }
        }

        private void focusScreenComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!updatingComboBoxes && focusScreenComboBox.SelectedIndex >= 0)
            {
                //only reset focus if the state has changed
                if (screenID != focusScreenComboBox.SelectedIndex)
                {
                    SetFocusScreen(focusScreenComboBox.SelectedIndex);
                    //reset focus to reflect the new change
                    ResetFocus();
                }
            }
        }
    }
}