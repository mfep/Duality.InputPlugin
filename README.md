# Duality.InputPlugin
Keyboard abstraction plugin for Duality game engine

[![Build status](https://ci.appveyor.com/api/projects/status/mytku45mcv8mjjb3?svg=true)](https://ci.appveyor.com/project/mfep/duality-inputplugin)

## Concepts

### `InputManager`
is a `Component` which enables the user to query the status of the `VirtualButtons`  defined in the `InputMapping`. There is an extension method to easily grab a reference to the `InputManager` in the `Scene.` There can be only one `InputManager` in the `Scene.` Example:
```csharp
float horizontalAxis = this.InputManager().GetAxis("Horizontal");
bool isVerticalPressed = this.InputManager().IsButtonPressed("Vertical");
```

### `InputMapping`
is a `Resource` which lets you define a dictionary of string identifiers and `VirtualButton`s. It is editable through the *Object Inspector*.

![Input mapping in inspector](readme_images/inputMapping.PNG?raw=true)

### `VirtualButton`
has zero or more positive keys and negative keys which can be keyboard keys, mouse buttons, gamepad buttons or gamepad axes. If any of them is hit/pressed/released in the current frame, `InputManager` queries will reflect that.

- Each `VirtualButton` has a current axis value between -1 and 1. Positive keys drive the axis value to 1 while negative keys drive it to -1, according to the `RiseTime` property.
- The `DeadZone` property helps eliminating analog joystick jitter. Any value below `DeadZone` is registered as zero.
- The `DirectionSnap` property if enabled, lets the axis value immediately jump to 0 when the input direction is changed. 

## Install
**`InputPlugin.core.dll` assembly has to be referenced from the game plugin's project to use the InputPlugin in your game!**

![Reference assembly](readme_images/reference.png?raw=true)

### Install from Source
1. Clone the repository
2. Build the `Source\Code\ProjectPlugins.sln` solution.
3. Copy `InputPlugin.core.dll` and `InputPlugin.core.pdb` from `Source\Code\CorePlugin\bin\Debug` to the `Plugins` folder of your Duality project.

### Install via the Duality Package Manager
1. In Dualitor open the package manager window from the `File` menu.
2. Select the `Online repository` option from the combobox labeled `View`, and from the list choose `InputPlugin`.
3. Click `Install` then `Apply`. These operations will download the plugin from nuget.org, and restart Dualitor with these changes.

## License
The code is under MIT license, details in the license file.
Icons used from the [fatcow.com](http://www.fatcow.com/free-icons) collection under [CC-BY-3.0 US](http://creativecommons.org/licenses/by/3.0/us/).
