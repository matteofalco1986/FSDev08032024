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
        public static string ServicesList = "SELECT * FROM ListaServiziAggiuntivi";
        public static string BookingsList = "SELECT * FROM Prenotazioni";

        public static string AggiungiPrenotazione = @"INSERT INTO Prenotazioni (ClienteId, CameraId, TipoPrenotazioneId, Data, Anno, InizioSoggiorno, FineSoggiorno, NumeroNotti, Caparra, Tariffa)
                                            VALUES (@ClienteId, @CameraId, @TipoPrenotazioneId, @Data, @Anno, @InizioSoggiorno, @FineSoggiorno, @NumeroNotti, @Caparra, @Tariffa)";
        public static string AggiungiAmenity = @"INSERT INTO Amenities (PrenotazioneId, ServizioId, Data, Quantita)
                                                    VALUES (@PrenotazioneId, @ServizioId, @Data, @Quantita)";
        public static string RoomsList = "SELECT * FROM Camere";
        public static string TypesOfBookingsList = "SELECT * FROM TipiPrenotazione";
        public static string RoomsDetails = @"SELECT * FROM Camere AS Camere 
                                              JOIN TipologieCamera AS TipologieCamera 
                                              ON Camere.TipologiaCameraId = TipologieCamera.TipologiaCameraId";
        public static string BookingsWithDetails = @"SELECT * FROM Prenotazioni AS Prenotazioni 
                                                     JOIN Clienti AS Clienti 
                                                     ON Prenotazioni.ClienteId = Clienti.ClienteId
                                                     JOIN Camere AS Camere
                                                     ON Prenotazioni.CameraId = Camere.CameraId
                                                     JOIN TipiPrenotazione AS TipiPrenotazione
                                                     ON Prenotazioni.TipoPrenotazioneId = TipiPrenotazione.TipoPrenotazioneId
                                                     JOIN TipologieCamera AS TipologieCamera
                                                     ON Camere.TipologiaCameraId = TipologieCamera.TipologiaCameraId";
        public static string SingleBookingWithDetails = @"SELECT * FROM Prenotazioni AS Prenotazioni 
                                                     JOIN Clienti AS Clienti 
                                                     ON Prenotazioni.ClienteId = Clienti.ClienteId
                                                     JOIN Camere AS Camere
                                                     ON Prenotazioni.CameraId = Camere.CameraId
                                                     JOIN TipiPrenotazione AS TipiPrenotazione
                                                     ON Prenotazioni.TipoPrenotazioneId = TipiPrenotazione.TipoPrenotazioneId
                                                     JOIN TipologieCamera AS TipologieCamera
                                                     ON Camere.TipologiaCameraId = TipologieCamera.TipologiaCameraId
                                                     WHERE Prenotazioni.PrenotazioneId = @PrenotazioneId";
        public static string AmenitiesById = @"SELECT * FROM Amenities AS Amenities
                                                JOIN ListaServiziAggiuntivi AS ListaServiziAggiuntivi
                                                ON Amenities.ServizioId = ListaServiziAggiuntivi.ServizioId
                                                WHERE Amenities.PrenotazioneId = @PrenotazioneId";
        }
    }