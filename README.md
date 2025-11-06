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

### ✅ 2. `Administrador.cs`

- **Ubicación:** `ReadRate_e4Gen.ApplicationCore/CEN/ReadRate_E4/AdministradorCEN.cs`  
- **Método:** `Encode`  
- **Acción:** Eliminar los { } que se generan dentro del payload, ya que producen un formato inválido (línea 87):

```csharp
var payload = new Dictionary<string, object>(){
        {} //eliminar esta linea
};
```
