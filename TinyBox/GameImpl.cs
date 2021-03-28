using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MLEM.Extensions;
using MLEM.Startup;

namespace TinyBox {
    public class GameImpl : MlemGame {

        public const int Width = 256;
        public const int Height = 256;

        public static GameImpl Instance { get; private set; }
        private RenderTarget2D screen;

        public GameImpl() {
            Instance = this;
            this.IsMouseVisible = true;
        }

        protected override void LoadContent() {
            this.GraphicsDeviceManager.PreferredBackBufferWidth = Width;
            this.GraphicsDeviceManager.PreferredBackBufferHeight = Height;
            this.GraphicsDeviceManager.ApplyChanges();

            base.LoadContent();

            this.screen = new RenderTarget2D(this.GraphicsDevice, Width, Height);
        }

        protected override void DoDraw(GameTime gameTime) {
            base.DoDraw(gameTime);

            // draw onto the virtual screen
            using (this.GraphicsDevice.WithRenderTarget(this.screen)) {
                this.GraphicsDevice.Clear(Color.CornflowerBlue);
            }

            // calculate virtual screen size and position
            var viewport = this.GraphicsDevice.Viewport.Bounds;
            var scale = MathF.Min(viewport.Width / (float) Width, viewport.Height / (float) Height);
            var offset = (viewport.Size.ToVector2() / 2 - new Vector2(Width, Height) * scale / 2).FloorCopy();

            // draw the virtual screen
            this.SpriteBatch.Begin(SpriteSortMode.Deferred, null, SamplerState.PointClamp);
            this.SpriteBatch.Draw(this.screen, offset, null, Color.White, 0, Vector2.Zero, scale, SpriteEffects.None, 0);
            this.SpriteBatch.End();
        }

    }
}