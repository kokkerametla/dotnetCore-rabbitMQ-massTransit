﻿using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using dotnetCore_rabbitMQ_massTransit.Models;
using MassTransit;
using Messaging.Model;

namespace dotnetCore_rabbitMQ_massTransit.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IPublishEndpoint _publishEndpoint;

    public HomeController(ILogger<HomeController> logger,
        IPublishEndpoint publishEndpoint)
    {
        _logger = logger;
        _publishEndpoint = publishEndpoint;
    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }

    [HttpPost]
    [Route("sendMessage")]
    public async Task<JsonResult> SendMessage(string message)
    {
        await _publishEndpoint.Publish(new TestMessage { Text = message }, x =>
        {
            x.Headers.Set("x-deduplication-header", message);
        });
        return Json(new { success = true });
    }
}