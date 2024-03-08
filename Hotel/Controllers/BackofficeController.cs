using Hotel.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Hotel.Controllers
{
    [Authorize]
    public class BackofficeController : Controller
    {
        // GET: Backoffice
        public ActionResult Index()
        {
            return View();
        }

        // GET Form aggiunta cliente
        [HttpGet]
        public ActionResult AddCustomer()
        {
            return View();
        }

        // Post form aggiunta cliente
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddCustomer([Bind(Exclude = "ClienteId")] Clienti cliente)
        {
            if (ModelState.IsValid)
            {
                var conn = Commands.ConnectToDb();
                SqlCommand command = new SqlCommand(Queries.AddCustomer, conn);
                command.Parameters.AddWithValue("@Nome", cliente.Nome);
                command.Parameters.AddWithValue("@Cognome", cliente.Cognome);
                command.Parameters.AddWithValue("@CodiceFiscale", cliente.CodiceFiscale);
                command.Parameters.AddWithValue("@Citta", cliente.Citta);
                command.Parameters.AddWithValue("@Provincia", cliente.Provincia);
                command.Parameters.AddWithValue("@Email", cliente.Email);
                command.Parameters.AddWithValue("@Telefono", cliente.Telefono);
                command.Parameters.AddWithValue("@Mobile", cliente.Mobile);

                command.ExecuteNonQuery();

                return RedirectToAction("Index", "Backoffice");
            }
            return View(cliente);
        }

        [HttpGet]
        public ActionResult ElencoClienti()
        {
            var conn = Commands.ConnectToDb();
            SqlCommand command = new SqlCommand(Queries.CustomersList, conn);
            var reader = command.ExecuteReader();

            List<Clienti> Customers = new List<Clienti>();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    var cliente = new Clienti();
                    cliente.ClienteId = (int)reader["ClienteId"];
                    cliente.Nome = (string)reader["Nome"];
                    cliente.Cognome = (string)reader["Cognome"];
                    cliente.CodiceFiscale = (string)reader["CodiceFiscale"];
                    cliente.Citta = (string)reader["Citta"];
                    cliente.Provincia = (string)reader["Provincia"];
                    cliente.Email = (string)reader["Email"];
                    cliente.Telefono = (string)reader["Telefono"];
                    cliente.Mobile = (string)reader["Mobile"];
                    Customers.Add(cliente);
                }
            }
            return View(Customers);
        }

        [HttpGet]
        public ActionResult Prenota()
        {
            var conn = Commands.ConnectToDb();

            // Comandi per recuperare le info da altre tabelle
            var commandClienti = new SqlCommand(Queries.CustomersList, conn);
            var commandCamere = new SqlCommand(Queries.RoomsList, conn);
            var commandTipiPrenotazione = new SqlCommand(Queries.TypesOfBookingsList, conn);

            var reader = commandClienti.ExecuteReader();

            // Lista clienti
            var clienti = new List<Clienti>();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    clienti.Add(new Clienti()
                    {
                        ClienteId = (int)reader["ClienteId"],
                        Nome = (string)reader["Nome"],
                        Cognome = (string)reader["Cognome"],
                        CodiceFiscale = (string)reader["CodiceFiscale"],
                        Citta = (string)reader["Citta"],
                        Provincia = (string)reader["Provincia"],
                        Email = (string)reader["Email"],
                        Telefono = (string)reader["Telefono"],
                        Mobile = (string)reader["Mobile"],

                    });
                }
            }
            reader.Close();
            reader = commandCamere.ExecuteReader();

            // Lista camere
            var camere = new List<Camere>();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    camere.Add(new Camere()
                    {
                        CameraId = (int)reader["CameraId"],
                        TipologiaCameraId = (int)reader["TipologiaCameraId"],
                        Descrizione = (string)reader["Descrizione"],
                    });
                }
            }
            reader.Close();
            reader = commandTipiPrenotazione.ExecuteReader();

            // Lista tipi prenotazione
            var tipiPrenotazione = new List<TipiPrenotazione>();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    tipiPrenotazione.Add(new TipiPrenotazione()
                    {
                        TipoPrenotazioneId = (int)reader["TipoPrenotazioneId"],
                        TipoPrenotazione = (string)reader["TipoPrenotazione"],
                    });
                }
            }
            reader.Close();
            conn.Close();
            ViewBag.Clienti = clienti;
            ViewBag.Camere = camere;
            ViewBag.TipiPrenotazione = tipiPrenotazione;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Prenota([Bind(Exclude = "PrenotazioneId")] Prenotazioni prenotazione)
        {
            if (ModelState.IsValid)
            {
                var conn = Commands.ConnectToDb();
                SqlCommand command = new SqlCommand(Queries.AggiungiPrenotazione, conn);
                command.Parameters.AddWithValue("@ClienteId", prenotazione.ClienteId);
                command.Parameters.AddWithValue("@CameraId", prenotazione.CameraId);
                command.Parameters.AddWithValue("@TipoPrenotazioneId", prenotazione.TipoPrenotazioneId);
                command.Parameters.AddWithValue("@Data", prenotazione.Data);
                command.Parameters.AddWithValue("@Anno", prenotazione.Anno);
                command.Parameters.AddWithValue("@InizioSoggiorno", prenotazione.InizioSoggiorno);
                command.Parameters.AddWithValue("@FineSoggiorno", prenotazione.FineSoggiorno);
                command.Parameters.AddWithValue("@Caparra", prenotazione.Caparra);
                command.Parameters.AddWithValue("@Tariffa", prenotazione.Tariffa);

                command.ExecuteNonQuery();

                return RedirectToAction("Index", "Backoffice");
            }
            return View(prenotazione);
        }

        [HttpGet]
        public ActionResult Camere()
        {
            var conn = Commands.ConnectToDb();
            SqlCommand command = new SqlCommand(Queries.RoomsDetails, conn);
            var reader = command.ExecuteReader();

            List<Camere> Camere = new List<Camere>();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    var camera = new Camere();
                    var TipologiaCamera = new TipologieCamera()
                    {
                        TipologiaCameraId = (int)reader["TipologiaCameraId"],
                        TipologiaCamera = (string)reader["TipologiaCamera"],

                    };
                    camera.CameraId = (int)reader["CameraId"];
                    camera.TipologiaCameraId = (int)reader["TipologiaCameraId"];
                    camera.Descrizione = (string)reader["Descrizione"];
                    camera.TipologiaCamera = TipologiaCamera;
                    Camere.Add(camera);
                }
            }
            return View(Camere);
        }

    }
}