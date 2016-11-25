# code-challenge-6
Sogeti Code Challenge de Navidad 2016
=====================================
Desafío #6: Contar las cartas
-----------------------------------
Fecha límite: **9 de enero de 2017**

En este desafío tenemos que ayudar a nuestro amigo Rocky.

Rocky juega a las cartas con tres compañeros: Lil (su pareja de juego), Shady y Danny (que son la otra pareja del juego). Está muy perjudicado por las horas del juego y la cantidad de alcohol que han ido tomando todos ellos, y no se acuerda ni de que juego están jugando ni de que cartas tiene su compañera Lil. Está muy borracho para entender los gestos que ella hace. Shady y Danny tampoco son tontos y se fijan en esos gestos para confundir a pobre Rocky.

Por suerte para Rocky, otro amigo (Nell) ha ido tomando nota de las señales que se han ido haciendo, de las cartas descartadas y de las cartas que tenía Rocky en cada momento en su mano. 

Problema a resolver
-------------------
Rocky necesita saber que cartas tiene Lil en su mano después de cada turno.  

* Se juega con las [cartas de poker](https://es.wikipedia.org/wiki/Baraja_inglesa), una sola baraja de 52 naipes, sin comodines
* Se reparten 4 cartas al principio para cada jugador
* En cada momento se pueden coger cartas del mazo a repartir, pasar la carta a otro jugador o descartar cartas a la pila de descartes.
* Las cartas decartadas no vuelven a la mano de ningún jugador
* Cuando un jugador descarta una carta, esta carta es visible para todos los jugadores

Las señales que Lil hace a Rocky son: coger una carta, pasar la carta a otro jugador y recibir una carta de otro jugador (que podía haber ocurrido antes en el turno). 

Por ejemplo, su turno podría ser:
1. recibir J de rombos y 10 de tréboles de Rocky y otras 2 cartas de Shady en la ronda
2. coger 3 cartas
3. descartar 7 de corazones
4. pasar 2 cartas a Danny
5. pasar J de picas a Rocky

Para Rocky son visibles el 7 de corazones descartado y el J de picas que recibe de Lil, así como las dos cartas (J de rombos y 10 de tréboles) que Rocky pasa a Lil. 

**Las únicas señales que le hace Lil a Rocky son sobre las cartas no visibles para él**: las tres que coge del mazo, las 2 que pasa a Danny y las 2 que recibe de Shady. Ojo, que los otros dos jugadores pueden hacer señales falsas para confundir a pobre Rocky.

**El órden de las acciones de Lil importa**. Por ejemplo, como ella cogió 3 cartas del mazo y se descartó de 7 de corazones, una de las cartas cogidas pordía haber sido 7 de corazones. 

* Sólo se hace el gesto de Lil después de acabar su turno
* Los gestos de Lil siempre son ciertos para todos los movimientos que hace
* Los otros jugadores pueden señalar cualquier movimiento, sin tener que tener cartas o nada por el estilo

Ejemplo
=======
### Entrada

Cada línea representa las acciones de un/a jugador/a en su turno. Consiste en su nombre seguido por un conjunto de acciones que hace en su turno. Si la acción es algo que Rocky no puede ver, la carta se marca con un símbolo ?. Si la carta es visible, su descripción es <valor><palo>, donde los valores son de 2-10, J, Q, K, A y los palos C (tréboles), D (rombos), S (picas), H (corazones). 

Por ejemplo:
```
Danny  +?? +?? +?? -??:Shady -??:Shady -7H:discard 
```
significa que Danny cogió 3 cartas desconocidas del mazo, pasó dos cartas desconocidas a Shady y que se descartó del 7 de corazones.

Los turnos de Rocky evidentemente no tienen interrogantes, porque son visibles para Nell. 
```
Rocky  +3C +5C +JD -AS:Lil -KD:Lil -9H:discard
```

Los turnos de Lil son parecidos a los demás jugadores:
```
Lil +AS:Rocky +KD:Rocky +?? -??:Danny -??:Danny -5D:discard
```

Fijaos que Nell es tan bueno apuntando que las dos cartas que Rocky pasa a Lil salen como - para Rocky y + para Lil, es decir las dos partes en la "transacción" están en el fichero de entrada.

Después del turno de Lil, hay una o más líneas marcadas con asterisco (*), que representan los gestos que Lil (y otros jugadores) hacen a Rocky.
```
* +QS -JC:Danny -10D:Danny
* +2C -7S:Danny -JD:Danny
* +7H -JC:Danny -10D:Danny
```
Como sabemos que Rocky tiene J de rombos (JD), la segunda linea es seguramente falsa. La tercera tampoco puede ser cierta ya que el 7 de corazones ya ha sido descartado por Danny. La primera línea *podría* ser cierta, así que si no hay más alternativas, debe ser la de Lil.

En algunos casos habrá más de una línea de señal válida. En este caso debemos seguir todas las posibles señales hasta que lleguemos a un punto muerto en alguna combinación.

Más detalles:
* La entrada del programa será un fichero llamado input.txt
* Las primeras 4 líneas del fichero serán el reparto inicial de cartas: Las de Lil y Rocky serán visibles y las de los otros dos jugadores invisibles.
* Siempre habrá una línea de señales válida en cada turno, que será la de Lil. Tenemos que seguir esta línea para determinar que cartas tiene Lil en cada momento.

### Salida

La salida del programa será un fichero llamado output.txt con una linea para cada uno de los turnos de Lil, con sus cartas listadas en el órden en las que ella las ha recibido. Por ejemplo, para cuatro turnos la salida será:

```
8H 9H JS 6H
9H QS
9H AH 8C
9H 8C 4D
9H
```
La primera línea es la mano que se le repartió al principio, y las cuatro restantes son su mano en cada uno de los cuatro turnos.


¿Cómo subir mi código a GitHub?
===============================
En vez de enviar el código a mi correo, tenéis que hacer lo siguiente:
* Hacer un fork de este repositorio (el de SogetiSpain, no el mío personal)
* **Crear una carpeta con vuestro nombre dentro de la carpeta principal del repositorio**
* Crear vuestra solución en esa carpeta
* Hacer _commit_ en vuestro fork
* Hacer un _pull request_ para que lo incluyamos en el repositorio al final del tiempo del desafío

Tenéis una guía de como hacer un fork y pull request en GitHub [aquí](https://help.github.com/articles/fork-a-repo/)




