using Microsoft.Xna.Framework;
using MLEM.Extensions;

namespace TinyBox.Hooks {
    public class Draw : Hook {

        public Draw(GameImpl game) : base(game) {
        }

        public void Pix(int x, int y, int color) {
            this.Rec(x, y, 1, 1, color);
        }

        public void Rec(int x, int y, int width, int height, int color) {
            var tex = this.Batch.GetBlankTexture();
            this.Batch.Draw(tex, new Rectangle(x, y, width, height), ColorHelper.FromHexRgb(color));
        }

    }
}