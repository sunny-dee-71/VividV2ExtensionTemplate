# VividV2 Extension SDK

> **A modding framework for building VR menu extensions — modules, animations, player actions, and categories — with a clean, structured API.**

---

## Table of Contents

- [Getting Started](#getting-started)
- [Manifest](#manifest)
- [Categories](#categories)
  - [Creating Categories](#creating-categories)
  - [Parent & Child Categories](#parent--child-categories)
  - [Hidden Categories](#hidden-categories)
  - [Custom Categories](#custom-categories)
- [Modules](#modules)
  - [Creating a Module](#creating-a-module)
  - [Lifecycle Methods](#lifecycle-methods)
  - [Variables](#variables)
    - [Bool Variable](#bool-variable)
    - [Int Variable](#int-variable)
    - [Float Variable](#float-variable)
    - [Array Variable](#array-variable)
    - [String Variable](#string-variable)
    - [Color Variable](#color-variable)
    - [Keybind Variable](#keybind-variable)
  - [Utility Methods](#utility-methods)
    - [Gun Utility](#gun-utility)
    - [Keyboard Utility](#keyboard-utility)
- [Player Modules](#player-modules)
- [Menu Animations](#menu-animations)
  - [Open Animation](#open-animation)
  - [Close Animation](#close-animation)

---

## Getting Started

Clone or download the extension template, then define your manifest, register your categories, and drop your modules into the appropriate namespaces. The framework handles registration and lifecycle automatically.

---

## Manifest

Every extension must declare a manifest. This identifies your extension to the host framework.

```csharp
internal class Manifest : ExtensionManifest
{
    public override string Name    { get; set; } = "MyExtension";
    public override string Author  { get; set; } = "YourName";
    public override string Version { get; set; } = "1.0.0";
}
```

| Property  | Description                         |
|-----------|-------------------------------------|
| `Name`    | Display name of the extension       |
| `Author`  | Author or team name                 |
| `Version` | Semantic version string             |

---

## Categories

Categories define where your modules appear in the menu. Register them in a central `Categories` class.

### Creating Categories

```csharp
internal class Categories
{
    public static readonly Category Movement = Category.Register("Movement");
    public static readonly Category Visual   = Category.Register("Visual");
}
```

### Parent & Child Categories

You can nest categories using the second parameter of `Register`:

```csharp
public static readonly Category Parent = Category.Register("Parent Category");
public static readonly Category Child  = Category.Register("Child Category", Parent);
```

### Hidden Categories

To prevent a category from appearing on the menu home page, pass `true` as the third parameter:

```csharp
public static readonly Category Hidden = Category.Register("Hidden Category", null, true);
```

### Custom Categories

Custom categories allow you to filter and sort modules within a category using custom logic. Inherit from `BaseCustomCategory` and override `GetButtons()`:

```csharp
public class EnabledCustomCategory : BaseCustomCategory
{
    public override List<BaseButton> GetButtons(List<BaseButton> buttons)
    {
        var enabled = new List<BaseButton>();

        foreach (var module in Main.GetModules())
            if (module.Enabled)
                enabled.Add(module);

        return enabled;
    }
}
```

Then use it when registering a category:

```csharp
public static readonly Category EnabledOnly = Category.Register(
    "Enabled Modules",
    null,
    false,
    new EnabledCustomCategory()
);
```

| Parameter | Type       | Description                                         |
|-----------|------------|-----------------------------------------------------|
| `name`    | `string`   | Display name shown in the menu                      |
| `parent`  | `Category` | *(Optional)* Parent category for nesting            |
| `hidden`  | `bool`     | *(Optional)* If `true`, hides from the home page    |
| `customCategory` | `BaseCustomCategory` | *(Optional)* Custom filtering/sorting logic |

---

## Modules

Modules are the core building block of extensions — each one represents a single feature or behaviour in the menu.

### Creating a Module

Inherit from `Module` and call the base constructor:

```csharp
internal class MyModule : Module
{
    public MyModule() : base("My Module", Categories.Movement, true)
    {
        // Initialize variables here
    }
}
```

| Parameter     | Type       | Description                                                                 |
|---------------|------------|-----------------------------------------------------------------------------|
| `name`        | `string`   | Display name shown in the menu                                              |
| `category`    | `Category` | The category this module belongs to                                         |
| `toggleable`  | `bool`     | If `true`, the module can be toggled on/off. If `false`, it fires once on press. |

> **Note:** When `toggleable` is `false`, `OnEnable` is called each time the button is pressed. `OnDisable` will never be called.

---

### Lifecycle Methods

| Method        | When it runs                                                  |
|---------------|---------------------------------------------------------------|
| `Update()`    | Every frame, regardless of whether the module is enabled      |
| `LateUpdate()`| Every LateUpdate frame, regardless of enabled state           |
| `OnEnable()`  | Called each time the module is toggled on (or pressed, if not toggleable) |
| `OnDisable()` | Called each time the module is toggled off                    |

Use the `Enabled` boolean inside `Update` or `LateUpdate` to guard per-frame logic:

```csharp
public override void Update()
{
    if (Enabled)
    {
        // Per-frame logic while the module is active
    }
}
```

---

### Variables

Variables are configurable settings that appear in the module's menu panel. Declare them in the constructor, then register them with `AddVariable`.

```csharp
FloatVariable speed = new FloatVariable("Speed", 1f, 0f, 10f);
AddVariable(speed);
```

Retrieve a variable at any time using `GetVariable<T>` with the exact name (case-sensitive):

```csharp
float value = GetVariable<FloatVariable>("Speed").Value;
```

> ⚠️ Variable names are **case-sensitive**.

---

#### Bool Variable

A simple true/false toggle.

```csharp
BoolVariable myBool = new BoolVariable("Enable Feature", true);
AddVariable(myBool);
```

| Parameter       | Type   | Description           |
|-----------------|--------|-----------------------|
| `name`          | string | Display name          |
| `defaultValue`  | bool   | Default state         |

---

#### Int Variable

An integer slider with min and max bounds.

```csharp
IntVariable myInt = new IntVariable("Jump Count", 5, 0, 10);
AddVariable(myInt);
```

| Parameter       | Type   | Description     |
|-----------------|--------|-----------------|
| `name`          | string | Display name    |
| `defaultValue`  | int    | Default value   |
| `min`           | int    | Minimum value   |
| `max`           | int    | Maximum value   |

---

#### Float Variable

A float slider with min and max bounds.

```csharp
FloatVariable myFloat = new FloatVariable("Speed", 1.0f, 0.0f, 10.0f);
AddVariable(myFloat);
```

| Parameter       | Type   | Description     |
|-----------------|--------|-----------------|
| `name`          | string | Display name    |
| `defaultValue`  | float  | Default value   |
| `min`           | float  | Minimum value   |
| `max`           | float  | Maximum value   |

---

#### Array Variable

A base class for array-based variables. Provides core functionality for multi-option selectors.

```csharp
ArrayVariable myArray = new ArrayVariable("Options", new object[] { "A", "B", "C" });
AddVariable(myArray);
```

| Parameter   | Type       | Description                                  |
|-------------|------------|----------------------------------------------|
| `name`      | string     | Display name                                 |
| `options`   | `object[]` | Array of selectable options (first = default)|

**Methods:**

| Method | Description |
|--------|-------------|
| `Set(string targetString, bool dontInvokeChange = false)` | Set value by matching option string |
| `Cycle(int direction)` | Cycle through options (1 = next, -1 = previous) |
| `SetOptions(object[] options)` | Update available options |
| `Reset()` | Reset to default value |

---

#### String Variable

A strongly-typed array variable for string options. The first entry in the array is the default.

```csharp
StringVariable myString = new StringVariable("Mode", new string[] { "Walk", "Run", "Sprint" });
AddVariable(myString);
```

Access the selected value:

```csharp
string mode = GetVariable<StringVariable>("Mode").StringValue;
```

| Parameter   | Type       | Description                                  |
|-------------|------------|----------------------------------------------|
| `name`      | string     | Display name                                 |
| `options`   | `string[]` | Array of selectable options (first = default)|

---

#### Color Variable

A colour picker using `UnityEngine.Color`.

```csharp
ColorVariable myColor = new ColorVariable("Trail Color", new UnityEngine.Color(1f, 0f, 0f));
AddVariable(myColor);
```

| Parameter       | Type              | Description                        |
|-----------------|-------------------|------------------------------------|
| `name`          | string            | Display name                       |
| `defaultValue`  | `UnityEngine.Color` | Default colour (RGB or RGBA)     |

---

#### Keybind Variable

Maps a module action to a VR controller input. Three keybind types are available:

---

##### `KeybindType.BothHands`

The user selects a button type only. The selected button triggers on **either** hand.

```csharp
KeybindVariable myKeybind = new KeybindVariable("Activate", KeybindType.BothHands);
AddVariable(myKeybind);
```

> Example: If set to **Trigger** — pressing either the left or right trigger returns `true`.

---

##### `KeybindType.SingleHand`

The user selects both a hand and a button. Only that exact combination triggers.

```csharp
KeybindVariable myKeybind = new KeybindVariable("Fire", KeybindType.SingleHand);
AddVariable(myKeybind);
```

> Example: If set to **Right Grip** — only the right grip returns `true`; left grip does not.

---

##### `KeybindType.Joystick`

The user selects a joystick (left or right). Use `JoystickValue` to read the `Vector2` input. The button parameter is ignored for this type.

```csharp
KeybindVariable myJoystick = new KeybindVariable("Move", KeybindType.Joystick, HandType.Right);
AddVariable(myJoystick);
```

| Property           | Description                          |
|--------------------|--------------------------------------|
| `JoystickValue.x`  | Horizontal axis input                |
| `JoystickValue.y`  | Vertical axis input                  |

---

### Utility Methods

| Method                          | Description                                                         |
|---------------------------------|---------------------------------------------------------------------|
| `SetEnabled(bool)`              | Programmatically enable or disable the module                       |
| `Logger.Log(string)`            | Log a message to the console                                        |
| `Logger.LogError(string)`       | Log an error message to the console                                 |

> ⚠️ Always use `VividV2.Core.Logger` — **not** the BepInEx logger.

---

#### Gun Utility

The Gun Utility provides a simple way to create gun-based interactions. It handles rendering the gun, detecting trigger input, tracking where the gun is pointing, and optionally snapping to players.

Call `GunLibUtils.UpdateGun()` once every frame inside your module's `Update()` method.

##### Method

```csharp
GunInfo GunLibUtils.UpdateGun(bool snapToPlayers);
```

###### Parameters

| Parameter | Type | Description |
|-----------|------|-------------|
| `snapToPlayers` | `bool` | If `true`, the gun will automatically snap to nearby players and populate `RigAimedAt`. If `false`, the gun behaves as a normal world-space pointer. |

###### Returns

`UpdateGun()` returns a `GunInfo` object containing information about the current state of the gun.

| Property | Type | Description |
|----------|------|-------------|
| `TriggerPressed` | `bool` | `true` while the trigger is being pressed. |
| `GunActive` | `bool` | Indicates whether the gun is currently active and should be used. |
| `RigAimedAt` | `VRRig` | The player currently being aimed at. Will be `null` if no player is targeted or player snapping is disabled. |
| `GunPosition` | `Vector3` | The current world-space position of the gun. |

###### Example

```csharp
public override void Update()
{
    GunInfo gun = GunLibUtils.UpdateGun(true);

    if (!gun.GunActive)
        return;

    if (gun.TriggerPressed && gun.RigAimedAt != null)
    {
        // Interact with the targeted player.
    }
}
```

> **Tip:** Use `snapToPlayers = true` for player-targeted modules (Kick, Tag, Crash, etc.). Use `false` when interacting with the world instead of players.

---

#### Keyboard Utility

The Keyboard Utility displays the in-game keyboard and asynchronously returns the text entered by the user.

Since keyboard input is asynchronous, it should always be awaited from an `async` method.

##### Methods

```csharp
Task<string> KeyboardUtils.RequestString(string prompt);

Task<string> KeyboardUtils.RequestString(
    string prompt,
    Action<string> onInputChanged
);
```

###### Parameters

RequestString(string prompt)

| Parameter | Type | Description |
|-----------|------|-------------|
| `prompt` | `string` | The message displayed above the keyboard. |

RequestString(string prompt, Action<string> onInputChanged)

| Parameter | Type | Description |
|-----------|------|-------------|
| `prompt` | `string` | The message displayed above the keyboard. |
| `onInputChanged` | `Action<string>` | Callback invoked every time the user changes the keyboard text. |

###### Returns

Both overloads return:

| Type | Description |
|------|-------------|
| `Task<string>` | Completes when the user presses **Enter**, returning the final text entered. |

###### Basic Example

```csharp
public override void OnEnable()
{
    Task.Run(async () => await OpenKeyboard());
}

private static async Task OpenKeyboard()
{
    string input = await KeyboardUtils.RequestString("Enter your name:");

    Logger.Log(input);
}
```

###### Live Input Example

```csharp
private static async Task OpenKeyboard()
{
    void OnInputChanged(string text)
    {
        Logger.Log($"Current Input: {text}");
    }

    string input = await KeyboardUtils.RequestString(
        "Enter your name:",
        OnInputChanged
    );

    Logger.Log($"Final Input: {input}");
}
```

> **Note:** `onInputChanged` is called every time the text changes, while the returned `Task<string>` completes only after the user submits the keyboard by pressing **Enter**.

---

## Player Modules

Player Modules function identically to regular modules, but are scoped to a specific player. They appear as per-player actions in the menu.

```csharp
internal class TeleportToPlayer : PlayerModule
{
    public TeleportToPlayer() : base("Teleport To", false)
    {
        // Variables can be added here just like normal modules
    }

    public override void OnEnable()
    {
        GorillaLocomotion.GTPlayer.Instance.TeleportTo(targetRig.transform);
    }
}
```

| Parameter     | Type    | Description                                  |
|---------------|---------|----------------------------------------------|
| `name`        | string  | Display name shown in the player menu        |
| `toggleable`  | bool    | Whether the action is toggleable or one-shot |

The `targetRig` property is automatically set to the rig of the player the module is being used on.

---

## Menu Animations

Animations control how the menu opens and closes. Inherit from `MenuAnimation` and implement the `Animate` method.

The `Animate` method receives:
- `AnimationPercentage` — a `float` from `0.0` (start) to `1.0` (end)
- `target` — a `MenuAnimator` you mutate and return

Properties available on `MenuAnimator` include `Scale`, `Position`, `Rotation`, and others.

---

### Open Animation

Called as the menu opens. `AnimationPercentage` goes from `0` → `1`.

```csharp
public class MyOpenAnimation : MenuAnimation
{
    public MyOpenAnimation() : base("MyOpenAnimation", MenuAnimationType.MenuOpen)
    {
    }

    public override MenuAnimator Animate(float AnimationPercentage, MenuAnimator target)
    {
        // Scale the menu in from zero to full size
        target.Scale = target.Scale - (target.Scale * (1 - AnimationPercentage));
        return target;
    }
}
```

---

### Close Animation

Called as the menu closes. `AnimationPercentage` goes from `0` → `1`.

```csharp
public class MyCloseAnimation : MenuAnimation
{
    public MyCloseAnimation() : base("MyCloseAnimation", MenuAnimationType.MenuClose)
    {
    }

    public override MenuAnimator Animate(float AnimationPercentage, MenuAnimator target)
    {
        // Scale the menu out to zero
        target.Scale = target.Scale - (target.Scale * AnimationPercentage);
        return target;
    }
}
```

---

*VividV2 Extension SDK — documentation reflects the current template version.*
