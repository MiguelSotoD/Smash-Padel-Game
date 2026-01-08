# 🎯 GUÍA COMPLETA: Implementar Contador de Puntos

## 📋 PASO 1: Verificar los Textos en el Canvas

1. **Abre tu escena** (SampleScene.unity)
2. **En la jerarquía**, busca tu Canvas
3. **Verifica que existan estos objetos:**
   - `Paddel1Score` (o `Paddle1Score`)
   - `Paddel2Score` (o `Paddle2Score`)

4. **IMPORTANTE - Verifica el tipo de texto:**
   - Selecciona `Paddel1Score` en la jerarquía
   - En el Inspector, verifica que tenga el componente **"TextMeshPro - Text (UI)"**
   - Si tiene "Text (Legacy)" en su lugar, necesitas cambiarlo:
     - Click derecho en el objeto → **UI → TextMeshPro - Text**
     - O elimina el componente Text y agrega TextMeshPro

## 📋 PASO 2: Configurar el GameManager

1. **Busca el objeto GameManager** en tu escena
   - Si no existe, créalo: Click derecho en jerarquía → Create Empty → Nómbralo "GameManager"
   - Agrega el componente `GameManager` script

2. **En el Inspector del GameManager**, verás estos campos:
   - `Player 1 Score Text` (TextMeshProUGUI)
   - `Player 2 Score Text` (TextMeshProUGUI)

3. **OPCIÓN A - Asignación Manual (RECOMENDADA):**
   - Arrastra `Paddel1Score` desde la jerarquía al campo `Player 1 Score Text`
   - Arrastra `Paddel2Score` desde la jerarquía al campo `Player 2 Score Text`

4. **OPCIÓN B - Automática:**
   - Deja los campos vacíos
   - El script los buscará automáticamente por nombre
   - **IMPORTANTE:** Los nombres deben ser exactamente `Paddel1Score` y `Paddel2Score`

## 📋 PASO 3: Verificar que el Sistema de Puntos Funcione

El sistema ya está configurado para funcionar automáticamente:

- **GoalZone** detecta cuando la pelota entra en una zona de gol
- Llama a `GameManager.PlayerScores(playerNumber)`
- Esto actualiza automáticamente los textos

## 📋 PASO 4: Probar el Sistema

1. **Ejecuta el juego** (Play)
2. **Observa la consola** de Unity:
   - Si ves advertencias sobre textos no encontrados, revisa los nombres
   - Si no hay errores, debería funcionar

3. **Haz que un jugador anote:**
   - Los textos deberían actualizarse automáticamente

## 🔧 SOLUCIÓN DE PROBLEMAS

### ❌ Los textos no se actualizan

**Causa 1:** Los textos no son TextMeshProUGUI
- **Solución:** Cambia a TextMeshPro - Text (UI)

**Causa 2:** Los nombres no coinciden
- **Solución:** Verifica que los nombres sean exactamente `Paddel1Score` y `Paddel2Score`
- O asigna manualmente en el Inspector

**Causa 3:** Los textos están desactivados
- **Solución:** Verifica que los objetos estén activos en la jerarquía

### ❌ Error de compilación

- Verifica que tengas el paquete **TextMeshPro** instalado
- Window → TextMeshPro → Import TMP Essential Resources

### ❌ Los textos muestran "0" pero no cambian

- Verifica que las GoalZones estén configuradas correctamente
- Verifica que la pelota tenga el tag "Ball"
- Revisa la consola para ver si hay errores

## 📝 NOTAS IMPORTANTES

1. **El script busca automáticamente** los textos si no están asignados
2. **Es mejor asignarlos manualmente** para mejor rendimiento
3. **Los textos deben ser TextMeshProUGUI**, no Text (Legacy)
4. **El sistema funciona con ambos métodos:**
   - `paddle1Scored()` / `paddle2Scored()` (métodos directos)
   - `PlayerScores(playerNumber)` (método usado por GoalZone)

## ✅ CHECKLIST FINAL

- [ ] Los textos `Paddel1Score` y `Paddel2Score` existen en el Canvas
- [ ] Los textos son de tipo TextMeshPro - Text (UI)
- [ ] Los textos están activos en la jerarquía
- [ ] El GameManager existe en la escena
- [ ] (Opcional) Los textos están asignados en el Inspector del GameManager
- [ ] Las GoalZones están configuradas correctamente
- [ ] La pelota tiene el tag "Ball"
- [ ] El juego compila sin errores

