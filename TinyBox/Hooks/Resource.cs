using System.Collections.Generic;
using System.IO;
using FontStashSharp;

namespace TinyBox.Hooks {
    public class Resource : Hook {

        public static Resource Instance { get; private set; }

        public readonly List<DynamicSpriteFont> Fonts = new();

        public Resource(GameImpl game) : base(game) {
            Instance = this;
        }

        public int Font(string name, int size) {
            var system = new FontSystem(this.Game.GraphicsDevice, 256, 256);
            system.AddFont(File.ReadAllBytes(this.ResolvePath(name, "Fonts")));
            this.Fonts.Add(system.GetFont(size));
            return this.Fonts.Count - 1;
        }

        public int StringWidth(int fontId, string strg) {
            var font = this.Fonts[fontId];
            return (int) font.MeasureString(strg).X;
        }

        public int StringHeight(int fontId, string strg) {
            var font = this.Fonts[fontId];
            return (int) font.MeasureString(strg).Y;
        }

        private string ResolvePath(string name, string builtinPath) {
            return name.StartsWith("builtin/") ? Path.Combine(this.Game.Content.RootDirectory, builtinPath, name.Substring(8)) : name;
        }

    }
}