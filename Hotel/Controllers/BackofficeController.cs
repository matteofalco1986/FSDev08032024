using Hotel.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Services.Description;

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
        public ActionResult Prenotazioni()
        {
            var conn = Commands.ConnectToDb();
            SqlCommand command = new SqlCommand(Queries.BookingsWithDetails, conn);

            var reader = command.ExecuteReader();

            List<Prenotazioni> Prenotazioni = new List<Prenotazioni>();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    var prenotazione = new Prenotazioni();
                    var tipoPrenotazione = new TipiPrenotazione()
                    {
                        TipoPrenotazioneId = (int)reader["TipoPrenotazioneId"],
                        TipoPrenotazione = (string)reader["TipoPrenotazione"],

                    };
                    var camera = new Camere()
                    {
                        CameraId = (int)reader["CameraId"],
                        TipologiaCameraId = (int)reader["TipologiaCameraId"],
                        Descrizione = (string)reader["Descrizione"],
                    };
                    var tipologiaCamera = new TipologieCamera()
                    {
                        TipologiaCameraId = (int)reader["TipologiaCameraId"],
                        TipologiaCamera = (string)reader["TipologiaCamera"],
                    };
                    var cliente = new Clienti()
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
                    };
                    prenotazione.PrenotazioneId = (int)reader["PrenotazioneId"];
                    prenotazione.ClienteId = (int)reader["ClienteId"];
                    prenotazione.CameraId = (int)reader["CameraId"];
                    prenotazione.TipoPrenotazioneId = (int)reader["TipoPrenotazioneId"];
                    prenotazione.Data = (DateTime)reader["Data"];
                    prenotazione.Anno = (int)reader["Anno"];
                    prenotazione.InizioSoggiorno = (DateTime)reader["InizioSoggiorno"];
                    prenotazione.FineSoggiorno = (DateTime)reader["FineSoggiorno"];
                    prenotazione.NumeroNotti = (int)((DateTime)reader["FineSoggiorno"] - (DateTime)reader["InizioSoggiorno"]).Days;
                    prenotazione.Caparra = Convert.ToDouble(reader["Caparra"]);
                    prenotazione.Tariffa = Convert.ToDouble(reader["Tariffa"]);
                    prenotazione.Cliente = cliente;
                    prenotazione.TipoPrenotazione = tipoPrenotazione;
                    prenotazione.Camera = camera;
                    prenotazione.Camera.TipologiaCamera = tipologiaCamera;
                    Prenotazioni.Add(prenotazione);
                }
            }

            return View(Prenotazioni);
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
                command.Parameters.AddWithValue("@NumeroNotti", (prenotazione.FineSoggiorno - prenotazione.InizioSoggiorno).Days);
                command.Parameters.AddWithValue("@Caparra", prenotazione.Caparra);
                command.Parameters.AddWithValue("@Tariffa", prenotazione.Tariffa);

                command.ExecuteNonQuery();

                return RedirectToAction("Prenotazioni", "Backoffice");
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

        [HttpGet]
        public ActionResult Amenities()
        {
            var conn = Commands.ConnectToDb();

            // Comandi per recuperare le info da altre tabelle
            var commandPrenotazioni = new SqlCommand(Queries.BookingsList, conn);
            var commandListaServiziAggiuntivi = new SqlCommand(Queries.ServicesList, conn);

            var reader = commandListaServiziAggiuntivi.ExecuteReader();

            // Lista servizi aggiuntivi
            var serviziAggiuntivi = new List<ListaServiziAggiuntivi>();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    serviziAggiuntivi.Add(new ListaServiziAggiuntivi()
                    {
                        ServizioId = (int)reader["ServizioId"],
                        TipoServizio = (string)reader["TipoServizio"],
                        Prezzo = Convert.ToDouble(reader["Prezzo"]),
                    });
                }
            }
            reader.Close();
            reader = commandPrenotazioni.ExecuteReader();

            // Lista prenotazioni
            var prenotazioni = new List<Prenotazioni>();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    prenotazioni.Add(new Prenotazioni()
                    {
                        PrenotazioneId = (int)reader["PrenotazioneId"],
                        ClienteId = (int)reader["ClienteId"],
                        CameraId = (int)reader["CameraId"],
                        TipoPrenotazioneId = (int)reader["TipoPrenotazioneId"],
                        Data = (DateTime)reader["Data"],
                        Anno = (int)reader["Anno"],
                        InizioSoggiorno = (DateTime)reader["InizioSoggiorno"],
                        FineSoggiorno = (DateTime)reader["FineSoggiorno"],
                        NumeroNotti = (int)((DateTime)reader["FineSoggiorno"] - (DateTime)reader["InizioSoggiorno"]).Days,
                        Caparra = Convert.ToDouble(reader["Caparra"]),
                        Tariffa = Convert.ToDouble(reader["Tariffa"]),
                    });
                }
            }
            reader.Close();
            conn.Close();


            ViewBag.ServiziAggiuntivi = serviziAggiuntivi;
            ViewBag.Prenotazioni = prenotazioni;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Amenities([Bind(Exclude = "AmenityId")] Amenities amenity)
        {
            if (ModelState.IsValid)
            {
                var conn = Commands.ConnectToDb();
                SqlCommand command = new SqlCommand(Queries.AggiungiAmenity, conn);
                command.Parameters.AddWithValue("@PrenotazioneId", amenity.PrenotazioneId);
                command.Parameters.AddWithValue("@ServizioId", amenity.ServizioId);
                command.Parameters.AddWithValue("@Data", amenity.Data);
                command.Parameters.AddWithValue("@Quantita", amenity.Quantita);


                command.ExecuteNonQuery();

                return RedirectToAction("Index", "Backoffice");
            }
            return View(amenity);
        }

        [AllowAnonymous]
        [HttpGet]
        public JsonResult PensioniComplete()
        {
            var conn = Commands.ConnectToDb();
            SqlCommand command = new SqlCommand(Queries.BookingsWithDetails, conn);

            var reader = command.ExecuteReader();

            List<Prenotazioni> Prenotazioni = new List<Prenotazioni>();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    var prenotazione = new Prenotazioni();
                    var tipoPrenotazione = new TipiPrenotazione()
                    {
                        TipoPrenotazioneId = (int)reader["TipoPrenotazioneId"],
                        TipoPrenotazione = (string)reader["TipoPrenotazione"],

                    };
                    var camera = new Camere()
                    {
                        CameraId = (int)reader["CameraId"],
                        TipologiaCameraId = (int)reader["TipologiaCameraId"],
                        Descrizione = (string)reader["Descrizione"],
                    };

                    var cliente = new Clienti()
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
                    };
                    var tipologiaCamera = new TipologieCamera()
                    {
                        TipologiaCameraId = (int)reader["TipologiaCameraId"],
                        TipologiaCamera = (string)reader["TipologiaCamera"],
                    };

                    prenotazione.PrenotazioneId = (int)reader["PrenotazioneId"];
                    prenotazione.ClienteId = (int)reader["ClienteId"];
                    prenotazione.CameraId = (int)reader["CameraId"];
                    prenotazione.TipoPrenotazioneId = (int)reader["TipoPrenotazioneId"];
                    prenotazione.Data = (DateTime)reader["Data"];
                    prenotazione.Anno = (int)reader["Anno"];
                    prenotazione.InizioSoggiorno = (DateTime)reader["InizioSoggiorno"];
                    prenotazione.FineSoggiorno = (DateTime)reader["FineSoggiorno"];
                    prenotazione.NumeroNotti = (int)((DateTime)reader["FineSoggiorno"] - (DateTime)reader["InizioSoggiorno"]).Days;
                    prenotazione.Caparra = Convert.ToDouble(reader["Caparra"]);
                    prenotazione.Tariffa = Convert.ToDouble(reader["Tariffa"]);
                    prenotazione.Cliente = cliente;
                    prenotazione.TipoPrenotazione = tipoPrenotazione;
                    prenotazione.Camera = camera;
                    prenotazione.Camera.TipologiaCamera = tipologiaCamera;
                    Prenotazioni.Add(prenotazione);
                }
            }
            return Json(Prenotazioni, JsonRequestBehavior.AllowGet);
        }

        [AllowAnonymous]
        [HttpGet]
        public JsonResult ElencoClientiJson()
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
            return Json(Customers, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult Checkout(int? id)
        {
            if (id == null) return RedirectToAction("Prenotazioni", "Backoffice");


            var conn = Commands.ConnectToDb();
            var commandPrenotazione = new SqlCommand(Queries.SingleBookingWithDetails, conn);
            var commandAmenities = new SqlCommand(Queries.AmenitiesById, conn);

            commandPrenotazione.Parameters.AddWithValue("@PrenotazioneId", id);
            commandAmenities.Parameters.AddWithValue("@PrenotazioneId", id);

            var reader = commandPrenotazione.ExecuteReader();

            var prenotazione = new Prenotazioni();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    var tipoPrenotazione = new TipiPrenotazione()
                    {
                        TipoPrenotazioneId = (int)reader["TipoPrenotazioneId"],
                        TipoPrenotazione = (string)reader["TipoPrenotazione"],

                    };
                    var camera = new Camere()
                    {
                        CameraId = (int)reader["CameraId"],
                        TipologiaCameraId = (int)reader["TipologiaCameraId"],
                        Descrizione = (string)reader["Descrizione"],
                    };
                    var tipologiaCamera = new TipologieCamera()
                    {
                        TipologiaCameraId = (int)reader["TipologiaCameraId"],
                        TipologiaCamera = (string)reader["TipologiaCamera"],
                    };
                    var cliente = new Clienti()
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
                    };
                    prenotazione.PrenotazioneId = (int)reader["PrenotazioneId"];
                    prenotazione.ClienteId = (int)reader["ClienteId"];
                    prenotazione.CameraId = (int)reader["CameraId"];
                    prenotazione.TipoPrenotazioneId = (int)reader["TipoPrenotazioneId"];
                    prenotazione.Data = (DateTime)reader["Data"];
                    prenotazione.Anno = (int)reader["Anno"];
                    prenotazione.InizioSoggiorno = (DateTime)reader["InizioSoggiorno"];
                    prenotazione.FineSoggiorno = (DateTime)reader["FineSoggiorno"];
                    prenotazione.NumeroNotti = (int)((DateTime)reader["FineSoggiorno"] - (DateTime)reader["InizioSoggiorno"]).Days;
                    prenotazione.Caparra = Convert.ToDouble(reader["Caparra"]);
                    prenotazione.Tariffa = Convert.ToDouble(reader["Tariffa"]);
                    prenotazione.Cliente = cliente;
                    prenotazione.TipoPrenotazione = tipoPrenotazione;
                    prenotazione.Camera = camera;
                    prenotazione.Camera.TipologiaCamera = tipologiaCamera;
                }
            }
            reader.Close();

            reader = commandAmenities.ExecuteReader();

            // Lista amenities
            var amenities = new List<Amenities>();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    {
                        var amenity = new Amenities();
                        var servizioAggiuntivo = new ListaServiziAggiuntivi()
                        {
                            ServizioId = (int)reader["ServizioId"],
                            TipoServizio = (string)reader["TipoServizio"],
                            Prezzo = Convert.ToDouble(reader["Prezzo"]),

                        };
                        amenity.AmenityId = (int)reader["AmenityId"];
                        amenity.PrenotazioneId = (int)reader["PrenotazioneId"];
                        amenity.ServizioId = (int)reader["ServizioId"];
                        amenity.Data = (DateTime)reader["Data"];
                        amenity.Quantita = (int)reader["Quantita"];
                        amenity.ServizioAggiuntivo = servizioAggiuntivo;
                        amenities.Add(amenity);
                    };
                }
            }
            reader.Close();
            conn.Close();

            // Calcolo totale prenotazione
            double totaleNotti = prenotazione.Tariffa * prenotazione.NumeroNotti;
            double totalePrenotazione = (prenotazione.Tariffa * prenotazione.NumeroNotti) - prenotazione.Caparra;

            // Calcolo amenities
            List<double> parzialiAmenities = new List<double>();
            double totaleAmenities = 0;
            foreach (Amenities amenity in amenities)
            {
                double parzialeSingolo = Convert.ToDouble(amenity.Quantita) * amenity.ServizioAggiuntivo.Prezzo;
                parzialiAmenities.Add(parzialeSingolo);
                totaleAmenities += parzialeSingolo;
            }

            // Calcolo totale dovuto
            double totaleDovuto = totalePrenotazione + totaleAmenities;

            // Passaggio informazioni alla view
            ViewBag.TotaleNotti = totaleNotti;
            ViewBag.ParzialiAmenities = parzialiAmenities;
            ViewBag.TotaleAmenities = totaleAmenities;
            ViewBag.TotalePrenotazione = totalePrenotazione;
            ViewBag.TotaleDovuto = totaleDovuto;
            ViewBag.Amenities = amenities;
            return View(prenotazione);
        }
    }
}
