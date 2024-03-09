Per far funzionare l'applicazione è fondamentale fare tre cose:

- CREARE IL DATABASE
- COLLEGARE LA APP DATABASE LOCALE
- SOSTITURE L'URL PER LE OPERAZIONI ASINCRONE CON IL PROPRIO LOCALHOST

CREARE IL DATABASE
La cartella principale contiene un file "ScriptToBuildDatabase.sql". Il file contiene una query che va eseguita in SSMS per creare il DB. Il database contiene
un piccolo assortimento di dati gia pronti per testare. E' comunque possibile aggiungerne tramite SSMS o tramite le funzioni della WebApp.

COLLEGARE LA APP AL PROPRIO DATABASE LOCALE
Per farlo, andare nel file Web.config e sostituire alla ConnectionString corrente quella per il collegamento al proprio server. La stringa sarà come segue:
	    <add
			name="DbHotelConnection"
			connectionString="Server=LAPTOP-1M2QKVCO\SQLEXPRESS; Initial Catalog=Blog; Integrated Security=true"
			providerName="System.Data.SqlClient"
		/>

SOSTITUIRE L'URL PER LE OPERAZIONI ASINCRONE CON IL PROPRIO LOCALHOST
  -  Andare nel file Views/Backoffice/Prenotazioni.cshtml
  -  Alle righe 69, 83, 126, trovare il comando "fetch("https://localhost:44367/Backoffice/PensioniComplete")"
  -  Per ogni riga, sostituire la parte "https://localhost:44367" con il proprio url di hosting, se diverso.

NOTA
I seguenti endpoint ritornano JSON per chiamate asincrone:
  - "/Backoffice/ElencoClientiJson"
  - "/Backoffice/PensioniComplete"

L'accesso dal BackofficeController è autorizzato anche da utenti non loggati. È possibile usarlo per costruire la propria interfaccia in HTML esterno.
