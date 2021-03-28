using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MLEM.Extensions;

namespace TinyBox.Hooks {
    public static class Draw {

        private static SpriteBatch Batch => GameImpl.Instance.SpriteBatch;

        public static void Pix(int x, int y, int color) {
            var tex = Batch.GetBlankTexture();
            Batch.Draw(tex, new Rectangle(x, y, 1, 1), ColorHelper.FromHexRgb(color));
        }

        public static void Rect(int x, int y, int width, int height, int color) {
            var tex = Batch.GetBlankTexture();
            Batch.Draw(tex, new Rectangle(x, y, width, height), ColorHelper.FromHexRgb(color));
        }

        public static void Tex(int textureId, int x, int y, int u, int v, int w, int h, int color, int scale, int flip) {
            var texture = Resource.Textures[textureId];
            Batch.Draw(texture, new Vector2(x, y), new Rectangle(u, v, w, h), ColorHelper.FromHexRgb(color), 0, Vector2.Zero, scale, (SpriteEffects) flip, 0);
        }

        public static void String(int fontId, int x, int y, string strg, int color) {
            var font = Resource.Fonts[fontId];
            font.Value.DrawString(Batch, strg, new Vector2(x, y), ColorHelper.FromHexRgb(color), 0, Vector2.Zero, font.Scale, SpriteEffects.None, 0);
        }

    }
}