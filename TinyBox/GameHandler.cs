using System;
using IronPython.Hosting;
using Microsoft.Scripting.Hosting;
using Microsoft.Scripting.Runtime;
using Microsoft.Xna.Framework;

namespace TinyBox {
    public static class GameHandler {

        public static LoadedGame Game { get; private set; }
        private static ScriptEngine engine;

        public static void Initialize() {
            engine = Python.CreateEngine();
            engine.Runtime.LoadAssembly(typeof(GameHandler).Assembly);
            engine.SetSearchPaths(new[] {$"{GameImpl.Instance.Content.RootDirectory}/Source"});
        }

        public static void LoadGame(string path) {
            var scope = engine.CreateScope();
            var script = engine.CreateScriptSourceFromFile(path);
            script.Execute(scope);
            Game = new LoadedGame(path, scope, scope.GetVariable("Game"));
            Game.Run();
        }

        public static void Update(GameTime time) {
            Game?.Update(time.ElapsedGameTime.TotalSeconds);
        }

        public static void Draw(GameTime time) {
            Game?.Draw(time.ElapsedGameTime.TotalSeconds);
        }

    }

    public class LoadedGame {

        public readonly string Path;

        private readonly ScriptScope scope;
        private readonly object gameClass;
        private dynamic instance;

        public LoadedGame(string path, ScriptScope scope, object gameClass) {
            this.Path = path;
            this.scope = scope;
            this.gameClass = gameClass;
        }

        public void Run() {
            // set some module variables
            if (this.scope.TryGetVariable("game", out var game)) {
                game.width = GameImpl.Width;
                game.height = GameImpl.Height;
            }

            // create the game instance
            var operations = this.scope.Engine.CreateOperations(this.scope);
            this.instance = operations.CreateInstance(this.gameClass);
        }

        public void Update(double delta) {
            this.instance.update(delta);
        }

        public void Draw(double delta) {
            this.instance.draw(delta);
        }

    }
}