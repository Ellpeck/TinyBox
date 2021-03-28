using System;
using IronPython.Hosting;
using Microsoft.Scripting.Hosting;
using Microsoft.Xna.Framework;

namespace TinyBox {
    public class PythonHandler {

        private readonly ScriptEngine engine;
        private dynamic game;

        public PythonHandler(GameImpl game) {
            this.engine = Python.CreateEngine();
            this.engine.Runtime.LoadAssembly(this.GetType().Assembly);
            this.engine.SetSearchPaths(new[] {$"{game.Content.RootDirectory}/Source"});
        }

        public void LoadGame(string path) {
            var scope = this.engine.CreateScope();
            var operations = scope.Engine.CreateOperations(scope);

            var script = this.engine.CreateScriptSourceFromFile(path);
            script.Execute(scope);

            var gameClass = scope.GetVariable("Game");
            this.game = operations.CreateInstance(gameClass);
        }

        public void Update(GameTime time) {
            if (this.game != null)
                this.game.update(time.ElapsedGameTime.TotalSeconds);
        }

        public void Draw(GameTime time) {
            if (this.game != null)
                this.game.draw(time.ElapsedGameTime.TotalSeconds);
        }

    }
}