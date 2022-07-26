global using AutoMapper;
global using Microsoft.EntityFrameworkCore;
global using System.Text.Json.Serialization;
global using System.Text.Json;
global using System.ComponentModel.DataAnnotations;
global using RabbitMQ.Client;
global using Microsoft.Extensions.Options;
global using System.Text;

global using FH.FPL.FantasyDataProvider.Data;
global using FH.FPL.FantasyDataProvider.Models;
global using FH.FPL.FantasyDataProvider.Dtos;
global using FH.FPL.FantasyDataProvider.Dtos.External;
global using FH.FPL.FantasyDataProvider.Config;

global using FH.EventProcessing;
global using FH.EventProcessing.Dtos;
global using FH.EventProcessing.Helpers;