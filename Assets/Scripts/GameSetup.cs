using UnityEngine;

/// <summary>
/// Script de utilidad para ayudar a configurar la escena del juego.
/// Este script puede ser usado para configurar automáticamente los objetos necesarios.
/// </summary>
public class GameSetup : MonoBehaviour
{
    [ContextMenu("Configurar Escena")]
    public void SetupScene()
    {
        Debug.Log("Configurando escena del juego...");
        
        // Configura las etiquetas necesarias
        SetupTags();
        
        // Configura las capas necesarias
        SetupLayers();
        
        Debug.Log("Configuración completada. Por favor, asigna los componentes manualmente a los objetos.");
    }
    
    private void SetupTags()
    {
        // Las etiquetas deben configurarse manualmente en Edit > Project Settings > Tags and Layers
        // Etiquetas necesarias:
        // - "Player"
        // - "Ball"
        // - "Ground"
        // - "Wall"
        // - "LeftGoal"
        // - "RightGoal"
    }
    
    private void SetupLayers()
    {
        // Las capas deben configurarse manualmente en Edit > Project Settings > Tags and Layers
        // Capas necesarias:
        // - "Ground" (para el suelo)
    }
}

