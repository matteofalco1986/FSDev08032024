using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Hotel.Models
{
    public static class Queries
    {
        public static string AdminData = "SELECT * FROM Admins WHERE Username = @username AND Password = @password";
        public static string AddCustomer = @"INSERT INTO Clienti (Nome, Cognome, CodiceFiscale, Citta, Provincia, Email, Telefono, Mobile)
                                            VALUES (@Nome, @Cognome, @CodiceFiscale, @Citta, @Provincia, @Email, @Telefono, @Mobile)";
        public static string CustomersList = "SELECT * FROM Clienti";
        public static string AggiungiPrenotazione = @"INSERT INTO Prenotazioni (ClienteId, CameraId, TipoPrenotazioneId, Data, Anno, InizioSoggiorno, FineSoggiorno, Caparra, Tariffa)
                                            VALUES (@ClienteId, @CameraId, @TipoPrenotazioneId, @Data, @Anno, @InizioSoggiorno, @FineSoggiorno, @Caparra, @Tariffa)";
        public static string RoomsList = "SELECT * FROM Camere";
        public static string TypesOfBookingsList = "SELECT * FROM TipiPrenotazione";
        public static string RoomsDetails = @"SELECT * FROM Camere AS Camere 
                                              JOIN TipologieCamera AS TipologieCamera 
                                              ON Camere.TipologiaCameraId = TipologieCamera.TipologiaCameraId";

    }
}