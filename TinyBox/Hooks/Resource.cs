using System;
using System.Collections.Generic;
using System.IO;
using FontStashSharp;
using Microsoft.Xna.Framework.Graphics;
using MLEM.Extended.Font;
using MLEM.Extensions;
using MLEM.Font;

namespace TinyBox.Hooks {
    public static class Resource {

        private static GameImpl Game => GameImpl.Instance;

        public static readonly List<FontRef> Fonts = new();
        public static readonly List<Texture2D> Textures = new();

        public static int Font(string name, float scale) {
            var path = ResolvePath(name, "Fonts");
            var font = StaticSpriteFont.FromBMFont(File.ReadAllText(path), f =>
                new TextureWithOffset(Texture2D.FromFile(Game.GraphicsDevice, Path.Join(Path.GetDirectoryName(path), f))));
            Fonts.Add(new FontRef(new GenericStashFont(font), scale));
            return Fonts.Count - 1;
        }

        public static int Tex(string name) {
            var tex = Texture2D.FromFile(Game.GraphicsDevice, ResolvePath(name, "Textures"));
            Textures.Add(tex);
            return Textures.Count - 1;
        }

        public static int StringWidth(int fontId, string strg) {
            var font = Fonts[fontId];
            return (font.Value.MeasureString(strg).X * font.Scale).Ceil();
        }

        public static int StringHeight(int fontId, string strg) {
            var font = Fonts[fontId];
            return (font.Value.MeasureString(strg).Y * font.Scale).Ceil();
        }

        private static string ResolvePath(string name, string builtinPath) {
            if (name.StartsWith("builtin/")) {
                return Path.Combine(Game.Content.RootDirectory, builtinPath, name.Substring(8));
            } else {
                var path = Path.GetDirectoryName(GameHandler.Game.Path);
                return Path.Combine(path!, name);
            }
        }

        public class FontRef {

            public readonly GenericFont Value;
            public readonly float Scale;

            public FontRef(GenericFont value, float scale) {
                this.Value = value;
                this.Scale = scale;
            }

        }

    }
}