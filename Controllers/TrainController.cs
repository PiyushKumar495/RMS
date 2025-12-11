using System;
using System.Linq;
using System.Web.Mvc;
using RailwayManagement.Models;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace RailwayManagement.Controllers
{
    public class TrainController : Controller
    {
        private readonly RailwayDBContext db = new RailwayDBContext(); 
        public ActionResult Search()
        {
            ViewBag.Stations1 = db.Trains.Select(t => t.SourceStation).Distinct().ToList();
            ViewBag.Stations2 = db.Trains.Select(t => t.DestinationStation).Distinct().ToList();
            return View();
        }
        [HttpPost]
        public ActionResult SearchResults(string source, string destination, DateTime travelDate)
        {
            var trains = db.Trains
                .Where(t => t.SourceStation == source && t.DestinationStation == destination && t.DepartureTime.Date == travelDate)
                .ToList();

            if (!trains.Any())
            {
                ViewBag.Message = "No routes available.";
                return View(new List<Trains>());
            }

            return View(trains);
        }

        public ActionResult SearchPNR()
        {
            return View();
        }
        // POST: Train/SearchPNR
        [HttpPost]
        public ActionResult SearchPNR(string pnrNumber)
        {
            var booking = db.Bookings
                .Include("Passengers")  // Include passengers in the query
                .FirstOrDefault(b => b.Pnrnumber == pnrNumber); // Search by PNR Number

            if (booking == null)
            {
                ViewBag.Message = "No booking found for this PNR.";
                return View();
            }

            var trainDetails = db.Trains.FirstOrDefault(t => t.TrainId == booking.TrainId);

            ViewBag.Booking = booking;
            ViewBag.Train = trainDetails;
            ViewBag.Passengers = booking.Passengers;  // Pass passenger details to View

            return View();
        }




    }
}
