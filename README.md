# ENDLESS RUNNER

**[English](#english) · [Español](#español)**

**[Play it on itch.io](https://ignosuke.itch.io/run-capsule)** ·

<!-- Drop a gameplay gif or screenshot here -->

---

## English

A 2D side-scrolling endless runner built in Unity. The world comes at you, the ground is never the same twice, and the only thing you control is when to leave it.

### How to play

| | |
|---|---|
| **Jump** | `Left Mouse Button` |
| **Pause** | `Esc` |

Hold the button for a full jump, tap it for a short one. The difference is real height, not a cosmetic flourish, and clearing a bat without overshooting into the next gap depends on it.

### The world

The ground is assembled from chunks that spawn ahead of you and are discarded once they leave the screen behind. Which chunk comes next is a weighted draw, so flat stretches are common and the ones with water in the middle are rarer — but never impossible two in a row.

Obstacles, coins and power ups don't spawn on a timer. They live in slots placed inside each chunk, which is why nothing ever appears floating over a gap it has no business being in.

Behind all of it, three parallax layers scroll at their own speeds. The sprites for all three come from a single biome asset, so swapping the entire backdrop is one call.

### Scoring

Points accrue with distance, and faster scrolling means faster scoring. Coins are worth 5, diamonds 50.

### Power ups

**Power Donut** — five seconds of tinted invulnerability. Anything you run into gets launched off the screen, spinning. The HUD counts the seconds down. It won't save you from a gap, though: the floor doesn't care how many donuts you've had.

**Extra life** — dying spends it instead of ending the run. The world keeps scrolling, you fade back in above the ground, and you hang there until you decide to drop. The clock keeps running the whole time; the score doesn't.

### Built with

Unity · C# · 2D physics · TextMeshPro · Audio Mixer · Particle System · Trail Renderer

### How it's put together

**ScriptableObjects for design data.** Jump forces and gravity curves, scroll speed, chunk weights, pickup values, biome sprites — all in assets, none of it hardcoded in a MonoBehaviour.

**Custom gravity instead of a real parabola.** A jump that follows physics honestly feels floaty. This one rises light, falls heavier, and past a certain fall speed gets heavier still, which reads as weight rather than simulation.

**Layers for the question, components for the answer.** Whether something kills you or can be picked up is a physics layer — a yes-or-no filter the engine resolves for free. What it's worth or how long it lasts is a component, because that's data the layer can't carry.

**Events, not manager references.** The player broadcasts jumps, pickups, deaths and respawns. Audio, HUD and scoring subscribe to what they care about; nothing in the player knows they exist.

**A trail that fakes forward motion.** The player never moves on X, so the trail renderer would never emit. The script disables its emission entirely, adds a point per frame, and shifts the existing points at world speed — so the streak trails behind a character who is, technically, standing still.

### Running it locally

```
git clone <repo-url>
```

Open the project in Unity, load `MainMenuScene`, press Play.

---

## Español

Un endless runner 2D de scroll lateral hecho en Unity. El mundo viene hacia vos, el suelo nunca es igual dos veces, y lo único que controlás es cuándo dejarlo.

### Cómo se juega

| | |
|---|---|
| **Saltar** | `Click izquierdo` |
| **Pausa** | `Esc` |

Mantené el botón para un salto completo, tocalo para uno corto. La diferencia es altura real, no un detalle cosmético, y pasar un murciélago sin pasarte de largo hacia el próximo hueco depende de eso.

### El mundo

El suelo se arma con chunks que aparecen adelante y se descartan cuando salen de pantalla por atrás. Cuál viene después es un sorteo ponderado, así que los tramos llanos son comunes y los que tienen agua en el medio son más raros, aunque nunca imposibles dos veces seguidas.

Los obstáculos, monedas y power ups no aparecen por temporizador. Viven en slots colocados dentro de cada chunk, que es la razón por la que nada queda nunca flotando sobre un hueco donde no tendría por qué estar.

Detrás de todo, tres capas de parallax se desplazan a su propia velocidad. Los sprites de las tres salen de un único asset de bioma, así que cambiar el fondo entero es una sola llamada.

### Puntaje

Los puntos se acumulan con la distancia, y cuanto más rápido el scroll, más rápido suman. Las monedas valen 5, los diamantes 50.

### Power ups

**Power Donut** — cinco segundos de invulnerabilidad, con el personaje teñido. Todo lo que choques sale disparado y girando fuera de pantalla. El HUD cuenta los segundos. Eso sí, no te salva de un hueco: al piso no le importa cuántas donas te comiste.

**Vida extra** — morir la gasta en lugar de terminar la partida. El mundo sigue corriendo, reaparecés sobre el suelo, y te quedás ahí flotando hasta que decidas caer. El reloj corre todo ese rato; el puntaje no.

### Hecho con

Unity · C# · físicas 2D · TextMeshPro · Audio Mixer · Particle System · Trail Renderer

### Cómo está armado

**ScriptableObjects para los datos de diseño.** Fuerzas de salto y curvas de gravedad, velocidad de scroll, pesos de los chunks, valores de los pickups, sprites de bioma: todo en assets, nada hardcodeado en un MonoBehaviour.

**Gravedad custom en lugar de una parábola real.** Un salto que sigue la física con honestidad se siente flotante. Este sube liviano, cae más pesado, y pasada cierta velocidad de caída se pone más pesado todavía, lo que se lee como peso y no como simulación.

**Layers para la pregunta, componentes para la respuesta.** Si algo te mata o se puede agarrar es una physics layer, un filtro binario que el motor resuelve gratis. Cuánto vale o cuánto dura es un componente, porque eso es un dato que la layer no puede cargar.

**Eventos, no referencias al manager.** El jugador avisa cuándo salta, agarra algo, muere o reaparece. El audio, el HUD y el puntaje se suscriben a lo que les interesa; nada dentro del jugador sabe que existen.

**Una estela que finge movimiento.** El jugador nunca se mueve en X, así que el trail renderer no emitiría nunca. El script desactiva su emisión por completo, agrega un punto por frame, y desplaza los existentes a la velocidad del mundo, de modo que la estela queda atrás de un personaje que, técnicamente, está quieto.

### Correrlo localmente

```
git clone <repo-url>
```

Abrí el proyecto en Unity, cargá `MainMenuScene` y dale Play.

---

## Credits · Créditos

**Game assets**

- Music / SFX — [pixabay.com](https://pixabay.com)
- Sprites — Kenney, *Pixel Platformer*
- UI — Kenney, *Pixel Adventure UI*

**Developed by** — [@Ignosuke](https://github.com/Ignosuke)

---

Built for a game development course.
