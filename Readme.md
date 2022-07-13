# N-OMINO

![](n-ominoServer/wwwroot/Domino.jpg)

> Proyecto de Programación II.
> Facultad de Matemática y Computación - Universidad de La Habana.
> Curso 2022.

## Descripción del Proyecto

Este proyecto se basa en simular variantes del popular juego _Dominó_ que en esta variante desarrollada en _MATCOM_ denominaremos _N-OMINO_ y su desarrollo, mediante la interacción de jugadores virtuales. Las variaciones más notables que se aprecian en el proyecto son la forma de las fichas del juego y la forma en la que estas se ubican en el tablero, además de las estrategias de los jugadores virtuales y las reglas que estos deben seguir.

Al usuario se le brinda la posibilidad de configurar un juego juego totalmente nuevo, pero siempre manteniendo algunas ideas básicas del juego original, además de ofrecer juegos precreados con algunas variantes clásicas de _Dominó_ y otras que suponen su invención en este proyecto. También se le permite al usuario configurar las estategias de los distintos jugadores que participan durante la ejecución del juego, las cuales se rigen por las pautas del juego tradicional.

Dada la variedad de opciones con que cuenta el usuario para configurar y simular el juego, lo invitamos a probar las diferentes implementaciones de este y a descubrir las diversas maneras de jugar al _N-OMINO_, unas pueden tener sentido otras no, pero todas cuentan con la imaginación y creatividad que surgen de extender el popular juego del _Dominó_.

## Detalles del Proyecto

Este proyecto está desarrollado para la versión objetivo de C# 10, .NET Core 6, en la implementación de la
interfaz gráfica se utilizó Blazor, un framework de C# orientado al desarrollo web.

El proyecto está estructurado por una única solución que contiene 6 bibliotecas de clases que se encuentran en la carpeta `n-ominoEngine` donde se aloja la parte lógica y un server de blazor situado en la carpeta `n-ominoServer` que se encarga de representar la interfaz gráfica.

### Dependencias del Proyecto

Para ejecutar el proyecto debe contar en su sistema operativo con un compilador que soporte la versión de C# hacia la cual está orientada este proyecto y además disponga de blazor en sus paquetes instalados. Una vez hecho esto solo debe situarse en la raíz del proyecto y ejecutar en consola:

```bash
make dev #Si su sistema operativo es linux
dotnet watch run --project n-ominoServer #Si su sistema operativo es windows
```

Automáticamente se montara un servidor en su computadora, al cual se puede acceder desde su navegador web mediante la url que se especifique en la consola.
