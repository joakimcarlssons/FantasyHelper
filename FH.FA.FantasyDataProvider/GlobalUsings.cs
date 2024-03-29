﻿global using AutoMapper;
global using Microsoft.EntityFrameworkCore;
global using System.Text.Json.Serialization;
global using System.Text.Json;
global using System.ComponentModel.DataAnnotations;
global using System.Net;
global using Microsoft.Extensions.Options;
global using RabbitMQ.Client;
global using System.Text;
global using RabbitMQ.Client.Events;
global using System.Reflection;

global using FH.FA.FantasyDataProvider.Models;
global using FH.FA.FantasyDataProvider.Dtos;
global using FH.FA.FantasyDataProvider.Dtos.External;
global using FH.FA.FantasyDataProvider.Data;
global using FH.FA.FantasyDataProvider.Config;
global using FH.FA.FantasyDataProvider.EventProcessing;

global using FH.EventProcessing;
global using FH.EventProcessing.Dtos;
global using FH.EventProcessing.Helpers;
global using FH.EventProcessing.Config;