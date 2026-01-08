# 🎮 Guía Completa de Implementación - Juego Padel (Estilo Otto's Tennis Game)

## 📋 Tabla de Contenidos

1. [Configuración de Tags y Layers](#1-configuración-de-tags-y-layers)
2. [Configuración de los Jugadores](#2-configuración-de-los-jugadores)
3. [Configuración de la Pelota](#3-configuración-de-la-pelota)
4. [Configuración del Suelo](#4-configuración-del-suelo)
5. [Configuración de Zonas de Gol](#5-configuración-de-zonas-de-gol)
6. [Configuración del GameManager](#6-configuración-del-gamemanager)
7. [Configuración del UIManager](#7-configuración-del-uimanager)
8. [Configuración de Animaciones (Opcional)](#8-configuración-de-animaciones-opcional)
9. [Verificación Final](#9-verificación-final)

---

## 1. Configuración de Tags y Layers

### Paso 1.1: Crear Tags

1. Ve a **Edit > Project Settings > Tags and Layers**
2. En la sección **Tags**, expande el campo si es necesario
3. Crea los siguientes tags (en este orden si es posible):
   - `Player`
   - `Ball`
   - `Ground`
   - `Wall`
   - `LeftGoal`
   - `RightGoal`

**Nota:** Si no hay suficientes campos, Unity usa las primeras 32 tags. Asegúrate de usar estos nombres exactos.

### Paso 1.2: Crear Layers (Opcional pero Recomendado)

1. En la misma ventana, sección **Layers**
2. Asigna un Layer personalizado para el suelo (por ejemplo, Layer 6):
   - Layer 6: `Ground`

---

## 2. Configuración de los Jugadores

### Paso 2.1: Seleccionar el Objeto del Jugador 1

1. En la **Hierarchy**, selecciona el objeto del jugador 1 (ej: `Character1_0` o `personaje_0`)
2. Verifica que tenga un **SpriteRenderer** con el sprite asignado

### Paso 2.2: Agregar Rigidbody2D al Jugador 1

1. Con el jugador seleccionado, en el **Inspector**:
   - Clic en **Add Component**
   - Busca y agrega **Rigidbody 2D**
2. Configura el Rigidbody2D:
   - **Body Type:** Dynamic
   - **Gravity Scale:** 2
   - **Linear Drag:** 0
   - **Angular Drag:** 0
   - **Collision Detection:** Continuous
   - **Sleeping Mode:** Never Sleep
   - **Constraints:** 
     - ✅ Freeze Position X
     - ✅ Freeze Rotation Z

### Paso 2.3: Agregar Collider2D al Jugador 1

1. En el mismo objeto:
   - Clic en **Add Component**
   - Agrega **Box Collider 2D** o **Circle Collider 2D**
2. Configura el Collider:
   - Ajusta el **Size** para que coincida con el sprite
   - **Is Trigger:** ❌ Desmarcado
   - **Material:** (ninguno, o un Physics Material 2D si quieres rebotes especiales)

### Paso 2.4: Agregar PlayerController al Jugador 1

1. Clic en **Add Component**
2. Busca y agrega **Player Controller (Script)**
3. Configura el PlayerController:
   - **Jump Force:** 10
   - **Max Height:** 4
   - **Gravity Scale:** 2
   - **Jump Cooldown:** 0.1
   - **Player Number:** 1
   - **Rb:** Arrastra el componente Rigidbody2D aquí (o déjalo vacío para auto-buscar)
   - **Animator:** (opcional, si tienes animaciones configuradas)
   - **Ground Check:** (opcional, ver Paso 2.6)

### Paso 2.5: Crear GroundCheck para Jugador 1 (Opcional pero Recomendado)

1. Con el jugador seleccionado:
   - Clic derecho en la **Hierarchy** > **Create Empty**
   - Nómbralo `GroundCheck`
   - Arrástralo para que sea hijo del jugador
2. Posiciona el GroundCheck:
   - **Position Y:** -1 (ajusta según el tamaño del personaje)
   - Debe estar en la parte inferior (pies) del personaje
3. En el **PlayerController** del jugador:
   - Arrastra `GroundCheck` al campo **Ground Check**
   - **Ground Check Radius:** 0.2
   - **Ground Check Distance:** 0.15

### Paso 2.6: Crear Raqueta para Jugador 1

1. Con el jugador seleccionado:
   - Clic derecho > **Create Empty**
   - Nómbralo `Racket`
   - Arrástralo para que sea hijo del jugador
2. Posiciona la Raqueta:
   - **Position:** Ajusta según dónde quieras la raqueta (ej: X: 0.5, Y: 0.3)
3. Agregar componentes a la Raqueta:
   - **Add Component > Racket Controller (Script)**
   - **Add Component > Box Collider 2D** o **Circle Collider 2D**
4. Configurar el Collider de la Raqueta:
   - **Is Trigger:** ✅ Marcado (MUY IMPORTANTE)
   - **Size:** Ajusta al tamaño de la raqueta
5. Configurar RacketController:
   - **Hit Force:** 12
   - **Sweet Spot Force:** 18
   - **Sweet Spot Radius:** 0.3
   - **Player Controller:** Arrastra el componente PlayerController del jugador padre
   - **Sweet Spot Center:** (opcional, deja vacío para usar el centro)

### Paso 2.7: Repetir para Jugador 2

Repite todos los pasos anteriores (2.1 a 2.6) para el Jugador 2, pero:
- **Player Number:** 2
- Posición en el lado derecho de la escena
- **Ground Check** también en los pies
- **Racket** posicionada según corresponda

---

## 3. Configuración de la Pelota

### Paso 3.1: Seleccionar el Objeto de la Pelota

1. En la **Hierarchy**, selecciona el objeto de la pelota (ej: `tennis_0`)
2. Verifica que tenga un **SpriteRenderer** con el sprite de la pelota

### Paso 3.2: Agregar Rigidbody2D a la Pelota

1. **Add Component > Rigidbody 2D**
2. Configura el Rigidbody2D:
   - **Body Type:** Dynamic
   - **Gravity Scale:** 0.5 (gravedad leve)
   - **Linear Drag:** 0
   - **Angular Drag:** 0
   - **Collision Detection:** Continuous
   - **Sleeping Mode:** Never Sleep
   - **Constraints:** Ninguno (puede moverse libremente)

### Paso 3.3: Agregar Collider2D a la Pelota

1. **Add Component > Circle Collider 2D** (recomendado para pelotas)
2. Configura el Collider:
   - **Radius:** Ajusta al tamaño del sprite
   - **Is Trigger:** ❌ Desmarcado (para colisiones con el suelo)
   - **Offset:** (0, 0) normalmente

### Paso 3.4: Agregar BallController a la Pelota

1. **Add Component > Ball Controller (Script)**
2. Configura el BallController:
   - **Initial Speed:** 5
   - **Max Speed:** 15
   - **Bounce Force:** 1
   - **Min Bounce Velocity:** 3
   - **Gravity Scale:** 0.5
   - **Ground Bounce Force:** 8
   - **Ceiling Bounce Force:** 8
   - **Slow Motion Threshold:** 1
   - **Slow Motion Scale:** 0.3
   - **Rb:** (auto, o arrastra el Rigidbody2D)
   - **Player 1 Pos:** (opcional, se encuentra automáticamente)
   - **Player 2 Pos:** (opcional, se encuentra automáticamente)

### Paso 3.5: Asignar Tag a la Pelota

1. En el **Inspector**, en la parte superior:
   - **Tag:** Selecciona `Ball`

---

## 4. Configuración del Suelo

### Paso 4.1: Seleccionar el Objeto del Suelo

1. En la **Hierarchy**, selecciona el objeto del suelo (ej: `Square`)

### Paso 4.2: Verificar o Agregar BoxCollider2D

1. Si no tiene **Box Collider 2D**, agrégalo:
   - **Add Component > Box Collider 2D**
2. Configura el Collider:
   - **Size:** Ajusta para cubrir toda la parte inferior de la pantalla
   - **Is Trigger:** ❌ Desmarcado
   - **Offset:** (0, 0)

### Paso 4.3: Agregar Ground Script

1. **Add Component > Ground (Script)**
2. Este script solo marca el objeto como suelo

### Paso 4.4: Asignar Tag y Layer

1. **Tag:** `Ground`
2. **Layer:** `Ground` (si lo creaste en el Paso 1.2)

---

## 5. Configuración de Zonas de Gol

### Paso 5.1: Crear LeftGoal (Zona de Gol Izquierda)

1. **Hierarchy:** Clic derecho > **Create Empty**
2. Nómbralo `LeftGoal`
3. Configura el Transform:
   - **Position X:** -8 (ajusta según el tamaño de tu pantalla, debe estar fuera de la vista izquierda)
   - **Position Y:** 0
   - **Position Z:** 0
   - **Scale X:** 1
   - **Scale Y:** 20 (lo suficientemente alto para cubrir toda la altura)
   - **Scale Z:** 1

### Paso 5.2: Agregar Collider a LeftGoal

1. **Add Component > Box Collider 2D**
2. Configura el Collider:
   - **Size X:** 1
   - **Size Y:** 20 (ajusta según la altura de la pantalla)
   - **Is Trigger:** ✅ Marcado (MUY IMPORTANTE)
   - **Offset:** (0, 0)

### Paso 5.3: Agregar GoalZone Script a LeftGoal

1. **Add Component > Goal Zone (Script)**
2. Configura:
   - **Player Number:** 1

### Paso 5.4: Asignar Tag a LeftGoal

1. **Tag:** `LeftGoal`

### Paso 5.5: Crear RightGoal (Zona de Gol Derecha)

Repite los pasos 5.1 a 5.4 para **RightGoal**, pero:
- **Position X:** 8 (lado derecho, fuera de la vista)
- **Player Number:** 2
- **Tag:** `RightGoal`

---

## 6. Configuración del GameManager

### Paso 6.1: Crear GameObject GameManager

1. **Hierarchy:** Clic derecho > **Create Empty**
2. Nómbralo `GameManager`

### Paso 6.2: Agregar GameManager Script

1. **Add Component > Game Manager (Script)**

### Paso 6.3: Configurar GameManager

1. En el **Inspector**, configura:
   - **Points To Win:** 60
   - **Points Per Score:** 10
   - **Current Game Mode:** Classic
   - **Fast Ball Speed:** 8
   - **Low Gravity Scale:** 1
   - **Ball:** Arrastra el objeto de la pelota desde la Hierarchy
   - **Player1:** Arrastra el objeto del Jugador 1
   - **Player2:** Arrastra el objeto del Jugador 2
   - **UI Manager:** (se asignará después, Paso 7.4)

---

## 7. Configuración del UIManager

### Paso 7.1: Crear Canvas

1. **Hierarchy:** Clic derecho > **UI > Canvas**
2. Si Unity pregunta sobre crear EventSystem, selecciona **Yes**
3. El Canvas se crea automáticamente

### Paso 7.2: Crear Texto para Marcador del Jugador 1

1. Con el **Canvas** seleccionado:
   - Clic derecho > **UI > Text - TextMeshPro**
2. Si Unity pregunta sobre importar TMP Essentials, selecciona **Import TMP Essentials**
3. Nombra el texto: `Player1ScoreText`
4. Configura el texto:
   - **Text:** "0 - 0"
   - **Font Size:** 48 (o el tamaño que prefieras)
   - **Alignment:** Centrado
   - **Color:** Blanco (o el color que prefieras)
5. Posiciona el texto:
   - **Anchor:** Top Left
   - **Position:** (50, -50, 0) (ajusta según prefieras)

### Paso 7.3: Crear Texto para Marcador del Jugador 2

1. Repite el Paso 7.2 para **Player2ScoreText**
2. Posición:
   - **Anchor:** Top Right
   - **Position:** (-50, -50, 0)

### Paso 7.4: Crear Panel de Game Over

1. Con el **Canvas** seleccionado:
   - Clic derecho > **UI > Panel**
2. Nómbralo: `GameOverPanel`
3. Configura el Panel:
   - **Color:** Negro con transparencia (A: 200)
   - **Anchors:** Stretch/Stretch (cubre toda la pantalla)
4. Crea un texto hijo:
   - Clic derecho en GameOverPanel > **UI > Text - TextMeshPro**
   - Nómbralo: `GameOverText`
   - **Text:** "¡Jugador X Gana!"
   - **Font Size:** 72
   - **Alignment:** Centrado
5. Inicialmente desactiva el panel:
   - En el **Inspector**, desmarca el checkbox junto al nombre del GameObject

### Paso 7.5: Crear GameObject UIManager

1. **Hierarchy:** Clic derecho > **Create Empty**
2. Nómbralo: `UIManager`
3. **Add Component > UI Manager (Script)**
4. Configura el UIManager:
   - **Player1 Score Text:** Arrastra `Player1ScoreText` desde el Canvas
   - **Player2 Score Text:** Arrastra `Player2ScoreText` desde el Canvas
   - **Game Over Text:** Arrastra `GameOverText` desde el Canvas
   - **Game Over Panel:** Arrastra `GameOverPanel` desde el Canvas

### Paso 7.6: Conectar UIManager con GameManager

1. Selecciona el **GameManager**
2. En el **Inspector**, en GameManager:
   - **UI Manager:** Arrastra el objeto `UIManager`

---

## 8. Configuración de Animaciones (Opcional)

### Paso 8.1: Verificar Animator Controller

1. Ve a `Assets/Animation/Character2_0.controller`
2. Verifica que existan los estados:
   - `iddle`
   - `jump`
   - `Fall`

### Paso 8.2: Agregar Animator al Jugador

1. Selecciona el jugador en la Hierarchy
2. **Add Component > Animator**
3. Configura:
   - **Controller:** Arrastra `Character2_0.controller`
   - **Avatar:** None
   - **Apply Root Motion:** ❌ Desmarcado

### Paso 8.3: Conectar Animator con PlayerController

1. En el **PlayerController** del jugador:
   - **Animator:** Arrastra el componente Animator

**Nota:** Si las animaciones no funcionan correctamente, verifica la guía de animaciones en el código.

---

## 9. Verificación Final

### Checklist de Verificación

#### Jugadores
- [ ] Jugador 1 tiene Rigidbody2D configurado correctamente
- [ ] Jugador 1 tiene Collider2D
- [ ] Jugador 1 tiene PlayerController con Player Number = 1
- [ ] Jugador 2 tiene Rigidbody2D configurado correctamente
- [ ] Jugador 2 tiene Collider2D
- [ ] Jugador 2 tiene PlayerController con Player Number = 2
- [ ] Cada jugador tiene una Raqueta con RacketController
- [ ] Las raquetas tienen Collider2D con Is Trigger = true

#### Pelota
- [ ] Pelota tiene Rigidbody2D con Gravity Scale = 0.5
- [ ] Pelota tiene Collider2D
- [ ] Pelota tiene BallController configurado
- [ ] Pelota tiene el tag `Ball`

#### Suelo
- [ ] Suelo tiene BoxCollider2D
- [ ] Suelo tiene el componente Ground
- [ ] Suelo tiene el tag `Ground`

#### Zonas de Gol
- [ ] LeftGoal tiene BoxCollider2D con Is Trigger = true
- [ ] LeftGoal tiene GoalZone con Player Number = 1
- [ ] LeftGoal tiene el tag `LeftGoal`
- [ ] RightGoal tiene BoxCollider2D con Is Trigger = true
- [ ] RightGoal tiene GoalZone con Player Number = 2
- [ ] RightGoal tiene el tag `RightGoal`

#### GameManager
- [ ] GameManager existe en la escena
- [ ] GameManager tiene referencias a Ball, Player1, Player2 y UIManager
- [ ] Points To Win = 60
- [ ] Points Per Score = 10

#### UI
- [ ] Canvas existe en la escena
- [ ] Existen Player1ScoreText y Player2ScoreText
- [ ] Existe GameOverPanel con GameOverText
- [ ] UIManager tiene todas las referencias asignadas
- [ ] UIManager está conectado al GameManager

---

## 🎮 Cómo Jugar

### Controles
- **Jugador 1:** W o Espacio para saltar
- **Jugador 2:** Flecha Arriba (↑) para saltar

### Reglas
- Los jugadores solo pueden saltar (no moverse lateralmente)
- Mantén presionado para "volar" (saltos consecutivos)
- La pelota siempre rebota en el suelo
- Los puntos se otorgan cuando la pelota sale por los lados del oponente
- Cada punto visual (0-10) equivale a 10 puntos reales
- Gana el primero en llegar a 60 puntos reales (6 puntos visuales)
- El jugador 1 siempre saca

### Características Especiales
- **Gravedad en la pelota:** La pelota tiene gravedad leve para trayectorias más realistas
- **Cámara lenta:** Se activa cuando la pelota pasa a un jugador y va hacia la zona de gol
- **Sweet Spot:** Golpear la pelota en el centro de la raqueta genera golpes más potentes

---

## 🔧 Ajustes Recomendados

### Valores del PlayerController
- **Jump Force:** 10-15 (fuerza de salto)
- **Max Height:** 3-5 (altura máxima)
- **Gravity Scale:** 2-3 (gravedad del jugador)

### Valores del BallController
- **Gravity Scale:** 0.3-0.7 (gravedad de la pelota)
- **Initial Speed:** 5-7 (velocidad inicial)
- **Max Speed:** 12-18 (velocidad máxima)
- **Slow Motion Scale:** 0.2-0.5 (intensidad de cámara lenta)
- **Slow Motion Threshold:** 0.5-2.0 (distancia para activar cámara lenta)

### Valores del GameManager
- **Points To Win:** 60 (puntos para ganar)
- **Points Per Score:** 10 (puntos por punto)

---

## 🐛 Solución de Problemas

### La pelota no rebota en el suelo
- ✅ Verifica que el suelo tenga el tag `Ground`
- ✅ Verifica que el suelo tenga un Collider2D (no trigger)
- ✅ Verifica que la pelota tenga un Collider2D

### Los puntos no se otorgan
- ✅ Verifica que las zonas de gol tengan los tags `LeftGoal` y `RightGoal`
- ✅ Verifica que las zonas de gol tengan Collider2D con Is Trigger = true
- ✅ Verifica que las zonas de gol tengan el componente GoalZone
- ✅ Verifica que el GameManager esté en la escena

### La cámara lenta no funciona
- ✅ Verifica que el BallController tenga referencias a Player1Pos y Player2Pos (o las encuentra automáticamente)
- ✅ Ajusta el `Slow Motion Threshold` si es necesario
- ✅ Verifica que `Slow Motion Scale` esté entre 0 y 1

### Los jugadores no saltan
- ✅ Verifica que tengan Rigidbody2D
- ✅ Verifica que el PlayerController esté configurado correctamente
- ✅ Verifica que el Player Number sea 1 o 2
- ✅ Prueba los controles: W/Espacio para jugador 1, ↑ para jugador 2

### El jugador 1 no saca
- ✅ Verifica que el BallController tenga referencia a Player1Pos
- ✅ Verifica que el método StartBall() esté configurado correctamente

---

## 📝 Notas Finales

- Todos los scripts están en `Assets/Scripts/`
- Las animaciones están en `Assets/Animation/`
- Los sprites están en `Assets/Sprites/`
- Asegúrate de guardar la escena después de todos los cambios
- Prueba el juego frecuentemente para verificar que todo funciona

---

¡Listo! Con estos pasos deberías tener el juego completamente funcional. 🎉

