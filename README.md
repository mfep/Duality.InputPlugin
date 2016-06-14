# Duality.InputPlugin
Keyboard abstraction plugin for Duality game engine
## Building from source
1. Clone the repository
2. Run DualityEditor.exe. It'll download several packages from the Duality NuGet repository.
3. Don't open any Scene yet, nor run the game. First the plugins have to be built. Open the Visual Studio solution (second button on the Editor's toolstrip).
4. In Visual Studio, first build the CorePlugin project, then the EditorPlugin project. This order is neccessary.
5. Done. The Input Mapping menu is available under View on the menustrip.

(a NuGet package of the plugin will be available in the near future)
