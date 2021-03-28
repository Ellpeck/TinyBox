using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MLEM.Extensions;
using MonoGame.Extended.BitmapFonts;

namespace TinyBox.Hooks {
    public class Draw : Hook {

        public Draw(GameImpl game) : base(game) {
        }

        public void Pix(int x, int y, int color) {
            this.Rect(x, y, 1, 1, color);
        }

        public void Rect(int x, int y, int width, int height, int color) {
            var tex = this.Batch.GetBlankTexture();
            this.Batch.Draw(tex, new Rectangle(x, y, width, height), ColorHelper.FromHexRgb(color));
        }

        public void String(int fontId, int x, int y, string strg, int color) {
            var font = Resource.Instance.Fonts[fontId];
            font.Font.DrawString(this.Batch, strg, new Vector2(x, y), ColorHelper.FromHexRgb(color), 0, Vector2.Zero, font.Scale, SpriteEffects.None, 0);
        }

    }
}