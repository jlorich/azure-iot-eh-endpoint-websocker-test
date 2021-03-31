# Azure IoT Hub Event Hub Compatible Endpoint WebSocket Test

This repo contains a program which will test connectivity to the Event Hub compaitble endpoint for an IoT Hub using the AmqpWebSockets Transport method.  The current documentation for Iot Hub states that this is not supported, however it works just fine.

## Classes

#### `Program`

The main program file.  This loads the config options from `config.json` and starts a `Producer` and `Consumer`

#### `IoTEventHubEndpointTestOptions`

Stongly typed options that the contenxt of `config.json` is loaded into

#### `Producer`

This connects to an IoT Hub as a device and emits simple messages every 1 second.

#### `Consumer`

This connects to the Event Hub compaitlbe endpoint of the relevant IoT Hub over AmqpWebSockets and prints out the messages it receives.
