global using AutoMapper;
global using Microsoft.EntityFrameworkCore;
global using System.Text.Json.Serialization;
global using System.Text.Json;
global using System.ComponentModel.DataAnnotations;
global using System.Net;
global using Microsoft.Extensions.Options;
global using RabbitMQ.Client;
global using System.Text;

global using FantasyHelper.FA.DataProvider.Models;
global using FantasyHelper.FA.DataProvider.Config;
global using FantasyHelper.FA.DataProvider.Dtos;
global using FantasyHelper.FA.DataProvider.Dtos.External;
global using FantasyHelper.FA.DataProvider.Data;
global using FH.FA.FantasyDataProvider.Config;
global using FH.FA.FantasyDataProvider.MessagePublishers;

global using FH.EventProcessing;
global using FH.EventProcessing.Dtos;
global using FH.EventProcessing.Helpers;