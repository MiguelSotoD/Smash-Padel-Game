# 🎾 Smash Padel Game

Un juego de pádel 2D desarrollado en Unity con controles simples y múltiples modos de juego. Dos jugadores compiten en emocionantes partidos donde el control del salto es clave para golpear la pelota.

![Unity](https://img.shields.io/badge/Unity-2022+-black?logo=unity)
![C#](https://img.shields.io/badge/C%23-10.0-blue?logo=csharp)
![License](https://img.shields.io/badge/license-MIT-green)

## 📋 Descripción

Smash Padel Game es un juego arcade de pádel en 2D donde dos jugadores controlan personajes que solo pueden saltar para moverse verticalmente. El objetivo es golpear la pelota con la raqueta y hacer que pase por el lado del oponente para marcar puntos.

### ✨ Características Principales

- 🎮 **Multiplayer Local**: Modo para 2 jugadores en el mismo dispositivo
- 🎯 **Sistema de Puntuación**: Contador de puntos configurable
- 🎪 **Múltiples Modos de Juego**:
  - **Clásico**: Partida tradicional hasta X puntos
  - **Pelota Rápida**: Velocidad aumentada para mayor desafío
  - **Gravedad Baja**: Física alterada para partidas más estratégicas
  - **Aleatorio**: Modo sorpresa que cambia cada ronda
- 🎨 **Estilo Pixel Art**: Gráficos retro con animaciones fluidas
- 🔊 **Sistema de Audio**: Efectos de sonido para saltos, golpes y puntos
- 🎯 **Sweet Spot System**: Golpes potenciados al golpear con la zona perfecta de la raqueta
- ⏸️ **Sistema de Pausa**: Controla el flujo del juego
- 🎬 **Animaciones**: Personajes animados (idle, salto, caída)

## 🎮 Controles

### Jugador 1 (Izquierda)
- **Saltar**: `Espacio` / `W`
- Mantén presionado para "volar" con múltiples saltos

### Jugador 2 (Derecha)
- **Saltar**: `Flecha Arriba` / Segundo control configurado
- Mantén presionado para "volar" con múltiples saltos

### Generales
- **Pausa**: `ESC`
- **Menú Principal**: Accesible desde la pausa

## 🛠️ Requisitos del Sistema

### Para Desarrollo
- **Unity**: 2022.3 LTS o superior
- **Sistema Operativo**: Windows 10/11, macOS, o Linux
- **.NET**: Compatible con la versión de Unity

### Para Jugar
- **Procesador**: Intel Core i3 o equivalente
- **Memoria RAM**: 2 GB
- **Gráficos**: Tarjeta gráfica compatible con DX10
- **Almacenamiento**: 100 MB de espacio disponible

## 📦 Instalación

### Clonar el Repositorio

```bash
git clone https://github.com/MiguelSotoD/Smash-Padel-Game.git
cd Smash-Padel-Game
```

### Abrir en Unity

1. Abre **Unity Hub**
2. Click en **"Open"** o **"Add"**
3. Navega a la carpeta del proyecto clonado
4. Selecciona la carpeta `PadelGame`
5. Unity abrirá y cargará el proyecto automáticamente

### Primera Ejecución

1. En Unity, ve a `File > Build Settings`
2. Asegúrate de que las escenas estén agregadas:
   - `MainMenu.unity` (Escena 0)
   - `SampleScene.unity` (Escena 1)
3. Presiona **Play** en el editor para probar el juego

## 🎯 Cómo Jugar

1. **Inicia el juego** desde el menú principal
2. **Selecciona el modo de juego** deseado
3. **Objetivo**: Golpea la pelota con tu raqueta y haz que pase por el lado del oponente
4. **Estrategia**: 
   - Usa saltos múltiples para posicionarte correctamente
   - Golpea la pelota en el "sweet spot" para golpes más potentes
   - Anticipa la trayectoria de la pelota
5. **Victoria**: El primer jugador en alcanzar el límite de puntos gana

## 📁 Estructura del Proyecto

```
PadelGame/
├── Assets/
│   ├── Animation/          # Animaciones de personajes y UI
│   ├── Audio/             # Efectos de sonido y música
│   ├── Scenes/            # Escenas del juego
│   │   ├── MainMenu.unity
│   │   └── SampleScene.unity
│   ├── Scripts/           # Scripts de C#
│   │   ├── BallController.cs
│   │   ├── PlayerController.cs
│   │   ├── RacketController.cs
│   │   ├── GameManager.cs
│   │   ├── UIManager.cs
│   │   ├── GoalZone.cs
│   │   ├── MainMenu.cs
│   │   ├── PausarJuego.cs
│   │   └── PlayerSoundController.cs
│   ├── Sprites/           # Sprites y recursos gráficos
│   ├── Settings/          # Configuración de render pipeline
│   └── TextMesh Pro/      # Fuentes y recursos de TextMesh Pro
├── ProjectSettings/       # Configuración del proyecto Unity
├── Packages/             # Paquetes y dependencias
└── README.md
```

## 🧩 Componentes Principales

### Scripts del Juego

- **`GameManager.cs`**: Gestiona el estado del juego, puntuación y modos
- **`PlayerController.cs`**: Controla el movimiento y salto de los jugadores
- **`BallController.cs`**: Maneja la física y comportamiento de la pelota
- **`RacketController.cs`**: Detecta colisiones y aplica fuerzas a la pelota
- **`UIManager.cs`**: Gestiona la interfaz de usuario y mensajes
- **`GoalZone.cs`**: Detecta cuando la pelota sale del campo
- **`MainMenu.cs`**: Controla la navegación del menú principal
- **`PausarJuego.cs`**: Sistema de pausa

### Sistemas Implementados

- ✅ Sistema de física 2D
- ✅ Sistema de colisiones
- ✅ Sistema de animaciones
- ✅ Sistema de audio
- ✅ Sistema de UI con TextMesh Pro
- ✅ Sistema de modos de juego
- ✅ Sistema de spawn y reset
- ✅ Sistema de entrada (Input System)

## 🔧 Configuración del Juego

### Tags Requeridos
- `Player`
- `Ball`
- `Ground`
- `Wall`
- `LeftGoal`
- `RightGoal`

### Layers Requeridos
- `Ground`

### Configuración Recomendada en GameManager
- **Puntos para Ganar**: 5 (ajustable)
- **Velocidad Pelota Rápida**: 8f
- **Escala de Gravedad Baja**: 1f

## 🚀 Build y Distribución

### Crear Build para Windows

1. Ve a `File > Build Settings`
2. Selecciona **Windows** como plataforma
3. Asegúrate de que las escenas estén en orden:
   - MainMenu (índice 0)
   - SampleScene (índice 1)
4. Click en **Build** y elige una carpeta de destino
5. El juego ejecutable estará en la carpeta seleccionada

### Crear Build para otras Plataformas

Sigue el mismo proceso seleccionando la plataforma deseada (Mac, Linux, WebGL, etc.)

## 📚 Documentación Adicional

- [Guía de Implementación Completa](Assets/Scripts/GUIA_IMPLEMENTACION_COMPLETA.md)
- [Guía del Contador de Puntos](GUIA_CONTADOR_PUNTOS.md)
- [Instrucciones Rápidas de Setup](Assets/Scripts/INSTRUCCIONES_RAPIDAS.txt)
- [Setup de Configuración](Assets/Scripts/README_SETUP.md)

## 🐛 Resolución de Problemas

### El contador de puntos no funciona
- Verifica que los objetos `Paddel1Score` y `Paddel2Score` existan en el Canvas
- Asegúrate de usar TextMeshPro en lugar de Text (Legacy)
- Revisa las referencias en el GameManager Inspector

### Los personajes no saltan
- Verifica que tengan el componente `Rigidbody2D`
- Asegúrate de que el Gravity Scale esté configurado (2 recomendado)
- Revisa que las teclas estén correctamente asignadas

### La pelota no rebota correctamente
- Verifica que el suelo tenga el tag `Ground`
- Asegúrate de que la pelota tenga `Rigidbody2D` con Gravity Scale 0
- Revisa el `BallController` y su configuración

## 🤝 Contribuciones

Las contribuciones son bienvenidas. Para contribuir:

1. Fork el proyecto
2. Crea una rama para tu feature (`git checkout -b feature/AmazingFeature`)
3. Commit tus cambios (`git commit -m 'Add some AmazingFeature'`)
4. Push a la rama (`git push origin feature/AmazingFeature`)
5. Abre un Pull Request

## 📝 Licencia

Este proyecto está bajo la Licencia MIT. Ver archivo `LICENSE` para más detalles.

## 👨‍💻 Autor

**Miguel Soto**
- GitHub: [@MiguelSotoD](https://github.com/MiguelSotoD)

## 🎮 Capturas del Juego

_[Aquí puedes agregar capturas de pantalla del juego]_

## 🙏 Agradecimientos

- Unity Technologies por el motor de juego
- Comunidad de Unity por recursos y tutoriales
- TextMesh Pro por el sistema de texto mejorado
- Universal Render Pipeline (URP) para mejores gráficos 2D

---

⭐ Si te gusta este proyecto, ¡no olvides darle una estrella en GitHub!
