using System.IO;
using FontStashSharp;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MLEM.Extended.Font;

namespace TinyBox {
    public static class Extensions {

        public static GenericStashFont LoadBitmapFont(string path) {
            return new(StaticSpriteFont.FromBMFont(File.ReadAllText(path), f =>
                new TextureWithOffset(Texture2D.FromFile(GameImpl.Instance.GraphicsDevice, Path.Join(Path.GetDirectoryName(path), f)))));
        }

    }
}