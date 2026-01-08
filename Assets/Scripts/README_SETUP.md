# Guía de Configuración del Juego Padel

## Scripts Creados

### Scripts Principales

1. **PlayerController.cs**
   - Controla el movimiento del jugador (solo saltos)
   - Maneja saltos consecutivos para "volar"
   - Limita la altura máxima
   - Requiere: Rigidbody2D, Player Number (1 o 2)

2. **BallController.cs**
   - Controla la física de la pelota
   - Rebotes automáticos en el suelo
   - Detección de puntos cuando sale por los lados
   - Requiere: Rigidbody2D, Tags: "Ball", "Ground", "LeftGoal", "RightGoal"

3. **RacketController.cs**
   - Detecta golpes de la pelota
   - Sistema de "sweet spot" para golpes potenciados
   - Requiere: Collider2D (Trigger), referencia a PlayerController

4. **GameManager.cs**
   - Gestiona puntos y estado del juego
   - Controla los modos de juego (Clásico, Pelota Rápida, Gravedad Baja, Aleatorio)
   - Requiere: Referencias a BallController, PlayerController (x2), UIManager

5. **UIManager.cs**
   - Gestiona la interfaz de usuario
   - Muestra marcador y mensajes de game over
   - Requiere: TextMeshProUGUI para scores y game over

6. **GoalZone.cs**
   - Detecta cuando la pelota sale por los lados
   - Requiere: Collider2D (Trigger), Tag: "LeftGoal" o "RightGoal"

### Scripts Auxiliares

7. **Boundary.cs** - Marca objetos como paredes
8. **Ground.cs** - Marca objetos como suelo
9. **GameSetup.cs** - Utilidad para configuración

## Configuración de la Escena

### 1. Configurar Tags (Edit > Project Settings > Tags and Layers)

Crear los siguientes tags:
- `Player`
- `Ball`
- `Ground`
- `Wall`
- `LeftGoal`
- `RightGoal`

### 2. Configurar Capas (Edit > Project Settings > Tags and Layers)

Crear la capa:
- `Ground`

### 3. Configurar Jugadores

Para cada jugador (personaje_0 y personaje2):

1. Agregar componente `Rigidbody2D`:
   - Gravity Scale: 2
   - Constraints: Freeze Position X, Freeze Rotation

2. Agregar componente `PlayerController`:
   - Player Number: 1 (izquierda) o 2 (derecha)
   - Jump Force: 10
   - Max Height: 4
   - Gravity Scale: 2

3. Agregar componente `BoxCollider2D` o `CircleCollider2D`:
   - Ajustar tamaño según el sprite

4. Crear un hijo para la raqueta:
   - Agregar componente `RacketController`
   - Agregar componente `BoxCollider2D` o `CircleCollider2D` (marcar como Trigger)
   - Configurar Sweet Spot Radius: 0.3

### 4. Configurar Pelota

1. Agregar componente `Rigidbody2D`:
   - Gravity Scale: 0
   - Drag: 0
   - Angular Drag: 0

2. Agregar componente `BallController`:
   - Initial Speed: 5
   - Max Speed: 15
   - Min Bounce Velocity: 3

3. Agregar componente `CircleCollider2D`:
   - Ajustar radio según el sprite

4. Asignar tag: `Ball`

### 5. Configurar Suelo

1. El objeto "Square" ya tiene `BoxCollider2D`
2. Agregar componente `Ground`
3. Asignar tag: `Ground`
4. Asignar layer: `Ground`

### 6. Configurar Zonas de Gol

Crear dos objetos vacíos (GameObject vacío):

**LeftGoal** (lado izquierdo de la pantalla):
1. Agregar componente `BoxCollider2D`:
   - Marcar como Trigger
   - Ajustar tamaño para cubrir el lado izquierdo
2. Agregar componente `GoalZone`:
   - Player Number: 1
3. Asignar tag: `LeftGoal`

**RightGoal** (lado derecho de la pantalla):
1. Agregar componente `BoxCollider2D`:
   - Marcar como Trigger
   - Ajustar tamaño para cubrir el lado derecho
2. Agregar componente `GoalZone`:
   - Player Number: 2
3. Asignar tag: `RightGoal`

### 7. Configurar Paredes (Opcional)

Si quieres paredes en los lados superior e inferior:
1. Crear objetos con `BoxCollider2D`
2. Agregar componente `Boundary`
3. Asignar tag: `Wall`

### 8. Configurar GameManager

1. Crear un GameObject vacío llamado "GameManager"
2. Agregar componente `GameManager`:
   - Points To Win: 5
   - Current Game Mode: Classic
   - Asignar referencias a Ball, Player1, Player2, UIManager

### 9. Configurar UI

1. Crear Canvas (si no existe)
2. Crear TextMeshPro para Player1Score
3. Crear TextMeshPro para Player2Score
4. Crear Panel para GameOver con TextMeshPro
5. Agregar componente `UIManager` a un GameObject:
   - Asignar referencias a los elementos UI

## Controles

- **Jugador 1**: W o Espacio para saltar
- **Jugador 2**: Flecha Arriba (↑) para saltar

## Modos de Juego

- **Clásico**: Partida normal hasta X puntos
- **Pelota Rápida**: La pelota se mueve más rápido
- **Gravedad Baja**: Los saltos son más flotantes
- **Aleatorio**: Cambia de modo aleatoriamente cada ronda

## Notas

- Los jugadores solo pueden saltar, no moverse lateralmente
- La pelota siempre rebota en el suelo
- Los puntos se otorgan cuando la pelota sale por los lados del oponente
- El "sweet spot" en la raqueta genera golpes más potentes

