# Ejercicio 2: Intercambio de Información entre Vehículos

## Descripción

Este ejercicio implementa la simulación de una carretera de 100 km donde múltiples vehículos (clientes) se mueven en direcciones norte o sur. Cada vehículo es un proceso independiente que envía su posición al servidor, y el servidor actualiza y redistribuye la información de la carretera (estado de todos los vehículos) a todos los clientes. Los clientes visualizan las posiciones de todos los vehículos en la consola.

El objetivo es lograr un intercambio eficiente de datos entre clientes y servidor utilizando sockets y la clase `NetworkStreamClass`, asegurando concurrencia y manejo de errores.

## Estructura del Proyecto

- **Código Base**: Incluye las clases `Vehiculo`, `Carretera`, y una implementación parcial de `NetworkStreamClass`.
- **Archivos Principales**:
  - `Client.cs`: Gestiona la lógica del cliente (crear vehículo, enviar posición, recibir datos de la carretera).
  - `Server.cs`: Gestiona conexiones de clientes, actualiza la carretera, y envía datos a todos los clientes.
  - `NetworkStreamClass.cs`: Contiene métodos para serializar/deserializar y enviar/recibir objetos `Vehiculo` y `Carretera`.
- **Dependencias**: .NET Core para sockets y concurrencia.

## Etapas Implementadas

### Etapa 1: Programación de Métodos en `NetworkStreamClass`

Se implementaron los métodos para enviar y recibir objetos `Vehiculo` y `Carretera` a través de `NetworkStream`:

- `EscribirDatosCarreteraNS`: Serializa un objeto `Carretera` y lo envía como bytes.
- `LeerDatosCarreteraNS`: Lee bytes y deserializa a un objeto `Carretera`.
- `EscribirDatosVehiculoNS`: Serializa un objeto `Vehiculo` y lo envía.
- `LeerDatosVehiculoNS`: Lee bytes y deserializa a un objeto `Vehiculo`.

Estos métodos reutilizan las rutinas de serialización/deserialización de las clases `Vehiculo` y `Carretera`, asegurando un intercambio robusto de datos.

### Etapa 2: Crear y Enviar Datos de un Vehículo

- **Cliente**: Crea un objeto `Vehiculo` con un ID asignado por el servidor (recibido en el handshake de Ejercicio 1) y lo envía al servidor usando `EscribirDatosVehiculoNS`.
- **Servidor**: Recibe el vehículo con `LeerDatosVehiculoNS`, lo añade a la carretera (`AñadirVehiculo`), y muestra la lista de vehículos en la consola.
- **Prueba**: Se conectaron múltiples clientes, verificando que los vehículos aparecen en la carretera (todos en posición 0 inicialmente).

### Etapa 3: Mover los Vehículos

- **Cliente**:
  - Ejecuta un bucle de 0 a 100 para simular el movimiento del vehículo, actualizando `Pos` en cada iteración.
  - Usa `Thread.Sleep(Velocidad)` para controlar la velocidad (valor aleatorio entre 100 y 500 ms).
  - Envía los datos actualizados del vehículo al servidor en cada iteración.
  - Marca `Acabado = true` al llegar a 100.
- **Servidor**:
  - Ejecuta un bucle que lee datos del vehículo y actualiza la carretera (`ActualizarVehiculo`).
  - Muestra los datos de la carretera para verificar el avance.
- **Prueba**: Los vehículos avanzan a diferentes velocidades, y el servidor refleja correctamente las posiciones.

### Etapa 4: Enviar Datos del Servidor a Todos los Clientes

- **Servidor**:
  - Implementa un método que itera sobre la lista de clientes conectados (almacenada en Ejercicio 1).
  - Envía el objeto `Carretera` a cada cliente usando `EscribirDatosCarreteraNS` cada vez que un vehículo actualiza su posición.

### Etapa 5: Recepción de Información en el Cliente

- **Cliente**:
  - Crea un hilo secundario para escuchar datos del servidor (`LeerDatosCarreteraNS`) de forma concurrente mientras el vehículo avanza.
  - Muestra los datos de la carretera (posiciones de todos los vehículos) en la consola.
  - Usa `try-catch` para manejar errores de red y evitar bloqueos.
