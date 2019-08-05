using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Capstone.Web.Models;
using Capstone.Web.DAL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace Capstone.Web.Controllers
{
    public class SurveyController : Controller
    {
        public ISurveyDAO surveyDAO;
        public INatParkDAO parkDAO;

        public SurveyController(ISurveyDAO surveyDAO, INatParkDAO parkDAO)
        {
            this.surveyDAO = surveyDAO;
            this.parkDAO = parkDAO;
        }

        /// <summary>
        /// The page where the user takes the survey
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Index()
        {
            // Create a park object so the view has access to the readonly lists
            SurveyViewModel model = new SurveyViewModel();
            model.AllParks = parkDAO.GetAllParks();
            return View(model);
        }

        /// <summary>
        /// Post survey
        /// </summary>
        /// <param name="survey">Survey the user fills out</param>
        /// <returns>Redirect to results if form is complete, previous form page if not</returns>
        [HttpPost]
        public IActionResult Index(SurveyViewModel survey)
        {
            if(ModelState.IsValid)
            {
                surveyDAO.SaveSurvey(survey);
                return RedirectToAction("Results");
            }
            else
            {
                return RedirectToAction("Index");
            }
        }

        /// <summary>
        /// The user sees the results of the survey here
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Results()
        {
            Dictionary<string, int> results = surveyDAO.GetSurveyResults();
            return View(results);
        }
    }
}