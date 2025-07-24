# PatientSolution

Dieses Projekt ist eine interaktive **Blazor WebAssembly-Anwendung**, die als Frontend für die [HL7-Datenintegrations-Pipeline](https://github.com/k0mbinator/HL7DataProcessor) dient. Ich habe diese Anwendung entwickelt, um die übersichtliche Visualisierung und Verwaltung von Patientendaten zu ermöglichen, die zuvor aus HL7v2-Nachrichten extrahiert und in einer Oracle-Datenbank gespeichert wurden.

Mit der PatientSolution zeige ich meine Fähigkeit, eine vollständige End-to-End-Lösung von der Backend-Datenverarbeitung bis zur benutzerfreundlichen Frontend-Visualisierung zu realisieren.

---

## Funktionalität

* **Interaktive Patientenübersicht:** Ich habe die Anwendung so gestaltet, dass sie alle in der Datenbank vorhandenen Patientendaten in einem dynamischen und filterbaren Grid anzeigt.
* **Datenvisualisierung:** Ich habe eine klare Darstellung relevanter Patienteninformationen wie ID, Name, Geburtsdatum, Geschlecht und Adresse implementiert.
* **Moderne Benutzeroberfläche:** Für das Design und die responsiven UI-Komponenten nutzte ich das Blazor Bootstrap Framework.
* **API-Integration:** Die Anwendung kommuniziert nahtlos mit einer separaten ASP.NET Core Web API, um Patientendaten sicher abzurufen.

---

## Technologien

Für dieses Projekt nutzte ich folgende Haupttechnologien und Tools:

* **Frontend:**
    * **Blazor WebAssembly:** Ich setzte Blazor WebAssembly für die Entwicklung der interaktiven Single-Page-Application (SPA) im Browser ein.
    * **Blazor Bootstrap:** Diese UI-Komponentenbibliothek integrierte ich, um Bootstrap 5 in Blazor zu nutzen.
    * **C# / .NET 8:** Dies war meine primäre Programmiersprache und das Framework für die Entwicklung.
* **Backend (API - separate Solution):**
    * **ASP.NET Core Web API:** Ich erstellte diese, um die RESTful-Schnittstelle zum Abrufen der Patientendaten bereitzustellen.
    * **Entity Framework Core:** Für die Datenzugriffsschicht zur Oracle-Datenbank nutzte ich Entity Framework Core.
    * **Oracle Database:** Als Datenspeicher für die Patientendaten verwendete ich eine Oracle-Datenbank.
* **Entwicklungstools & Qualitätssicherung:**
    * **GitHub Copilot:** Ich setzte GitHub Copilot zur Beschleunigung der Entwicklung und als Unterstützung bei der Code-Generierung ein.
    * **SonarQube:** Für statische Code-Analyse zur Sicherstellung hoher Code-Qualität und Einhaltung von Best Practices nutzte ich SonarQube.
    * **CodeQL & Secret Scanning (GitHub):** Zusätzlich aktivierte ich die integrierten Sicherheitsfunktionen von GitHub, um potenzielle Schwachstellen frühzeitig zu erkennen.

---

## Architektur und Projektstruktur

Die PatientSolution ist als Client-Server-Anwendung konzipiert. Ich habe sie in mehrere logische Projekte unterteilt, um eine klare Trennung der Verantwortlichkeiten, Wiederverwendbarkeit und Wartbarkeit zu gewährleisten:

1.  **`PatientSolution.Client` (Blazor WebAssembly Client):**
    * Dies ist die Frontend-Anwendung, die im Browser des Benutzers ausgeführt wird.
    * Ich habe sie für die Darstellung der Benutzeroberfläche, die Benutzerinteraktion und das Senden von Anfragen an das API-Backend entwickelt.
    * Die Aufteilung als separates Projekt fördert die **Trennung von Frontend-Logik und Backend-Diensten**.

2.  **`PatientSolution.Api` (ASP.NET Core Web API):**
    * Dies ist die separate Backend-Anwendung, die die Geschäftslogik und den Datenzugriff zur Oracle-Datenbank kapselt.
    * Ich habe sie zur Bereitstellung der RESTful-Schnittstelle erstellt, über die der Blazor Client Patientendaten sicher abrufen kann.
    * Die Trennung des Backends in ein eigenes Projekt ermöglicht eine **unabhängige Entwicklung, Bereitstellung und Skalierung** von API und Frontend.

3.  **`PatientSolution.Shared` (Shared Library):**
    * Dieses Projekt enthält gemeinsame Modelle (`Patient.cs`) und Schnittstellen, die sowohl vom Client- als auch vom API-Projekt benötigt werden.
    * Ich nutzte es, um die **Wiederverwendbarkeit von Code** zu fördern und sicherzustellen, dass die Datenstrukturen zwischen Frontend und Backend konsistent sind, ohne Redundanzen zu schaffen.

Diese modulare Struktur verbessert die **Wartbarkeit** und **Testbarkeit** der Anwendung und ermöglicht eine klare **Separation of Concerns**.

---

## Komponentenübersicht

Hier ist eine detailliertere Übersicht über die Schlüsselkomponenten innerhalb der einzelnen Projekte, die ich entwickelt habe:

### `PatientSolution.Api` Komponenten

* **`Program.cs`:**
    * Der Einstiegspunkt der API-Anwendung.
    * Ich konfigurierte hier die Dependency Injection (Services), die HTTP-Request-Pipeline (Middleware) und das Routing.
    * Ich implementierte das sichere Laden des Datenbank-Connection Strings aus User Secrets (für die Entwicklung) oder Umgebungsvariablen (für die Produktion).
* **`Controllers/PatientsController.cs`:**
    * Ich definierte hier den API-Endpunkt (`/api/patients`) für den Zugriff auf Patientendaten.
    * Ich stellte eine HTTP GET-Methode bereit, um alle Patientendaten abzurufen, und nutzte den `IPatientService` zur Geschäftslogik und Fehlerbehandlung.
* **`Services/IPatientService.cs`:**
    * Ich definierte den Vertrag für den Patientendaten-Service.
    * Diese Schnittstelle dient dem Abrufen von Patientendaten.
* **`Services/PatientService.cs`:**
    * Ich implementierte den `IPatientService`-Vertrag.
    * Diese Klasse enthält die Geschäftslogik für den Datenzugriff auf die Datenbank und nutzt Entity Framework Core, um Patientendaten abzufragen.
* **`Data/PatientContext.cs`:**
    * Dies ist der Entity Framework Core `DbContext` für die Interaktion mit der Oracle-Datenbank.
    * Ich definierte hier das `DbSet` für die `Patient`-Entität und konfigurierte das Mapping zwischen der C#-Klasse `Patient` und der Oracle-Tabelle `PATIENTS` sowie deren Spalten.

### `PatientSolution.Client` Komponenten

* **`Program.cs`:**
    * Der Einstiegspunkt der Blazor WebAssembly-Anwendung.
    * Ich konfigurierte hier die Dependency Injection (Services), einschließlich des `HttpClient` und des `PatientService`.
    * Ich implementierte das Lesen der API-Basis-URL aus der `appsettings.json`.
* **`Services/PatientService.cs`:**
    * Diese Klasse stellt Methoden für den Client bereit, um mit der Backend-API zu kommunizieren.
    * Ich nutzte den konfigurierten `HttpClient`, um GET-Anfragen an den `/api/patients`-Endpunkt der API zu senden und die JSON-Antwort in `Patient`-Objekte zu deserialisieren.
* **`Pages/PatientList.razor`:**
    * Dies ist die Razor-Komponente, die die Patientenübersichtsseite darstellt.
    * Ich nutzte die Blazor Bootstrap `Grid`-Komponente, um die Patientendaten interaktiv anzuzeigen.
    * Die `PatientsDataProvider`-Methode ist für das Abrufen und Aufbereiten der Daten für das Grid zuständig.

### `PatientSolution.Shared` Komponenten

* **`Models/Patient.cs`:**
    * Ich definierte hier das gemeinsame Datenmodell für die `Patient`-Entität.
    * Es wird sowohl vom API-Backend (für die Datenbankinteraktion und API-Antworten) als auch vom Blazor Client (für die Datenbindung in der UI) verwendet und stellt sicher, dass die Datenstruktur zwischen Frontend und Backend konsistent ist.

---

## Setup und Ausführung

Um die PatientSolution lokal auszuführen, müssen Sie sowohl das API-Backend als auch die Blazor WebAssembly-Anwendung starten.

### Voraussetzungen

* **Visual Studio 2022** (oder höher)
* **.NET 8 SDK**
* **Oracle-Datenbank:** Eine lauffähige Oracle-Datenbankinstanz.
    * **Oracle Express Edition (XE):** Falls noch nicht vorhanden, können Sie die [Oracle Database Express Edition (XE)](https://www.oracle.com/database/technologies/xe-downloads.html) installieren, um eine lokale Datenbankinstanz bereitzustellen.
    * **Einrichtung der `PATIENTS`-Tabelle:** Details zur Erstellung der `PATIENTS`-Tabelle finden Sie im [HL7-Datenintegrations-Pipeline Repository](https://github.com/k0mbinator/HL7DataProcessor) (TableCreation.sql) unter dem SQLWorksheets-Ordner.

### 1. API-Backend konfigurieren und starten

Das API-Backend (`PatientSolution.Api`) benötigt einen Datenbank-Connection String. Für die lokale Entwicklung wird dieser sicher über User Secrets verwaltet.

1.  **User Secrets konfigurieren:**
    * Öffnen Sie Visual Studio und navigieren Sie zum Projekt `PatientSolution.Api`.
    * Rechtsklicken Sie auf das Projekt `PatientSolution.Api` im Projektmappen-Explorer und wählen Sie **"Benutzergeheimnisse verwalten"** (Manage User Secrets).
    * Fügen Sie in der geöffneten `secrets.json`-Datei den vollständigen Connection String für Ihre Oracle-Datenbank hinzu:
        ```json
        {
          "ConnectionStrings": {
            "OracleDbConnection": "DATA SOURCE=localhost:1521/XEPDB1;USER ID=PatientAppUser;PASSWORD=IhrSicheresDatenbankPasswortHier;"
          }
        }
        ```
        *Ersetzen Sie `IhrSicheresDatenbankPasswortHier` durch Ihr tatsächliches Datenbankpasswort.*
2.  **API starten:**
    * Stellen Sie sicher, dass `PatientSolution.Api` als Startprojekt in Visual Studio ausgewählt ist (oder konfigurieren Sie mehrere Startprojekte).
    * Starten Sie das API-Projekt (z.B. mit **F5** oder "Debug" > "Starten"). Die API sollte auf `https://localhost:7190` (oder dem in Ihrer `launchSettings.json` definierten HTTPS-Port) lauschen.

### 2. Blazor WebAssembly Client konfigurieren und starten

Der Blazor Client (`PatientSolution.Client`) benötigt die Basis-URL Ihrer API.

1.  **API-Basis-URL konfigurieren:**
    * Im Wurzelverzeichnis des `PatientSolution.Client`-Projekts (dort, wo die `.csproj`-Datei liegt), erstellen oder öffnen Sie die Datei `appsettings.json`.
    * Fügen Sie dort die Basis-URL Ihrer API hinzu:
        ```json
        {
          "ApiBaseUrl": "https://localhost:7190/"
        }
        ```
        *Stellen Sie sicher, dass der Port (`7190`) mit dem Port übereinstimmt, auf dem Ihre API läuft.*

2.  **Client starten:**
    * Stellen Sie sicher, dass `PatientSolution.Client` als Startprojekt in Visual Studio ausgewählt ist (oder konfigurieren Sie mehrere Startprojekte, um API und Client gleichzeitig zu starten).
    * Starten Sie das Client-Projekt (z.B. mit **F5** oder "Debug" > "Starten"). Die Anwendung sollte in Ihrem Browser geöffnet werden.
    * Navigieren Sie zur `/patients`-Seite, um die Patientendaten im Grid anzuzeigen.

---

### Screenshots



---

## Verwandtes Projekt

Dieses Projekt ist eng mit der [HL7-Datenintegrations-Pipeline](https://github.com/k0mbinator/HL7DataProcessor) verbunden, die für die Extraktion und Speicherung der Patientendaten verantwortlich ist.
