using System;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MLEM.Extensions;
using MLEM.Input;
using MLEM.Textures;
using MLEM.Ui;
using MLEM.Ui.Style;
using TinyBox.Ui;

namespace TinyBox {
    public class GameImpl : Game {

        public const int Width = 128;
        public const int Height = 128;

        public static GameImpl Instance { get; private set; }
        public SpriteBatch SpriteBatch { get; private set; }
        public InputHandler Input { get; private set; }
        public UiSystem UiSystem { get; private set; }

        private readonly GraphicsDeviceManager graphicsDeviceManager;
        private RenderTarget2D screen;
        private Rectangle screenArea;
        private float screenScale;

        public GameImpl() {
            Instance = this;
            this.IsMouseVisible = true;
            this.Content.RootDirectory = "Content";
            this.graphicsDeviceManager = new GraphicsDeviceManager(this) {HardwareModeSwitch = false};
            this.Window.ClientSizeChanged += (o, a) => this.CalculateScreenSize();
            this.Window.AllowUserResizing = true;
        }

        protected override void LoadContent() {
            this.graphicsDeviceManager.PreferredBackBufferWidth = Width * 6;
            this.graphicsDeviceManager.PreferredBackBufferHeight = Height * 6;
            this.graphicsDeviceManager.ApplyChanges();

            this.SpriteBatch = new SpriteBatch(this.GraphicsDevice);
            this.Input = new InputHandler(this);
            this.Components.Add(this.Input);
            this.UiSystem = new UiSystem(this, new UntexturedStyle(this.SpriteBatch) {
                Font = Extensions.LoadBitmapFont(Path.Combine(this.Content.RootDirectory, "Fonts", "Chava.fnt")),
                ScrollBarBackground = this.SpriteBatch.GenerateTexture(ColorHelper.FromHexRgb(0x2a232d), Color.Transparent),
                ScrollBarScrollerTexture = new NinePatch(new TextureRegion(UiHelper.Texture, 48, 0, 16, 16), 2),
                TextColor = Color.Black,
                TextScale = 0.1F
            }, this.Input, false);
            this.Components.Add(this.UiSystem);
            this.screen = new RenderTarget2D(this.GraphicsDevice, Width, Height);

            this.CalculateScreenSize();
            GameHandler.Initialize();

            UiHelper.CreateDesktop(this.UiSystem, GetDesktop());
        }

        protected override void Update(GameTime gameTime) {
            base.Update(gameTime);
            GameHandler.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime) {
            this.UiSystem.DrawEarly(gameTime, this.SpriteBatch);
            base.Draw(gameTime);

            // draw onto the virtual screen
            using (this.GraphicsDevice.WithRenderTarget(this.screen)) {
                this.GraphicsDevice.Clear(Color.CornflowerBlue);
                this.SpriteBatch.Begin(SpriteSortMode.Deferred, null, SamplerState.PointClamp);
                GameHandler.Draw(gameTime);
                this.SpriteBatch.End();
            }

            // draw the virtual screen
            this.SpriteBatch.Begin(SpriteSortMode.Immediate, null, SamplerState.PointClamp);
            this.SpriteBatch.Draw(this.screen, this.screenArea, Color.White);
            this.SpriteBatch.End();

            this.UiSystem.Draw(gameTime, this.SpriteBatch);
        }

        private void CalculateScreenSize() {
            var viewport = this.GraphicsDevice.Viewport.Bounds;
            this.screenScale = MathF.Min(viewport.Width / (float) Width, viewport.Height / (float) Height);
            this.screenArea = new Rectangle(
                (viewport.Size.ToVector2() / 2 - new Vector2(Width, Height) * this.screenScale / 2).ToPoint(),
                (new Vector2(Width, Height) * this.screenScale).CeilCopy().ToPoint());
            this.UiSystem.Viewport = this.screenArea;
            this.UiSystem.GlobalScale = this.screenScale;
        }

        public static T LoadContent<T>(string name) {
            return Instance.Content.Load<T>(name);
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