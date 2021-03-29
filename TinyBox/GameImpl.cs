using System;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MLEM.Extensions;
using MLEM.Input;

namespace TinyBox {
    public class GameImpl : Game {

        public const int Width = 256;
        public const int Height = 256;

        public static GameImpl Instance { get; private set; }
        public SpriteBatch SpriteBatch { get; private set; }
        public InputHandler Input { get; private set; }

        private readonly GraphicsDeviceManager graphicsDeviceManager;
        private RenderTarget2D screen;

        public GameImpl() {
            Instance = this;
            this.Content.RootDirectory = "Content";
            this.graphicsDeviceManager = new GraphicsDeviceManager(this) {HardwareModeSwitch = false};
            this.Window.AllowUserResizing = true;
        }

        protected override void LoadContent() {
            this.graphicsDeviceManager.PreferredBackBufferWidth = Width * 3;
            this.graphicsDeviceManager.PreferredBackBufferHeight = Height * 3;
            this.graphicsDeviceManager.ApplyChanges();

            this.SpriteBatch = new SpriteBatch(this.GraphicsDevice);
            this.Input = new InputHandler(this, true, false, false, false);
            this.screen = new RenderTarget2D(this.GraphicsDevice, Width, Height);

            GameHandler.Initialize();
            GameHandler.LoadGame("Content/Example/font.py");
        }

        protected override void Update(GameTime gameTime) {
            base.Update(gameTime);
            this.Input.Update(gameTime);
            GameHandler.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime) {
            base.Draw(gameTime);

            // draw onto the virtual screen
            using (this.GraphicsDevice.WithRenderTarget(this.screen)) {
                this.GraphicsDevice.Clear(Color.CornflowerBlue);
                this.SpriteBatch.Begin(SpriteSortMode.Deferred, null, SamplerState.PointClamp);
                GameHandler.Draw(gameTime);
                this.SpriteBatch.End();
            }

            // calculate virtual screen size
            var viewport = this.GraphicsDevice.Viewport.Bounds;
            var scale = MathF.Min(viewport.Width / (float) Width, viewport.Height / (float) Height);
            var pos = (viewport.Size.ToVector2() / 2 - new Vector2(Width, Height) * scale / 2).FloorCopy();

            // draw the virtual screen
            this.SpriteBatch.Begin(SpriteSortMode.Immediate, null, SamplerState.PointClamp);
            this.SpriteBatch.Draw(this.screen, pos, null, Color.White, 0, Vector2.Zero, scale, SpriteEffects.None, 0);
            this.SpriteBatch.End();
        }

        public static DirectoryInfo GetDesktop() {
            return GetDataDirectory().CreateSubdirectory("Desktop");
        }

        private static DirectoryInfo GetDataDirectory() {
            var appData = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            return new DirectoryInfo(Path.Combine(appData, "TinyBox"));
        }

    }
}