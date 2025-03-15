using System.Data;

namespace ToggleDualMonitorPointer.Views
{
    public partial class HotKey : Form
    {
        private List<Keys> freePressedKeys = [];
        private List<Keys> blockPressedKeys = [];
        private readonly string path;
        public HotKey(string jsonPath)
        {
            path = jsonPath;
            InitializeComponent();

            if (Program.config != null && Program.config.GetBlockPointerHotKey().Count > 0)
            {
                TbBlockPointer.Text = string.Join("+", Program.config.GetBlockPointerHotKey().Select(k => k.ToString()));
            }

            if (Program.config != null && Program.config.GetFreePointerHotKey().Count > 0)
            {
                TbFreePointer.Text = string.Join("+", Program.config.GetFreePointerHotKey().Select(k => k.ToString()));
            }
        }

        private void BtSave_Click(object sender, EventArgs e)
        {
            if (freePressedKeys.Count == 1)
            {
                freePressedKeys.Clear();
            }

            if (blockPressedKeys.Count == 1)
            {
                blockPressedKeys.Clear();
            }

            Program.config?.SetFreePointerHotKey(freePressedKeys);
            Program.config?.SetBlockPointerHotKey(blockPressedKeys);

            if (Program.config != null)
            {
                Program.WriteJson(path, Program.config);
            }

            this.Close();
        }

        private void TbBlockPointer_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                blockPressedKeys.Clear();
                UpdateBlockTextBox();
                return;
            }

            if (blockPressedKeys.Count < 2)
            {
                if (!blockPressedKeys.Contains(e.KeyCode))
                {
                    blockPressedKeys.Add(e.KeyCode);
                }
            }

            if (blockPressedKeys.Count == 2)
            {
                UpdateBlockTextBox();
            }
        }

        private void TbFreePointer_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                freePressedKeys.Clear();
                UpdateFreeTextBox();
                return;
            }

            if (freePressedKeys.Count < 2)
            {
                if (!freePressedKeys.Contains(e.KeyCode))
                {
                    freePressedKeys.Add(e.KeyCode);
                }
            }

            if (freePressedKeys.Count == 2)
            {
                UpdateFreeTextBox();
            }
        }

        private void UpdateBlockTextBox()
        {
            string keysString = string.Join("+", blockPressedKeys.Select(k => k.ToString()));
            TbBlockPointer.Text = keysString;
        }

        private void UpdateFreeTextBox()
        {
            string keysString = string.Join("+", freePressedKeys.Select(k => k.ToString()));
            TbFreePointer.Text = keysString;
        }
    }
}
