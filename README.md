# 📚 OOH4RIA – Read&Rate (Equipo 4)

Este proyecto contiene la definición del modelo, así como las funcionalidades transaccionales y no transaccionales de la aplicación **Read&Rate**.

> ⚠️ **IMPORTANTE:** Si descargas el proyecto y vuelves a generarlo con OOH4RIA, debes aplicar los siguientes ajustes manuales en el código generado para evitar errores de ejecución.

> ⚠️ Se han intentado diversas alternativas para solucionar estes problemas pero no ha sido posible arreglarlos, dado que cuando se genera el proyecto otra vez se borran todas las soluciones propuestas
---

## 🔧 Ajustes necesarios tras regenerar con OOH4RIA

### ✅ 1. `UsuarioRepository.cs`

- **Ubicación:** `ReadRate_e4Gen.Infraestructure/Repository/ReadRate_E4/UsuarioRepository.cs`  
- **Método:** `DesuscribirDeClub`  
- **Acción:** Comentar la siguiente línea (línea 355):

```csharp
if (usuarioEN.ClubSuscrito.Contains (clubSuscritoENAux) == true) {
        usuarioEN.ClubSuscrito.Remove (clubSuscritoENAux);
        //clubSuscritoENAux.UsuarioMiembro.Remove (usuarioEN);
}
else
```

### ✅ 2. `AdministradorCEN.cs`

- **Ubicación:** `ReadRate_e4Gen.ApplicationCore/CEN/ReadRate_E4/AdministradorCEN.cs`  
- **Método:** `Encode`  
- **Acción:** Eliminar los { } que se generan dentro del payload, ya que producen un formato inválido (línea 87):

```csharp
var payload = new Dictionary<string, object>(){
        {} //eliminar esta linea
};
```

## 🔧 Comentarios
En varias partes del código se han dejado bloques comentados porque, aunque la lógica que queríamos implementar está hecha, al ejecutar el proyecto aparecen errores que creemos que provienen del código autogenerado por OOH4RIA. Nuestra hipótesis es que la generación no está gestionando correctamente la herencia entre `Usuario` y sus subtipos `Autor` / `Lector`, lo que provoca fallos en operaciones que relacionan `Usuario` y `Club`. Pero no podemos / sabemos cómo arreglar este problema porque proviene internamente del código que se autogenera por el programa.

En concreto:
- Hemos comentado una línea dentro de `ReadRate_E4.UsuarioRepository.DesuscribirDeClub` (ReadRate_e4Gen.Infraestructure/Repository/ReadRate_E4/UsuarioRepository.cs) para evitar errores en la creación de la base de datos.
- Otras operaciones que usan esa misma relación Usuario-Club también han requerido comentarios parciales para que el proceso de createDB avance y podamos ejecutar el resto de pruebas: `expulsarUsuario`, `obtenerParticipantes` y `obtenerMiembros`. Todas ellas están relacionadas con `Usuario` y `Club` y creemos que tienen el mismo tipo de fallo.
- En el caso de `cambiarPassword` hemos aplicado una solución alternativa que fuerza la ejecución del método llamando por separado a los manejadores de `Autor` y `Lector`, «ignorando» la herencia. Esto permite que el método se ejecute, pero no estamos seguros de que sea una implementación correcta desde el punto de vista del modelo de dominio, ya que rompe la abstracción que debería ofrecer la jerarquía de tipos.

Se han dejado comentados estas partes para poder ejecutar las pruebas que sí están correctamente implementadas desde el createDB.
