using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MLEM.Extensions;
using MLEM.Misc;
using MLEM.Textures;
using MLEM.Ui;
using MLEM.Ui.Elements;

namespace TinyBox.Ui {
    public static class UiHelper {

        public static readonly Texture2D Texture = GameImpl.LoadContent<Texture2D>("Textures/Ui");

        public static void CreateDesktop(UiSystem ui, DirectoryInfo desktop) {
            var group = new Group(Anchor.TopLeft, Vector2.One, false);
            AddFileButtons(group, desktop);
            ui.Add("Desktop", group);
        }

        private static void AddFileButtons(Element element, DirectoryInfo directory) {
            foreach (var dir in directory.EnumerateDirectories()) {
                element.AddChild(CreateFileButton(dir.Name, new Rectangle(0, 0, 16, 16), e => {
                    var name = $"Directory{dir.Name}";
                    var group = new Group(Anchor.TopLeft, Vector2.One, false);
                    var panel = group.AddChild(new Panel(Anchor.Center, new Vector2(105), Vector2.Zero) {
                        ChildPadding = new Padding(3, 2, 2, 2),
                        Texture = new NinePatch(new TextureRegion(Texture, 32, 0, 16, 16), 7)
                    });
                    panel.AddChild(new Paragraph(Anchor.TopLeft, 1, Path.GetRelativePath(GameImpl.GetDesktop().Parent.FullName, dir.FullName).Replace("\\", "/")));
                    panel.AddChild(new Image(Anchor.TopRight, new Vector2(4), new TextureRegion(Texture, 0, 16, 4, 4)) {
                        OnUpdated = (i, time) => ((Image) i).Color = i.IsMouseOver ? Color.LightGray : Color.White,
                        OnPressed = i => i.System.Remove(name),
                        PositionOffset = new Vector2(-1),
                        CanBeSelected = true,
                        CanBeMoused = true
                    });
                    var content = panel.AddChild(new Panel(Anchor.TopLeft, Vector2.One, new Vector2(0, 6), false, true, new Point(3, 5)) {
                        ChildPadding = new Padding(0, 1, 0, 0),
                        PreventParentSpill = true,
                        Texture = null
                    });
                    AddFileButtons(content, dir);
                    e.System.Remove(name);
                    e.System.Add(name, group);
                }));
            }
            foreach (var file in directory.EnumerateFiles()) {
                element.AddChild(CreateFileButton(file.Name, new Rectangle(16, 0, 16, 16), e => {
                }));
            }
        }

        private static Group CreateFileButton(string name, Rectangle icon, Element.GenericCallback onPressed) {
            var button = new Group(Anchor.AutoInline, new Vector2(32, 24), false) {
                OnDrawn = (e, time, batch, alpha) => {
                    if (e.IsMouseOver)
                        batch.Draw(batch.GetBlankTexture(), e.DisplayArea, new Color(Color.Black, 0.25F));
                },
                Padding = new Vector2(1),
                CanBeSelected = true,
                OnPressed = onPressed
            };
            button.AddChild(new Image(Anchor.TopCenter, new Vector2(16), new TextureRegion(Texture, icon)));
            button.AddChild(new Paragraph(Anchor.BottomCenter, 1, name, true) {PositionOffset = new Vector2(0, 2)});
            return button;
        }

    }
}