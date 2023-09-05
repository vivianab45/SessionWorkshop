using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using SessionWorkshop.Models;

namespace SessionWorkshop.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }
    [HttpGet("")]
    public IActionResult Index()
    {
        return View();
    }

    [HttpPost("user/create")]
    public IActionResult CreateUser(User newUser)
    {
        if(ModelState.IsValid)
        {
            HttpContext.Session.SetString("Username", newUser.Name);
            HttpContext.Session.SetInt32("Display", 22);
            return RedirectToAction("Dashboard");
        } else {
            return View("Index");
        }
    }

    

            // HttpContext.Session.GetInt32("Display");      

    [HttpGet("dashboard")]
    public IActionResult Dashboard()
    {   //can't access dashboard without entering name:
        if(HttpContext.Session.GetString("Username") == null)
        {
            return RedirectToAction("Index");
        }
        return View("Dashboard");
    }

    //log out- clear session
    [HttpPost("reset")]
    public IActionResult Reset()
    {
        HttpContext.Session.Clear();
        return RedirectToAction("Index");
    }
    //routes for the buttons
    [HttpPost("process/rnd")]
    public IActionResult AddingRandom()
    {
        AddRandom();
        return RedirectToAction("Dashboard");
    }

    [HttpPost("process/add")]
    public IActionResult AddingOne()
    {
        AddOne();
        return RedirectToAction("Dashboard");
    }

    [HttpPost("process/sub")]
    public IActionResult SubtractingOne()
    {
        SubOne();
        return RedirectToAction("Dashboard");
    }

    [HttpPost("process/mult")]
    public IActionResult MultiplyByTwo()
    {
        TimesTwo();
        return RedirectToAction("Dashboard");
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
    // math logic
    public void AddRandom()
    {
        int? display=HttpContext.Session.GetInt32("Display"); 
        if (display!=null)
        {
            Random rand = new Random();
            int addRnd = rand.Next(1,11);
            display+=addRnd;
            Console.WriteLine(display); 
            HttpContext.Session.SetInt32("Display", (int)display);
        }
         ///hard coded to see if the math worked:
        // int display = 22;
        // Random rand = new Random();
        // int addRnd = rand.Next(1,11);
        // {
        //     if(addRnd != 0)
        //     {
        //         display+=addRnd;
        //         Console.WriteLine(display);
        //     } 
        // }    
        // HttpContext.Session.SetInt32("Display", display);
    }

    public void AddOne()
    {   
        int? display=HttpContext.Session.GetInt32("Display"); 
        if (display!=null)
        {
            display+=1;
            Console.WriteLine(display); 
            HttpContext.Session.SetInt32("Display", (int)display);
        }
         ///hard coded to see if the math worked:
        // // int display =22;
        // display+=1;
        // Console.WriteLine(display); 
        // HttpContext.Session.SetInt32("Display", display);
    }

    public void SubOne()
    {
        int? display=HttpContext.Session.GetInt32("Display"); 
        if (display!=null)
        {
            display-=1;
            Console.WriteLine(display); 
            HttpContext.Session.SetInt32("Display", (int)display);
        }
        ///hard coded to see if the math worked:
        // int display =22;
        // display-=1;
        // Console.WriteLine(display); 
        // HttpContext.Session.SetInt32("Display", display);
    }

    public void TimesTwo()
    {
        int? display=HttpContext.Session.GetInt32("Display"); 
        if (display!=null)
        {
            display*=2;
            Console.WriteLine(display); 
            HttpContext.Session.SetInt32("Display", (int)display);
        }
         ///hard coded to see if the math worked:
        // int display =22;
        // display *=2;
        // Console.WriteLine(display); 
        // HttpContext.Session.SetInt32("Display", display);
    }
}
