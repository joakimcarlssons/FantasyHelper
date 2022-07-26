global using AutoMapper;
global using Microsoft.EntityFrameworkCore;
global using System.Text.Json.Serialization;
global using System.Text.Json;
global using System.ComponentModel.DataAnnotations;
global using System.Net;
global using Microsoft.Extensions.Options;
global using RabbitMQ.Client;
global using System.Text;

global using FH.FA.DataProvider.Models;
global using FH.FA.DataProvider.Config;
global using FH.FA.DataProvider.Dtos;
global using FH.FA.DataProvider.Dtos.External;
global using FH.FA.DataProvider.Data;
global using FH.FA.FantasyDataProvider.Config;
global using FH.FA.FantasyDataProvider.MessagePublishers;

global using FH.EventProcessing;
global using FH.EventProcessing.Dtos;
global using FH.EventProcessing.Helpers;