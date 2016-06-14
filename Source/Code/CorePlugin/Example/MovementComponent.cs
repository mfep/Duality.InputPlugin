using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Duality;
using Key = Duality.Input.Key;

namespace MFEP.Duality.Plugins.InputPlugin.Example
{
    public class MovementComponent : Component, ICmpUpdatable, ICmpInitializable
    {
        public float MovementSpeed { get; set; }

        public void OnInit(InitContext context)
        {
            //var leftBtn = new VirtualButton("Left", new Key[] { Key.Left, Key.A, Key.J, Key.Keypad4 });
            //var rightBtn = new VirtualButton("Right", new Key[] { Key.Right, Key.D });
            //var upBtn = new VirtualButton("Up", new Key[] { Key.Up, Key.W });
            //var downBtn = new VirtualButton("Down", new Key[] { Key.Down, Key.S, Key.K, Key.Keypad5, Key.Keypad2 });
            //InputManager.RegisterButton(leftBtn);
            //InputManager.RegisterButton(rightBtn);
            //InputManager.RegisterButton(upBtn);
            //InputManager.RegisterButton(downBtn);
        }

        public void OnShutdown(ShutdownContext context)
        {
        }

        public void OnUpdate()
        {
            Vector2 direction = Vector2.Zero;
            if (InputManager.IsButtonPressed("Right")) {
                direction += Vector2.UnitX;
            }
            if (InputManager.IsButtonPressed("Left")) {
                direction -= Vector2.UnitX;
            }
            if (InputManager.IsButtonPressed("Up")) {
                direction -= Vector2.UnitY;
            }
            if (InputManager.IsButtonPressed("Down")) {
                direction += Vector2.UnitY;
            }
            GameObj.Transform.MoveByAbs(direction * MovementSpeed * Time.TimeMult);
        }
    }
}
