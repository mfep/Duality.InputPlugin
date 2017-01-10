# Duality.InputPlugin
Keyboard abstraction plugin for Duality game engine

[![Build status](https://ci.appveyor.com/api/projects/status/6slo8ymu84yvbbs3?svg=true)](https://ci.appveyor.com/project/mfep/duality-inputplugin)

## Plugin architecture
The tool consists of two plugins, **InputPlugin (Core)** and **InputPlugin (Editor)**. The former contains the extended engine functionalty, which lets you tie multiple physical controls (mouse buttons and keyboard keys) to a so called Virtual Button, and use that in your game logic. All the operations are intended to proceed via the `InputManager` static class (see the usage example below).
The Editor plugin provides a visual user interface for editing the Virtual Buttons in Dualitor. The binding is bi-directional, thus it is useful when debugging button changes at runtime too.
The Virtual Button data is saved to the `keyMapping.xml` file.

## Install
1. In Dualitor open the package manager window from the `File` menu.
2. Select the `Online repository` option from the combobox labeled `View`, and from the list choose `InputPlugin (Editor)`.
3. Click `Install` then `Apply`. These operations will download the two (Editor and Core) plugins from nuget.org, and restart Dualitor with these changes.

## Usage
There are two distinct use cases with the plugin:

### Static input mapping
With static input mapping the Virtual Buttons are not changed at runtime. This case is common with smaller scoped projects, such as game jam entries. For these, the mapping should be defined entirely via the graphic user interface.

1. Open the Input Mapping editor from the `View` menu of Dualitor.
2. Add a Virtual Button by clicking the `Add New Virtual Button` button on the toolstrip.
3. Name the Virtual Button accordingly.
4. Click the `Select input device` to toggle between mouse and keyboard keys.
5. Select the desired mouse button/keyboard key from the dropdown list.
6. Click the green `Add` button to assign the selected key to the VirtualButton.
7. Repeat for all desired Virtual Buttons.

![Static Mapping](https://github.com/mfep/Duality.InputPlugin/raw/master/readme_images/static.png)

#### Using the Virtual Buttons in the game logic
At first, a reference to the InputPlugin.core assembly has to be added to your game plugin project. It can be found at `{Project Directory}\Plugins\InputPlugin.core.dll`. In Visual Studio, it should look like this:

![Reference in Visual Studio](https://github.com/mfep/Duality.InputPlugin/raw/master/readme_images/reference.png)

In your code files, the state of the Virtual Buttons can be requested from `InputManager`. These functions follow Duality's native input naming scheme:

``` csharp
using MFEP.Duality.Plugins.InputPlugin;
...
  public void OnUpdate ()
  {
    if (InputManager.IsButtonPressed ("Left")) {
      // do something
    }
    if (InputManager.IsButtonHit ("Right")) {
      // do something else
    }
    if (InputManager.IsButtonReleased ("Hop")) {
      // hop!
    }
  }
...
```

### Dyanmic input mapping
When the keys associated to the Virtual Buttons change at runtime, it is the case of dynamic input mapping. This can also be done via the `InputManager` static class, with the following application programming interface. The `ButtonTuple` class contains the Virtual Button's name, and the `KeyValues` associated with it. The `KeyValue` struct itself can be casted to `Duality.Input.Key` or `Duality.Input.MouseButton` according to its `KeyType` property.

``` csharp
public static class InputManager
{
  public static IEnumerable<ButtonTuple> Buttons { get; }

  public static event Action<ButtonTuple> ButtonAdded;
  public static event Action<string> ButtonRemoved;
  public static event Action<string, string> ButtonRenamed;
  public static event Action<string, KeyValue> KeyAddedToButton;
  public static event Action<string, KeyValue> KeyRemovedFromButton;

  public static KeyValue[] GetKeysOfButton (string buttonName);

  public static void RegisterButton ();

  public static bool RegisterButton (ButtonTuple newButton);

  public static bool RemoveButton (string buttonName);

  public static bool AddToButton (string buttonName, KeyValue keyValue);

  public static bool AddToButton (string buttonName, Key newKey);

  public static bool AddToButton (string buttonName, MouseButton mouseButton);

  public static bool RemoveFromButton (string buttonName, KeyValue keyValue);

  public static bool RemoveFromButton (string buttonName, Key key);

  public static bool RemoveFromButton (string buttonName, MouseButton mouseButton);

  public static bool RenameButton (string originalName, string newName);

  public static bool IsButtonPressed (string buttonName);

  public static bool IsButtonHit (string buttonName);

  public static bool IsButtonReleased (string buttonName);
}
```

## License
The code is under MIT license, details in the license file.
Icons used from the [fatcow.com](http://www.fatcow.com/free-icons) collection under [CC-BY-3.0 US](http://creativecommons.org/licenses/by/3.0/us/).
