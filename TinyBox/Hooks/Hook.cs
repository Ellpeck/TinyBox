using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.Xna.Framework.Graphics;

namespace TinyBox.Hooks {
    public class Hook {

        private static Dictionary<string, Hook> hooks;

        protected readonly GameImpl Game;
        protected SpriteBatch Batch => this.Game.SpriteBatch;

        public Hook(GameImpl game) {
            this.Game = game;
        }

        public static void Initialize(GameImpl game) {
            hooks = Assembly.GetExecutingAssembly().GetExportedTypes().Where(t => typeof(Hook).IsAssignableFrom(t))
                .ToDictionary(t => t.Name, t => (Hook) t.GetConstructor(new[] {typeof(GameImpl)}).Invoke(new object[] {game}));
        }

        public static Hook Get(string name) {
            return hooks[name];
        }

    }
}