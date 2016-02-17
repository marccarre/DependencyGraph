# DependencyGraph
C# Dependency Graph based on Reflection

**Features**:
- List classes and dependencies in the provided assembly.
- Can find dependencies for:
  - inheritance (via both `class`es and `interface`s)
  - constructor arguments
  - `private`/`protected`/`public` method arguments
  - `private`/`protected`/`public` `static` method arguments
  - `private`/`protected`/`public` instance variables
  - `private`/`protected`/`public` `static` variables
  - temporary variables
- Fully unit tested and test-driven.

**Installation & Usage**:
- Go to the release page and download [`DependencyGraphCLI.zip`](https://github.com/marccarre/DependencyGraph/releases/download/snapshot/DependencyGraphCLI.zip).
- Extract `DependencyGraphCLI.zip`.
- Open command prompt (`cmd`) and `cd` to the extracted directory.
- Run `DependencyGraphCLI.exe -a "<path\to\assembly.dll|exe>"`.

The tool will print the dependency graph of the provided assembly to the standard output.
Feel free to redirect the output to a file at your convenience.
