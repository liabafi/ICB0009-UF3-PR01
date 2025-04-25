# **Ejercicio 1: Conexión de Clientes**

## **Descripción**
El objetivo de este ejercicio es establecer una conexión entre un servidor y múltiples clientes (vehículos) en un entorno concurrente. Este ejercicio se divide en varias etapas para facilitar su implementación y comprensión.

---

## **Objetivos**
1. Crear un servidor capaz de aceptar múltiples clientes.
2. Asignar un identificador único (`ID`) y una dirección (`Norte` o `Sur`) a cada cliente conectado.
3. Implementar la comunicación entre cliente y servidor mediante **NetworkStream**.
4. Almacenar información de los clientes conectados para gestionarlos eficientemente.

---

### **Etapa 1: Conexión Servidor-Cliente**
- Se estableció la conexión inicial entre el servidor y el cliente.
- Mensajes en consola para verificar el estado de la conexión.

---

### **Etapa 2: Aceptación de Clientes**
- Implementación de programación concurrente en el servidor mediante hilos.
- Cada cliente conectado es gestionado por un hilo independiente.

---

### **Etapa 3: Asignación de ID y Dirección**
- El servidor asigna un ID único y una dirección (`Norte` o `Sur`) a cada cliente.
- Uso de `lock` para proteger el acceso concurrente a las variables compartidas.

---

### **Etapa 4: Obtener el NetworkStream**
- El servidor y el cliente obtienen sus respectivos `NetworkStream` para la comunicación.
- Este `NetworkStream` será usado en las siguientes etapas para leer y escribir datos.

---

### **Etapa 5: Métodos para Leer y Escribir en NetworkStream**
- Implementación de los métodos:
  - `EscribirMensajeNetworkStream`: Envía datos a través del stream.
  - `LeerMensajeNetworkStream`: Recibe datos del stream.
- Centralización en la clase `NetworkStreamClass` para facilitar la reutilización.

---

### **Etapa 6: Handshake**
- El cliente inicia un handshake con el servidor enviando el mensaje `"INICIO"`.
- El servidor responde con el ID único asignado.
- El cliente confirma la recepción enviando el mismo ID de vuelta.

---

### **Etapa 7: Almacenar Información de Clientes**
- Implementación de la clase `Cliente`, que almacena el ID y el `NetworkStream` de cada cliente.
- Uso de una lista compartida para gestionar los clientes conectados.
