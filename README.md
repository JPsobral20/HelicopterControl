# VR Helicopter Control

A Virtual Reality helicopter control simulation developed with **Unity**, featuring both keyboard-based and VR controller-based flight controls. The project explores different interaction methods for controlling helicopter movement, rotation, and altitude in an immersive environment.

## Overview

The project implements two helicopter control systems:

- **Keyboard Control:** Allows users to control the helicopter using keyboard inputs.
- **VR Controller Control:** Uses SteamVR tracking and controller inputs to provide a more immersive, motion-based interaction experience.

## Features

### Keyboard Controls

The keyboard-based controller supports:

- Forward and backward movement.
- Left and right rotation (yaw).
- Upward and downward movement to control altitude.
- Configurable movement, lift, and rotation speeds.

| Action | Input |
|---|---|
| Move forward | `Up Arrow` |
| Move backward | `Down Arrow` |
| Rotate left | `Left Arrow` |
| Rotate right | `Right Arrow` |
| Increase altitude | `W` |
| Decrease altitude | `S` |

### VR Controls

The VR controller implementation uses SteamVR tracking to translate physical controller movements into helicopter commands.

- **Forward and backward movement:** Based on the controller's positional movement along the Z-axis.
- **Rotation:** Controlled through wrist rotation (yaw).
- **Altitude:** Supports analog thumbstick input, analog input, or discrete up/down buttons.
- **Recentring:** Establishes a neutral controller position and rotation to calibrate motion-based controls.
- **Adjustable sensitivity:** Configurable movement distances, dead zones, rotation angles, and movement speeds.

## Technologies

- **Unity 6** (6000.0.38f1)
- **C#**
- **SteamVR**
- **OpenVR**
- **Unity Input System**
- **Universal Render Pipeline (URP)**

## Getting Started

### Requirements

- Unity 6 (6000.0.38f1)
- SteamVR
- A compatible VR headset and controllers for VR interaction.

### Setup

1. Clone the repository
2. Open the project using Unity Hub with Unity 6 (6000.0.38f1).
3. Allow Unity to resolve and import the project dependencies.
4. Open the `SampleScene` scene.
5. Configure SteamVR and the VR input bindings when using a compatible headset.

### Running the Project

The project includes keyboard and VR controller implementations. The appropriate controller script and input references must be configured in the Unity Inspector for the intended interaction mode.

## Project Structure

```text
VR-HelicopterControl/
├── Assets/
│   ├── Prefabs/
│   ├── Scenes/
│   │   └── SampleScene.unity
│   ├── Scripts/
│   │   ├── HelicopterController.cs
│   │   └── HelicopterVRController.cs
│   └── SteamVR/
├── IBM-Statistics/
├── Packages/
└── ProjectSettings/
```

## Project Context

This project explores the implementation of interactive flight controls in a Virtual Reality environment, comparing conventional keyboard input with motion-based VR controller interaction.
