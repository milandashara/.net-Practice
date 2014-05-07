using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MovieRental.Models;
using MovieRental.Data;
using System.Data.Entity.Validation;
using MovieRental.Externals;


namespace MovieRental.Areas.Admin.Controllers
{
    public class MovieController : Controller
    {
        MovieRepository rep = new MovieRepository();


        //
        // GET: /Admin/Movie/

        public ActionResult Index()
        {
            IEnumerable<Movie> movies = rep.GetAllMovies();
            return View(movies);
        }

        //
        // GET: /Admin/Movie/Details/5

        public ActionResult Details(string id)
        {
            Movie movie = rep.getMovie(id);
            return View(movie);
        }

        //
        // GET: /Admin/Movie/Create

        public ActionResult Create()
        {
            Movie m = new Movie();
            return View(m);
        }

        //
        // POST: /Admin/Movie/Create

        [HttpPost]
        public ActionResult Create(Movie movie)
        {
            try
            {
                // TODO: Add insert logic here
                rep.Add(movie);
                rep.Save();
                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                if (e.GetType() != typeof(DbEntityValidationException))
                {
                    if (this.HttpContext.IsDebuggingEnabled)
                    {
                        ModelState.AddModelError(string.Empty, e.ToString());

                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "tech error");
                    }

                    
                }
                return View(movie);
            }
        }

        //
        // GET: /Admin/Movie/Edit/5

        public ActionResult Edit(string id)
        {
            Movie movie = rep.getMovie(id);
            return View(movie);
        }

        //
        // POST: /Admin/Movie/Edit/5

        [HttpPost]
        public ActionResult Edit(string id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here
                Movie m = rep.getMovie(id);
                UpdateModel(m);
                rep.Save();
                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                if (e.GetType() != typeof(DbEntityValidationException))
                {
                    if (this.HttpContext.IsDebuggingEnabled)
                    {
                        ModelState.AddModelError(string.Empty, e.ToString());

                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "tech error");
                    }


                }
                return View();

            }
        }

        //
        // GET: /Admin/Movie/Delete/5

        public ActionResult Delete(string id)
        {


            return View(rep.getMovie(id));
        }

        //
        // POST: /Admin/Movie/Delete/5

        [HttpPost]
        public ActionResult Delete(string id, Movie movie)
        {
            try
            {
                // TODO: Add delete logic here
                rep.Delete(id);
                rep.Save();
                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                if (e.GetType() != typeof(DbEntityValidationException))
                {
                    if (this.HttpContext.IsDebuggingEnabled)
                    {
                        ModelState.AddModelError(string.Empty, e.ToString());

                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "tech error");
                    }


                }
                return View(movie);

            }
        }

        [HttpPost]
        public ActionResult OMDBSearch(string title)
        {
            Movie m = OMDBService.GetMoviesByTitle(title);
            return Json(m);
        }
    }
}
