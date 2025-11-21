# Mewton Games Modules

A collection of reusable game modules for Unity.

## Requirements

- Unity 2022.3 or higher

## Installation

Add the package to your Unity project via the Package Manager using the Git URL:

```
https://github.com/mewtongames/unity-game-modules.git
```

Or add directly to your `manifest.json`:

```json
{
    "dependencies": {
        "com.mewtongames.modules": "https://github.com/mewtongames/unity-game-modules.git"
    }
}
```

## Modules

- **Advertisement** - Ads integration with multiple providers (CAS, Web, Mock)
- **Audio** - Audio management with mixer support
- **Common** - Base utilities (Bootstrap, GameContext, ObjectPool, ReactiveProperty, Singleton)
- **DataStorage** - Data persistence with multiple backends (PlayerPrefs, Disk, Web)
- **Device** - Device detection and platform-specific functionality
- **Extensions** - Extension methods for Camera, Collections, GameObject, Physics, Transform, Vector
- **Helpers** - Utility helpers for Enum, GameObject, Layer, Type operations
- **JSON** - JSON serialization with Unity and Newtonsoft converters
- **Leaderboards** - Leaderboard integration
- **Localization** - Multi-language support with various providers
- **Payments** - In-app purchases (Unity IAP, Web, Mock)
- **PushNotifications** - Push notification handling
- **Time** - Time management and timers
- **UI** - UI system with windows, stages and components
- **Vibration** - Haptic feedback support

## License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.
