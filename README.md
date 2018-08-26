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
using mfep.Duality.Plugins.InputPlugin;
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
/// <summary>
/// Manager class for every Virtual Button related operation, such as adding and removing buttons,
/// associating keyboard keys and mouse buttons with them, renaming buttons, and getting their status.
/// </summary>
public static class InputManager
{
  /// <summary>
  /// Enumerate through all the <see cref="ButtonTuple"/>s used by the game at the moment.
  /// </summary>
  public static IEnumerable<ButtonTuple> Buttons  {  get  }

  /// <summary>
  /// Called when a new Button is added to the <see cref="InputManager"/>.
  /// </summary>
  public static event Action<ButtonTuple> ButtonAdded;

  /// <summary>
  /// Called when a currently associated Button has been removed from the <see cref="InputManager"/>.
  /// </summary>
  public static event Action<string> ButtonRemoved;

  /// <summary>
  /// Called when a currently associated Button has been renamed.
  /// </summary>
  public static event Action<string, string> ButtonRenamed;

  /// <summary>
  /// Called when a new <see cref="KeyValue"/> has been added to an existing Button.
  /// </summary>
  public static event Action<string, KeyValue> KeyAddedToButton;

  /// <summary>
  /// Called when a <see cref="KeyValue"/> has been removed from an existing Button.
  /// </summary>
  public static event Action<string, KeyValue> KeyRemovedFromButton;

  /// <summary>
  /// Get the <see cref="KeyValue"/>s associated with a particular Button identifier string.
  /// </summary>
  /// <param name="buttonName">The identifier string.</param>
  /// <returns></returns>
  public static KeyValue[] GetKeysOfButton (string buttonName);

  /// <summary>
  /// Registers a new empty Virtual Button with a default name.
  /// </summary>
  public static void RegisterButton ();

  /// <summary>
  /// Registers a new Virtual Button.
  /// </summary>
  /// <param name="newButton">The identifier string and <see cref="KeyValue"/>s of the new Button.</param>
  /// <returns>If the registration was successful, the method returns true, otherwise false.</returns>
  public static bool RegisterButton (ButtonTuple newButton);

  /// <summary>
  /// Removes an existing Virtual Button from the <see cref="InputManager"/>.
  /// </summary>
  /// <param name="buttonName">The particular string identifier of the Button to remove.</param>
  /// <returns>Returns true if the operation succeeded.</returns>
  public static bool RemoveButton (string buttonName);

  /// <summary>
  /// Associates a new <see cref="KeyValue"/> with an existing Virtual Button.
  /// </summary>
  /// <param name="buttonName">The string identifier of the Virtual Button.</param>
  /// <param name="keyValue">The new <see cref="KeyValue"/> to associate.</param>
  /// <returns>Returns true if the operation succeeded.</returns>
  public static bool AddToButton (string buttonName, KeyValue keyValue);

  /// <summary>
  /// Associates a new <see cref="Key"/> with an existing Virtual Button.
  /// </summary>
  /// <param name="buttonName">The string identifier of the Virtual Button.</param>
  /// <param name="newKey">The new <see cref="Key"/> to associate.</param>
  /// <returns>Returns true if the operation succeeded.</returns>
  public static bool AddToButton (string buttonName, Key newKey);

  /// <summary>
  /// Associates a new <see cref="MouseButton"/> with an existing Virtual Button.
  /// </summary>
  /// <param name="buttonName">The string identifier of the Virtual Button.</param>
  /// <param name="mouseButton">The new <see cref="MouseButton"/> to associate.</param>
  /// <returns>Returns true if the operation succeeded.</returns>
  public static bool AddToButton (string buttonName, MouseButton mouseButton);

  /// <summary>
  /// Removes a <see cref="KeyValue"/> from an existing Virtual Button.
  /// </summary>
  /// <param name="buttonName">The string identifier of the Virtual Button.</param>
  /// <param name="keyValue">The <see cref="KeyValue"/> to remove.</param>
  /// <returns>Returns true if the operation succeeded.</returns>
  public static bool RemoveFromButton (string buttonName, KeyValue keyValue);

  /// <summary>
  /// Removes a <see cref="Key"/> from an existing Virtual Button.
  /// </summary>
  /// <param name="buttonName">The string identifier of the Virtual Button.</param>
  /// <param name="key">The <see cref="Key"/> to remove.</param>
  /// <returns>Returns true if the operation succeeded.</returns>
  public static bool RemoveFromButton (string buttonName, Key key);

  /// <summary>
  /// Removes a <see cref="MouseButton"/> from an existing Virtual Button.
  /// </summary>
  /// <param name="buttonName">The string identifier of the Virtual Button.</param>
  /// <param name="mouseButton">The <see cref="MouseButton"/> to remove.</param>
  /// <returns>Returns true if the operation succeeded.</returns>
  public static bool RemoveFromButton (string buttonName, MouseButton mouseButton);

  /// <summary>
  /// Changes the string identifier of an existing Virtual Button.
  /// </summary>
  /// <param name="originalName">The original identifier.</param>
  /// <param name="newName">The new identifier.</param>
  /// <returns>Returns true if the operation succeeded.</returns>
  public static bool RenameButton (string originalName, string newName);

  /// <summary>
  /// Checks if any of the <see cref="KeyValue"/>s associated with a Virtual Button is pressed at the moment.
  /// </summary>
  /// <param name="buttonName">The string identifier of the Virtual Button.</param>
  /// <returns>Returns true, if any of the <see cref="KeyValue"/>s is pressed.</returns>
  public static bool IsButtonPressed (string buttonName);

  /// <summary>
  /// Checks if any of the <see cref="KeyValue"/>s associated with a Virtual Button has been hit in the current frame.
  /// </summary>
  /// <param name="buttonName">The string identifier of the Virtual Button.</param>
  /// <returns>Returns true, if any of the <see cref="KeyValue"/>s is hit.</returns>
  public static bool IsButtonHit (string buttonName);

  /// <summary>
  /// Checks if any of the <see cref="KeyValue"/>s associated with a Virtual Button is released in the current frame.
  /// </summary>
  /// <param name="buttonName">The string identifier of the Virtual Button.</param>
  /// <returns>Returns true, if any of the <see cref="KeyValue"/>s is released.</returns>
  public static bool IsButtonReleased (string buttonName);
}
```

## License
The code is under MIT license, details in the license file.
Icons used from the [fatcow.com](http://www.fatcow.com/free-icons) collection under [CC-BY-3.0 US](http://creativecommons.org/licenses/by/3.0/us/).
