# Duality.InputPlugin
Keyboard abstraction plugin for Duality game engine
## Building from source
1. Clone the repository
2. Run DualityEditor.exe. It'll download several packages from the Duality NuGet repository.
3. Don't open any Scene yet, nor run the game. First the plugins have to be built. Open the Visual Studio solution (second button on the Editor's toolstrip).
4. In Visual Studio, first build the CorePlugin project, then the EditorPlugin project. This order is neccessary.
5. Done. The Input Mapping menu is available under View on the menustrip.

(a NuGet package of the plugin will be available in the near future)

##Usage
Under `View` menu, there's a new entry called `Input Mapping`. It spawns a new editor window, which can be snapped, docked and positioned like any other. In that menu, you need to add and name the so called Virtual Buttons, then assign physical keys to them. Do this by selecting the key's name in the drop down list next to the green plus icon, then clicking the icon. The assigned keys can be removed by clicking the red minus icon, or they can be modified just by The plugin saves these changes automatically to the `keyMapping.res` resource file, which you need to ship with the project. If the Virtual Buttons are created, you can refer to them in the code by their name, for example:
``` csharp
Vector2 direction = Vector2.Zero;
if (InputManager.IsButtonPressed("Right")) //Right is the Virtual Button's name
{
    direction += Vector2.UnitX;
}
```
##Known Issues

* At the moment, the user can assing new VirtualButtons to the InputManager via code. These changes do not reflect in the editor window. The user might need to do this (in game keybinding management for example), so it'd be nice to implement this.
* ~~Multiple instances of the single key cannot be added to the same Virtual Button, but if the user changes an existing key, the program allows the same key. This leads to weird behaviour, thus needs to be fixed.~~

##License
The code is under MIT license, details in the license file.
Icons used from the [fatcow.com](http://www.fatcow.com/free-icons) collection under [CC-BY-3.0 US](http://creativecommons.org/licenses/by/3.0/us/).
![](http://i.imgur.com/gK4DzQo.png)
