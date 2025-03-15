using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToggleDualMonitorPointer.Config
{
    internal class ConfigJson
    {
        public List<string>? FreePointerHotKey { get; set; } = null;
        public List<string>? BlockPointerHotKey { get; set; } = null;

        public List<Keys> GetFreePointerHotKey()
        {
            return FreePointerHotKey?.Select(key => (Keys)Enum.Parse(typeof(Keys), key)).ToList() ?? new List<Keys>();
        }

        public List<Keys> GetBlockPointerHotKey()
        {
            return BlockPointerHotKey?.Select(key => (Keys)Enum.Parse(typeof(Keys), key)).ToList() ?? new List<Keys>();
        }

        public void SetFreePointerHotKey(List<Keys> keys)
        {
            FreePointerHotKey = keys.Select(key => key.ToString()).ToList();
        }

        public void SetBlockPointerHotKey(List<Keys> keys)
        {
            BlockPointerHotKey = keys.Select(key => key.ToString()).ToList();
        }
    }
}
