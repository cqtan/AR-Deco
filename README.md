# AR Decorator
![Example inspired by IKEA's PLACE app](https://user-images.githubusercontent.com/33485290/32548812-4be056de-c487-11e7-9a4c-17fb70320ded.jpg)
----
## Zusammenfassung

> Es soll eine mobile AR-Anwendung entwickelt werden, mit der es möglich ist, Möbelstücke bzw. Gegenstände virtuell in die reale Welt anzuzeigen und zu positionieren. Mit Hilfe eines Head-mounted-displays (HMD) für das Smartphone und einen Handtracking-Sensor sollte kontaktlose Interaktionen mit den virtuellen Objekten möglich sein, während man slebst sich frei in allen Richtungen drehen kann.

----
## Tools
* Smartphone
* HMD ([Für Smartphones](https://www.amazon.de/gp/product/B01L8DZA3O/ref=oh_aui_detailpage_o00_s01?ie=UTF8&psc=1))
* Handtracking Sensor ([LeapMotion sensor](https://www.leapmotion.com/))
* Unity (mit [Vuforia SDK](https://www.vuforia.com/))
* Vuforia Marker

----
## Anforderungen
### 1) Reale Welt anzeigen
Es soll zunächst möglich sein, mit dem HMD die reale Welt zu sehen, die von dem Smartphone aufgenommen wurde. Der einfacher halbdurchlässiger Spiegel-Methode wäre zu einfach. Daher sollten wir auf eine stereoscopische Bildübertragung zurückgreifen.

### 2) Virtuelle Objekte anzeigen
Mit Hilfe von Vuforia-Marker sollen virtuelle Objekte in die reale Welt angezeigt werden. Dazu soll die App die Marker erkennen und das entsprechende virtuelle Objekt über dem Marker kreieren. Sobald der Marker nicht mehr im Blickfeld der Kamera ist, verschwindet das Objekt wieder.

### 3) Handgesten Interaktionen
Ein Handtracking Sensor von LeapMotion soll ebenso auf dem HMD befestigt werden, damit Handgesten vor dem HMD erkannt werden können. Mögliche Interaktionen mit virtuellen Obkjekten:

* positionieren im 3D Raum
* rotieren
* skalieren
* entfernen aus der Szene

### 4) (optional: 6DoF implementieren)
Mobile VR/AR Anwendungen können momentan nur 'on-the-spot' Bewegungen tracken, sprich, man SDKs die die Tiefenwahrnehmung durch die Smartphone-Kamera erkennen können ist noch sehr neu. Es gibt zwar externe Devices dafür, aber die sind sehr teuer. Stattdessen gibt es zumindest auf iOS Seite mittels ARKit SDK vielleicht diese Möglichkeit. Für Android müsste man noch recherschieren. Damit  würde man nicht nur auf der Stellle sich umschauen, sondern auch frei im Raum bewegen können.

----
## Mögliche Vorgehensweise

* Zunächst Stereoskopie mit Vuforia implementieren und Markers tracken können. In der neusten Versionen von Unity ist dieses Feature schon eingebaut, so dass man Stereoskopie mit einem Häkchen aktivieren kann. Die Verbindung mit Vuforia muss noch geschafft werden (auf iOS ist das noch recht schwierig, vielleicht auf Android ist das leichter?). Hierzu ein ([Link](https://library.vuforia.com/articles/Solution/Working-with-Digital-Eyewear-in-Unity.html))


* Da wir ja verschiedene Geräte haben (Android und iOS), wäre es sinnvoll auf platformunabhängige SDKs (Google VR, etc.) zurückzugreifen, falls notwendig.


* Die LeapMotion funktioniert kabellos mit dem Smartphone noch nicht. Daher müsste man eine kleine Server/Client Anwendung machen, damit die Kommunikation zwischen den beiden stattfindet.


----
## Mögliches Szenario

Ein User führt die 'AR-Deco' App auf dem Smartphone aus und befestigt diese an einem HMD. Der LeapMotion Sensor befindet sich auch an der vorderen Seite des HMDs. Die Kamera des Smartphones nimmt die reale Welt auf und auf dem Display wird das Aufgenommene für beide Augen jeweils angezeigt.

Der User nimmt ein Vuforia-Marker vor der Kamera, um ein virtuelles Objekt auszuwählen und auf die reale Welt anzuzeigen. Damit dieses Objekt in der Szene bleibt, sollte der User dieses mit der Hand greifen (z.B.  mit einem Grab-Gesture).

Andere Interaktionen mit den virtuellen Objekten sollten auch möglch sein, z.B. rotieren, skalieren und entfernen.

----
## Links 
* Installing Vuforia ([link](https://library.vuforia.com/articles/Training/getting-started-with-vuforia-in-unity-2017-2-beta.html))

* Stereoscopic Rendering with Vuforia ([link](https://library.vuforia.com/articles/Solution/Working-with-Digital-Eyewear-in-Unity.html))

* Establishing communication between Smartphone and LeapMotion ([link](https://github.com/TangoChen/Coloreality))

* Using Smartphone camera in Unity ([link](https://www.youtube.com/watch?v=T6bd_MQ2ass))

* AR development example for Android and iOS devices ([link](https://programminghistorian.org/lessons/intro-to-augmented-reality-with-unity))


