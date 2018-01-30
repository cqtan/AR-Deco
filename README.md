# AR-Decorator 🙌
----
![ardeco01_title](https://user-images.githubusercontent.com/33485290/35540354-ccadab36-0555-11e8-9edf-3284cf3b02e5.png)

---
## Zusammenfassung

> Es soll eine mobile AR-Anwendung entwickelt werden, mit der es möglich ist, Möbelstücke bzw. Gegenstände virtuell in die reale Welt anzuzeigen und zu positionieren. Mit Hilfe eines Head-mounted-displays (HMD) für das Smartphone und einen Handtracking-Sensor sollte kontaktlose Interaktionen mit den virtuellen Objekten möglich sein, während man sich selbst frei in allen Richtungen drehen kann.

---

## Technologien & Hardware
* Leap Motion Controller ([LeapMotion](https://www.leapmotion.com/))
* Unity 2017.2.0f3 (mit [Vuforia SDK](https://www.vuforia.com/))
* Coloreality by TangoChen ([Link](https://github.com/TangoChen/Coloreality))
* Xcode 9.2
* iPhone 6
----
## Anforderungen
### 1) Reale Welt anzeigen
Es soll zunächst möglich sein, mit dem HMD die reale Welt zu sehen, die von dem Smartphone aufgenommen wird.

### 2) Virtuelle Objekte anzeigen
Mit Hilfe von Vuforia-Marker sollen virtuelle Objekte in die reale Welt angezeigt werden. Dazu soll die App die Marker erkennen und das entsprechende virtuelle Objekt über dem Marker kreieren. Sobald der Marker nicht mehr im Blickfeld der Kamera ist, verschwindet das Objekt wieder.

### 3) Handgesten Interaktionen
Ein Handtracking Sensor von LeapMotion soll ebenso auf dem HMD befestigt werden, damit Handgesten vor dem HMD erkannt werden können. Mögliche Interaktionen mit virtuellen Obkjekten:

* positionieren im 3D Raum
* rotieren
* skalieren
* entfernen aus der Szene


----
## Mögliches Szenario

Ein User führt die 'AR-Deco' App auf dem Smartphone aus und befestigt diese an einem HMD. Der Leap Motion Sensor befindet sich auch an der vorderen Seite des HMDs. Die Kamera des Smartphones nimmt die reale Welt auf und auf dem Display wird das Aufgenommene für beide Augen jeweils angezeigt.

Der User nimmt ein Vuforia-Marker vor der Kamera, um ein virtuelles Objekt auszuwählen und auf die reale Welt anzuzeigen. Damit dieses Objekt in der Szene bleibt, sollte der User dieses mit der Hand greifen (z.B.  mit einem Grab-Gesture).

Andere Interaktionen mit den virtuellen Objekten sollten auch möglch sein, z.B. rotieren, skalieren und entfernen.

----
## Setup Guide
Um die Anwendung benutzen zu können, müssen folgende Schritte durchgeführt werden:

### 1) Folgendes bereithalten:

* Computer mit Windows (für den Coloreality Server)
* iPhone (auf dem die Anwendung läuft)
* Leap Motion Controller
* HMD (Smartphone Rückenkamera muss freigehalten sein)
* Mac (für die Installation auf dem iPhone)
* Ausgedruckte VuMarker

### 2) Anwendung Installieren:
* siehe *Build and Installation (iOS)*

### 3) Geräte kalibrieren und Server starten:
* (Windows): Leap Motion mit dem Leap Motion Control Panel kalibrieren. Setup auf Leap Motion Seite folgen ([hier](https://developer.leapmotion.com/unity/#116)).
* (Windows): Coloreality.io Server starten auf einem. IP und Port merken. Die ausführbare Datei befindet sich im Projekt under 'others/Coloreality/executables/ColorealityServer_PC'
* (iOS): Anwendung starten und mit dem Server verbinden.
* Smartphone und Leap am HMD befestigen.

![LeapServerSettingsHMD](https://user-images.githubusercontent.com/33485290/33844441-9f7dec92-dea0-11e7-8b59-69678a0dfd94.png)


---

## User Guide:
Die folgenden Interaktionsmöglicheiten sind implementiert:

* Möbel anzeigen mit VuMarker
* Möbel anvisieren
* Position der Möbel mit ändern (Grab Gesture mit 1 Hand)
* Distanz variieren (Grab und Pinch Gesture)
* Rotation (Grab Gesture mit beiden Händen)
* Skalieren (Pinch Gesture mit beiden Händen)
* Löschen mit VuButton

### 1) Begriffserklärung:

* Grab Gesture: Wenn mit der Hand eine Faust geformt wird. Entweder mit der linken oder rechten Hand.
* Pinch Gesture: Wenn der Zeigerfinger den Daumen berührt. Jeweils mit der linken oder rechten Hand.
* Anvisieren: In diesem Kontext leuchten die Konturen von Möbel gelb, wenn sie im Blickfeld zentriert sind.
* VuMarker: Ein von Vuforia generiertes Bild um virtuelle Objekte zu ankern. Wird hier benutzt, um virtuelle Objekte anzuzeigen, sobald der VuMarker von der Kamera erfasst wird.
* VuButton: Die runde Fläche unterhalb des VuMarkers. Simuliert einen Knopf, der bei dessen Verdeckung aktiviert wird.
* Objekt: Möbel und Objekte sind hier gleichzusetzen.


### 2) Interaktionen:
##### Möbel anzeigen:
Halte einen VuMarker vor der Kamera, damit er erfasst werden kann. Bei guten Lichverhältnissen wird das Tracken erleichtert. Hierfür sind 4 Exemplare im Projekt beigelegt (Sofa, Cabinett, Bed, Bin)

![ardeco02_show](https://user-images.githubusercontent.com/33485290/35540434-22e68b26-0556-11e8-825c-16f1fe5f6ece.png)

---

##### Möbel instanziieren und positionieren:
Beim Anvisieren eines Möbelstücks vom VuMarker kann man mit einem Grab Gesture das gleiche Objekt instanziieren und die Position ändern.

Somit ist das Objekt unabhängig von dem VuMarker. Während das Grab Gesture erkannt wird, bleibt das Objekt im Blickfeld. So können auch Objekte, die weiter weg sind vom User genommen und platziert werden. Dabei muss die Hand nicht zwangsläufig das Objekt berühren, sondern sie muss lediglich von der Leap Motion erkannt werden.

![ardeco03_position](https://user-images.githubusercontent.com/33485290/35540450-34c56218-0556-11e8-9929-955fa65b7c03.png)

---

##### Distanz ändern:
Nachdem man ein Objekt mit dem Grab Gesture erfasst hat, kann man mit der anderen Hand ein Pinch Gesture vorführen, um den Abstand zwischen Objekt und User zu ändern.

Sobald beide Gestures von der Leap erkannt werden, erhöht sich der Abstand in Relation zum Abstand beider Hände. Im Umkehrschluss verringert sich der Abstand, je näher die Hände zusammenkommen.

![ardeco04_distance](https://user-images.githubusercontent.com/33485290/35540453-383d0892-0556-11e8-946e-207c9d8fd090.png)

---

##### Möbel rotieren:
Hat man ein Objekt anvisiert, kann man mit einem beidhändigen Grab Gesture die Rotation des Objekts beeinflußen.

Man kann es sich so vorstellen, als hätte man zwischen den Händen einen Ball. Rotiert man den Ball im Uhrzeigersinn, dann rotiert das Objekt auch in der selben Richtung. Hierbei spielen die Position beider Hände eine große Rolle. Je nachdem wie diese beiden zueinander im 3D-Raum stehen, so ändert sich auch die Rotation des anvisierten Objekts.

![ardeco05_rotate](https://user-images.githubusercontent.com/33485290/35540457-3b71fc8e-0556-11e8-9ce5-9c6159477d43.png)

---

##### Möbel skalieren:
Sobald ein Objekt anvisiert wird, kann man mit einem beidhändigen Pinch Gesture die Größe des Objekts ändern.

Dieses Feature funktioniert ähnlich wie bei der obigen Erklärung zur Änderung der Distanz. Je größer der Abstand beider Hände sind, desto größer wird das Objekt.

![ardeco06_scale](https://user-images.githubusercontent.com/33485290/35540459-408c5ce6-0556-11e8-92f3-6e4219467832.png)

---

##### Möbel löschen
Wie beim ersten Punkt, zeigt man hierfür den VuMarker der für das Löschen von Objekten zuständig ist. In diesem Fall der, der das 'Bin' Objekt (Mülleimer) anzeigt. Wenn dieser sichtbar ist und man ein Objekt anvisiert hat, kann man den VuButton verdecken, um das anvisierte Objekt zu löschen.

![ardeco07_delete](https://user-images.githubusercontent.com/33485290/35540465-43f3400c-0556-11e8-9375-39cbee74b82c.png)

----
## Project Setup

* Bei einem neuen Projekt die Ar-Deco Assets mit der beigefügten Datei 'ArDeco.unitypackage' laden.

* Entweder im Ordner 'ArLeapAssets' das Prefab 'ArLeap' in die Szene bzw. Hierarchy ziehen und die standardmäßige Main Camera entfernen.

* Oder unter 'ArLeapAssets/Scenes' die Szene 'VuLeap' mit der letzten Version (Bsp: _v200) auswählen.

* Um Vuforia zu aktivieren, die Player Settings im nächsten Abschnitt beachten.


----
## Build and Installation (iOS)
Auf dem '**Build Settings**' Fenster sollte man zunächst auf dem Platform '**iOS**' wählen, um schließlich Änderungen auf dem '**Player Settings**' durchführen zu können.

![ar_navigatebuildsettings](https://user-images.githubusercontent.com/33485290/33219956-8541a3b8-d145-11e7-844b-bfbb03117ee9.png)

![ar_buildwindow](https://user-images.githubusercontent.com/33485290/33219958-8909e884-d145-11e7-8b0f-114c1bfbee45.png)

![ar_playersettings](https://user-images.githubusercontent.com/33485290/33219964-8bae6998-d145-11e7-9836-f9495c634005.png)

Unter dem Reiter '**Other Settings**' muss eine ID im Textfeld '**Bundle Identifier**' und im Textfeld '**Camera Usage Description**' irgendein Text eingegeben werden (Letzteres, weil sonst schwarzes Display).

![ar_othersettings](https://user-images.githubusercontent.com/33485290/33219967-8ebb570e-d145-11e7-918d-7803e1ec0c8d.png)

Unter dem Reiter '**XR Settings**' braucht '**Virtual Reality Supported**' ein Häkchen und beide SDKs '**Vuforia**' und '**Cardboard**' müssen hinzugefügt werden. Cardboard ist notwendig, da sonst das Builden mit Xcode nicht funktioniert wird (Fehlende Dateien etc.).

![ar_xrsettings](https://user-images.githubusercontent.com/33485290/33219969-91694b5a-d145-11e7-8d5e-7b76ff6e471b.png)

Schließlich den '**Build**' Knopf auf dem Build Settings Fenster wählen und ein Ziel festlegen.

## Deploy (iOS)
In dem '**Build**' Ordner, der in Unity generiert wurde, befinden sich Xcode Projekt Dateien. Statt der gewohnten '**.xcodeproj**' Datei zu öffnen, sollte man stattdessen '**.xcworkspace**' öffnen, da sonst die Cardboard Dateien nicht gefunden werden (hier zB. die Podfiles).

![ar_xcodefile](https://user-images.githubusercontent.com/33485290/33219974-939a9866-d145-11e7-9498-6a4b58f2add8.png)

Nach dem Ausführen der '**xcworkspace**' Datei gelangt man auf die Build Settings, indem man links auf '**Unity-iPhone**' wählt.

![ar_xcodesettings](https://user-images.githubusercontent.com/33485290/33219975-960e8b52-d145-11e7-8932-539af61a88ac.png)

Hier kann man noch den endgültigen Namen der App eingeben. Im '**Signing**' Bereich muss man im Team einen Provisioning Profile auswählen (einen Kostenlosen reicht auch aus, um ein Paar Tage zu testen. Danach muss man diesen Xcode Schritt nochmal machen.

Schließlich ein iOS Gerät per USB verbinden, das Gerät auswählen und über '**Run**' installieren lassen. Die App sollte dann automatisch ausgeführt werden.

![ar_xcoderun](https://user-images.githubusercontent.com/33485290/33219976-980d6874-d145-11e7-9af6-728fd93ab724.png)


