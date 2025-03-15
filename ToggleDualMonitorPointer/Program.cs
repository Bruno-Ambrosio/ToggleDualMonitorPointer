using Gma.System.MouseKeyHook;
using Newtonsoft.Json;
using System.Runtime.InteropServices;
using ToggleDualMonitorPointer.Config;
using ToggleDualMonitorPointer.Views;

namespace ToggleDualMonitorPointer
{
    internal static class Program
    {
        [DllImport("user32.dll")]
        private static extern void ClipCursor(ref Rectangle rect);

        [DllImport("user32.dll")]
        private static extern void ClipCursor(IntPtr rect);

        private static System.Windows.Forms.Timer? cursorRestrictionTimer;
        private static IKeyboardMouseEvents? _hook;
        private static List<Keys>? keysPressed;
        private static string blockKeyText = "Bloquear cursor";
        private static string freeKeyText = "Liberar cursor";
        internal static ConfigJson? config = new ConfigJson();

        [STAThread]
        static void Main()
        {
            string configPath = "config.json";

            string json = ReadFile(configPath);

            if (!string.IsNullOrEmpty(json))
            {
                try
                {
                    config = ReadJson(json);
                }
                catch (Exception)
                {
                    File.Delete(configPath);
                    WriteJson(configPath, new ConfigJson());
                }
            }
            else
            {
                WriteJson(configPath, new ConfigJson());
            }
           
            ContextMenuStrip contextMenu = new ContextMenuStrip();
            ToolStripMenuItem freePointerMenuItem = new ToolStripMenuItem("");
            ToolStripMenuItem blockPointerMenuItem = new ToolStripMenuItem("");
            ToolStripMenuItem configHotKey = new ToolStripMenuItem("Alterar atalho");
            ToolStripMenuItem exitMenuItem = new ToolStripMenuItem("Sair");

            freePointerMenuItem.Click += (sender, e) => FreePointer();
            blockPointerMenuItem.Click += (sender, e) => BlockPointer();
            configHotKey.Click += (sender, e) => ChangeHotKeys(configPath);
            exitMenuItem.Click += (sender, e) => Application.Exit();

            contextMenu.Items.Add(freePointerMenuItem);
            contextMenu.Items.Add(blockPointerMenuItem);
            contextMenu.Items.Add(configHotKey);
            contextMenu.Items.Add(exitMenuItem);

            InitializeKeyListener(ref freePointerMenuItem, ref blockPointerMenuItem);

            string imagePath = @"Images\icon.ico";
            NotifyIcon notifyIcon = new NotifyIcon
            {
                Icon = new Icon(imagePath),
                ContextMenuStrip = contextMenu,
                Visible = true
            };

            notifyIcon.MouseClick += (sender, e) =>
            {
                if (e.Button == MouseButtons.Right)
                {
                    contextMenu.Items[0].Text = freeKeyText;
                    contextMenu.Items[1].Text = blockKeyText;
                    contextMenu.Show(Cursor.Position);
                }
            };

            Application.Run(new Form
            {
                WindowState = FormWindowState.Minimized,
                ShowInTaskbar = false
            });
        }

        private static void BlockPointer()
        {
            if (blockKeyText.Contains(" X"))
            {
                return;
            }

            blockKeyText += " X";
            freeKeyText = freeKeyText?.Replace(" X", "") ?? string.Empty;

            if (Screen.PrimaryScreen is null)
            {
                MessageBox.Show("Monitor Primário não encontrado.", "Erro");
                return;
            }

            cursorRestrictionTimer = new System.Windows.Forms.Timer
            {
                Interval = 100,
                Enabled = true
            };
            cursorRestrictionTimer.Tick += (sender, e) =>
            {
                if (Screen.PrimaryScreen != null)
                {
                    Rectangle primaryScreenBounds = Screen.PrimaryScreen.Bounds;
                    ClipCursor(ref primaryScreenBounds);
                }
            };
        }
        public static string ReadFile(string path)
        {
            try
            {
                if (File.Exists(path))
                {
                    return File.ReadAllText(path);
                }
                else
                {
                    return string.Empty;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        private static void FreePointer()
        {
            if (freeKeyText.Contains(" X"))
            {
                return;
            }

            freeKeyText += " X";
            blockKeyText = blockKeyText?.Replace(" X", "") ?? string.Empty;

            if (cursorRestrictionTimer != null)
            {
                cursorRestrictionTimer.Dispose();
            }
            ClipCursor(IntPtr.Zero);
        }

        private static void ChangeHotKeys(string path)
        {
            try
            {
                using HotKey form = new(path);
                form.ShowDialog();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public static ConfigJson ReadJson(string json)
        {
            return JsonConvert.DeserializeObject<ConfigJson>(json) ?? new ConfigJson();
        }

        public static void WriteJson(string configPath, ConfigJson config)
        {
            string json = JsonConvert.SerializeObject(config, Formatting.Indented);
            File.WriteAllText(configPath, json);
        }

        private static void InitializeKeyListener(ref ToolStripMenuItem liberarMenuItem, ref ToolStripMenuItem bloquearMenuItem)
        {
            _hook = Hook.GlobalEvents();

            _hook.KeyDown += (sender, e) =>
            {
                keysPressed?.Add(e.KeyCode);

                if (config?.GetFreePointerHotKey().Count > 0)
                {
                    if (AreAllKeysPressed(keysPressed ?? [], config.GetFreePointerHotKey()) && e.KeyCode == config.GetFreePointerHotKey().Last())
                    {
                        FreePointer();
                    }
                }

                if (config?.GetBlockPointerHotKey().Count > 0)
                {
                    if (AreAllKeysPressed(keysPressed ?? [], config.GetBlockPointerHotKey()) && e.KeyCode == config.GetBlockPointerHotKey().Last())
                    {
                        BlockPointer();
                    }
                }

            };

            _hook.KeyUp += (sender, e) =>
            {
                keysPressed?.Remove(e.KeyCode);
            };
        }

        private static bool AreAllKeysPressed(List<Keys> keys, List<Keys> configKeys)
        {
            return keys.All(key => configKeys.Contains(key));
        }

    }
}