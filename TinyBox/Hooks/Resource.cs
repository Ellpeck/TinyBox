using System.Collections.Generic;
using System.IO;
using MLEM.Extended.Font;
using MLEM.Extensions;
using MLEM.Font;
using MonoGame.Extended.BitmapFonts;

namespace TinyBox.Hooks {
    public class Resource : Hook {

        public static Resource Instance { get; private set; }

        public readonly List<FontRef> Fonts = new();

        public Resource(GameImpl game) : base(game) {
            Instance = this;
        }

        public int Font(string name, float scale) {
            var font = this.Game.Content.Load<BitmapFont>(this.ResolvePath(name, "Fonts"));
            this.Fonts.Add(new FontRef(new GenericBitmapFont(font), scale));
            return this.Fonts.Count - 1;
        }

        public int StringWidth(int fontId, string strg) {
            var font = this.Fonts[fontId];
            return (font.Font.MeasureString(strg).X * font.Scale).Ceil();
        }

        public int StringHeight(int fontId, string strg) {
            var font = this.Fonts[fontId];
            return (font.Font.MeasureString(strg).Y * font.Scale).Ceil();
        }

        private string ResolvePath(string name, string builtinPath) {
            return name.StartsWith("builtin/") ? $"{builtinPath}/{name.Substring(8)}" : name;
        }

        public class FontRef {

            public readonly GenericFont Font;
            public readonly float Scale;

            public FontRef(GenericFont font, float scale) {
                this.Font = font;
                this.Scale = scale;
            }

        }

    }
}