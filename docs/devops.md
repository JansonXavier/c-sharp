# DevOps

## Setup

### Install .NET SDK

**macOS (Homebrew):**

```bash
brew install dotnet
```

**Other platforms:**
https://dotnet.microsoft.com/download

### Verify Installation

```bash
dotnet --version
```

## Build & Run

**Run from project folder** (e.g., `hello-world/` or `advent-of-code/`):

**Run directly:**

```bash
cd hello-world
dotnet run
```

**Build then run:**

```bash
cd hello-world
dotnet build
dotnet run
```

**Build output location:**
`bin/Debug/net9.0/HelloWorld.exe` (or `.dll` on non-Windows)

## Build Folders

**`bin/`** - Final compiled output (executables, DLLs)

- Contains runnable files
- Safe to delete (regenerated on build)

**`obj/`** - Intermediate build artifacts (cache, temp files)

- Used during compilation
- Speeds up incremental builds
- Safe to delete (regenerated on build)

Both folders are auto-generated and should be in `.gitignore`.

## Troubleshooting

**Version mismatch error:**

- Check installed version: `dotnet --version`
- Update `TargetFramework` in `.csproj` to match (e.g., `net9.0`)
