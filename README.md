
# Pray Status App

A Windows Forms application displaying prayer times in Persian. The app is borderless, draggable, and shows a notification icon in the system tray.

## Features
- Transparent, draggable, borderless form.
- Displays prayer times in Persian.
- System tray icon with context menu.
- Embedded resources for icons and images.

---

## Prerequisites
- **.NET Framework 4.7.2 or later**.
- **C# Compiler (`csc.exe`)** available in your system (`C:\Windows\Microsoft.NET\Framework\v4.0.30319` for .NET Framework 4.x).

---

## How to Build

### Step 1: Compile the Application

To build the `PrayStatusApp` using the C# compiler (`csc`), follow these steps:

1. Open **Command Prompt**.
2. Navigate to the directory containing `PrayStatusApp.cs`:
   ```bash
   cd path\to\your\project
   ```
3. Run the following command to compile the application:

   ```bash
   "C:\Windows\Microsoft.NET\Framework\v4.0.30319\csc.exe" /target:winexe /out:PrayStatusApp.exe /platform:x86 /resource:icon.ico,icon.ico /resource:logo.png,logo.png PrayStatusApp.cs
   ```

   **Explanation of the command:**
   - `/target:winexe`: Creates a Windows executable without a console window.
   - `/out:PrayStatusApp.exe`: Sets the output file name.
   - `/platform:x86`: Targets 32-bit systems (adjust to `x64` or `anycpu` if needed).
   - `/resource:icon.ico,icon.ico`: Embeds `icon.ico` as a resource.
   - `/resource:logo.png,logo.png`: Embeds `logo.png` as a resource.
   - `PrayStatusApp.cs`: The main source code file.

---

## How to Publish

### Step 1: Prepare a Release Build
1. Ensure all necessary resources (`icon.ico`, `logo.png`) are available and embedded.
2. Compile the app using the **release configuration** by running the same command as above.

### Step 2: Distribute the Executable
1. Copy `PrayStatusApp.exe` to the desired location or package it into a `.zip` file.
2. Distribute `PrayStatusApp.exe` as a standalone executable.

---

## Notes

1. **Icon File Requirements**: Ensure `icon.ico` is a valid `.ico` file format (Windows icons only support `.ico`).
2. **Resource Embedding**: Embedded resources can be verified using tools like **ILSpy** or **dotPeek** to confirm correct embedding.
3. **Adjustments**: Change the paths or target platform (`x64` or `AnyCPU`) based on your environment needs.

---

## Troubleshooting

### Error: `Resource Not Found`
- Verify that the resource names are correctly referenced in the code:
  ```csharp
  LoadEmbeddedIcon("icon.ico");
  LoadEmbeddedImage("logo.png");
  ```
- Ensure resources are embedded using `/resource` in the `csc` command.
- Check the `.ico` and `.png` paths and make sure they are correct.
  
---

## Author
- Developed by: **@AR-Taheri** , **@AliMirDeveloper**
```

---

Let me know if you need more help!
