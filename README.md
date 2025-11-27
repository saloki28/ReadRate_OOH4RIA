# 📚 OOH4RIA – Read&Rate (Equipo 4)

Este proyecto contiene la definición del modelo, las funcionalidades transaccionales y no transaccionales, así como el scaffolding de la aplicación **Read&Rate**.

> ⚠️ **IMPORTANTE:** En el apartado de Login de momento tenemos una solución mejorable en que llama a la columna de número de modificaciones para poder hashear la contraseña el número de veces necesarios para que se iguale a la que está en la base de datos, esto ocurre porque el metódo modificar de usuario se ha autogenerado mal y hashea la contraseña toda vez que se modifica un usuario. Dado que la solución proporcionada por el profesor ha sido muy cerca de la entrega no hemos podido arreglarlo, sin embargo, para la siguiente entrega se hará el cambio: transformar el método modificar en un customizable y quitar el hasheo que hace en la contraseña.

> ⚠️ **IMPORTANTE:** En la entrega anterior habían errores en la herencia de usuario: OOH4RIA autogenera mal las herencias. Hemos podido arreglarlo pasando todas las relaciones que antes estaban en usuario a autor y lector.
---

## 🔧 Posible login para poder entrar en la página (se pueden utilizar otros)
Para poder hacer login con otro usuario habría que buscar su contraseña en createDB.

### ✅ `LOGIN`

- **Email:** `marina.lectora@email.com`  
- **Contraseña:** `passMarina`  
