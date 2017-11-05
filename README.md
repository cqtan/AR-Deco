# AR Decorator

----
## Zusammenfassung

> Es soll eine mobile AR-Anwendung entwickelt werden, mit der es möglich ist, Möbelstücke bzw. Gegenstände virtuell in der realen Welt zu anzuzeigen und zu positionieren. Mit Hilfe eines Head-mounted-displays (HMD) für das Smartphone und einen Handtracking-Sensor sollte kontaktlose Interaktionen mit den virtuellen Objekten möglich sein, während man sich frei in allen Richtungen drehen kann.

----
## Tools
* Smartphone
* HMD ([Für Smartphones] (https://www.amazon.de/gp/product/B01L8DZA3O/ref=oh_aui_detailpage_o00_s01?ie=UTF8&psc=1))
* Handtracking Sensor ([LeapMotion sensor] (https://www.leapmotion.com/))
* Unity (mit [Vuforia SDK] (https://www.vuforia.com/))
* Vuforia Marker

----
## Anforderungen
### 1) Reale Welt anzeigen
Es soll zunächst möglich sein, mit dem HMD die reale Welt zu sehen, die von dem Smartphone aufgenommen wurde. 

### 2) Virtuelle Objecte anzeigen
Mit Hilfe von Vuforia-Markers sollen virtuelle Objekte in die reale Welt angezeigt werden. Dazu soll die App die Marker erkennen und das entsprechende virtuelle Objekt über dem Marker kreieren. Sobald der Marker nicht mehr im Blickfeld der Kamera ist, verschwindet das Objekt wieder.

### 3) Handgesten Interaktionen
Ein Handtracking Sensor von LeapMotion soll ebenso auf dem HMD befestigt werden, damit Handgesten vor dem HMD erkannt werden können. Mögliche Interaktionen mit virtuellen Obkjekten:

* positionieren im 3D Raum
* rotieren
* skalieren
* entfernen aus der Szene

----
## Setup

----
## Links 
* Installing Vuforia ([link] (https://library.vuforia.com/articles/Training/getting-started-with-vuforia-in-unity-2017-2-beta.html))

* Establishing communication between Smartphone and LeapMotion ([link] (https://github.com/TangoChen/Coloreality))

* Using Smartphone camera in Unity ([link] (https://www.youtube.com/watch?v=c6NXkZWXHnc))

* AR development example for Android and iOS devices ([link] (https://programminghistorian.org/lessons/intro-to-augmented-reality-with-unity))


